using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.Base.Model.TranOut;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Util;
using Games.NB.Match.Emulator.WPF.Entity;
using Games.NB.Match.Emulator.WPF.Entity.Statistics;
using Games.NBall.Common;
using Games.NBall.Entity.Enums;
using System.Data.SqlClient;

namespace Games.NB.Match.Emulator.WPF.Mgrs
{
    public class EmulatorHelper
    {
        #region GetProcess
        public static byte[] GetProcess(Guid matchId, int matchType)
        {
            string date = Games.NBall.Bll.Share.ShareUtil.GetDateFromComb(matchId);
            string typePrefix = string.Empty;
            switch ((EnumMatchType)matchType)
            {
                case EnumMatchType.Tour:
                    typePrefix = "Tour_MatchProcess_";
                    break;
                case EnumMatchType.Ladder:
                    typePrefix = "Ladder_MatchProcess_";
                    break;
                case EnumMatchType.TourElite:
                    typePrefix = "Elite_MatchProcess_";
                    break;
                case EnumMatchType.Dailycup:
                    typePrefix = "Dailycup_MatchProcess_";
                    break;
                case EnumMatchType.League:
                    typePrefix = "League_MatchProcess_";
                    break;
                case EnumMatchType.WorldChallenge:
                    typePrefix = "WCH_MatchProcess_";
                    break;
                case EnumMatchType.Friend:
                    typePrefix = "Friend_MatchProcess_";
                    break;
                case EnumMatchType.PlayerKill:
                    typePrefix = "Pk_MatchProcess_";
                    break;
                case EnumMatchType.Arena:
                    typePrefix = "Arena_MatchProcess_";
                    break;
                case EnumMatchType.GuildWar:
                    typePrefix = "GWar_MatchProcess_";
                    break;
                case EnumMatchType.Revelation:
                    typePrefix = "Revelation_MatchProcess";
                    break;
                case EnumMatchType.Crowd:
                    typePrefix = "Crowd_MatchProcess";
                    break;
                case EnumMatchType.Giants:
                    typePrefix = "Giants_MatchProcess";
                    break;
                case EnumMatchType.CrossCrowd:
                    typePrefix = "CrossCrowd_MatchProcess";
                    break;
                case EnumMatchType.CrossLadder:
                    typePrefix = "CrossLadder_MatchProcess";
                    break;
               

            }
            string sql = string.Format("select * from {0}{1} where idx='{2}'", typePrefix, date, matchId);
            string conStr = Games.NBall.Dal.ConnectionFactory.Instance.GetConnectionString(EnumDbType.Process);
            using (var con = new SqlConnection(conStr))
            {
                var cmd = con.CreateCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = sql;
                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        return (byte[])dr["Process"];
                }
            }
            return null;
        }
        #endregion

        #region LoadProcess
        public static void LoadTestProcess(string folderName, Guid matchId)
        {
            string folder = "d:\\test\\" + folderName + "\\";

            string path = folder + matchId + ".bin";
            LoadProcess(path, matchId);
        }

        public static MatchReport LoadProcess(string fileName, Guid matchId)
        {
            MatchReport match = null;
            try
            {

                match = IOUtil.BinRead<MatchReport>(fileName, 0);
                const string xmlpath = @"d:\test\datainfofromdb.xml";
                IOUtil.XmlWrite(xmlpath, match);
                return match;
            }
            catch (Exception ex)
            {
                if (match != null)
                {
                    try
                    {
                        string xmlpath = @"d:\test\data" + matchId + ".xml";
                        IOUtil.XmlWrite(xmlpath, match);
                    }
                    catch
                    {
                    }

                }
                LogHelper.Insert(ex, fileName);
                return null;
            }

        }
        #endregion

        #region GetStateContent
        public static string GetStateContent(int state)
        {
            string content = "";
            if (state == 0)
            {
                content = "跑动";
            }
            if (state == 1)
            {
                content = "扑救";
            }
            if (state == 2)
            {
                content = "站立";
            }
            if (state == 3)
            {
                content = "停球";
            }
            if (state == 4)
            {
                content = "抢断";
            }
            if (state == 5)
            {
                content = "铲球";
            }
            if (state == 6)
            {
                content = "带球";
            }
            if (state == 7)
            {
                content = "长传";
            }
            if (state == 8)
            {
                content = "短传";
            }
            if (state == 9)
            {
                content = "射门";
            }
            if (state == 10)
            {
                content = "大力抽射";
            }
            if (state == 11)
            {
                content = "过人";
            }
            if (state == 12)
            {
                content = "走动";
            }
            if (state == 13)
            {
                content = "边线手抛球";
            }
            if (state == 14)
            {
                content = "头球";
            }
            if (state == 15)
            {
                content = "倒挂金钩";
            }
            if (state == 16)
            {
                content = "扑救成功";
            }
            if (state == 17)
            {
                content = "乌龙射门";
            }
            return content;
        }
        #endregion

        #region GetPositionStr
        public static string GetPositionStr(int position)
        {
            string strPosition;
            switch (position)
            {
                case (int)Position.Forward:
                    strPosition = "F";
                    break;
                case (int)Position.Fullback:
                    strPosition = "B";
                    break;
                case (int)Position.Midfielder:
                    strPosition = "M";
                    break;
                case (int)Position.Goalkeeper:
                    strPosition = "GK";
                    break;
                default:
                    strPosition = "Unk";
                    break;
            }
            return strPosition;
        }
        #endregion

