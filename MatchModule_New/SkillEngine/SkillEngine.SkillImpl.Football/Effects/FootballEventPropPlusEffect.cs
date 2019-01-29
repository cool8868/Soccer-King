using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillCore;
using SkillEngine.Extern.Enum;

namespace SkillEngine.SkillImpl.Football
{
    public class FootballEventPropPlusEffect : EffectBase, INormalEffect
    {
        #region .ctor
        public FootballEventPropPlusEffect(int[] buffId,
            bool mainFlag, bool pureFlag, bool debuffFlag)
            : base(EnumBuffType.PropPlus, buffId, mainFlag, pureFlag, debuffFlag)
        {
            this.ExecType = EnumBuffExec.None;
            this.EventType = EnumBuffEventType.None;
            this.Side = EnumEventTargetSide.OwnPlayer;
        }
        #endregion

        #region Data
        public EnumEventTargetSide Side
        {
            get;
            set;
        }
        public EnumBuffExec ExecType
        {
            get;
            set;
        }
        public EnumBuffEventType EventType
        {
            get;
            set;
        }
        #endregion

        #region Facade
        public bool EffectManager(ISkill srcSkill, ISkillPlayer caster, IList<ISkillManager> dstManagers)
        {
            return false;
        }
        public bool EffectPlayers(ISkill srcSkill, ISkillPlayer caster, IList<ISkillPlayer> dstPlayers)
        {
            if (null == dstPlayers || dstPlayers.Count == 0)
                return false;
            if (EventType == EnumBuffEventType.None)
                return false;
            foreach (var target in dstPlayers)
            {
                switch (EventType)
                {
                    case EnumBuffEventType.Blur:
                        target.BlurEvent -= OnBlur;
                        target.BlurEvent += OnBlur;
                        target.BlurSrcSkill = srcSkill;
                        break;
                    case EnumBuffEventType.Foul:
                        target.FoulEvent -= OnFoul;
                        target.FoulEvent += OnFoul;
                        target.FoulSrcSkill = srcSkill;
                        break;
                }
            }
            return true;
        }
        public bool UnEffecttPlayers(ISkill srcSkill, ISkillPlayer caster, List<ISkillPlayer> dstPlayers)
        {
            if (null == dstPlayers || dstPlayers.Count == 0)
                return true;
            if (EventType == EnumBuffEventType.None)
                return true;
            foreach (var target in dstPlayers)
            {
                switch (EventType)
                {
                    case EnumBuffEventType.Blur:
                        target.BlurEvent -= OnBlur;
                        break;
                    case EnumBuffEventType.Foul:
                        target.FoulEvent -= OnFoul;
                        break;
                }
            }
            return true;
        }
        void OnBlur(object sender, BlurEventArgs e)
        {
            if (null == e)
                return;
            var target = GetTarget(e.OwnPlayer, e.OppPlayer);
            if (null == target)
                return;
            AddBuff(e.SrcSkill, e.OwnPlayer, target);
        }
        void OnFoul(object sender, FoulEventArgs e)
        {
            if (null == e)
                return;
            var target = GetTarget(e.OwnPlayer,e.OppPlayer);
            if (null == target)
                return;
            AddBuff(e.SrcSkill, e.OwnPlayer, target);
        }
        #endregion

        #region Tools
        ISkillOwner GetTarget(ISkillPlayer ownPlayer,ISkillPlayer oppPlayer)
        {
            switch (Side)
            {
                case EnumEventTargetSide.OwnPlayer:
                    return ownPlayer;
                case EnumEventTargetSide.OppPlayer:
                    return oppPlayer ?? ownPlayer.OppSkillPlayer;
                case EnumEventTargetSide.OwnManager:
                    return ownPlayer.SkillManager;
                case EnumEventTargetSide.OppManager:
                    return ownPlayer.SkillManager.OppSkillManager;
                default:
                    return ownPlayer;
            }
        }
        protected bool AddBuff(ISkill srcSkill, ISkillPlayer caster, ISkillOwner target)
        {
            if (!this.PureFlag)
            {
                if (BoostUtil.IfAntiDebuff(this, srcSkill, target, true, (int)EnumBoostRootType.AntiPropDebuff))
                    return true;
            }
            if (this.ExecType != EnumBuffExec.None)
            {
                int rate = this.Rate;
                int maxRate = SkillDefines.MAXStorePercent;
                if (rate >= 0 && rate < maxRate)
                {
                    if (srcSkill.Context.RandomPercent(maxRate) > rate)
                        return false;
                }
            }
            target.AddBuff(InnerCreatePropBuff(srcSkill, caster));
            return true;
        }
        IBuff InnerCreatePropBuff(ISkill srcSkill, ISkillPlayer caster)
        {
            short last = BuffUtil.GetBuffLast(srcSkill, caster, this.Last);
            if (last > 0)
                last += srcSkill.Context.MatchRound;
            //int arg;
            //if (!int.TryParse(srcSkill.Args, out arg))
            //    arg = 1;
            return new FootballPropBuff(srcSkill, this.BuffId)
            {
                Rate = SkillDefines.MAXStorePercent,
                //Point = Convert.ToInt32(this.Point * (0.95 + arg * 0.05)),
                //Percent = Convert.ToInt32(this.Percent * (0.95 + arg * 0.05)),
                Point = this.Point,
                Percent = this.Percent,
                TimeEnd = last,
                Times = this.Repeat,
            };
        }
        #endregion

        public INormalEffect Clone()
        {
            var effect = new FootballEventPropPlusEffect(this.BuffId, this.MainFlag, this.PureFlag, this.DebuffFlag);
            effect.CopyValue(this);
            effect.Side = this.Side;
            effect.ExecType = this.ExecType;
            effect.EventType = this.EventType;
            return effect;
        }
    }
}
