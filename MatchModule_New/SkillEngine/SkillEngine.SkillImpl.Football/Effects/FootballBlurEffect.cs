using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillCore;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model;

namespace SkillEngine.SkillImpl.Football
{
    public class FootballBlurEffect : BlurEffect
    {
        #region .ctor
        public FootballBlurEffect(EnumBlurType blurType, EnumBlurBuffCode blurCode, EnumBuffExec execType,
            bool mainFlag, bool pureFlag)
            : base(blurType, (int)blurCode, execType, mainFlag, pureFlag)
        { }
        #endregion

        protected override bool AddBuff(ISkill srcSkill, ISkillPlayer caster, ISkillPlayer target, bool pureFlag, int last, int rate)
        {
            if (this.BlurCode == (int)EnumBlurBuffCode.Stun)
                last = srcSkill.Context.GetBuffLast(srcSkill, caster, last, target);
            if (!pureFlag)
            {
                if (BoostUtil.IfAntiDebuff(this, srcSkill, target, true, (int)EnumBoostRootType.AntiBlur, this.BlurCode))
                    return true;
                BoostUtil.GetEaseRate(out rate, rate, target, (int)EnumBoostRootType.BlurRate, this.BlurCode);
                if (this.Last > 0)
                {
                    BoostUtil.GetEaseLast(out last, last, target, (int)EnumBoostRootType.BlurLast, this.BlurCode);
                    last = srcSkill.Context.GetBuffLast(srcSkill, caster, last);
                }
            }
            if (!BuffUtil.ExecRate(out rate, rate, srcSkill.Context, this.ExecType))
                return false;
            if (rate <= 0)
                return true;
            int buffId = this.BuffId[0];
            if (buffId == (int)EnumBlurType.BanMan)
            {
                target.DisableState = (int)this.BlurCode;
                target.RaiseBlurEvent(new BlurEventArgs(target.BlurSrcSkill, target, target, (EnumBlurType)buffId, (EnumBlurBuffCode)this.BlurCode));
            }
            else
            {
                if (buffId == (int)EnumBlurType.LockMotion)
                    target.RemoveBuff(buffId, 0);
                if (!target.AddBuff(InnerCreateBuff(srcSkill, target, (short)last, rate)))
                    return false;
                if (this.BlurCode == (int)EnumBlurBuffCode.Rebel)
                    ((IPlayer)target).AddSilenceBuff(last);
                if(this.BlurCode==(int)EnumBlurBuffCode.Falldown)
                    target.RaiseBlurEvent(new BlurEventArgs(target.BlurSrcSkill, target, target, (EnumBlurType)buffId, (EnumBlurBuffCode)this.BlurCode));
            }
            this.AddTgtShowModel(srcSkill, target, last);
            return true;
        }
        protected override IBuff InnerCreateBuff(ISkill srcSkill, ISkillPlayer target, short last, int rate)
        {
            if (last > 0)
                last += srcSkill.Context.MatchRound;
            IBuff buff = null;
            if (this.BuffId[0] == (int)EnumBlurType.BanMan)
            {
                buff = new FootballDisableBuff(srcSkill, (EnumBlurBuffCode)this.BlurCode);
            }
            else
                buff = new FootballBlurBuff(srcSkill, (EnumBlurType)this.BuffId[0], (EnumBlurBuffCode)this.BlurCode);
            buff.Rate = rate;
            buff.TimeEnd = last;
            buff.Times = this.Repeat;
            return buff;
        }
        public override INormalEffect Clone()
        {
            var effect = new FootballBlurEffect((EnumBlurType)this.BuffId[0], (EnumBlurBuffCode)this.BlurCode, this.ExecType, this.MainFlag, this.PureFlag);
            effect.CopyValue(this);
            return effect;
        }
       
    }
}
