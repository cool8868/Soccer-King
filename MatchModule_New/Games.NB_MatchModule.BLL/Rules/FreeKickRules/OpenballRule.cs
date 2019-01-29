/********************************************************************************
 * 文件名：OpenballRule
 * 创建人：
 * 创建时间：5/13/2010 10:08:23 AM
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using Games.NB.Match.AI.States;
using Games.NB.Match.AI.States.Pass;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;

namespace Games.NB.Match.BLL.Rules.FreeKickRules
{
    /// <summary>
    /// Represents the openball rule.
    /// 表示了开球的逻辑规则
    /// </summary>
    static class OpenballRule
    {
        /// <summary>
        /// Starts a openball round.
        /// 开启一个开球回合
        /// </summary>
        /// <param name="openBallManager"></param>
        public static void Start(IManager openBallManager)
        {
            #region 重置所有的物体
            var match = openBallManager.Match;
            if (match.Status.BreakState != (int)EnumMatchBreakState.SectionOpen)
                match.Status.Break(EnumMatchBreakState.MFOpen);
            match.Football.Reset();

            foreach (IManager manager in match.Managers)
            {
                foreach (IPlayer player in manager.Players)
                {
                    // 下场及有异常状态的球员不移动位置
                    if (player.SkillLock)
                        continue;

                    player.Reset();
                    player.Status.ForceState(IdleState.Instance);
                    player.Status.Speed = 0;
                    player.MoveTo(player.Status.HalfDefault);
                    player.Rotate(match.Football.Current);
                }
            }

            // 防止对方站入中圈内
            bool isHome = openBallManager.Side == Side.Home;
            double difX = 0;
            double difY=0;
            int cnt = 0;
            foreach (IPlayer player in openBallManager.Opponent.Players)
            {
                difX = Math.Abs(player.Current.X - 105);
                if (difX >= 25)
                    continue;
                difX = isHome ? 130 + cnt % 2 * 5 : 80 - cnt % 2 * 5;
                difY = player.Current.Y + (cnt % 2 == 0 ? 3 : -3);
                cnt++;
                player.MoveTo(difX, difY);
                player.Rotate(match.Football.Current);
            }
            #endregion

            #region 找出1个发球人和1个接应人
            List<IPlayer> openballArray = new List<IPlayer>(2);
            IPlayer tmp = null;
            for (int i = openBallManager.Players.Count - 1; i >= 0; i--)
            {
                tmp = openBallManager[i];
                if (tmp.SkillLock)
                    continue;

                if (tmp.Input.AsPosition == Base.Enum.Position.Goalkeeper)
                    continue;

                openballArray.Add(openBallManager[i]);

                if (openballArray.Count == 2)
                {
                    break;
                }
            }

            if (openballArray.Count == 0) // 没有任何发球人
            {
                openballArray.Add(openBallManager[0]);
                openballArray[0].Status.Hasball = true;
                match.SaveRpt();
                return;
            }
            else // 有足够的发球人
            {
                if (openballArray.Count == 1) // 只有1个发球人
                {
                    openballArray[0].MoveTo(_openballPosition1);
                    openballArray[0].Rotate(match.Football.Current);

                    openballArray.Add(openBallManager[0]); // 添加守门员为第二接球人
                    openballArray[0].Status.Hasball = true;
                    match.SaveRpt();
                    return;
                }
                else // 拥有2个发球人
                {
                    openballArray[0].MoveTo(_openballPosition1);
                    openballArray[1].MoveTo(_openballPosition2);
                    openballArray[0].Rotate(match.Football.Current);
                    openballArray[1].Rotate(match.Football.Current);

                    openballArray[0].Status.Hasball = true;
                }
            }
            #endregion

            #region 发球
            match.SaveRpt();
            match.Status.Round++;
            match.RoundInit();
            openballArray[0].Status.ForceState(ShortPassState.Instance);
            openballArray[1].Status.Hasball = true;
            match.Football.MoveTo(openballArray[1].Current);
            match.SaveRpt();
            match.Status.Round++;
            match.RoundInit();
            openballArray[0].Status.ForceState(IdleState.Instance);
            openballArray[1].Status.ForceState(ShortPassState.Instance);
            openballArray[1].Status.PassStatus.PassTarget = PassTargetDecideRule.OpenballDecide(openballArray[1]);
            openballArray[1].Action();
            match.SaveRpt();
            match.Status.Round++;
            openballArray[1].Status.ForceState(IdleState.Instance);
            #endregion

        }

        private readonly static Coordinate _openballPosition1 = Coordinate.Parse(Defines.Position.OPENBALL_POSITION_1);
        private readonly static Coordinate _openballPosition2 = Coordinate.Parse(Defines.Position.OPENBALL_POSITION_2);
    }
}
