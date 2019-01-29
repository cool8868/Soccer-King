using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.Emulator.WPF.Entity.LocalData;
using Games.NBall.Bll.Frame;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Share;
using Games.NBall.Bll.Cache;
using Games.NBall.Entity;
using Games.NBall.Entity.NBall.Custom.ItemUsed;
using Games.NBall.Entity.Response.Frame;

namespace Games.NB.Match.Emulator.WPF.Mgrs
{
    public class LocalManagerHelper
    {
        private static readonly int PLAERCOUNT = SystemConstants.TeammemberCount;
        public static ManagerInput BuildTransfer(LocalTransferManagerEntity localManager)
        {
            var buffView = GetMemberView(localManager);

            var dstData = new ManagerInput();
            dstData.Mid = LocalHelper.BuildLocalManagerId(localManager.Id);
            dstData.Logo = "1";
            dstData.Name = localManager.Name;
            dstData.FormId = localManager.FormationId;
            dstData.FormLv = localManager.FormationLevel;
            dstData.Skills = buffView.LiveSkillList;
            dstData.SubSkills = buffView.SubSkills;

            MatchTransferUtil.BuildManagerData(dstData, buffView, 0, 100);

            return dstData;
        }

        public static void SaveBefore(LocalTransferManagerEntity localManager)
        {
            var view = GetMemberView(localManager);
            if(view==null)
                return;
            var formation = LocalHelper.LocalCache.Formations.Find(d => d.Key == localManager.FormationId.ToString());
            if (formation != null)
                localManager.FormationStr = formation.Value;
            
            localManager.Kpi = view.Kpi;
            foreach (var buffMember in view.BuffMembers.Values)
            {
                var p = localManager.Players.Find(d => d.PlayerId == buffMember.Pid);
                if (p != null)
                    p.Kpi = buffMember.Kpi;
            }
        }


        public static DTOBuffMemberView GetMemberView(LocalTransferManagerEntity localManager)
        {
            var data = new DTOBuffMemberView();
            //TODO: CombLevel

            var managerSBMList = new List<string>();
            //套装字典 套装id->数量
            var suitDic = new Dictionary<int, int>();
            //套装id->套装类型
            var suitTypeDic = new Dictionary<int, int>();

            var buffPlayers = new Dictionary<Guid, DTOBuffPlayer>(PLAERCOUNT);
            foreach (var playerEntity in localManager.Players)
            {
                buffPlayers.Add(Guid.NewGuid(),BuildPlayer(playerEntity));
            }
            
            //套装
            //TeammemberDataHelper.FillSuitData(suitDic, suitTypeDic, ref managerSBMList);
            //阵型加成
            TeammemberDataHelper.FillFormationData(localManager.FormationId, localManager.FormationLevel, ref managerSBMList);
            //天赋意志处理
            var skills = new List<string>();
            string[] subSkills = new string[2];
            //天赋
            if (localManager.TalentId > 0)
            {
                var localTalent = LocalHelper.TalentList.Talents.Find(d => d.Id == localManager.TalentId);
                if (localTalent != null)
                {
                    string talent = string.Empty;
                    LocalTalentEntity talentCfg = null;
                    int len = 0;
                    foreach (var talentdata in localTalent.Talentdatas)
                    {
                        talent = talentdata.Id;
                        talentCfg = LocalHelper.LocalCache.Talents.Find(i => i.Id == talent);
                        if (null == talentCfg)
                            continue;
                        if (talentCfg.DriveType == 0 || len >= 2)
                            skills.Add(talent);
                        else
                            subSkills[len++] = talent;
                    }
                }
            }
            //意志 组合
            if (localManager.WillId > 0)
            {
                var localWill = LocalHelper.WillList.Wills.Find(d => d.Id == localManager.WillId);
                if (localWill != null)
                {
                    foreach (var willdata in localWill.Willdatas)
                    {
                        skills.Add(willdata.Id);
                    }
                }
            }
            //套装效果
            if (localManager.SuitId > 0)
            {
                var locaSuit = LocalHelper.SuitList.Suits.Find(d => d.Id == localManager.SuitId);
                if (locaSuit != null)
                {
                    foreach (var suitdata in locaSuit.Suitdatas)
                    {
                        skills.Add(suitdata.Id.ToString());
                    }
                }
            }
            var rankSkills = BuffCache.Instance().GetRankedSkillList(skills);
            foreach (var item in skills)
            {
                if (item.Substring(0, 1).ToUpper() == "H")
                    rankSkills[1].Add(item);
            }
            var buffPack = new DTOBuffPack();
            const bool homeFlag=true;
            buffPack.SetBuffPlayers(homeFlag, buffPlayers);
            buffPack.SetOnBuffPlayers(homeFlag, buffPlayers.Values.ToList());
            BuffFlowFacade.ProcManagerBuff(buffPack, homeFlag, rankSkills[2], false);
            data.ReadySkillList = rankSkills[0];
            data.LiveSkillList = rankSkills[1];
            data.SubSkills = subSkills;
            BuffDataCore.Instance().FillBuffView(data, buffPlayers, true, 1);
            //TODO:球员组合
            //DTOBuffPlayer player = null;
            //foreach (var member in data.BuffMembers.Values)
            //{
            //    if (!buffPlayers.TryGetValue(member.Tid, out player))
            //        continue;
            //    if (string.IsNullOrEmpty(player.StarSkill))
            //        continue;
            //    if (null == member.LiveSkillList)
            //        member.LiveSkillList = new List<string>();
            //    member.LiveSkillList.Add(player.StarSkill);
            //}
            return data;
        }

        static DTOBuffPlayer BuildPlayer(LocalTransferPlayerEntity playerEntity)
        {
            var cfg = LocalHelper.LocalCache.Players.Find(d => d.Idx == playerEntity.PlayerId);
            var rawProps = GetTeammemberProps(playerEntity);
            var obj = new DTOBuffPlayer();
            obj.Pid = playerEntity.PlayerId;
            obj.Pos = playerEntity.Position;
            obj.Clr = cfg==null?0:cfg.CardLevel;
            obj.Props = new DTOBuffProp[rawProps.Length];
            for (int i = 0; i < rawProps.Length; ++i)
            {
                obj.Props[i] = new DTOBuffProp { Orig = rawProps[i] };
            }
            rawProps = null;
            obj.Level = 1;
            obj.SBMList = new List<string>();
            obj.Strength = 0;
            obj.ActionSkill = playerEntity.Skill;
            obj.StarSkill = playerEntity.StarSkill;
            return obj;
        }

        public static double[] GetTeammemberProps(LocalTransferPlayerEntity src)
        {
            return new double[]{src.Speed,src.Shooting,src.FreeKick,
                src.Balance,src.Stamina,src.Strength,
                src.Aggression,src.Disturb,src.Interception,
                src.Dribble,src.Passing,src.Mentality,
                src.Reflexes,src.Positioning,src.Handling,
                src.Acceleration};
        }
    }
}
