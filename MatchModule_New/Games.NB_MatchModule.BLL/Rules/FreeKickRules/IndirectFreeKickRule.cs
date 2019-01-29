/********************************************************************************
 * 文件名：IndirectFreeKickRule
 * 创建人：
 * 创建时间：4/20/2010 3:30:14 PM
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System.Collections.Generic;
using Games.NB.Match.AI.States;
using Games.NB.Match.AI.States.Pass;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Common;
using Games.NB.Match.Common.Random;

namespace Games.NB.Match.BLL.Rules.FreeKickRules
{
    /// <summary>
    /// Represents a Indirect Free Kick rule.
    /// </summary>
    class IndirectFreeKickRule : FreeKickRule
    {
        bool _isOutBorder = false;
        /// <summary>
        /// Initializes a new instance of the <see cref="IndirectFreeKickRule"/> class.
        /// </summary>
        /// <param name="manager">Represents the take kick player manager.</param>
        /// <param name="point">Represents a <see cref="Coordinate"/> that is the open ball point.</param>
        public IndirectFreeKickRule(IManager manager, Coordinate point,bool isOutborder=false)
            : base(manager, point)
        {
            _isOutBorder = isOutborder;
        }

        /// <summary>
        /// Starts a free kick.
        /// 执行一次间接任意球
        /// </summary>
        public unsafe override void Start()
        {
            #region 站位回合
            var random = manager.Match;
            manager.Match.Status.Round++; // 当前回合加1
            manager.Match.RoundInit();
            if (!_isOutBorder)
                manager.Match.Status.Break(EnumMatchBreakState.IndirectKick);
            #region 找出罚球人-> 找出离球最近的人

            IPlayer takeKickPlayer = MatchRule.GetClosestPlayerFromBallInMySide(manager);
            if (takeKickPlayer == null)
            {
                return;
            }

            takeKickPlayer.Status.Hasball = true;

            #endregion

            // 罚球人站到球面前
            takeKickPlayer.Status.ForceState(IdleState.Instance);
            takeKickPlayer.MoveTo(point);
            takeKickPlayer.Rotate((manager.Side == Side.Home) ? manager.Match.Pitch.AwayGoal : manager.Match.Pitch.HomeGoal);

            #region 防守方移动至防守位置
            foreach (IPlayer p in takeKickPlayer.Manager.Opponent.Players)
            {
                // 下场及有异常状态的球员不移动位置
                if (p.SkillLock)
                    continue;

                Coordinate coor;
                if (p.Input.AsPosition != Position.Goalkeeper)
                {
                    coor = CloseMove(p.Status.HalfDefault.X, p.Status.HalfDefault.Y);
                }
                else
                {
                    coor = p.Status.Default;
                }

                p.Status.ForceState(IdleState.Instance);
                p.MoveTo(coor);
                p.Rotate(point);
            }
            #endregion

            #region 进攻方除罚球人移动至进攻位置
            foreach (IPlayer p in takeKickPlayer.Manager.Players)
            {
                // 下场及有异常状态的球员不移动位置
                if (p.SkillLock)
                    continue;

                if (p.ClientId == takeKickPlayer.ClientId) 
                    continue;                
                if (p.Input.AsPosition == Position.Goalkeeper)
                {
                    p.MoveTo(p.Status.Default);
                    p.Rotate(point);
                    p.Status.ForceState(IdleState.Instance);
                    continue;
                }

                double x = (p.Manager.Side == Side.Home) ? p.Status.HalfDefault.X * 1.6 : p.Status.HalfDefault.X * 1.6 - Defines.Pitch.MAX_WIDTH * 0.6;
                double y = p.Status.Default.Y;
                y = random.RandomBool() ? y + 5 : y - 5;

                Coordinate coor = CloseMove(x, y);

                p.Status.ForceState(IdleState.Instance);
                p.MoveTo(coor);
                p.Rotate(point);
            }
            #endregion

            // 停顿时间
            for (int i = 0; i < 4; i++)
            {
                manager.Match.SaveRpt();
                manager.Match.Status.Round++;
                manager.Match.RoundInit();
            }

            #endregion

            #region 开球回合
            manager.Match.RoundInit();           
            takeKickPlayer.Status.ForceState(PassState.Instance);

            //边线球
            if (manager.Match.Football.IsOutBorder)
            {
                takeKickPlayer.Status.SubState.SetSubState(EnumSubState.HandThrow, manager.Match.Status.Round);
                takeKickPlayer.Status.ForceState(ShortPassState.Instance);
                takeKickPlayer.DecideEnd();
                takeKickPlayer.Status.PassStatus.PassTarget = PassTargetDecideRule.PassClosest(takeKickPlayer);
            }
            else
            {
                takeKickPlayer.Status.State.Enter(takeKickPlayer);

                #region 如果没有合适的传球人
                if (takeKickPlayer.Status.PassStatus.PassTarget == null) // 没有合适传球的人
                {
                    takeKickPlayer.Status.PassStatus.PassTarget = PassTargetDecideRule.PassClosest(takeKickPlayer);
                }
                #endregion

                if (takeKickPlayer.Current.SimpleDistance(takeKickPlayer.Status.PassStatus.PassTarget.Current) < Defines.Player.SHORT_PASS_MAX_RANGEPow)
                {
                    takeKickPlayer.Status.ForceState(ShortPassState.Instance);

                }
                else
                {
                    takeKickPlayer.Status.ForceState(LongPassState.Instance);
                }
            }

            takeKickPlayer.Rotate(takeKickPlayer.Status.PassStatus.PassTarget.Current);
            takeKickPlayer.Action();

            manager.Match.SaveRpt();
            #endregion
        }
    }

}