        #region SaveMatchToFile
        public static bool SaveMatchToFile(MatchReport match)
        {
            try
            {
                if (match == null)
                {
                    return false;
                }
                const string path = @"d:\test\data.bin";
                const string xmlpath = @"d:\test\datainfo.xml";
                IOUtil.BinWrite(path, match, 0);
                IOUtil.XmlWrite(xmlpath, match);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return false;
            }
        }

        public static bool SaveMatchToFile(MatchReport match, Guid matchId, string folderName)
        {
            try
            {
                if (match == null)
                {
                    return false;
                }
                string folder = "d:\\test\\" + folderName + "\\";
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                string path = folder + matchId + ".bin";
                IOUtil.BinWrite(path, match, 0);
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return false;
            }
        }
        #endregion

        #region PlayMatchProcess


        static Coordinate _awayGoal = Coordinate.Parse(Defines.Pitch.AWAY_GOAL);
        static Coordinate _homeGoal = Coordinate.Parse(Defines.Pitch.HOME_GOAL);
        static Coordinate CalShootCoordinate(int offenseSide, ref int homeScore, ref int awayScore, int curRound, int rawside, int startRound, int endRound, Coordinate start, int score)
        {
            if (rawside == 0)
            {
                if (curRound == endRound)
                    homeScore += score;
            }
            else
            {
                if (curRound == endRound)
                    awayScore += score;
            }
            if (offenseSide == 0)
            {
                return CalCurCoordinate(3, curRound, startRound, endRound, start, _awayGoal);
            }
            else
            {
                return CalCurCoordinate(3, curRound, startRound, endRound, start, _homeGoal);
            }
        }

        static Coordinate CalReboundCoordinate(int offenseSide, int curRound, int startRound, int endRound, Coordinate end)
        {
            if (offenseSide == 0)
                return CalCurCoordinate(curRound, startRound, endRound, _awayGoal, end);
            else
            {
                return CalCurCoordinate(curRound, startRound, endRound, _homeGoal, end);
            }
        }

        private static Coordinate CalCurCoordinate(int curRound, int startRound, int endRound, Coordinate start,
                                                   Coordinate end)
        {
            return CalCurCoordinate(0, curRound, startRound, endRound, start, end);
        }

        static Coordinate CalCurCoordinate(int offsetRound, int curRound, int startRound, int endRound, Coordinate start, Coordinate end)
        {
            startRound = startRound + offsetRound;
            if (curRound <= startRound)
            {
                return start;
            }
            else if (curRound >= endRound)
            {
                return end;
            }
            else
            {
                int total = endRound - startRound;
                int offSet = curRound - startRound;
                double x = 0;
                double y = 0;
                x = start.X + (end.X - start.X) * offSet / total;
                y = start.Y + (end.Y - start.Y) * offSet / total;
                return new Coordinate(x, y);
            }
        }
        #endregion

        #region GetMaxRound
        public static int GetMaxRound(MatchReport match)
        {
            int round = 0;
            foreach (var m in new[] { match.HomeManager, match.AwayManager })
            {
                foreach (var p in m.Players)
                {
                    if (p.CntMoveResults > round)
                    {
                        round = p.CntMoveResults;
                    }
                }
            }
            return round;
        }
        #endregion

        #region CreateMatch
        public static void CreateMatch(IMatch match, StatisticsMatchEntity statisticsMatch)
        {
            MainLoop(match, statisticsMatch);
        }

        
        static void MainLoop(IMatch match, StatisticsMatchEntity statisticsMatch)
        {
            match.SessionInit();
            while (match.Status.Round < match.Status.TotalRound)
            {
                doMatch(match);
            }
            match.SaveRptEnd();
            if (statisticsMatch != null)
            {
                statisticsMatch.SetTotal();
            }
        }

        static void doMatch(IMatch match)
        {
            match.Status.Round++;
            // 回合初始化数据
            match.RoundInit();
            var ballHandler = match.Status.BallHandler;
            byte ballHandlerId = match.Status.BallHandler.ClientId;
            //经理技能
            SkillEngine.SkillImpl.SkillFacade.TriggerManagerSkills(match.HomeManager, 0);
            SkillEngine.SkillImpl.SkillFacade.TriggerManagerSkills(match.AwayManager, 0);
            // 持球人决策
            ballHandler.QuickDecide();

            #region 进攻方决策
            foreach (var player in ballHandler.Manager.Players)
            {
                if (player == ballHandler)
                {
                    continue;
                }

                player.QuickDecide();
            }
            #endregion

            #region 防守方决策
            foreach (var player in ballHandler.Manager.Opponent.Players)
            {
                player.QuickDecide();
            }
            #endregion
            match.HomeManager.SetDoneState();
            match.AwayManager.SetDoneState();
            // 持球人行动
            ballHandler = match.Status.BallHandler;
            ballHandlerId = match.Status.BallHandler.ClientId;
            ballHandler.Action();

            #region 所有球员一起行动
            foreach (var manager in match.Managers)
            {
                foreach (var player in manager.Players)
                {
                    if (player.ClientId == ballHandlerId)
                    {
                        continue;
                    }

                    if (player.Disable)
                    {
                        continue;
                    }

                    player.Action();
                }
            }
            #endregion

            // 足球移动
            match.Football.Move();

            match.SaveRpt();
        }
        #endregion

        #region ConvertToDouble
        public static double ConvertToDouble(string param)
        {
            if (string.IsNullOrEmpty(param))
                return 0d;
            double i = 0d;
            double.TryParse(param, out i);
            return i;
        }
        #endregion
    }
}
