using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillBase
{
    public interface ISkill : ISkillTarget
    {
        #region Data
        EnumSkillState SkillState
        {
            get;
            set;
        }
        EnumSkillFlag SkillFlag
        {
            get;
        }
        bool InvalidFlag
        {
            get;
            set;
        }
        short TimeStart
        {
            get;
        }
        ISkillContext Context
        {
            get;
        }
        ISkillOwner Owner
        {
            get;
        }
        string Args
        {
            get;
        }
        IRawSkill RawSkill
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
        List<IEffector> RedoEffectors
        {
            get;
        }
        #endregion

        void Load(IRawSkill rawSkill);
        void FitCasters();
        bool Invoke();
        bool Repeat();
        bool Revoke();
        bool Invoke(ISkillPlayer caster);
        bool CoolDown(ISkillPlayer caster);
        bool Repeat(ISkillPlayer caster);
        bool AddEffect(IEffect effect);
        bool RemoveEffect(IEffect effect);
        bool AddEffector(IEffector effector);
        bool RemoveEffector(int effectorId);
    }
    
}