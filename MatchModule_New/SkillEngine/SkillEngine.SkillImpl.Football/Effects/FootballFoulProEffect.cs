﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.Extern.Enum.Football;
using SkillEngine.SkillCore;
using Games.NB.Match.Base.Interface;


namespace SkillEngine.SkillImpl.Football
{
    public class FootballProFoulEffect : FoulEffect
    {
        #region .ctor
        public FootballProFoulEffect(EnumFoulBuffCode foulCode, bool mainFlag, bool pureFlag)
            : base((int)foulCode, CastFoulState(foulCode), mainFlag, pureFlag)
        {
        }
        #endregion

        public override bool EffectPlayers(ISkill srcSkill, ISkillPlayer caster, IList<ISkillPlayer> dstPlayers)
        {
            if (null == dstPlayers || dstPlayers.Count == 0)
                return false;
            int rate;
            int buffId = this.BuffId[0];
            GetAmp(srcSkill, caster, buffId, out rate);
            short last = srcSkill.Context.GetBuffLast(srcSkill, caster, this.Last);
            this.AddSrcShowModel(srcSkill, caster, last);
            bool rtnVal = false;
            foreach (var dstPlayer in dstPlayers)
            {
                rtnVal |= FoulPlayer(srcSkill, caster, dstPlayer, buffId, rate, 1);
                if (rtnVal)
                    break;
            }
            if (!rtnVal)
                this.RemoveShowModel(srcSkill, caster, false);
            return rtnVal;
        }

        protected override bool InnerFoul(ISkill srcSkill, ISkillPlayer target, int foulType)
        {
            if (srcSkill.Context.RandomPercent(SkillDefines.MAXStorePercent) <= this.Percent)
                foulType = this.FoulType;
            ((IPlayer)target).Foul((byte)foulType);
            return true;
        }

        public override INormalEffect Clone()
        {
            var effect = new FootballProFoulEffect((EnumFoulBuffCode)this.BuffId[0], this.MainFlag, this.PureFlag);
            effect.CopyValue(this);
            return effect;
        }

        static byte CastFoulState(EnumFoulBuffCode foulCode)
        {
            switch (foulCode)
            {
                case EnumFoulBuffCode.FoulNormal:
                    return (byte)EnumFoulState.FoulNormal;
                case EnumFoulBuffCode.FoulYellow:
                    return (byte)EnumFoulState.FoulYellow;
                case EnumFoulBuffCode.FoulRed:
                    return (byte)EnumFoulState.FoulRed;
            }
            return (byte)EnumFoulState.FoulNormal;
        }
    }
}