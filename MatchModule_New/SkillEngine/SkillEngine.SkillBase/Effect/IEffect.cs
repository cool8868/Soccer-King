using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillBase
{
    #region Base
    public interface IEffect : IEffectSkills
    {
        #region Data
        double AsRate
        {
            get;
        }
        double AsPoint
        {
            get;
        }
        double AsPerPoint
        {
            get;
        }
        double AsPercent
        {
            get;
        }
        EnumBuffType BuffType
        {
            get;
        }
        int[] BuffId
        {
            get;
        }
        bool MainFlag
        {
            get;
        }
        bool PureFlag
        {
            get;
        }
        bool DebuffFlag
        {
            get;
        }
        int Last
        {
            get;
            set;
        }
        short Repeat
        {
            get;
            set;
        }
        bool Recycle
        {
            get;
            set;
        }
        int Rate
        {
            get;
            set;
        }
        int Point
        {
            get;
            set;
        }
        int Percent
        {
            get;
            set;
        }
        SkillModelSetting SrcModelSetting
        {
            get;
        }
        SkillModelSetting TgtModelSetting
        {
            get;
        }
        #endregion

        bool RedoState(ISkill srcSkill, ISkillPlayer caster);
        void AddSrcShowModel(ISkill srcSkill, ISkillPlayer caster, int last);
        void AddTgtShowModel(ISkill srcSkill, ISkillOwner target, int last);
        void RemoveShowModel(ISkill srcSkill, ISkillOwner owner, bool tgtFlag);
    }
    #endregion

    #region Normal
    public interface INormalEffect : IEffect, IEffectManager, IEffectPlayers
    {
        INormalEffect Clone();
    }
    #endregion

    #region Boost
    public interface IBoostEffect : IEffect, IEffectManager, IEffectPlayers
    {
        #region Data
        bool InvalidFlag
        {
            get;
        }
        ISkill SrcSkill
        {
            get;
        }
        EnumBoostType BoostType
        {
            get;
        }
        int[] AntiSkillType
        {
            get;
        }
        short Times
        {
            get;
            set;
        }
        short TimeEnd
        {
            get;
            set;
        }
        #endregion

        IBoostEffect Clone(ISkill srcSkill);
    }
    #endregion

    #region Spec
    public interface ISpecEffect : IEffect, IEffectManager, IEffectPlayers, IPlayerAction
    {
        #region Data
        bool InvalidFlag
        {
            get;
        }
        ISkill SrcSkill
        {
            get;
        }
        int SpecTiming
        {
            get;
        }
        short TimeEnd
        {
            get;
            set;
        }
        #endregion

        ISpecEffect Clone(ISkill srcSkill);
    }
    #endregion

}
