using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillCore;

namespace SkillEngine.SkillImpl
{
    public class SkillCore : ISkillCore
    {
        #region Cache
        readonly ISkillContext _context;
        List<ISkill> _skillList;
        Dictionary<EnumSkillTimeType, List<ISkill>> _skillDic;
        int _invokeIndex = 0;
        SkillInvokeState _invokeState = new SkillInvokeState();
        SkillModelResult _modelState = new SkillModelResult();
        Dictionary<int, SkillClipResult> _clipDic = new Dictionary<int, SkillClipResult>();
        static readonly Dictionary<byte, byte> s_dicNullTargets = new Dictionary<byte, byte>();
        #endregion

        #region .ctor
        public SkillCore(ISkillContext context)
        {
            this._context = context;
            var dic = new Dictionary<EnumSkillTimeType, List<ISkill>>(6);
            dic[EnumSkillTimeType.Session] = new List<ISkill>();
            dic[EnumSkillTimeType.Section] = new List<ISkill>();
            dic[EnumSkillTimeType.Minute] = new List<ISkill>();
            dic[EnumSkillTimeType.Round] = new List<ISkill>();
            dic[EnumSkillTimeType.PreDecide] = new List<ISkill>();
            this._skillDic = dic;
            this._skillList = new List<ISkill>();
        }
        #endregion

        #region Data
        public List<ISkill> SkillList
        {
            get
            {
                return this._skillList;
            }
        }
        public short ModelId
        {
            get
            {
                if (this._modelState.RoundEnd < 0 || this._modelState.RoundEnd > _context.MatchRunRound)
                    return this._modelState.ModelId;
                return 0;
            }
        }
        public ICollection<SkillClipResult> ClipList
        {
            get
            {
                return this._clipDic.Values;
            }
        }
        #endregion

