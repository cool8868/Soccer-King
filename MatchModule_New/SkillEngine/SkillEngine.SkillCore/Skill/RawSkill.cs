using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;

namespace SkillEngine.SkillCore
{
    public class RawSkill : IRawSkill
    {
        #region .ctor
        public RawSkill(int skillId, string skillCode, List<ITrigger> triggers, List<IEffector> effectors)
        {
            this.RawSkillId = skillId;
            this.SkillCode = skillCode;
            this.Triggers = triggers ?? new List<ITrigger>();
            this.RedoTriggers = new List<ITrigger>();
            this.SubEffectors = new List<IEffector>();
            foreach (var trigger in triggers)
            {
                if (trigger.Repeat || trigger.Recycle)
                    this.RedoTriggers.Add(trigger);
            }
            foreach (var effector in effectors)
            {
                if (effector.MainFlag && null == this.MainEffector)
                    this.MainEffector = effector;
                else
                    this.SubEffectors.Add(effector);
            }
        }
        #endregion

        #region Data
        public int RawSkillId
        {
            get;
            private set;
        }
        public string SkillCode
        {
            get;
            set;
        }
        public byte SkillLevel
        {
            get;
            set;
        }
        public string SkillName
        {
            get;
            set;
        }
        public int OpenClipId
        {
            get;
            set;
        }
        public EnumSkillSrcType SrcType
        {
            get;
            set;
        }
        public EnumSkillActType ActType
        {
            get;
            set;
        }
        public EnumSkillTimeType TimeType
        {
            get;
            set;
        }
        public bool CastFlag
        {
            get;
            set;
        }
        public byte ParalFlag
        {
            get;
            set;
        }
        public int CD
        {
            get;
            set;
        }
        public short Rate
        {
            get;
            set;
        }
        public int RedoLast
        {
            get;
            set;
        }
        #endregion

        #region
        public List<ITrigger> Triggers
        {
            get;
            private set;
        }
        public List<ITrigger> RedoTriggers
        {
            get;
            private set;
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
        #endregion

      
    }
}
