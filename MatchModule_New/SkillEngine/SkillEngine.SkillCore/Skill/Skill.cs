using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillBase.Enum.NBA;

namespace SkillEngine.SkillCore
{
    public partial class Skill : ISkill
    {
        #region Cache
        readonly ISkillContext _context;
        readonly ISkillOwner _owner;
        readonly string _args;
        readonly BoostCore boostCore;
        IRawSkill _rawSkill;
        List<ISkillPlayer> _casters;
        int _skillId;
        bool _invalidFlag;
        int _redoLast;
        bool _undoFlag;
        bool _delayFlag=false;
        short _undoState;
        short _cdTimeEnd;
        short _lastTimeEnd;
        EnumSkillFlag _skillFlag;
        #endregion

        #region .ctor
        public Skill(ISkillContext context, ISkillOwner owner, string args)
        {
            this._context = context;
            this._owner = owner;
            this._args = args;
            this.boostCore = new BoostCore(context);
            this.SubEffectors = new List<IEffector>();
            this.RedoEffectors = new List<IEffector>();
        }
        public void Load(IRawSkill rawSkill)
        {
            this._rawSkill = rawSkill;
            this._skillId = this._owner.InnerId * 100000 + rawSkill.RawSkillId;
            IEffector effector = null;
            this._skillFlag = EnumSkillFlag.None;
            if (null != rawSkill.MainEffector)
            {
                effector = rawSkill.MainEffector.Clone(this);
                this.MainEffector = effector;
                this._skillFlag |= rawSkill.MainEffector.SkillFlag;
                if (effector.RedoFlag || effector.UndoFlag)
                    this.RedoEffectors.Add(effector);
            }
            if (null != rawSkill.SubEffectors && rawSkill.SubEffectors.Count > 0)
            {
                foreach (var item in rawSkill.SubEffectors)
                {
                    effector = item.Clone(this);
                    this.SubEffectors.Add(effector);
                    this._skillFlag |= item.SkillFlag;
                    if (effector.RedoFlag || effector.UndoFlag)
                        this.RedoEffectors.Add(effector);
                }
            }
            if (this.Owner is ISkillManager)
            {
                if (rawSkill.CastFlag)
                    this._casters = ((ISkillManager)this.Owner).SkillPlayerList;
                else
                    this._casters = null;
            }
            else if (this.Owner is ISkillPlayer)
            {
                this._casters = new List<ISkillPlayer>();
                this._casters.Add((ISkillPlayer)this.Owner);
            }
            this.CheckRedoFlag();
        }
        #endregion

        #region Data
        public int InnerId
        {
            get { return this._skillId; }
        }
        public bool InvalidFlag
        {
            get { return this._invalidFlag; }
            set { this._invalidFlag = value; }
        }
        public string Args
        {
            get { return this._args; }
        }
        public short TimeStart
        {
            get;
            set;
        }
        public ISkillContext Context
        {
            get { return this._context; }
        }
        public ISkillOwner Owner
        {
            get { return this._owner; }
        }
        public IRawSkill RawSkill
        {
            get { return this._rawSkill; }
        }
        public EnumSkillFlag SkillFlag
        {
            get { return this._skillFlag; }
        }
        public EnumSkillState SkillState
        {
            get
            {
                if (this._invalidFlag)
                    return EnumSkillState.Invalid;
                if (this._undoState > 0 || this._lastTimeEnd < 0 || this._context.MatchRound < this._lastTimeEnd)
                {
                    if (this._cdTimeEnd == 0)
                        return EnumSkillState.Triggering;
                    else
                        return EnumSkillState.Effecting;
                }
                else
                {
                    if (this._cdTimeEnd > this._context.MatchRound)
                        return EnumSkillState.CoolDown;
                    else
                        return EnumSkillState.Activate;
                }
            }
            set
            {
                switch (value)
                {
                    case EnumSkillState.Invalid:
                        this._invalidFlag = true;
                        return;
                    case EnumSkillState.Activate:
                        this._invalidFlag = false;
                        this._cdTimeEnd = 0;
                        return;
                    case EnumSkillState.Triggering:
                        this._cdTimeEnd = 0;
                        this._lastTimeEnd = (short)(this._context.MatchRound + 2);
                        return;
                    case EnumSkillState.Effecting:
                        int cd = this.RawSkill.CD;
                        int last = this._redoLast;
                        IBoostBuff boost = null;
                        if (cd > 0 && this.TryGetSkillCD(ref boost))
                            cd = Convert.ToInt32(cd + boost.Point + cd * boost.AsPercent);
                        if (cd != 0)
                            cd = _context.GetBuffLast(this, null, cd);
                        if (last > 0)
                            last = _context.GetBuffLast(this, null, last);
                        short round = this._context.MatchRound;
                        if (cd >= 0)
                            cd += round;
                        if (last > 0)
                            last += round;
                        this.TimeStart = round;
                        this._cdTimeEnd = (short)cd;
                        this._lastTimeEnd = (short)last;
                        return;
                }
            }
        }
        public IEffector MainEffector
        {
            get;
            private set;
        }
        public List<IEffector> SubEffectors
        {
            get;
            private set;
        }
        public List<IEffector> RedoEffectors
        {
            get;
            private set;
        }
        #endregion

