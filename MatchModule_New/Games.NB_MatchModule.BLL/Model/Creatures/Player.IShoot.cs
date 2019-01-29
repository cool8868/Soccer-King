/********************************************************************************
 * 文件名：PlayerShoot
 * 创建人：
 * 创建时间：2009-12-19 10:30:19
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Drawing;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Interface.Manager;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.BLL.Rules;
using Games.NB.Match.Common.Random;
using Games.NB.Match.Log;
using SkillEngine.Extern.Enum;

namespace Games.NB.Match.BLL.Model.Creatures
{

    /// <summary>
    /// Represents the partial <see cref="Player"/> class that implements the interface of the <see cref="IShoot"/>.
    /// 球员分布对象，继承了<see cref="IShoot"/>接口.
    /// </summary>
    public sealed partial class Player
    {
        #region IShoot Members

        /// <summary>
        /// Player to shoot.
        /// 射门
        /// </summary>
        public void Shoot()
        {
          
            double coord = (Side == Side.Home) ? 210 - this.Current.X : this.Current.X;
            double fix = coord / 3;
            InternalShoot(AI.States.Shoot.DefaultShootState.Instance, _manager, PlayerProperty.Shooting, _match.RandomInt32(30, 45), fix, coord);
        }
        /// <summary>
        /// 乌龙射门
        /// </summary>
        public void RebelShoot()
        {
            double coord = (Side == Side.Home) ? this.Current.X : 210 - this.Current.X;
            double fix = coord / 3;
            InternalShoot(AI.States.Shoot.RebelShootState.Instance, _manager.Opponent, PlayerProperty.Shooting, _match.RandomInt32(30, 45), fix, coord);
        }

        /// <summary>
        /// Player to action a volley shoot.
        /// 球员发动一次大力抽射
        /// </summary>
        public void VolleyShoot()
        {

            // rnd(10)+15+x/10

            double coord = (Side == Side.Home) ? 210 - this.Current.X : this.Current.X;
            double fix = coord / 3;
            int speed = Convert.ToInt32(_match.RandomInt32(0, 10) + 35 + _propCore[PlayerProperty.Strength] / 10);
            InternalShoot(AI.States.Shoot.VolleyShootState.Instance, _manager, PlayerProperty.Shooting, speed, fix, coord);
        }

        /// <summary>
        /// Player to action a Direct free kick shoot.
        /// 球员发动一次直接任意球射门
        /// </summary>
        /// <remarks>
        /// 修正任意球属性无效
        /// </remarks>
        public void FreeKickShoot()
        {

            double distance = (Side == Side.Home) ? 210 - this.Current.X : this.Current.X;
            double fk = _propCore[PlayerProperty.FreeKick];
            double fix = (distance / 30) * 2 * ((fk / 4) * Math.Pow(fk / 100, 3) + 25 * Math.Pow(fk / 100, 2) + 25 * (fk / 100));


            // rnd(10)+15+x/10
            int speed = Convert.ToInt32(_match.RandomInt32(0, 10) + 35 + _propCore[PlayerProperty.Strength] / 10);
            InternalShoot(AI.States.Shoot.FreekickShootState.Instance, _manager, PlayerProperty.FreeKick, speed, fix, distance);
        }

        #endregion

        #region encapuslation

        /// <summary>
        /// Internal logic of the shoot.
        /// 射门的内部逻辑
        /// </summary>
        /// <param name="property">Represents the property id.</param>
        /// <param name="speed">Represents the football's speed.</param>
        /// <param name="fix">Represents the fix value.</param>
        private void InternalShoot(IState doneState, IManager manager, byte property, int speed, double fix, double coord)
        {
            // 球员朝向
            IPlayer gk = manager.Opponent.GetPlayersByPosition(Position.Goalkeeper)[0];
            Rotate(gk.Current);
            _match.Football.MoveTo(gk.Current);
            bool missFlag = false;
            // 射中门框           
            if (_match.RandomPercent() < Defines.Shoot.ShootToFramePercentage)
            {
                _status.ShootStatus.ShootTargetIndex = 0;
                _status.ShootStatus.ShootTarget = Match.Pitch.Goal.GetRandomDoorFrame(_match);
                missFlag = true;
            }
            else
            {
                int index = PlayerRule.GetShootTargetIndex(_match, _propCore[property], this, fix, coord);
                _status.ShootStatus.ShootTargetIndex = index;
                _status.ShootStatus.ShootTarget = _match.Pitch.Goal.GetShootTargetByIndex(_match, index);
                _status.ShootStatus.ShootSpeed = speed;
                if (index == 0 || index == 4)
                {
                    missFlag = true;
                    if (index == 4)
                        _match.Status.Break(EnumMatchBreakState.ShootFly);
                    else
                        _match.Status.Break(EnumMatchBreakState.Shooted);
                }
                else if (!gk.SkillEnable || gk.SkillLimitState != 0)
                {
                    _status.SetDoneState(doneState, EnumDoneStateFlag.Succ);
                    _status.ShootStatus.SuccFlag = 1;
                    _status.ShootStatus.NewSuccRate = 100;
                    _status.ShootStatus.RawSuccRate = 0;
                    _match.Goal(manager);
                    return;
                }
            }
            if (missFlag)
            {
                _status.SetDoneState(doneState, EnumDoneStateFlag.Fail);
                _status.ShootStatus.SuccFlag = 0;
                _status.ShootStatus.NewSuccRate = 0;
                _status.ShootStatus.RawSuccRate = 0;
                _match.MissGoal(manager, true);
            }
        }
        #endregion
    }
}
