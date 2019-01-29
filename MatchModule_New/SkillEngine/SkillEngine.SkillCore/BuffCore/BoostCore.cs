using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public sealed class BoostCore : WaitBuffBase, ISyncBuff, IBoostCore
    {
        #region Cache
        readonly ISkillContext _context;
        readonly List<IBoostEffect> lstBoost = new List<IBoostEffect>();
        readonly List<ISkill> lstToSkill = new List<ISkill>(16);
        readonly Dictionary<int, IBoostBuff> dicBuff = new Dictionary<int, IBoostBuff>(8);
        bool _syncFlag;
        short _nextSyncTime;
        short _lastSyncTime;
        #endregion

        #region .ctor
        public BoostCore(ISkillContext context)
        {
            this._context = context;
        }
        #endregion

        #region IWaitBuff
        protected override bool OnSetWaitBuffEnd(EnumBuffLast lastType)
        {
            int lastVal = (int)lastType;
            IBoostEffect item = null;
            for (int i = lstBoost.Count - 1; i >= 0; i--)
            {
                item = lstBoost[i];
                if (item.InvalidFlag
                        || item.TimeEnd <= (short)EnumBuffLast.TillWaitEnd && item.TimeEnd == lastVal)
                {
                    lstBoost.RemoveAt(i);
                    if (this._syncFlag)
                        this._syncFlag = false;
                }
            }
            ISkill skill = null;
            for (int i = lstToSkill.Count - 1; i >= 0; i--)
            {
                skill = lstToSkill[i];
                ((IBoostCore)skill).SetWaitBuffEnd(lastType);
                if (!skill.WaitingFlag)
                    lstToSkill.RemoveAt(i);
            }
            return true;
        }
        #endregion

        #region ISyncBuff
        public bool SyncFlag
        {
            get
            {
                return this._syncFlag &&
                    (this._nextSyncTime <= 0 || this._nextSyncTime > this._context.MatchRound);
            }
        }
        public short NextSyncTime
        {
            get { return this._nextSyncTime; }
        }
        public short LastSyncTime
        {
            get { return this._lastSyncTime; }
        }
        public bool ForceSyncBoost(bool forceFlag)
        {
            this._syncFlag = false;
            if (!forceFlag)
                return true;
            return SyncBuff(true);
        }
        public bool SyncBuff(bool forceFlag)
        {
            if (!forceFlag && this.SyncFlag)
                return true;
            this.Clear();
            short minTimeEnd = 0;
            short maxTimeEnd = 0;
            IBoostEffect item = null;
            short timeEnd = 0;
            for (int i = lstBoost.Count - 1; i >= 0; i--)
            {
                item = lstBoost[i];
                if (item.InvalidFlag)
                {
                    lstBoost.RemoveAt(i);
                    continue;
                }
                timeEnd = item.TimeEnd;
                if (timeEnd <= (short)EnumBuffLast.TillWaitEnd)
                    this.SetWaitingFlag((EnumBuffLast)timeEnd, true);
                else
                {
                    if (minTimeEnd == 0 || timeEnd < minTimeEnd)
                        minTimeEnd = timeEnd;
                    if (timeEnd > maxTimeEnd)
                        maxTimeEnd = timeEnd;
                }
                PlusBuff(item);
            }
            this._nextSyncTime = minTimeEnd;
            this._lastSyncTime = maxTimeEnd;
            this._syncFlag = true;
            return true;
        }
        #endregion

        #region IBoostCore
        public bool TryGetSkillCD(ref IBoostBuff buff)
        {
            return TryGetBuffCore(ref buff, EnumBoostType.SkillCD);
        }
        public bool TryGetSkillRate(ref IBoostBuff buff)
        {
            return TryGetBuffCore(ref buff, EnumBoostType.SkillRate);
        }
        public bool TryGetPureBuff(ref IBoostBuff buff)
        {
            return TryGetBuffCore(ref buff, EnumBoostType.PureBuff);
        }
        public bool TryGetAntiDebuff(ref IBoostBuff buff)
        {
            return TryGetBuffCore(ref buff, EnumBoostType.AntiDebuff);
        }
        public bool TryGetAmpLast(ref IBoostBuff buff, params int[] buffIds)
        {
            return TryGetBuffCore(ref buff, EnumBoostType.AmpLast, buffIds);
        }
        public bool TryGetAmpRate(ref IBoostBuff buff, params int[] buffIds)
        {
            return TryGetBuffCore(ref buff, EnumBoostType.AmpRate, buffIds);
        }
        public bool TryGetAmpValue(ref IBoostBuff buff, params int[] buffIds)
        {
            return TryGetBuffCore(ref buff, EnumBoostType.AmpValue, buffIds);
        }
        public bool TryGetEaseLast(ref IBoostBuff buff, params int[] buffIds)
        {
            return TryGetBuffCore(ref buff, EnumBoostType.EaseLast, buffIds);
        }
        public bool TryGetEaseRate(ref IBoostBuff buff, params int[] buffIds)
        {
            return TryGetBuffCore(ref buff, EnumBoostType.EaseRate, buffIds);
        }
        public bool TryGetEaseValue(ref IBoostBuff buff, params int[] buffIds)
        {
            return TryGetBuffCore(ref buff, EnumBoostType.EaseValue, buffIds);
        }
        public bool TryGetAntiRate(ref IBoostBuff buff, params int[] buffIds)
        {
            return TryGetBuffCore(ref buff, EnumBoostType.AntiRate, buffIds);
        }

        public bool AddBoost(IBoostEffect inBoost)
        {
            if (null == inBoost || inBoost.InvalidFlag)
                return false;
            if (lstBoost.IndexOf(inBoost) < 0)
                lstBoost.Add(inBoost);
            if (inBoost.TimeEnd <= (short)EnumBuffLast.TillWaitEnd)
                this.SetWaitingFlag((EnumBuffLast)inBoost.TimeEnd, true);
            this._syncFlag = false;
            return true;
        }
        public bool RemoveBoost(IBoostEffect inBoost)
        {
            if (lstBoost.Remove(inBoost))
                this._syncFlag = false;
            return true;
        }
        public bool AddBoostToSkill(ISkill inSkill)
        {
            if (null == inSkill)
                return false;
            if (lstToSkill.IndexOf(inSkill) < 0)
                lstToSkill.Add(inSkill);
            return true;
        }
        #endregion

        #region Tools
        int CastKey(EnumBoostType boostType, int buffId)
        {
            return (int)boostType * 100000 + buffId;
        }
        bool TryGetBuffCore(ref IBoostBuff buff, EnumBoostType boostType, params int[] buffIds)
        {
            SyncBuff(false);
            if (dicBuff.Count == 0)
                return null != buff && buff.ValuedFlag;
            switch (boostType)
            {
                case EnumBoostType.SkillCD:
                case EnumBoostType.SkillRate:
                case EnumBoostType.PureBuff:
                case EnumBoostType.AntiDebuff:
                    buffIds = new int[] { 0 };
                    break;
                default:
                    if (null == buffIds || buffIds.Length == 0)
                        return null != buff && buff.ValuedFlag;
                    break;
            }
            IBoostBuff inBuff;
            int key = 0;
            foreach (var buffId in buffIds)
            {
                key = CastKey(boostType, buffId);
                if (dicBuff.TryGetValue(key, out inBuff))
                {
                    if (null == buff)
                        buff = inBuff.Clone();
                    else
                        buff.PlusBuff(inBuff);
                }
            }
            return null != buff && buff.ValuedFlag;
        }
        void Clear()
        {
            this._waitIndex = 0;
            this._nextSyncTime = 0;
            this._lastSyncTime = 0;
            this.dicBuff.Clear();
        }
        bool PlusBuff(IBoostEffect effect)
        {
            if (null == effect)
                return false;
            int[] keys;
            switch (effect.BoostType)
            {
                case EnumBoostType.SkillCD:
                case EnumBoostType.SkillRate:
                case EnumBoostType.PureBuff:
                case EnumBoostType.AntiDebuff:
                    keys = new int[] { CastKey(effect.BoostType, 0) };
                    break;
                default:
                    int cnt = effect.BuffId.Length;
                    keys = new int[cnt];
                    for (int i = 0; i < cnt; i++)
                    {
                        keys[i] = CastKey(effect.BoostType, effect.BuffId[i]);
                    }
                    break;
            }
            IBoostBuff inBuff;
            foreach (int key in keys)
            {
                if (dicBuff.TryGetValue(key, out inBuff))
                    PlusBoostBuff(inBuff, effect);
                else
                    dicBuff[key] = CreateBoostBuff(effect);
            }
            return true;
        }
        IBoostBuff CreateBoostBuff(IBoostEffect effect)
        {
            if (null == effect)
                return null;
            if (effect.BoostType == EnumBoostType.AntiRate)
            {
                if (effect.Times > 0)
                    return new BoostAntiBuff(effect.Point * effect.Times, effect.Percent * effect.Times, effect.AntiSkillType);
                else
                    return new BoostAntiBuff(effect.Point, effect.Percent, effect.AntiSkillType);
            }
            if (effect.Times > 0)
                return new BoostBuff(effect.Point * effect.Times, effect.Percent * effect.Times);
            else
                return new BoostBuff(effect.Point, effect.Percent);
        }
        bool PlusBoostBuff(IBoostBuff dstBuff, IBoostEffect effect)
        {
            if (null == dstBuff || null == effect)
                return false;
            if (effect.Times > 0)
            {
                dstBuff.Point += effect.Point * effect.Times;
                dstBuff.Percent += effect.Percent * effect.Times;
            }
            else
            {
                dstBuff.Point += effect.Point;
                dstBuff.Percent += effect.Percent;
            }
            return true;
        }
        #endregion

    }
}