        #region Effect
        public bool Invoke()
        {
            EnumSkillState skillState = this.SkillState;
            if (skillState == EnumSkillState.Effecting)
            {
                this.Repeat();
                return false;
            }
            if (skillState != EnumSkillState.Activate)
                return false;
            ISkillOwner owner = null;
            if (null == this._casters || this._casters.Count == 0)
                owner = this.Owner;
            else if (this._casters.Count == 1)
                owner = this._casters[0];
            byte paralFlag = this.RawSkill.ParalFlag;
            bool checkFlag = null != owner && null != owner.SkillCore;
            if (checkFlag && !owner.SkillCore.GetInvokeFlag(paralFlag))
                return false;
            short rate = this.RawSkill.Rate;
            if (rate >= 0 && rate < 100)
            {
                IBoostBuff boost = null;
                if (this.TryGetSkillRate(ref boost))
                    rate = Convert.ToInt16(rate + boost.AsPoint + rate * boost.AsPercent);
                if (rate <= 0)
                    return false;
                if (rate < 100 && this._context.RandomPercent() > rate)
                    return false;
            }
            if (checkFlag)
            {
                if (null == this._casters || this._casters.Count == 0)
                    return InvokeCore(null, true, false, true);
                else if (this._casters.Count == 1)
                    return InvokeCore(_casters[0], true, false, true);
            }
            foreach (var caster in this._casters)
            {
                if (InvokeCore(caster, true, true, true))
                    return true;
            }
            return false;
        }
        public bool Repeat()
        {
            if (this.SkillState != EnumSkillState.Effecting)
                return false;
            if (null == this._casters || this._casters.Count == 0)
                return RepeatCore(null, true);
            else if (this._casters.Count == 1)
                return RepeatCore(this._casters[0], true);
            var dicCast = GetCasterFlag();
            int flag = 0;
            bool rtnVal = false;
            int paralSize = 1;
            foreach (var caster in this._casters)
            {
                if (dicCast.TryGetValue(caster.InnerId, out flag) && flag > 0)
                {
                    rtnVal |= RepeatCore(caster, false);
                    continue;
                }
                if (paralSize <= 0)
                    continue;
                if(RepeatCore(caster, true))
                {
                    rtnVal = true;
                    paralSize--;
                }
            }
            this.CheckUndoState();
            return rtnVal;
        }
        public bool Revoke()
        {
            this.SkillState = EnumSkillState.Invalid;
            if (null != this.MainEffector)
            {
                this.MainEffector.Revoke(this);
            }
            foreach (var item in this.SubEffectors)
            {
                item.Revoke(this);
            }
            return true;
        }
        public bool Invoke(ISkillPlayer caster)
        {
            EnumSkillState skillState = this.SkillState;
            if (skillState == EnumSkillState.Effecting)
            {
                this.Repeat(caster);
                return false;
            }
            if (skillState != EnumSkillState.Activate)
                return false;
            short rate = this.RawSkill.Rate;
            if (rate >= 0 && rate < 100)
            {
                IBoostBuff boost = null;
                if (this.TryGetSkillRate(ref boost))
                    rate = Convert.ToInt16(rate + boost.AsPoint + rate * boost.AsPercent);
                if (rate <= 0)
                    return false;
                if (rate < 100 && this._context.RandomPercent() > rate)
                    return false;
            }
            return InvokeCore(caster, true, true, true);
        }
        public bool CoolDown(ISkillPlayer caster)
        {
            if (this.SkillState != EnumSkillState.Triggering)
                return false;
            this.SkillState = EnumSkillState.Effecting;
            this.CheckUndoState();
            if (null != this.MainEffector)
                this.MainEffector.AddShowClip(this, caster);
            foreach (var item in this.SubEffectors)
            {
                if (this._delayFlag && item.RedoFlag)
                    continue;
                item.Effect(this, caster);
            }
            return true;
        }
        public bool Repeat(ISkillPlayer caster)
        {
            if (this.RedoEffectors.Count == 0)
                return false;
            if (this.SkillState != EnumSkillState.Effecting)
                return false;
            return this.RepeatCore(caster, true);
        }
        public bool AddEffect(IEffect effect)
        {
            if (null != this.MainEffector)
            {
                return this.MainEffector.AddEffect(effect);
            }
            else
            {
                if (this.SubEffectors.Count == 0)
                    return false;
                return this.SubEffectors[0].AddEffect(effect);
            }
        }
        public bool RemoveEffect(IEffect effect)
        {
            if (null != this.MainEffector)
            {
                return this.MainEffector.RemoveEffect(effect);
            }
            else
            {
                if (this.SubEffectors.Count == 0)
                    return false;
                return this.SubEffectors[0].RemoveEffect(effect);
            }
        }
        public bool AddEffector(IEffector effector)
        {
            if (null != this.SubEffectors.Find(i => i.InnerId == effector.InnerId))
                return true;
            this.SubEffectors.Add(effector);
            if (effector.RedoFlag || effector.UndoFlag)
            {
                this.RedoEffectors.Add(effector);
                this.CheckRedoFlag();
            }
            return true;
        }
        public bool RemoveEffector(int effectorId)
        {
            var effector = this.SubEffectors.Find(i => i.InnerId == effectorId);
            if (null != effector)
                this.SubEffectors.Remove(effector);
            return true;
        }
        public void FitCasters()
        {
            if (!this._rawSkill.CastFlag)
                return;
            var manager = this.Owner as ISkillManager;
            if (null == manager)
                return;
            var dic = this.Context.DicBuffer(10);
            dic.Clear();
            if (null != this._casters)
            {
                foreach (var item in this._casters)
                {
                    dic[item.InnerId] = 0;
                }
            }
            if (null != this.MainEffector)
                MainEffector.FitCasters(dic);
            foreach (var item in this.SubEffectors)
            {
                item.FitCasters(dic);
            }
        }
        #endregion

