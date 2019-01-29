    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.Base.Model.TranOut;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Frame;
using Games.NB.Match.Frame.Model;

namespace Games.NB.Match.Emulator.WPF.Mgrs
{
    public class DataMgr
    {
        public static MatchInput CreateDebugMatch(int tranTime)
        {
            return CreateDebugMatch(tranTime, 200, 200);
        }
        public static MatchInput CreateDebugMatch(int tranTime, double homePropVal, double awayPropVal)
        {
            var obj = new MatchInput();
            obj.MatchId = Guid.NewGuid();
            obj.TranTime = tranTime;
            obj.MapId = 1;
            obj.MatchType = 1;
            Guid homeId = new Guid("E5F9AE02-5754-4E0B-9ABE-A3CE00FE9D2A");
            Guid awayId = Guid.NewGuid();
            obj.HomeManager = CreateDebugManager(true, homeId, 0, 9, homePropVal);
            obj.AwayManager = CreateDebugManager(false, awayId, 1, 10, awayPropVal);
            FillFormData(obj);
            return obj;
        }
        public static ManagerInput CreateDebugManager(bool homeFlag, Guid mid, int index, int formId, double propVal)
        {
            var obj = new ManagerInput();
            obj.Mid = mid;
            obj.Kind = 0;
            obj.Name = "经理" + index;
            obj.Logo = "Logo" + index;
            obj.ClothId = 1;
            obj.FormId = formId;
            obj.FormLv = 9;
            obj.CoachId = 1;
            obj.CoachLv = 9;
            obj.BuffFact = 100;
            //obj.Skills = new string[] { "Test" }.ToList();
            //obj.SubSkills = new string[] { "Up,Down" };
            //obj.SubTactics = new string[] { "Up,Down" };
            if (homeFlag)
                obj.Skills = new string[] { "X001_10","X004_10" }.ToList();
            else
                obj.Skills = new string[] {}.ToList();
            const int cntPlayer=11;
            obj.Players = new List<PlayerInput>(cntPlayer);
            int pIdx=0;
            for (int i = 0; i < cntPlayer; i++)
            {
                pIdx = index * cntPlayer + i;
                obj.Players.Add(CreateDebugPlayer(homeFlag, pIdx, pIdx, propVal));
            }
            return obj;
        }
        public static PlayerInput CreateDebugPlayer(bool homeFlag,int index, int pid, double propVal)
        {
            var obj = new PlayerInput();
            obj.Pid = pid;
            obj.Tid = Guid.NewGuid();
            obj.Plus = 9;
            obj.Level = 30;
            obj.Position = 1;
            obj.Color = 1;
            obj.HeadStyle = 1;
            obj.BodyStyle = 2;
            obj.FirstName = "球员" + index.ToString();
            obj.FamilyName = "球员" + index.ToString();
            //obj.Skills = new string[] { "A002_1" }.ToList();
            //obj.SubSkills = new string[] { "Up,Down" };
            //if (homeFlag)
            //    obj.Skills = new string[] { "S006" }.ToList();
            //else
            //    obj.Skills = new string[] { }.ToList();
            obj.Speed = propVal;
            obj.Shooting = propVal;
            obj.FreeKick = propVal;
            obj.Balance = propVal;
            obj.Stamina = propVal;
            obj.Strength = propVal;
            obj.Aggression = propVal;
            obj.Disturb = propVal;
            obj.Interception = propVal;
            obj.Dribble = propVal;
            obj.Passing = propVal;
            obj.Mentality = propVal;
            obj.Reflexes = propVal;
            obj.Positioning = propVal;
            obj.Handling = propVal;
            obj.Acceleration = propVal;
            obj.PropList = new List<PropInput>();
            obj.BoostList = new List<BoostInput>();
            //obj.PropList.Add(new PropInput(2, 0.5, 1000, 1001));
            //obj.BoostList.Add(new BoostInput(7, 10, 0.1, 9901, 9903));
            return obj;
        }
        public static void FillFormData(MatchInput match)
        {
            List<FormationEntity> formCfg = null;
            foreach (var m in new[] { match.HomeManager, match.AwayManager })
            {
                formCfg = FormationCache.GetFormation(m.FormId);
                PlayerInput p = null;
                for (var i = 0; i < m.Players.Count; i++)
                {
                    p = m.Players[i];
                    p.Position = (byte) formCfg[i].Position;
                   
                }
            }
        }
        public static void FillFormData(MatchReport match)
        {
            List<FormationEntity> formCfg = null;
            int clientId = 0;
            foreach (var m in new[] { match.HomeManager, match.AwayManager })
            {
                clientId = m==match.HomeManager ? 0 : Defines.Match.MAX_PLAYER_COUNT;
                formCfg = FormationCache.GetFormation(m.FormId);
                PlayerReport p = null;
                for (var i = 0; i < m.Players.Count; i++)
                {
                    p = m.Players[i];
                    p.Position = (byte)formCfg[i].Position;
                    p.ClientId = i + clientId;
                    p.Name = p.ClientId.ToString();
                }
            }
 
        }
        public static void FillRoundData(MatchReport match)
        {
            int round = 0;
            foreach (var m in new[] { match.HomeManager, match.AwayManager })
            {
                PlayerReport p = null;
                for (var i = 0; i < m.Players.Count; i++)
                {
                    round = 0;
                    p = m.Players[i];
                    foreach (var mov in p.MoveResults)
                    {
                        if (round == 0 || mov.Round > 0)
                            round = mov.Round;
                        else
                            round++;
                        mov.AsRound = round;
                    }
                }
            }
            round = 0;
            foreach (var mov in match.BallResults)
            {
                if (round == 0 || mov.Round > 0)
                    round = mov.Round;
                else
                    round++;
                mov.AsRound = round;
            }
        }
    }
}
