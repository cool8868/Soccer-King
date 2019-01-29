/********************************************************************************
 * 文件名：DirectFreeKickRule
 * 创建人：
 * 创建时间：4/20/2010 3:29:42 PM
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
using Games.NB.Match.AI.States;
using Games.NB.Match.AI.States.Shoot;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Common;
using Games.NB.Match.Common.Random;
using Games.NB.Match.BLL.Model.Creatures;


namespace Games.NB.Match.BLL.Rules.FreeKickRules
{

    /// <summary>
    /// Represents the Direct Free Kick rule.
    /// </summary>
    class DirectFreeKickRule : FreeKickRule
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectFreeKickRule"/> class.
        /// </summary>
        /// <param name="manager">Represents the take kick player manager.</param>
        /// <param name="point">Represents a <see cref="Coordinate"/> that is the open ball point.</param>
        public DirectFreeKickRule(IManager manager, Coordinate point)
            : base(manager, point)
        {
        }

        /// <summary>
        /// Starts a free kick.
        /// </summary>
        public unsafe override void Start()
        {
            #region 站位回合
            var random = manager.Match;
            // 当前回合加1
            manager.Match.Status.Round++;
            manager.Match.RoundInit();
            manager.Match.Status.Break(EnumMatchBreakState.DirectKick);
            // 射门目标
            Coordinate target = (manager.Side == Side.Home) ? manager.Match.Pitch.AwayGoal : manager.Match.Pitch.HomeGoal;
            // 已经移动位置后的球员列表
            List<IPlayer> finArray = new List<IPlayer>(Defines.Match.MAX_PLAYER_COUNT * 2);

            #region  罚球人相关
            // 找出罚球人，罚球人为任意球属性最高的球员（不包含守门员）            
            IPlayer takeKickPlayer = MatchRule.GetHighestPropertyPlayer(manager, PlayerProperty.FreeKick);
            if (takeKickPlayer == null) // 如果没有可以罚球的球员，返回
            {
                return;
            }
            takeKickPlayer.Status.Hasball = true;
            finArray.Add(takeKickPlayer);

            // 罚球人站到球面前            
            takeKickPlayer.Status.ForceState(IdleState.Instance);
            takeKickPlayer.MoveTo(point);
            takeKickPlayer.Rotate(point);
            #endregion

            #region 找出人墙区
            const int distance = 20;  // 离人墙的距离
            const int pDistance = 5; // 人墙间的距离
            const int pDistanceY = 10;

            double xdiff = point.X - target.X;
            double ydiff = point.Y - target.Y;
            if (xdiff == 0) xdiff = 1;
            if (ydiff == 0)
                ydiff = 5;//ydiff = 1;

            double tDistance = target.Distance(point); // 总距离
            double l = xdiff / -ydiff;  // 斜率

            double x0 = point.X - xdiff / tDistance * distance;
            double y0 = point.Y - ydiff / tDistance * distance;

            #region 填充人墙坐标
            int wallPlayerCount = random.RandomBool() ? 2 : 3;
            List<Coordinate> wallPoints = new List<Coordinate>(wallPlayerCount);
            if (wallPlayerCount == 2)
            {
                double x = x0 - pDistance / 2 * xdiff / tDistance;
                double y = y0 + pDistanceY / 2 * ydiff / tDistance;
                wallPoints.Add(new Coordinate(x, y));

                x = x0 + pDistance / 2 * xdiff / tDistance;
                y = y0 - pDistanceY / 2 * ydiff / tDistance;
                wallPoints.Add(new Coordinate(x, y));
            }
            else
            { // wall player count = 3
                double x = x0 - pDistance * xdiff / tDistance;
                double y = y0 + pDistanceY * ydiff / tDistance;
                wallPoints.Add(new Coordinate(x, y));

                x = x0;
                y = y0;
                wallPoints.Add(new Coordinate(x, y));

                x = x0 + pDistance * xdiff / tDistance;
                y = y0 - pDistanceY * ydiff / tDistance;
                wallPoints.Add(new Coordinate(x, y));
            }

            #endregion

            #region 摆人墙

            // 从防守方找出离球最近的人摆人墙
            double* defenders = stackalloc double[Defines.Match.MAX_PLAYER_COUNT - 1];
            List<IPlayer> array = new List<IPlayer>(Defines.Match.MAX_PLAYER_COUNT - 1);
            int length = 0;
            foreach (var p in manager.Opponent.Players)
            {
                if (p.SkillLock)
                    continue;
                if (p.Input.AsPosition == Position.Goalkeeper
                    || p.Input.AsPosition == Position.Forward)
                    continue;
                defenders[length] = p.Current.SimpleDistance(point);
                array.Add(p);
                length++;
            }
            int[] indexes = Utility.SortMinDoubleArrayIndexQuick(defenders, length);
            for (int i = 0; i < wallPlayerCount; i++)
            {
                if (i >= array.Count) continue; // 修正人墙人数不够引发异常

                Coordinate p = wallPoints[i];
                array[indexes[i]].MoveTo(p);
                finArray.Add(array[indexes[i]]);
            }

            #endregion

            #endregion

            #region 防守方剩余球员站位
            foreach (var p in manager.Opponent.Players)
            {
                if (p.SkillLock)
                    continue;
                if (!finArray.Contains(p))
                {
                    if (p.Input.AsPosition == Position.Goalkeeper)
                        p.MoveTo(p.Status.Default);
                    else
                        p.MoveTo(CloseMove(p.Status.HalfDefault.X, p.Status.HalfDefault.Y));
                }
                p.Status.ForceState(IdleState.Instance);
                p.Rotate(manager.Match.Football.Current);
            }
            #endregion

            #region 进攻方剩余球员站位
            IPlayer lastMan = manager.Opponent.Status.LastPlayer; // 最后一个防守人
            if (null == lastMan)
                lastMan = FindOutLastDefender();
            foreach (var p in manager.Players)
            {
                if (p.SkillLock)
                    continue;

                if (p.ClientId != takeKickPlayer.ClientId)
                {
                    if (p.Input.AsPosition == Position.Goalkeeper)
                    {
                        p.MoveTo(p.Status.Default);
                    }
                    else
                    {
                        if (p.Input.AsPosition == Position.Forward)
                        {
                            // 前锋挤压阵型
                            int excursion = (manager.Side == Side.Home) ? -random.RandomInt32(0, 10) : random.RandomInt32(0, 10);
                            p.MoveTo(lastMan.Current.X + excursion, p.Status.HalfDefault.Y);
                        }
                        else
                        {
                            // 其余进攻球员
                            double x = (p.Manager.Side == Side.Home) ? p.Status.HalfDefault.X * 1.6 : p.Status.HalfDefault.X * 1.6 - Defines.Pitch.MAX_WIDTH * 0.6;
                            double y = p.Status.HalfDefault.Y;
                            p.MoveTo(CloseMove(x, y));
                        }
                    }
                }
                p.Status.ForceState(IdleState.Instance);
                p.Rotate(manager.Match.Football.Current);
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
            takeKickPlayer.Status.ForceState(FreekickShootState.Instance);
            SkillEngine.SkillImpl.SkillFacade.TriggerPlayerSkills(takeKickPlayer, 0, true);
            takeKickPlayer.Action();

            IPlayer gk = manager.Opponent.GetPlayersByPosition(Position.Goalkeeper)[0];
            // gk.Decide();
            gk.QuickDecide();
            gk.Action();

            manager.Match.SaveRpt();
            #endregion
        }

        /// <summary>
        /// Finds out the defence side's last defender.
        /// 找出最后一个防守者
        /// </summary>
        /// <returns>Returns the last defender.</returns>
        private unsafe IPlayer FindOutLastDefender()
        {
            double* array = stackalloc double[Defines.Match.MAX_PLAYER_COUNT];

            int length = 0;
            foreach (var p in manager.Opponent.Players)
            {
                if (p.Input.AsPosition == Position.Goalkeeper)
                    continue;
                array[length++] = p.Current.X;
            }

            int index = 0;
            if (manager.Opponent.Side == Side.Home)
            { // 主队的话需要找出X坐标做小
                index = Utility.GetDoubleMinIndexQuick(array, length) + 1;
            }
            else
            { // 客队需要找出X坐标最大
                index = Utility.GetDoubleMaxIndexQuick(array, length) + 1;
            }

            return manager.Opponent.Players[index];
        }


    }
}
