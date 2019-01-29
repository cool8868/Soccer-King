using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillBase
{
    public static class BoostUtil
    {
        #region GetBoost
        public static bool GetAmpLast(out int last, int inLast, ISkillTarget owner, params int[] buffIds)
        {
            return GetLastCore(out last, inLast, EnumBoostType.AmpLast, owner, buffIds);
        }
        public static bool GetAmpRate(out int rate, int inRate, ISkillTarget owner, params int[] buffIds)
        {
            return GetRateCore(out rate, inRate, EnumBoostType.AmpRate, owner, buffIds);
        }
        public static bool GetAmpValue(out int point, out int percent, int inPoint, int inPercent, ISkillTarget owner, params int[] buffIds)
        {
            return GetValueCore(out point, out percent, inPoint, inPercent, EnumBoostType.AmpValue, owner, buffIds);
        }
        public static bool GetAmpValue(out int point, out int percent, ISkillTarget owner, params int[] buffIds)
        {
            return GetValueCore(out point, out percent, EnumBoostType.AmpValue, owner, buffIds);
        }
        public static bool GetEaseLast(out int last, int inLast, ISkillOwner owner, params int[] buffIds)
        {
            return GetLastCore(out last, inLast, EnumBoostType.EaseLast, owner, buffIds);
        }
        public static bool GetEaseRate(out int rate, int inRate, ISkillOwner owner, params int[] buffIds)
        {
            return GetRateCore(out rate, inRate, EnumBoostType.EaseRate, owner, buffIds);
        }
        public static bool GetEaseValue(out int point, out int percent, int inPoint, int inPercent, ISkillOwner owner, params int[] buffIds)
        {
            return GetValueCore(out point, out percent, inPoint, inPercent, EnumBoostType.EaseValue, owner, buffIds);
        }
        public static bool GetEaseValue(out int point, out int percent, ISkillOwner owner, params int[] buffIds)
        {
            return GetValueCore(out point, out percent, EnumBoostType.EaseValue, owner, buffIds);
        }
        public static bool GetAntiRate(out int rate, ISkillOwner owner, params int[] buffIds)
        {
            return GetRateCore(out rate, 0, EnumBoostType.AntiRate, owner, buffIds);
        }
        public static bool IfPureBuff(IEffect effect, ISkill srcSkill, ISkillPlayer caster)
        {
            if (effect.PureFlag)
                return true;
            IBoostBuff boost = null;
            int maxRate = SkillDefines.MAXStorePercent;
            ISkillOwner owner = caster ?? srcSkill.Owner;
            owner.TryGetPureBuff(ref boost);
            if (srcSkill.TryGetPureBuff(ref boost))
            {
                int rate = boost.Percent;
                if (rate >= maxRate)
                    return true;
                if (rate > 0 && rate < maxRate)
                {
                    if (srcSkill.Context.RandomPercent(maxRate) <= rate)
                        return true;
                }
            }
            return false;
        }
        public static bool IfAntiDebuff(IEffect effect, ISkill srcSkill, ISkillOwner target, params int[] buffIds)
        {
            return IfAntiDebuff(effect, srcSkill, target, false, buffIds);
        }
        public static bool IfAntiDebuff(IEffect effect, ISkill srcSkill, ISkillOwner target, bool incRoot, params int[] buffIds)
        {
            if (effect.PureFlag || !effect.DebuffFlag)
                return false;
            IBoostBuff boost = null;
            int maxRate = SkillDefines.MAXStorePercent;
            int rate = 0;
            if (incRoot)
            {
                if (target.TryGetAntiDebuff(ref boost))
                {
                    rate = boost.Percent;
                    if (rate >= maxRate || rate > 0 && srcSkill.Context.RandomPercent(maxRate) <= rate)
                        return true;
                    boost.Clear();
                }
            }
            if (null == buffIds || buffIds.Length == 0)
                return false;
            if (target.TryGetAntiRate(ref boost, buffIds))
            {
                do
                {
                    var boostAnti = boost as IBoostAntiBuff;
                    if (null == boostAnti)
                        break;
                    var antiSKill = boostAnti.AntiSkillType;
                    if (null == antiSKill || antiSKill.Length == 0)
                        break;
                    int skillType = (int)srcSkill.RawSkill.SrcType;
                    for (int i = 0; i < antiSKill.Length; i++)
                    {
                        if (antiSKill[i] == skillType)
                            break;
                        else if (i == antiSKill.Length - 1)
                            return false;
                    }
                }
                while (false);
                rate = boost.Percent;
                if (rate >= maxRate || rate > 0 && srcSkill.Context.RandomPercent(maxRate) <= rate)
                    return true;
            }
            return false;
        }

        static bool GetLastCore(out int last, int inLast, EnumBoostType boostType, ISkillTarget owner, params  int[] buffIds)
        {
            last = inLast;
            if (last <= 0)
                return false;
            IBoostBuff boost = null;
            switch (boostType)
            {
                case EnumBoostType.AmpLast:
                    if (owner.TryGetAmpLast(ref boost, buffIds))
                        last = Convert.ToInt16(last + boost.Point + last * boost.AsPercent);
                    break;
                case EnumBoostType.EaseLast:
                    if (owner.TryGetEaseLast(ref boost, buffIds))
                        last = Convert.ToInt16(last + boost.Point + last * boost.AsPercent);
                    break;
            }
            return last != inLast;
        }
        static bool GetRateCore(out int rate, int inRate, EnumBoostType boostType, ISkillTarget owner, params  int[] buffIds)
        {
            rate = inRate;
            IBoostBuff boost = null;
            switch (boostType)
            {
                case EnumBoostType.AmpRate:
                    if (owner.TryGetAmpRate(ref boost, buffIds))
                        rate = Convert.ToInt32(inRate + boost.Point + inRate * boost.AsPercent);
                    break;
                case EnumBoostType.EaseRate:
                    if (owner.TryGetEaseRate(ref boost, buffIds))
                        rate = Convert.ToInt32(inRate + boost.Point + inRate * boost.AsPercent);
                    break;
                case EnumBoostType.AntiRate:
                    if (owner.TryGetAntiRate(ref boost, buffIds))
                        rate = boost.Percent;
                    return rate > 0;
            }
            return rate != inRate;
        }
        static bool GetValueCore(out int point, out int percent, EnumBoostType boostType, ISkillTarget owner, params  int[] buffIds)
        {
            point = 0;
            percent = 0;
            IBoostBuff boost = null;
            switch (boostType)
            {
                case EnumBoostType.AmpValue:
                    if (owner.TryGetAmpValue(ref boost, buffIds))
                    {
                        point = boost.Point;
                        percent = boost.Percent;
                    }
                    break;
                case EnumBoostType.EaseValue:
                    if (owner.TryGetEaseValue(ref boost, buffIds))
                    {
                        point = boost.Point;
                        percent = boost.Percent;
                    }
                    break;
            }
            return point != 0 || percent != 0;
        }
        static bool GetValueCore(out int point, out int percent, int inPoint, int inPercent, EnumBoostType boostType, ISkillTarget owner, params  int[] buffIds)
        {
            point = inPoint;
            percent = inPercent;
            IBoostBuff boost = null;
            switch (boostType)
            {
                case EnumBoostType.AmpValue:
                    if (owner.TryGetAmpValue(ref boost, buffIds))
                    {
                        if (point != 0)
                            point = Convert.ToInt32(inPoint + boost.Point + inPoint * boost.AsPercent);
                        if (percent != 0)
                            percent = Convert.ToInt32(inPercent + boost.Point + inPercent * boost.AsPercent);
                    }
                    break;
                case EnumBoostType.EaseValue:
                    if (owner.TryGetEaseValue(ref boost, buffIds))
                    {
                        if (point != 0)
                            point = Convert.ToInt32(inPoint + boost.Point + inPoint * boost.AsPercent);
                        if (percent != 0)
                            percent = Convert.ToInt32(inPercent + boost.Point + inPercent * boost.AsPercent);
                    }
                    break;
            }
            return point != inPoint || percent != inPercent;
        }
        #endregion
    }
}
