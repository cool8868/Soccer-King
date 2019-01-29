using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;

namespace SkillEngine.SkillBase
{
    public interface IRawSkill
    {
        #region Data
        int RawSkillId
        {
            get;
        }
        string SkillCode
        {
            get;
        }
        string SkillName
        {
            get;
        }
        byte SkillLevel
        {
            get;
        }
        int OpenClipId
        {
            get;
            set;
        }
        EnumSkillSrcType SrcType
        {
            get;
        }
        EnumSkillActType ActType
        {
            get;
        }
        EnumSkillTimeType TimeType
        {
            get;
        }
        bool CastFlag
        {
            get;
        }
        byte ParalFlag
        {
            get;
        }
        int CD
        {
            get;
        }
        short Rate
        {
            get;
        }
        int RedoLast
        {
            get;
        }
        List<ITrigger> Triggers
        {
            get;
        }
        List<ITrigger> RedoTriggers
        {
            get;
        }
        IEffector MainEffector
        {
            get;
        }
        List<IEffector> SubEffectors
        {
            get;
        }
        #endregion

    }
}
