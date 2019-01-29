using System;
using System.Collections.Generic;
using Games.NB.Match.Log;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.Base.Model.TranOut;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Util;
using Games.NB.Match.BLL.Model;
using Games.NB.Match.Frame;
using Games.NB.Match.AI;

namespace Games.NB.Match.BLL.Facade
{
    public class MatchFacade
    {
        static readonly byte[] ERRORBytes = new byte[] { 255 };

        #region Facade
        public static void Boot()
        {
            MoveRegionCache.Boot();
            FormationCache.Boot();
            StateInitializer.Initialize();
        }
        public static byte[] CreateMatchBin(byte[] rawInput)
        {
            if (null == rawInput || rawInput.Length < 2)
                return ERRORBytes;
            using (var watch = new LogWatch())
            {
                var input = MatchFromBin(rawInput, watch);
                var match = CreateMatchCore(input, watch);
                return MatchToBin(match, watch);
            }
        }
        public static byte[] CreateMatchToBin(MatchInput input)
        {
            if (null == input)
                return ERRORBytes;
            try
            {
                using (var watch = new LogWatch())
                {
                    var match = CreateMatchCore(input, watch);
                    return MatchToBin(match, watch);
                }
            }
            catch (Exception ex)
            {
                Log.LogHelper.Insert(ex);
                return ERRORBytes;
            }
        }
        public static IMatch CreateMatchFromBin(byte[] rawInput)
        {
            using (var watch = new LogWatch())
            {
                var input = MatchFromBin(rawInput, watch);
                return CreateMatchCore(input, watch);
            }
        }
        public static IMatch CreateMatch(MatchInput input)
        {
            using (var watch = new LogWatch())
            {
                return CreateMatchCore(input, watch);
            }
        }
        #endregion

        #region Native
        static IMatch CreateMatchCore(MatchInput input, LogWatch watch = null)
        {
            if (null == watch)
                watch = new LogWatch(true);
            if (input.ForceType == EnumForceWinType.HomeWin)
            {
                input.HomeManager.BuffFact = 110;
                input.AwayManager.BuffFact = 90;
            }
            else if (input.ForceType == EnumForceWinType.AwayWin)
            {
                input.HomeManager.BuffFact = 90;
                input.AwayManager.BuffFact = 110;
            }
            int loop = 0;
            const int end = 30;
            MatchEntity match;
            do
            {
                match = new MatchEntity(input);
                MainLoop(match);
                watch.LogCostTime(string.Format("Id:{0} Type:{1} Force:{2} Home:{3}[{4}] vs Away:{5}[{6}]. Result {7}:{8})",
                    input.MatchId, input.MatchType, input.ForceType,
                    match.Input.HomeManager.Mid, match.Input.HomeManager.Name,
                    match.Input.AwayManager.Mid, match.Input.AwayManager.Name,
                    match.HomeScore, match.AwayScore));
                if (input.ForceType == EnumForceWinType.None
                    || input.ForceType == EnumForceWinType.HomeWin && match.HomeScore > match.AwayScore
                    || input.ForceType == EnumForceWinType.NoDraw && match.HomeScore != match.AwayScore
                    || input.ForceType == EnumForceWinType.AwayWin && match.HomeScore < match.AwayScore)
                    break;
                loop++;
                if (input.ForceType == EnumForceWinType.NoDraw && loop >= 3)
                {
                    input.HomeManager.BuffFact = 110;
                    input.AwayManager.BuffFact = 90;
                }
            }
            while (loop < end && input.ForceType != EnumForceWinType.None);
            return match;
        }
        static MatchInput MatchFromBin(byte[] rawInput,LogWatch watch=null)
        {
            if (null == watch)
                watch = new LogWatch(true);
            MatchInput input = null;
            using (var ms = new System.IO.MemoryStream(rawInput))
            {
                var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                input = bf.Deserialize(ms) as MatchInput;
            }
            if (null == input)
                return null;
            watch.LogCostTime(string.Format("Home:{0}[{1}] vs Away:{2}[{3}]. Start)",
               input.HomeManager.Mid, input.HomeManager.Name,
               input.AwayManager.Mid, input.AwayManager.Name));
            return input;
        }
        static byte[] MatchToBin(IMatch match, LogWatch watch = null)
        {
            if (null == match)
                return ERRORBytes;
            if (null == watch)
                watch = new LogWatch(true);
            var bytes = IOUtil.BinWrite(match.Report, ReportAsset.RPTVerNo);
            watch.LogCostTime(string.Format("Home:{0}[{1}] vs Away:{2}[{3}]. End)",
              match.Input.HomeManager.Mid, match.Input.HomeManager.Name,
              match.Input.AwayManager.Mid, match.Input.AwayManager.Name));
            return bytes;
        }
        static IMatch MainLoop(IMatch match)
        {
            match.SessionInit();
            IPlayer ballHandler = null;
            byte ballHandlerId=0;
            while (match.Status.Round < match.Status.TotalRound)
            {
                match.Status.Round++;
                // 回合初始化数据
                match.RoundInit();
                // 持球人决策
                ballHandler = match.Status.BallHandler;
                ballHandlerId = match.Status.BallHandler.ClientId;
                //经理技能
                SkillEngine.SkillImpl.SkillFacade.TriggerManagerSkills(match.HomeManager, 0);
                SkillEngine.SkillImpl.SkillFacade.TriggerManagerSkills(match.AwayManager, 0);
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
            match.SaveRptEnd();
            return match;
        }
        #endregion
    }
}