        #region EffectCore
        bool InvokeCore(ISkillPlayer caster, bool cdFlag, bool checkPreFlag, bool checkPostFlag)
        {
            byte paralFlag = this.RawSkill.ParalFlag;
            var owner = caster ?? this.Owner;
            if (null == owner.SkillCore)
                checkPreFlag = checkPostFlag = false;
            if (checkPreFlag && !owner.SkillCore.GetInvokeFlag(paralFlag))
                return false;
            if (BuffUtil.IfSilence(this, owner))
                return false;
            foreach (var item in this.RawSkill.Triggers)
            {
                if (cdFlag && item.Delay)
                    continue;
                if (!item.Trigger(this, caster))
                    return false;
            }
            if (null != this.MainEffector)
            {
                if (!this.MainEffector.Effect(this, caster))
                    return false;
                if (!cdFlag)
                    this.MainEffector.AddShowClip(this, caster);
            }
            if (cdFlag)
            {
                this.SkillState = EnumSkillState.Triggering;
                if (null != this.MainEffector && this.MainEffector.MainEffect.Repeat == (short)EnumBuffRepeat.WaitOnce)
                    return false;
                if (!this.CoolDown(caster))
                    return false;
            }
            else
            {
                foreach (var item in this.SubEffectors)
                {
                    item.Effect(this, caster);
                }
            }
            if (checkPostFlag)
                owner.SkillCore.SetInvokeFlag(paralFlag);
            return true;
        }
        bool RepeatCore(ISkillPlayer caster, bool checkFlag)
        {
            bool undoFlag = false;
            foreach (var item in this.RawSkill.RedoTriggers)
            {
                if (!item.Trigger(this, caster))
                {
                    if (!this._undoFlag)
                        return false;
                    undoFlag = true;
                }
            }
            bool rtnVal = false;
            foreach (var item in this.RedoEffectors)
            {
                if (undoFlag)
                    rtnVal |= item.UnEffect(this, caster);
                else
                    rtnVal |= item.ReEffect(this, caster);
            }
            if (checkFlag)
                this.CheckUndoState();
            return rtnVal;
        }
        #endregion

        #region Tools
        void CheckRedoFlag()
        {
            int last = 0;
            bool undoFlag = false;
            bool delayFlag = false;
            foreach (var item in RawSkill.RedoTriggers)
            {
                if (item.Delay)
                {
                    delayFlag = true;
                    break;
                }
            }
            this._delayFlag = delayFlag;
            //if (this.RedoEffectors.Count > 0)
            if (this.RedoEffectors.Count > 0 && this.RawSkill.RedoTriggers.Count > 0)
            {
                foreach (var effector in this.RedoEffectors)
                {
                    if (effector.UndoFlag)
                        undoFlag = true;
                    foreach (var effect in effector.Effects)
                    {
                        if (effect.Repeat <= 0)
                            continue;
                        if (last == 0 || effect.Last > last)
                            last = effect.Last;
                    }
                }
            }
            if (null != _rawSkill && _rawSkill.RedoLast > 0)
                this._redoLast = _rawSkill.RedoLast;
            else
                this._redoLast = last;
            this._undoFlag = undoFlag;
        }
        void CheckUndoState()
        {
            short state = 0;
            if (this._undoFlag && this.RedoEffectors.Count > 0)
            {
                foreach (var effector in this.RedoEffectors)
                {
                    state += effector.UndoState;
                }
            }
            this._undoState = state;
            if (this._undoState == 0 && this._redoLast == (int)EnumBuffLast.Undo)
                this._lastTimeEnd = 0;
        }
        void AddShowClip(ISkillPlayer caster)
        {
            if (null != this.MainEffector)
                MainEffector.AddShowClip(this, caster);
            foreach (var item in this.SubEffectors)
            {
                item.AddShowClip(this, caster);
            }
        }
        Dictionary<int, int> GetCasterFlag()
        {
            var dic = this.Context.DicBuffer(10);
            dic.Clear();
            if (null != this.MainEffector)
                MainEffector.GetCasterFlag(dic);
            foreach (var item in this.SubEffectors)
            {
                item.GetCasterFlag(dic);
            }
            return dic;
        }
        #endregion
    }
}
