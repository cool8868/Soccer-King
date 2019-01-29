using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;

namespace SkillEngine.SkillCore
{
    public sealed class BuffCore : BuffCoreBase, IBuffCore
    {
        #region Cache
        readonly ISkillContext _context;
        readonly Dictionary<int, BuffSet> dicBuff = new Dictionary<int, BuffSet>();
        #endregion

        #region .ctor
        public BuffCore(ISkillContext context)
        {
            this._context = context;
        }
        #endregion

        #region IWaitBuff
        protected override bool OnAddBuff(IBuff inBuff)
        {
            if (null == inBuff)
                return false;
            bool rtnVal = false;
            foreach (int buffId in inBuff.BuffId)
            {
                var buffSet = GetBuffSet(buffId);
                if (null == buffSet)
                {
                    buffSet = new BuffSet(this._context, inBuff, buffId);
                    dicBuff[buffId] = buffSet;
                }
                rtnVal |= buffSet.AddBuff(inBuff);
            }
            return rtnVal;
        }
        protected override bool OnSetWaitBuffEnd(EnumBuffLast lastType)
        {
            foreach (var kvp in dicBuff)
            {
                if (null != kvp.Value)
                    kvp.Value.SetWaitBuffEnd(lastType);
            }
            return true;
        }
        #endregion

        #region IBuffCore
        public bool TryGetBuff(int buffId, ref IBuff buff)
        {
            var buffSet = GetBuffSet(buffId);
            if (null == buffSet)
                return null != buff && buff.ValuedFlag;
            if (null == buff)
                buff = buffSet.BuffRoot.Clone();
            else
                buff.PlusBuff(buffSet.BuffRoot);
            return buff.ValuedFlag;
        }
        public bool RemoveBuff(int buffId, int skillId)
        {
            var buffSet = GetBuffSet(buffId);
            if (null == buffSet)
                return true;
            return buffSet.RemoveBuff(skillId);
        }
        public bool ForceSyncBuff(int buffId, bool forceFlag)
        {
            var buffSet = GetBuffSet(buffId);
            if (null == buffSet)
                return true;
            return buffSet.ForceSyncBuff(forceFlag);
        }
        #endregion

        #region IPickBuff
        public bool TryPickBuff(int buffId, out IBuff buff)
        {
            buff = null;
            var buffSet = GetBuffSet(buffId);
            if (null == buffSet)
                return false;
            return buffSet.TryPickBuff(out buff);
        }
        public bool SetPickBuffEnd(int buffId, int skillId)
        {
            var buffSet = GetBuffSet(buffId);
            if (null == buffSet)
                return false;
            return buffSet.SetPickBuffEnd(skillId);
        }
        #endregion

        #region Tools
        BuffSet GetBuffSet(int buffId)
        {
            BuffSet obj;
            dicBuff.TryGetValue(buffId, out obj);
            return obj;
        }
        #endregion
    }

    #region NestClass
    class BuffSet : BuffCoreBase, ISyncBuff
    {
        #region Cache
        readonly ISkillContext _context;
        readonly IBuff _buffRoot;
        readonly Dictionary<int, IBuff> dicBuff = new Dictionary<int, IBuff>();
        bool _syncFlag;
        short _nextSyncTime;
        short _lastSyncTime;
        #endregion

        #region .ctor
        public BuffSet(ISkillContext context, IBuff refBuff, int buffId)
        {
            this._context = context;
            this._buffRoot = refBuff.Clone(buffId);
        }
        #endregion

        #region IWaitBuff
        protected override bool OnAddBuff(IBuff inBuff)
        {
            if (null == inBuff || inBuff.InvalidFlag)
                return false;
            IBuff obj = null;
            int skillId = 0;
            if (null != inBuff.SrcSkill)
                skillId = inBuff.SrcSkill.InnerId;
            if (inBuff.Times < 0)
                skillId = -skillId;
            if (!dicBuff.TryGetValue(skillId, out obj) || null == obj)
                dicBuff[skillId] = inBuff.Clone();
            else
                obj.CoverBuff(inBuff);
            this._syncFlag = false;
            return true;
        }
        protected override bool OnSetWaitBuffEnd(EnumBuffLast lastType)
        {
            int lastVal = (int)lastType;
            foreach (var item in dicBuff.Values)
            {
                if (item.InvalidFlag
                    || item.TimeEnd <= (short)EnumBuffLast.TillWaitEnd && item.TimeEnd == lastVal)
                {
                    item.TimeEnd = 0;
                    if (this._syncFlag)
                        this._syncFlag = false;
                }
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
        public bool ForceSyncBuff(bool forceFlag)
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
            var lstDel = new List<int>(dicBuff.Count);
            short minTimeEnd = 0;
            short maxTimeEnd = 0;
            short timeEnd = 0;
            IBuff item;
            foreach (var kvp in dicBuff)
            {
                item = kvp.Value;
                //if (item.BuffId[0] == (int)EnumBlurType.BanMan)
                //{
                //    ((IDisableBuff)item).ReEnable();
                //}
                if (item.InvalidFlag)
                {
                    lstDel.Add(kvp.Key);
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
                if (item.Times >= 0)
                    this._buffRoot.PlusBuff(item);
            }
            if (lstDel.Count > 0)
                lstDel.ForEach(i => dicBuff.Remove(i));
            this._nextSyncTime = minTimeEnd;
            this._lastSyncTime = maxTimeEnd;
            this._syncFlag = true;
            return true;
        }
        #endregion

        #region Facade
        public IBuff BuffRoot
        {
            get
            {
                SyncBuff(false);
                return this._buffRoot;
            }
        }
        public bool RemoveBuff(int skillId)
        {
            if (skillId == 0)
            {
                this.Clear();
                this.dicBuff.Clear();
                this._syncFlag = true;
                return true;
            }
            if (dicBuff.Remove(skillId) || dicBuff.Remove(-skillId))
                this._syncFlag = false;
            return true;
        }
        public bool TryPickBuff(out IBuff buff)
        {
            buff = null;
            foreach (var item in dicBuff.Values)
            {
                if (item.InvalidFlag)
                    continue;
                if (item.Times < 0)
                {
                    buff = item.Clone();
                    if (item.Times == (short)EnumBuffRepeat.PickOnce)
                        item.TimeEnd = 0;
                    break;
                }
            }
            //if (null != buff)
            //    buff.PlusBuff(this.BuffRoot);
            return null != buff && buff.ValuedFlag;
        }
        public bool SetPickBuffEnd(int skillId)
        {
            if (dicBuff.Remove(-skillId))
                this._syncFlag = false;
            return true;
        }
        #endregion

        #region Tools
        void Clear()
        {
            this._waitIndex = 0;
            this._nextSyncTime = 0;
            this._lastSyncTime = 0;
            this._buffRoot.Clear();
        }
        #endregion
    }
    #endregion

}
