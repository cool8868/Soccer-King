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
    /// 角球
    /// </summary>
    class CornerKickRules : FreeKickRule
    {
        public CornerKickRules(IManager manager, Coordinate point)
            : base(manager, point)
        {
        
        }

     
        public unsafe override void Start()
        {
            var random = manager.Match;
            #region 站位回合
            manager.Match.Status.Round++; // 当前回合加1
            manager.Match.RoundInit();

            #region 找出罚球人-> 找出离球最近的人
            IPlayer takeKickPlayer = MatchRule.GetClosestPlayerFromBallInMySide(manager);
            if (takeKickPlayer == null)
            {
                return;
            }
            takeKickPlayer.Status.Hasball = true;
            #endregion

            manager.Match.Football.MoveTo(point);
            takeKickPlayer.MoveTo(point);
            takeKickPlayer.Rotate((manager.Side == Side.Home) ? manager.Match.Pitch.AwayGoal : manager.Match.Pitch.HomeGoal);
            takeKickPlayer.Status.ForceState(IdleState.Instance);
            bool isHome = takeKickPlayer.Manager.Side == Side.Home;
            Coordinate coor;
            double x,y;
            var atkPlayers=takeKickPlayer.Manager.Players;
            IPlayer atkPlayer = null;
            //进攻球员
            foreach (var p in atkPlayers)
            {
                if (p.Disable)
                    continue;
                if (p.ClientId == takeKickPlayer.ClientId)
                    continue;
                if (p.Input.AsPosition == Position.Goalkeeper)
                    coor = p.Status.Default;
                else
                {
                    x = isHome ? (p.Status.Default.X + 100) : (p.Status.Default.X - 100);
                    y = p.Status.Default.Y;
                    if (y <= 38)
                        y += 20;
                    else if (y >= 98)
                        y -= 20;
                    coor = MatchRule.RandPointInRect(random,new Coordinate(x, y), 3, 8);
                }
                p.Status.ForceState(IdleState.Instance);
                p.MoveTo(coor);
                p.Rotate(point);
            }
            //防守球员
            int uStep=60;
            int dStep=76;
            foreach (var p in takeKickPlayer.Manager.Opponent.Players)
            {
                if (p.Disable)
                    continue;
                if (p.ClientId == takeKickPlayer.ClientId)
                    continue;
                if (p.Input.AsPosition == Position.Goalkeeper)
                    coor = p.Status.Default;
                else
                {
                    x = isHome ? (p.Status.Default.X + 60) : (p.Status.Default.X - 60);
                    y = p.Status.Default.Y;
                    coor = new Coordinate(x, y);
                    atkPlayer = MatchRule.GetNearPlayerInRound(coor, atkPlayers, 20);
                    if (null != atkPlayer)
                        coor = MatchRule.GetNearPointInLine(atkPlayer.Current, coor, 8);
                    else
                    {
                        coor = MatchRule.RandPointInRect(random, coor, 5, 15);
                        if (38 < coor.Y && coor.Y <= 68)
                        {
                            coor.Y = uStep;
                            uStep += 2;
                        }
                        else if (68 < coor.Y && coor.Y <= 98)
                        {
                            coor.Y = dStep;
                            dStep -= 2;
                        }
                    }
                    if (coor.Y <= 38)
                        coor.Y += 20;
                    else if (coor.Y >= 98)
                        coor.Y -= 20;
                }
                p.Status.ForceState(IdleState.Instance);
                p.MoveTo(coor);
                p.Rotate(point);
            }
         
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

            takeKickPlayer.Status.State.Enter(takeKickPlayer);

            #region 如果没有合适的传球人
            if (takeKickPlayer.Status.PassStatus.PassTarget == null) // 没有合适传球的人
            {
                takeKickPlayer.Status.PassStatus.PassTarget = PassTargetDecideRule.PassClosest(takeKickPlayer);
            }
            #endregion

            if (takeKickPlayer.Current.SimpleDistance(takeKickPlayer.Status.PassStatus.PassTarget.Current) <= Defines.Player.SHORT_PASS_MAX_RANGEPow)
            {
                takeKickPlayer.Status.ForceState(ShortPassState.Instance);
            }
            else
            {
                takeKickPlayer.Status.ForceState(LongPassState.Instance);
            }
            takeKickPlayer.Rotate(takeKickPlayer.Status.PassStatus.PassTarget.Current);
            takeKickPlayer.Action();

            manager.Match.SaveRpt();
            #endregion
        }
    }
}