        #region Facade
        public void LoadSkill(ISkillOwner owner, List<string> skills)
        {
            if (null == skills || skills.Count == 0)
                return;
            if (RawSkillCache.Instance().TRACEPlayerSkillFlag && owner is ISkillPlayer)
                LogUtil.Info("Loading...Player[{0}] Skills[{1}] ", ((ISkillPlayer)owner).SkillPlayerId, string.Join(",", skills));
            if (owner is ISkillManager)
                LogUtil.Info("Loading...Manager[{0}] Skills[{1}] ", ((ISkillManager)owner).InnerId, string.Join(",", skills));
            var dic = this._context.DicBuffer(1);
            dic.Clear();
            for (int i = 0; i < this._skillList.Count; i++)
            {
                dic[_skillList[i].RawSkill.RawSkillId] = i;
            }
            string skillCode = string.Empty;
            string args = string.Empty;
            int argsIdx = 0;
            IRawSkill rawSkill = null;
            ISkill skill = null;
            int idx = 0;
            foreach (var str in skills)
            {
                skillCode = str;
                argsIdx = str.IndexOf('.');
                if (argsIdx >= 0)
                {
                    args = str.Substring(argsIdx + 1);
                    skillCode = str.Substring(0, argsIdx);
                }
                if (!RawSkillCache.Instance().TryGetRawSkill(skillCode, out rawSkill))
                {
                    LogUtil.Info("No RawSkill for SkillCode:" + skillCode);
                    continue;
                }
                if (dic.TryGetValue(rawSkill.RawSkillId, out idx))
                {
                    dic[rawSkill.RawSkillId] = -1;
                    _skillList[idx].InvalidFlag = false;
                    _skillList[idx].FitCasters();
                    continue;
                }
                skill = new Skill(this._context, owner, args);
                skill.Load(rawSkill);
                this._skillList.Add(skill);
                switch (rawSkill.TimeType)
                {
                    case EnumSkillTimeType.Session:
                        _skillDic[EnumSkillTimeType.Session].Add(skill);
                        break;
                    case EnumSkillTimeType.Section:
                        _skillDic[EnumSkillTimeType.Session].Add(skill);
                        _skillDic[EnumSkillTimeType.Section].Add(skill);
                        break;
                    case EnumSkillTimeType.Minute:
                        _skillDic[EnumSkillTimeType.Section].Add(skill);
                        _skillDic[EnumSkillTimeType.Minute].Add(skill);
                        break;
                    case EnumSkillTimeType.Round:
                        _skillDic[EnumSkillTimeType.Minute].Add(skill);
                        _skillDic[EnumSkillTimeType.Round].Add(skill);
                        break;
                    case EnumSkillTimeType.PreDecide:
                        _skillDic[EnumSkillTimeType.PreDecide].Add(skill);
                        break;
                }
                this.AddOpenClip(skill);
            }
            foreach (var kvp in dic)
            {
                if (kvp.Value < 0)
                    continue;
                this._skillList[kvp.Value].Revoke();
            }
        }
        public void InvokeSkill(byte timeFlag)
        {
            short round = this._context.MatchRound;
            if (!this._invokeState.GetLoopFlag(timeFlag, round))
                return;
            var skills = timeFlag == 1 ? _skillDic[EnumSkillTimeType.PreDecide] : _skillDic[BuffUtil.CheckSkillTime(this._context)];
            int cnt = skills.Count;
            if (cnt == 0)
                return;
            int pos = _invokeIndex % cnt + 1;
            int idx = 0;
            ISkill skill = null;
            bool loopFlag = false;
            for (int i = pos; i < cnt + pos; i++)
            {
                idx = i % cnt;
                skill = skills[idx];
                if (skill.InvalidFlag)
                    continue;
                if (!skill.Invoke())
                    continue;
                if (_context.SkillSkip())
                    break;
                if (!loopFlag)
                    loopFlag = true;
                if (skill.RawSkill.ParalFlag <= 1)
                    this._invokeIndex = idx;
            }
            if (loopFlag)
                this._invokeState.SetLoopFlag(timeFlag, round);
        }
        public bool GetInvokeFlag(byte paralFlag)
        {
            if (paralFlag >= 2)
                return true;
            return this._invokeState.GetInvokeFlag(paralFlag, this._context.MatchRound);
        }
        public void SetInvokeFlag(byte paralFlag)
        {
            if (paralFlag >= 2)
                return;
            this._invokeState.SetInvokeFlag(paralFlag, this._context.MatchRound);
        }
        public void AddShowModel(ISkill srcSkill, short modelId, int last)
        {
            if (last > 0)
                last += _context.MatchRunRound;
            this._modelState.SkillId = null == srcSkill ? 0 : srcSkill.InnerId;
            this._modelState.Round = _context.MatchRunRound;
            this._modelState.ModelId = modelId;
            this._modelState.RoundEnd = (short)last;
        }
        public void RemoveShowModel(ISkill srcSkill, short modelId)
        {
            short inModelId = this._modelState.ModelId;
            if (inModelId == 0 || this._modelState.SkillId != srcSkill.InnerId)
                return;
            if (modelId > 0 && modelId != inModelId)
                return;
            this._modelState.ModelId = 0;
            this._modelState.RoundEnd = 0;
        }
        public void AddOpenClip()
        {
            if (_skillList.Count == 0)
                return;
            short round = _context.MatchRunRound;
            ushort clipId = 0;
            int key = 0;
            SkillClipResult rst = null;
            foreach (var skill in _skillList)
            {
                if (skill.InvalidFlag)
                    continue;
                clipId = (ushort)skill.RawSkill.OpenClipId;
                if (clipId <= 0)
                    continue;
                key = clipId * 10000 + round;
                if (!this._clipDic.TryGetValue(key, out rst))
                {
                    rst = new SkillClipResult()
                    {
                        Round = round,
                        ClipId = clipId,
                        Targets = s_dicNullTargets,
                    };
                    this._clipDic[key] = rst;
                }
            }

        }
        public void AddOpenClip(ISkill srcSkill)
        {
            ushort clipId = (ushort)srcSkill.RawSkill.OpenClipId;
            if (clipId <= 0)
                return;
            short round = _context.MatchRunRound;
            int key = clipId * 10000 + round;
            SkillClipResult rst = null;
            if (!this._clipDic.TryGetValue(key, out rst))
            {
                rst = new SkillClipResult()
                {
                    Round = round,
                    ClipId = clipId,
                    Targets = s_dicNullTargets,
                };
                this._clipDic[key] = rst;
            }
        }
        public void AddShowClip(ISkill srcSkill, SkillClipSetting clip, byte[] targets)
        {
            if (clip.ClipId <= 0)
                return;
            short clipLast = clip.ClipLast;
            if (clipLast > 0)
            {
                clipLast += _context.MatchRound;
                this._invokeState.SetExcluFlag(clipLast);
            }
            short round = _context.MatchRunRound;
            ushort clipId = clip.ClipId;
            int key = clipId * 10000 + round;
            SkillClipResult rst = null;
            if (!this._clipDic.TryGetValue(key, out rst))
            {
                rst = new SkillClipResult()
                {
                    Round = round,
                    ClipId = clipId,
                    Targets = new Dictionary<byte, byte>(),
                };
                this._clipDic[key] = rst;
            }
            if (null == targets)
                return;
            foreach (var target in targets)
            {
                rst.Targets[target] = target;
            }
        }
        #endregion
    }
    class SkillInvokeState
    {
        #region Defines
        const int PARALCapacity = 2;
        #endregion

        #region Cache
        short _excluRound = -1;
        short _paralRound = -1;
        short _paralCount = 0;
        short _loopRound = -1;
        short _preLoopRound = -1;
        #endregion

        #region Facade
        internal bool GetInvokeFlag(byte paralFlag, short round)
        {
            switch (paralFlag)
            {
                case 0:
                    return this._excluRound < round;
                case 1:
                    return this._paralRound < round
                        || (this._paralRound == round && this._paralCount < PARALCapacity);
                default:
                    return true;
            }
        }
        internal void SetInvokeFlag(byte paralFlag, short round)
        {
            switch (paralFlag)
            {
                case 0:
                    if (this._excluRound < round)
                        this._excluRound = round;
                    break;
                case 1:
                    if (this._paralRound != round)
                    {
                        this._paralRound = round;
                        this._paralCount = 1;
                    }
                    else
                        this._paralCount++;
                    break;
            }
        }
        internal void SetExcluFlag(short round)
        {
            if (this._excluRound < round)
                this._excluRound = round;
        }
        internal bool GetLoopFlag(byte preFlag, short round)
        {
            short loop = preFlag == 1 ? this._preLoopRound : this._loopRound;
            return loop < round;
        }
        internal void SetLoopFlag(byte preFlag, short round)
        {
            if (preFlag == 1)
                this._preLoopRound = round;
            else
                this._loopRound = round;
        }
        #endregion
    }
}
