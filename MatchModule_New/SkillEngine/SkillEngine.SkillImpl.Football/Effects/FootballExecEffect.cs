using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.SkillBase.Xtern;
using Games.NB.Match.Base.Interface;

namespace SkillEngine.SkillImpl.Football
{
    public class FootballExecEffect : EffectBase, INormalEffect
    {
        #region .ctor
        public FootballExecEffect(EnumSpecEffect buffId, bool mainFlag, bool pureFlag, bool debuffFlag)
            : base(EnumBuffType.Spec, new int[] { (int)buffId }, mainFlag, pureFlag, debuffFlag)
        { }
        #endregion

        #region Facade
        public bool EffectManager(ISkill srcSkill, ISkillPlayer caster, IList<ISkillManager> dstManagers)
        {
            if (null == dstManagers || dstManagers.Count == 0)
                return false;
            if (this.BuffId[0] != (int)EnumSpecEffect.FalldownThenInjure)
            {
                int maxRate = SkillDefines.MAXStorePercent;
                if (this.Rate <= 0 || this.Rate < maxRate && srcSkill.Context.RandomPercent(maxRate) > this.Rate)
                    return false;
            }
            switch ((EnumSpecEffect)this.BuffId[0])
            {
                case EnumSpecEffect.HighPass:
                    ((IMatch)srcSkill.Context).Football.ForceInAir();
                    break;
                case EnumSpecEffect.PassOutside:
                    foreach (var m in dstManagers)
                    {
                        foreach (IPlayer player in dstManagers)
                        {
                            if (((IMatch)srcSkill.Context).PassOutside(player))
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                case EnumSpecEffect.Reborn:
                    foreach (var m in dstManagers)
                    {
                        foreach (IPlayer player in dstManagers)
                        {
                            player.BlurEvent -= PlayerRebornHandler;
                            player.BlurEvent += PlayerRebornHandler;
                        }
                    }
                    return true;
                case EnumSpecEffect.FalldownThenInjure:
                    foreach (var m in dstManagers)
                    {
                        foreach (IPlayer player in dstManagers)
                        {
                            player.BlurEvent -= PlayerFalldownThenInjureHandler;
                            player.BlurEvent += PlayerFalldownThenInjureHandler;
                        }
                    }
                    return true;
            }
            return true;
        }

        public bool EffectPlayers(ISkill srcSkill, ISkillPlayer caster, IList<ISkillPlayer> dstPlayers)
        {
            if (null == dstPlayers || dstPlayers.Count == 0)
                return false;
            if (this.BuffId[0] != (int)EnumSpecEffect.FalldownThenInjure)
            {
                int maxRate = SkillDefines.MAXStorePercent;
                if (this.Rate <= 0 || this.Rate < maxRate && srcSkill.Context.RandomPercent(maxRate) > this.Rate)
                    return false;
            }
            this.AddSrcShowModel(srcSkill, caster, this.Last);
            bool rtnVal = false;
            switch ((EnumSpecEffect)this.BuffId[0])
            {
                case EnumSpecEffect.HighPass:
                    ((IMatch)srcSkill.Context).Football.ForceInAir();
                    rtnVal = true;
                    break;
                case EnumSpecEffect.PassOutside:
                    foreach (IPlayer player in dstPlayers)
                    {
                        if (((IMatch)srcSkill.Context).PassOutside(player))
                        {
                            rtnVal = true;
                            break;
                        }
                    }
                    break;
                case EnumSpecEffect.Reborn:
                    foreach (IPlayer player in dstPlayers)
                    {
                        player.BlurEvent -= PlayerRebornHandler;
                        player.BlurEvent += PlayerRebornHandler;
                    }
                    rtnVal = true;
                    break;
                case EnumSpecEffect.FalldownThenInjure:
                    foreach (IPlayer player in dstPlayers)
                    {
                        player.BlurEvent -= PlayerFalldownThenInjureHandler;
                        player.BlurEvent += PlayerFalldownThenInjureHandler;
                    }
                    rtnVal = true;
                    break;
            }
            if (!rtnVal)
                this.RemoveShowModel(srcSkill, caster, false);
            return true;
        }
        #endregion

        void PlayerRebornHandler(object sender, BlurEventArgs e)
        {
            if (null == e.OwnPlayer)
                return;
            if (e.BlurType == EnumBlurType.BanMan)
                e.OwnPlayer.DisableState = (int)EnumBlurBuffCode.Reborn;
        }

        void PlayerFalldownThenInjureHandler(object sender, BlurEventArgs e)
        {
            if (null == e.OwnPlayer)
                return;
            if (e.BlurCode == EnumBlurBuffCode.Falldown)
            {
                int maxRate = SkillDefines.MAXStorePercent;
                if (this.Rate <= 0 || this.Rate < maxRate && ((IPlayer)e.OwnPlayer).Match.RandomPercent(maxRate) > this.Rate)
                    return;
                e.OwnPlayer.DisableState = (int)EnumBlurBuffCode.FalldownThenInjure;
            }
        }

        public INormalEffect Clone()
        {
            var effect = new FootballExecEffect((EnumSpecEffect)this.BuffId[0], this.MainFlag, this.PureFlag, this.DebuffFlag);
            effect.CopyValue(this);
            return effect;
        }

    }
}
