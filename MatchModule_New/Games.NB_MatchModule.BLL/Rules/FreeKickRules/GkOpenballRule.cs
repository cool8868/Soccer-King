/********************************************************************************
 * 文件名：GkOpenballRule
 * 创建人：
 * 创建时间：7/15/2010 9:51:20 AM
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.AI.States.Idle;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.AI.States;

namespace Games.NB.Match.BLL.Rules.FreeKickRules
{
    /// <summary>
    /// 门球开球规则
    /// </summary>
    static class GkOpenballRule
    {
        /// <summary>
        /// 开始一个开球规则
        /// </summary>
        /// <param name="manager">开球的经理</param>
        public static void Start(IManager manager)
        {
            var match = manager.Match;
            match.Status.Break(EnumMatchBreakState.GKOpen);
            var gk = manager[0];
            if (gk.Disable)
            {
                foreach (var p in manager.Players)
                {
                    if (!p.Disable)
                    {
                        gk = p;
                        break;
                    }
                }
            }
           
            gk.MoveTo(manager[0].Status.Default);
            match.Football.MoveTo(gk.Current);
            gk.Status.Hasball = true;
            gk.Status.ActionLast = 0;
            gk.Status.ForceState(GKHoldBallState.Instance);

            foreach (IManager m in match.Managers)
            {
                foreach (IPlayer p in m.Players)
                {
                    if (p.SkillLock)
                        continue;
                    p.Status.ForceState(IdleState.Instance);
                    p.MoveTo(p.Status.Default);
                    p.Rotate(match.Football.Current);
                }
            }


            // 防止对方站入中圈内
            //foreach (IPlayer p in manager.Opponent.Players)
            //{
            //    if (p.Input.AsPosition == Position.Forward)
            //    {
            //        if (p.Side == Side.Away && p.Current.X < 130)
            //        {
            //            p.MoveTo(130, p.Current.Y);
            //        }

            //        if (p.Side == Side.Home && p.Current.X > 80)
            //        {
            //            p.MoveTo(80, p.Current.Y);
            //        }
            //    }
            //}

            // 停顿时间
            for (int i = 0; i < 4; i++)
            {
                match.SaveRpt();
                match.Status.Round++;
                match.RoundInit();
            }
        }
    }
}
