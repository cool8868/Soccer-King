using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillBase
{
    public abstract class EffectorBase<T> : IEffector where T : ISkillTarget
    {
        #region Cache
        protected readonly ILocator<T> _locator;
        protected readonly List<T> _targets;
        protected readonly Dictionary<ISkillPlayer, List<T>> _castTargets;
        protected readonly List<T> _undoTargets;
        protected readonly List<T> _newTargets;
        protected readonly List<IEffect> _effects;
        protected List<IBoostEffect> _booster;
        protected List<INormalEffect> _buffFeeder;
        protected List<ISpecEffect> _specFeeder;
        protected IEffect _mainEffect = null;
        protected bool _redoFlag = false;
        protected byte _undoFlag = 0;
        protected EnumSkillFlag _skillFlag = EnumSkillFlag.None;
        #endregion

        #region .ctor
        protected EffectorBase(ILocator<T> locator, List<IEffect> effects)
        {
            this._locator = locator;
            this._targets = new List<T>();
            this._castTargets = new Dictionary<ISkillPlayer, List<T>>(SkillPlayerComparer.Instance);
            this._undoTargets = new List<T>();
            this._newTargets = new List<T>();
            this._effects = new List<IEffect>();
            if (null != effects)
                this._effects.AddRange(effects);
            foreach (var effect in this._effects)
            {
                if (effect is INormalEffect)
                {
                    if (null == this._buffFeeder)
                        this._buffFeeder = new List<INormalEffect>();
                    this._buffFeeder.Add((INormalEffect)effect);
                }
                else if (effect is IBoostEffect)
                {
                    if (null == this._booster)
                        this._booster = new List<IBoostEffect>();
                    this._booster.Add((IBoostEffect)effect);
                }
                else if (effect is ISpecEffect)
                {
                    if (null == this._specFeeder)
                        this._specFeeder = new List<ISpecEffect>();
                    this._specFeeder.Add((ISpecEffect)effect);
                }
            }
            this.CheckEffectFlag();
        }
        protected EffectorBase(ISkill srcSkill, ILocator<T> locator, List<IBoostEffect> booster, List<INormalEffect> buffFeeder, List<ISpecEffect> specFeeder)
        {
            this._locator = locator;
            this._targets = new List<T>();
            this._castTargets = new Dictionary<ISkillPlayer, List<T>>(SkillPlayerComparer.Instance);
            this._undoTargets = new List<T>();
            this._newTargets = new List<T>();
            this._effects = new List<IEffect>();
            if (null != buffFeeder)
            {
                this._buffFeeder = new List<INormalEffect>(buffFeeder.Count);
                foreach (var item in buffFeeder)
                {
                    this._buffFeeder.Add(item);
                }
                this._effects.AddRange(this._buffFeeder);
            }
            if (null != booster && booster.Count > 0)
            {
                this._booster = new List<IBoostEffect>(booster.Count);
                foreach (var item in booster)
                {
                    this._booster.Add(item.Clone(srcSkill));
                }
                this._effects.AddRange(this._booster);
            }
            if (null != specFeeder && specFeeder.Count > 0)
            {
                this._specFeeder = new List<ISpecEffect>(specFeeder.Count);
                foreach (var item in specFeeder)
                {
                    this._specFeeder.Add(item.Clone(srcSkill));
                }
                this._effects.AddRange(this._specFeeder);
            }
            this.CheckEffectFlag();
        }
        #endregion

        #region Data
        public int InnerId
        {
            get { return this._locator.GetHashCode(); }
        }
        public IEffect MainEffect
        {
            get { return this._mainEffect; }
        }
        public List<IEffect> Effects
        {
            get { return this._effects; }
        }
        public bool MainFlag
        {
            get { return null != this._mainEffect; }
        }
        public bool RedoFlag
        {
            get { return this._redoFlag; }
        }
        public bool UndoFlag
        {
            get { return this._undoFlag > 0; }
        }
        public short UndoState
        {
            get
            {
                if (this._undoFlag == 0)
                    return 0;
                var tgts = GetTargets(null);
                if (null == tgts)
                    return 0;
                return (short)tgts.Count;
            }
        }
        public EnumSkillFlag SkillFlag
        {
            get { return this._skillFlag; }
        }
        public ILocator<T> Locator
        {
            get { return this._locator; }
        }
        public void FitCasters(Dictionary<int, int> dic)
        {
            if (this._castTargets.Count == 0)
                return;
            var lstDel = new List<ISkillPlayer>(this._castTargets.Count);
            foreach (var caster in _castTargets.Keys)
            {
                if (!dic.ContainsKey(caster.InnerId))
                    lstDel.Add(caster);
            }
            if (lstDel.Count > 0)
            {
                lstDel.ForEach(i => _castTargets.Remove(i));
                lstDel.Clear();
            }
        }
        public void GetCasterFlag(Dictionary<int, int> dic)
        {
            int casterId = 0;
            int flag = 0;
            foreach (var kvp in this._castTargets)
            {
                casterId = kvp.Key.InnerId;
                if (dic.TryGetValue(casterId, out flag))
                    dic[casterId] = flag + kvp.Value.Count;
                else
                    dic[casterId] = kvp.Value.Count;
            }
        }
        public List<T> GetTargets(ISkillPlayer caster)
        {
            if (null != caster)
            {
                List<T> lst;
                _castTargets.TryGetValue(caster, out lst);
                return lst;
            }
            if (_castTargets.Count == 1)
            {
                foreach (var kvp in _castTargets)
                {
                    return kvp.Value;
                }
            }
            else if (_castTargets.Count > 1)
            {
                _targets.Clear();
                foreach (var kvp in _castTargets)
                {
                    _targets.AddRange(kvp.Value);
                }
            }
            return _targets;
        }
        public SkillClipSetting ClipSetting
        {
            get;
            set;
        }
        #endregion

        #region Effect
        public bool ReEffect(ISkill srcSkill)
        {
            if (this._undoFlag==0 && !this._redoFlag)
                return false;
            if (this._castTargets.Count == 0)
                return EffectCore(srcSkill, null, this._undoFlag, true);
            bool rtnVal = false;
            int paralSize = 1;
            foreach (var kvp in this._castTargets)
            {
                if (kvp.Value.Count > 0)
                {
                    rtnVal |= EffectCore(srcSkill, kvp.Key, this._undoFlag, true);
                    continue;
                }
                if (paralSize <= 0)
                    continue;
                if (EffectCore(srcSkill, kvp.Key, this._undoFlag, false))
                {
                    rtnVal = true;
                    paralSize--;
                }
            }
            return rtnVal;
        }
        public bool UnEffect(ISkill srcSkill)
        {
            if (this._undoFlag == 0)
                return true;
            if (this._castTargets.Count == 0)
                return UnEffectCore(srcSkill, null, this._targets);
            foreach (var kvp in this._castTargets)
            {
                UnEffectCore(srcSkill, kvp.Key, kvp.Value);
            }
            return true;
        }
        public bool Revoke(ISkill srcSkill)
        {
            if (this._castTargets.Count == 0)
                return RevokeCore(srcSkill, null, this._targets);
            foreach (var kvp in this._castTargets)
            {
                RevokeCore(srcSkill, kvp.Key, kvp.Value);
            }
            return true;
        }
        public bool Effect(ISkill srcSkill, ISkillPlayer caster)
        {
            return EffectCore(srcSkill, caster, this._undoFlag, false);
        }
        public bool ReEffect(ISkill srcSkill, ISkillPlayer caster)
        {
            if (this._undoFlag==0 && !this._redoFlag)
                return false;
            return EffectCore(srcSkill, caster, this._undoFlag, true);
        }
        public bool UnEffect(ISkill srcSkill, ISkillPlayer caster)
        {
            if (this._undoFlag==0)
                return true;
            var targets = GetTargets(caster);
            return UnEffectCore(srcSkill, caster, targets);
        }
        public bool AddEffect(IEffect effect)
        {
            if (effect is IBoostEffect)
                return false;
            if (this._effects.IndexOf(effect) >= 0)
                return true;
            if (effect is INormalEffect)
            {
                if (null == this._buffFeeder)
                    this._buffFeeder = new List<INormalEffect>();
                this._buffFeeder.Add((INormalEffect)effect);
                this._effects.Add(effect);
            }
            else if (effect is ISpecEffect)
            {
                if (null == this._specFeeder)
                    this._specFeeder = new List<ISpecEffect>();
                this._specFeeder.Add((ISpecEffect)effect);
                this._effects.Add(effect);
            }
            this.CheckEffectFlag();
            return true;
        }
        public bool RemoveEffect(IEffect effect)
        {
            if (effect is IBoostEffect)
                return false;
            if (this._effects.IndexOf(effect) < 0)
                return true;
            if (effect is INormalEffect)
            {
                if (null == this._buffFeeder)
                    return true;
                this._buffFeeder.Remove((INormalEffect)effect);
                this._effects.Remove(effect);
            }
            else if (effect is ISpecEffect)
            {
                if (null == this._specFeeder)
                    return true;
                this._specFeeder.Remove((ISpecEffect)effect);
                this._effects.Remove(effect);
            }
            this.CheckEffectFlag();
            return true;
        }
        #endregion

        #region  EffectCore
        protected virtual bool EffectCore(ISkill srcSkill, ISkillPlayer caster, byte undoFlag, bool redoFlag)
        {
            if (null == srcSkill)
                return false;
            var targets = Locate(srcSkill, caster, undoFlag);
            if (null == targets || targets.Count == 0)
                return false;
            if (redoFlag && !this._redoFlag)
            {
                if (undoFlag == 0 || _undoTargets.Count == 0)
                    return false;
            }
            bool rtnVal = false;
            if (null != this._booster && this._booster.Count > 0)
            {
                foreach (var effect in this._booster)
                {
                    if (undoFlag > 1 && effect.Recycle && effect.Repeat <= 0)
                    {
                        if (_newTargets.Count > 0) //if (!redoFlag && undoTargets.Count > 0)
                            rtnVal |= EffectTarget(effect, srcSkill, caster, _newTargets);
                        continue;
                    }
                    //if (redoFlag && !effect.RedoState(srcSkill, caster))
                    if (redoFlag && effect.Repeat <= 0)
                        continue;
                    rtnVal |= EffectTarget(effect, srcSkill, caster, targets);
                }
            }
            if (null != this._buffFeeder && this._buffFeeder.Count > 0)
            {
                foreach (var effect in this._buffFeeder)
                {
                    if (undoFlag > 1 && effect.Recycle && effect.Repeat <= 0)
                    {
                        if (_newTargets.Count > 0)
                            rtnVal |= EffectTarget(effect, srcSkill, caster, _newTargets);
                        continue;
                    }
                    //if (redoFlag && !effect.RedoState(srcSkill, caster))
                    if (redoFlag && effect.Repeat <= 0)
                        continue;
                    rtnVal |= EffectTarget(effect, srcSkill, caster, targets);
                }
            }
            if (null != this._specFeeder && this._specFeeder.Count > 0)
            {
                foreach (var effect in this._specFeeder)
                {
                    if (undoFlag > 1 && effect.Recycle && effect.Repeat <= 0)
                    {
                        if (_newTargets.Count > 0)
                            rtnVal |= EffectTarget(effect, srcSkill, caster, _newTargets);
                        continue;
                    }
                    if (redoFlag && !effect.RedoState(srcSkill, caster))
                        continue;
                    rtnVal |= EffectTarget(effect, srcSkill, caster, targets);
                }
            }
            //if (rtnVal && !MainFlag)
            if (rtnVal && !redoFlag && !MainFlag)
                this.AddShowClip(srcSkill, caster);
            return rtnVal;
        }
        protected virtual bool UnEffectCore(ISkill srcSkill, ISkillPlayer caster, List<T> targets)
        {
            if (null == targets || targets.Count == 0)
                return true;
            if (null != this._booster && this._booster.Count > 0)
            {
                foreach (var effect in this._booster)
                {
                    if (!effect.Recycle)
                        continue;
                    this.UnEffectTarget(effect, srcSkill, caster, targets);
                }
            }
            if (null != this._buffFeeder && this._buffFeeder.Count > 0)
            {
                foreach (var effect in this._buffFeeder)
                {
                    if (!effect.Recycle)
                        continue;
                    this.UnEffectTarget(effect, srcSkill, caster, targets);
                }
            }
            targets.Clear();
            return true;
        }
        protected virtual bool RevokeCore(ISkill srcSkill, ISkillPlayer caster, List<T> targets)
        {
            if (null == targets || targets.Count == 0)
                return true;
            if (null != this._booster && this._booster.Count > 0)
            {
                this.RevokeBoost(srcSkill, caster, targets);
            }
            if (null != this._buffFeeder && this._buffFeeder.Count > 0)
            {
                foreach (var effect in this._buffFeeder)
                {
                    this.RevokeEffect(effect, srcSkill, caster, targets);
                }
            }
            return true;
        }
        #endregion

        #region Locate
        protected List<T> Locate(ISkill srcSkill, ISkillPlayer caster, byte undoFlag)
        {
            var targets = this._targets;
            if (null != caster)
            {
                targets = GetTargets(caster);
                if (null == targets)
                {
                    targets = new List<T>();
                    _castTargets[caster] = targets;
                }
            }
            if (undoFlag>0)
            {
                _undoTargets.Clear();
                _undoTargets.AddRange(targets);
            }
            if (!this._locator.Locate(targets, srcSkill, caster))
                return null;
            if (undoFlag == 0)
                return targets;
            if (undoFlag == 1 && _undoTargets.Count == 0)
                undoFlag = 2;
            GetUndoTargets(targets, this._undoTargets, this._newTargets, undoFlag);
            if (this._undoTargets.Count == 0)
                return targets;
            foreach (var effect in this._effects)
            {
                if (!effect.Recycle)
                    continue;
                if (effect is IBoostEffect)
                    this.UnEffectTarget((IBoostEffect)effect, srcSkill, caster, this._undoTargets);
                else if (effect is INormalEffect)
                    this.UnEffectTarget((INormalEffect)effect, srcSkill, caster, this._undoTargets);
            }
            return targets;
        }
        void GetUndoTargets(List<T> targets, List<T> undoTargets, List<T> newTargets, byte undoFlag)
        {
            int cnt = Math.Max(targets.Count, undoTargets.Count);
            var dic = new Dictionary<T, byte>(cnt, SkillTargetComparer.Instance);
            foreach (var item in undoTargets)
            {
                dic[item] = 0;
            }
            foreach (var item in targets)
            {
                if (!dic.ContainsKey(item))
                    dic[item] = 1;
                else
                    dic[item] = 2;
            }
            undoTargets.Clear();
            newTargets.Clear();
            foreach (var kvp in dic)
            {
                if (kvp.Value == 0)
                    undoTargets.Add(kvp.Key);
                else if (kvp.Value == 1 && undoFlag > 1)
                    newTargets.Add(kvp.Key);
            }
            dic.Clear();
        }
        #endregion

        #region SkillShow
        public void AddShowClip(ISkill srcSkill, ISkillPlayer caster)
        {
            var clip = this.ClipSetting;
            if (null == clip || clip.ClipId <= 0)
                return;
            ISkillOwner owner = caster ?? srcSkill.Owner;
            if (null == owner.SkillCore)
                return;
            byte[] targets = null;
            if (clip.ClipType > 0)
                targets = GetClipTargets(caster);
            owner.SkillCore.AddShowClip(srcSkill, clip, targets);
        }
        #endregion

        #region Tools
        protected void CheckEffectFlag()
        {
            this._mainEffect = null;
            this._redoFlag = false;
            this._undoFlag = 0;
            this._skillFlag = EnumSkillFlag.None;
            foreach (var effect in this._effects)
            {
                if (effect.MainFlag && null == this._mainEffect)
                    this._mainEffect = effect;
                if (effect.Repeat > 0)
                    this._redoFlag = true;
                if (effect.Recycle)
                    this._undoFlag = Math.Max(this._undoFlag, (byte)(effect.Last == (short)EnumBuffLast.Undo ? 1 : 2));
                if (this._skillFlag != EnumSkillFlag.Mix)
                    this._skillFlag |= effect.DebuffFlag ? EnumSkillFlag.Debuff : EnumSkillFlag.Buff;
            }
        }
        #endregion

        #region Abstract
        protected abstract bool EffectTarget(INormalEffect effect, ISkill srcSkill, ISkillPlayer caster, List<T> targets);
        protected abstract bool EffectTarget(IBoostEffect effect, ISkill srcSkill, ISkillPlayer caster, List<T> targets);
        protected abstract bool EffectTarget(ISpecEffect effect, ISkill srcSkill, ISkillPlayer caster, List<T> targets);
        protected abstract bool UnEffectTarget(INormalEffect effect, ISkill srcSkill, ISkillPlayer caster, List<T> targets);
        protected virtual bool UnEffectTarget(IBoostEffect effect, ISkill srcSkill, ISkillPlayer caster, List<T> targets)
        {
            if (null != effect.SrcModelSetting && null != caster)
                effect.RemoveShowModel(srcSkill, caster, false);
            foreach (var target in targets)
            {
                target.RemoveBoost(effect);
            }
            return true;
        }
        protected abstract bool RevokeEffect(INormalEffect effect, ISkill srcSkill, ISkillPlayer caster, List<T> targets);
        protected virtual bool RevokeBoost(ISkill srcSkill, ISkillPlayer caster, List<T> targets)
        {
            foreach (var target in targets)
            {
                target.ForceSyncBoost(true);
            }
            return true;
        }
        protected abstract byte[] GetClipTargets(ISkillPlayer caster);
        public abstract IEffector Clone(ISkill srcSkill);
        #endregion

        #region NestClass
        class SkillTargetComparer : IEqualityComparer<T>
        {
            private SkillTargetComparer() { }
            public static readonly SkillTargetComparer Instance = new SkillTargetComparer();

            public bool Equals(T x, T y)
            {
                if (null == x || null == y)
                    return false;
                return x.InnerId == y.InnerId;
            }
            public int GetHashCode(T obj)
            {
                return obj.InnerId;
            }
        }
        class SkillPlayerComparer : IEqualityComparer<ISkillPlayer>
        {
            private SkillPlayerComparer() { }
            public static readonly SkillPlayerComparer Instance = new SkillPlayerComparer();

            public bool Equals(ISkillPlayer x, ISkillPlayer y)
            {
                if (null == x || null == y)
                    return false;
                return x.InnerId == y.InnerId;
            }
            public int GetHashCode(ISkillPlayer obj)
            {
                return obj.InnerId;
            }
        }
        #endregion
    }
}
