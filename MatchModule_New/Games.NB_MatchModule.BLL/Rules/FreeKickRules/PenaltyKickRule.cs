/********************************************************************************
 * 文件名：PenaltyKickRule
 * 创建人：
 * 创建时间：5/15/2010 1:02:58 PM
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：点球规则
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.AI.States;
using Games.NB.Match.AI.States.Shoot;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Common;
using Games.NB.Match.Common.Random;


namespace Games.NB.Match.BLL.Rules.FreeKickRules
{
    /// <summary>
    /// Represents a rule that defines the penalty kick.
    /// 点球规则    
    /// </summary>
    class PenaltyKickRule : FreeKickRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PenaltyKickRule"/> class.
        /// </summary>
        /// <param name="manager">Represents the take kick player manager.</param>
        /// <param name="point">Represents a <see cref="Coordinate"/> that is the open ball point.</param>
        public PenaltyKickRule(IManager manager, Coordinate point)
            : base(manager, point)
        {
        }

        /// <summary>
        /// Kicks the penalty kick.
        /// 罚点球
        /// </summary>
        public unsafe override void Start()
        {
            #region 站位回合
            var random = manager.Match;
            // 当前回合加1
            manager.Match.Status.Round++;
            manager.Match.Status.Break(EnumMatchBreakState.PenaltyKick);
            #region 找出罚球人-> 找出离球最近的人
            // 找出罚球人，罚球人为任意球属性最高的球员（不包含守门员）                        
            IPlayer takeKickPlayer = MatchRule.GetHighestPropertyPlayer(manager, PlayerProperty.FreeKick);
            if (takeKickPlayer == null) // 没有可以罚球的人，跳出逻辑
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
            var region = (manager.Side == Side.Home) ? manager.Match.Pitch.AwayPenaltyRegion : manager.Match.Pitch.HomePenaltyRegion;
            foreach (IPlayer p in takeKickPlayer.Manager.Opponent.Players)
            {
                Coordinate coor;
                if (p.Input.AsPosition != Position.Goalkeeper)
                {
                    coor = CloseMove(p.Status.HalfDefault.X, p.Status.HalfDefault.Y);
                }
                else
                {
                    coor = p.Status.Default;
                }

                if (p.Input.AsPosition == Position.Fullback) // 将防守人移动至禁区线上
                {
                    if (region.Start.X == 0)
                    {
                        coor = new Coordinate(region.End.X, coor.Y);
                    }
                    else
                    {
                        coor = new Coordinate(region.Start.X, coor.Y);
                    }
                }

                p.Status.ForceState(IdleState.Instance);
                p.MoveTo(coor);
                p.Rotate(point);
            }
            #endregion

            #region 进攻方除罚球人移动至进攻位置
            foreach (IPlayer p in takeKickPlayer.Manager.Players)
            {
                if (p.ClientId == takeKickPlayer.ClientId) continue;
                if (p.Input.AsPosition == Position.Goalkeeper)
                {
                    p.MoveTo(p.Status.Default);
                    p.Rotate(point);
                    p.Status.ForceState(IdleState.Instance);
                    continue;
                }

                double x = (p.Manager.Side == Side.Home) ? p.Status.HalfDefault.X * 1.6 : p.Status.HalfDefault.X * 1.6 - Defines.Pitch.MAX_WIDTH * 0.6;
                double y = p.Status.HalfDefault.Y;
                y = random.RandomBool() ? y + 5 : y - 5;

                Coordinate coor = CloseMove(x, y);

                p.Status.ForceState(IdleState.Instance);
                p.MoveTo(coor);
                p.Rotate(point);
            }
            #endregion

            #region 防止除罚球人外任何人进入禁区            
            foreach (IManager m in manager.Match.Managers)
            {
                foreach (IPlayer player in m.Players)
                {
                    if (player.ClientId == takeKickPlayer.ClientId)
                    {
                        continue;
                    }

                    if (player.Input.AsPosition == Position.Goalkeeper)
                    {
                        continue;
                    }

                    if (region.IsCoordinateInRegion(player.Current))
                    {
                        if (region.Start.X == 0)
                        {
                            player.MoveTo(region.End.X, player.Current.Y);
                        }
                        else
                        {
                            player.MoveTo(region.Start.X, player.Current.Y);
                        }
                    }
                }
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
            takeKickPlayer.Status.ForceState(VolleyShootState.Instance);
            takeKickPlayer.AddFinishingBuff(1);
            takeKickPlayer.Action();

            IPlayer gk = manager.Opponent.GetPlayersByPosition(Position.Goalkeeper)[0];
            gk.QuickDecide();
            gk.Action();
            manager.Match.SaveRpt();
            #endregion
        }
    }
}
