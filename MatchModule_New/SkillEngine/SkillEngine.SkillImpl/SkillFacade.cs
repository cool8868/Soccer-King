using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SkillEngine.Extern;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillCore;

namespace SkillEngine.SkillImpl
{
    public static class SkillFacade
    {
        #region Boot
        public static void Boot()
        {
            RawSkillCache.Instance().Init();
        }
        #endregion

        #region Load
        public static void LoadManagerSkills(ISkillContext context, ISkillManager owner, List<string> skills)
        {
            if (null == owner.SkillCore)
                owner.SkillCore = new SkillCore(context);
            owner.SkillCore.LoadSkill(owner, skills);
        }
        public static void LoadPlayerSkills(ISkillContext context, ISkillPlayer owner, List<string> skills)
        {
            if (null == owner.SkillCore)
                owner.SkillCore = new SkillCore(context);
            owner.SkillCore.LoadSkill(owner, skills);
        }
        public static void LoadHideSkills(ISkillContext context, ISkillManager owner)
        {
            var hideSkills = RawSkillCache.Instance().GetHideSkills();
            if (hideSkills.Count == 0)
                return;
            ISkill skill = null;
            foreach (var rawSkill in hideSkills)
            {
                skill = new Skill(context, owner, "");
                skill.Load(rawSkill);
                skill.Invoke();
            }
        }
        #endregion

        #region Trigger
        public static void TriggerPlayerSkills(ISkillPlayer srcPlayer, byte timeFlag,bool checkFlag=false)
        {
            if (null != srcPlayer.SkillCore)
            {
                if (!checkFlag || !BuffUtil.IfSilence(srcPlayer))
                    srcPlayer.SkillCore.InvokeSkill(timeFlag);
            }
        }
        public static void TriggerManagerSkills(ISkillManager srcManager, byte timeFlag)
        {
            if (null != srcManager.SkillCore)
                srcManager.SkillCore.InvokeSkill(timeFlag);
        }
        #endregion

        #region 免疫犯规
        public static bool IfAntiFoul(ISkillPlayer player)
        {
            if (null == player)
                return false;
            IBoostBuff boost = null;
            int maxRate = SkillDefines.MAXStorePercent;
            if (player.TryGetAntiRate(ref boost, (int)EnumBoostRootType.AntiFoul))
            {
                int rate = boost.Percent;
                if (rate >= maxRate)
                    return true;
                if (rate > 0 && rate < maxRate)
                {
                    if (player.SkillManager.SkillMatch.RandomPercent(maxRate) <= rate)
                        return true;
                }
            }
            return false;
        }
        #endregion
    }
}
