using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;


namespace SkillEngine.SkillBase
{
    public static class BuffUtil
    {
        #region 执行概率
        public static bool ExecRate(out int hitRate, int rate, ISkillContext context, EnumBuffExec execType)
        {
            hitRate = rate;
            int maxRate = SkillDefines.MAXStorePercent;
            switch (execType)
            {
                case EnumBuffExec.None:
                    return true;
                case EnumBuffExec.IgnoreHit:
                    if (context.RandomPercent(maxRate) <= rate)
                        hitRate = maxRate;
                    else
                        hitRate = 0;
                    return true;
                case EnumBuffExec.MustHit:
                    if (context.RandomPercent(maxRate) <= rate)
                        hitRate = maxRate;
                    else
                        hitRate = 0;
                    return hitRate > 0;
            }
            return true;
        }
        #endregion

        #region 判断异常
        public static bool IfSilence(ISkillPlayer player)
        {
            IBuff blurBuff = null;
            return player.TryGetBuff((int)EnumBlurType.LockMotion, ref blurBuff)
                || player.TryGetBuff((int)EnumBlurType.LockSkill, ref blurBuff);
        }
        public static bool IfSilence(ISkill srcSkill, ISkillOwner owner)
        {
            IBuff blurBuff = null;
            if (owner.TryGetBuff((int)EnumBlurType.LockMotion, ref blurBuff))
                return true;
            if (!owner.TryGetBuff((int)EnumBlurType.LockSkill, ref blurBuff))
                return false;
            int maxRate = SkillDefines.MAXStorePercent;
            int rate = blurBuff.Rate;
            if (rate >= maxRate)
                return true;
            if (rate > 0 && rate < maxRate)
            {
                if (srcSkill.Context.RandomPercent(maxRate) <= rate)
                    return true;
            }
            return false;
        }
        public static void ClearBlur(ISkillOwner owner)
        {
            owner.RemoveBuff((int)EnumBlurType.LockMotion, 0);
            owner.RemoveBuff((int)EnumBlurType.LockSkill, 0);
        }
        #endregion

        #region 触发时间
        public static EnumSkillTimeType CheckSkillTime(ISkillContext context)
        {
            int round = context.MatchRound;
            if (round <= 0)
                return EnumSkillTimeType.Session;
            int weight = context.RoundPerSection;
            if (weight > 0 && round % weight == 1)
                return EnumSkillTimeType.Section;
            weight = context.RoundPerMinute;
            if (weight > 0 && round % weight == 1)
                return EnumSkillTimeType.Minute;
            return EnumSkillTimeType.Round;
        }
        public static int GetTimeWeight(ISkillContext context, EnumSkillTimeType timeType)
        {
            switch (timeType)
            {
                case EnumSkillTimeType.Minute:
                    return context.RoundPerMinute;
                case EnumSkillTimeType.Section:
                    return context.RoundPerSection;
                default:
                    return 1;
            }
        }
        #endregion

        #region 持续时间
        public static short GetBuffLast(ISkill srcSkill, ISkillPlayer caster, int last)
        {
            return srcSkill.Context.GetBuffLast(srcSkill, caster, last);
        }
        #endregion
    }
}
