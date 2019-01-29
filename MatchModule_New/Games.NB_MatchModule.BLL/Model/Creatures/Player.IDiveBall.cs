/********************************************************************************
 * 文件名：Player
 * 创建人：
 * 创建时间：5/21/2010 10:49:38 AM
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Drawing;
using Games.NB.Match.AI;
using Games.NB.Match.AI.States;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Interface.Manager;
using Games.NB.Match.Base.Interface.Player;
using Games.NB.Match.Common.Random;
using Games.NB.Match.Log;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Model;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.Extern.Enum;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.SkillImpl.Football;

namespace Games.NB.Match.BLL.Model.Creatures
{
    /// <summary>
    /// Represents the player entity.
    /// This partial class implemented the interface of the <see cref="IDiveBall"/>.
    /// </summary>
    public partial class Player
    {
        /// <summary>
        /// 扑救
        /// </summary>
        public void DiveBall()
        {
            // 当守门员离场时直接返回
            if (!this.SkillEnable)
            {
                return;
            }

            var shooter = this._match.Status.BallHandler;
            var shootIndex = shooter.Status.ShootStatus.ShootTargetIndex;
            var speed = shooter.Status.ShootStatus.ShootSpeed;

            _status.DiveStatus.DiveDirection = GetDiveDirection(shooter);

            // 球打飞
            if (shootIndex == 4 || shootIndex == 0)
            {
                shooter.Status.SetDoneState(shooter.Status.State, EnumDoneStateFlag.Fail);
                _match.MissGoal(_manager.Opponent, true);
                return;
            }

            double n = 0d;

            if (shootIndex == 1)
            {
                n = 0.9d;
            }

            if (shootIndex == 2)
            {
                n = 0.6d;
            }

            if (shootIndex == 3)
            {
                n = 0.4d;
            }

            n = n * (1 - _propCore[PlayerProperty.Positioning] * 0.00167);

            // y=(v-30)/35*0.2+n-x*0.125%
            double shootRate = ((double)(speed - 30) / 35 * 0.2 + n - _propCore[PlayerProperty.Reflexes] * 0.00125) * 100;
            double diverRate = 100 - shootRate;
            shootRate = _propCore.GetActionRate(EnumBuffCode.ShootSuccRate, shootRate);
            shooter.Status.ShootStatus.NewSuccRate = (int)Math.Round(shootRate, 0);
            if (diverRate > 0)
                diverRate = shooter.PropCore.GetActionRate(EnumBuffCode.DiveSuccRate, diverRate);
            if (diverRate >= 100 && shootRate < diverRate)
                shootRate = 0;
            else if (diverRate < 100 && shootRate < 100)
                shootRate = (3 * shootRate + 2 * (100 - diverRate)) / 5;
            //shooter.Status.ShootStatus.NewSuccRate = (int)shootRate;
            shooter.Status.ShootStatus.RawSuccRate = (int)Math.Round(diverRate, 0);
            if (shootRate >= 100 || shootRate > 0 && _match.RandomPercent() <= shootRate)
            {
                _status.SetDoneState(AI.States.DiveBallState.Instance, EnumDoneStateFlag.Fail);
                shooter.Status.SetDoneState(shooter.Status.State, EnumDoneStateFlag.Succ);
                shooter.Status.ShootStatus.SuccFlag = 1;
                _match.Goal(_manager.Opponent);
            }
            else
            {
                _status.SetDoneState(AI.States.DiveBallState.Instance, EnumDoneStateFlag.Succ);
                shooter.Status.SetDoneState(shooter.Status.State, EnumDoneStateFlag.Fail);
                shooter.Status.ShootStatus.SuccFlag = 0;
                _status.Hasball = true;
                OutOfHand(shooter);
            }
        }

        /// <summary>
        /// 获取扑救方向
        /// </summary>
        /// <param name="shooter">射门的球员</param>
        /// <returns>扑救方向</returns>
        private static byte GetDiveDirection(IPlayer shooter)
        {
            if (shooter.Status.ShootStatus.ShootTarget.IsFrame) // shoot to the door frame
            {
                switch (shooter.Status.ShootStatus.ShootTarget.X)
                {
                    case 1:
                    case 2:
                        return (byte)Direction.Left;
                    case 3:
                    case 4:
                        return (byte)Direction.Right;
                }
            }

            if (shooter.Status.ShootStatus.ShootTarget.X < 6)
            {
                return (byte)Direction.Left;
            }

            if (shooter.Status.ShootStatus.ShootTarget.X > 13)
            {
                return (byte)Direction.Right;
            }

            return (byte)Direction.Center;
        }

        /// <summary>
        /// 守门员脱手
        /// </summary>
        /// <param name="shooter">射门的球员</param>
        private void OutOfHand(IPlayer shooter)
        {
            bool isOutHand = this.SkillLimitState==(int)EnumBlurBuffCode.OutHand;
            double speed = shooter.Status.ShootStatus.ShootSpeed;
            if (!isOutHand)
            {
                double outhandRate = speed * 2 - _propCore[PlayerProperty.Handling] * 0.25;
                outhandRate = this.PropCore.GetActionRate(EnumBuffCode.OutHandRate, outhandRate);
                isOutHand = _match.RandomPercent() < outhandRate;
            }
            if (!isOutHand)
            {
                _match.MissGoal(_manager.Opponent, false);
                return;
            }
            //免疫脱手
            IBoostBuff boost = null;
            int maxRate = SkillDefines.MAXStorePercent;
            if (this.TryGetAntiRate(ref boost, (int)EnumBlurBuffCode.OutHand))
            {
                double rate = boost.Percent;
                if (rate >= maxRate || rate > 0 && _match.RandomPercent(maxRate) <= rate)
                {
                    _match.MissGoal(_manager.Opponent, false);
                    return;
                }
            }
            //角球
            if (this.Status.DiveStatus.DiveDirection != (byte)Direction.Center
                && _match.RandomPercent() <= Defines.Player.OUTHAND_CORNER_PERCENTAGE)
            {
                bool isLeft = this.Status.DiveStatus.DiveDirection == (byte)Direction.Left;
                bool isHome = this.Side == Base.Enum.Side.Home;
                Coordinate corner = new Coordinate(-1, isLeft ? 50 : Defines.Pitch.MAX_HEIGHT - 50);
                if (!isHome)
                    corner = corner.Mirror();
                Match.Football.Kick(corner, speed, this);
                Match.Status.IsNoBallHandler = true;
                return;
            }

            Double y1 = Defines.Pitch.MAX_HEIGHT - shooter.Current.Y;
            const Double y2 = Defines.Pitch.MAX_HEIGHT / 2;
            const Double x = Defines.Pitch.MAX_WIDTH / 2;

            Int32 y = _match.RandomInt32((Int32)y1, (Int32)y2);

            Match.Football.Kick(new Coordinate(x, y), speed * 0.6, this);
            Match.Status.IsNoBallHandler = true;
        
        }
    }
}
