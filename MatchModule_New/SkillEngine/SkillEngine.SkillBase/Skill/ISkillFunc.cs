using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillBase
{
    public interface ISkillTarget : IBoostCore
    {
        int InnerId
        {
            get;
        }
    }
    public interface ISkillOwner : ISkillTarget, IBuffCore, ISpecBuffCore
    {
        ISkillCore SkillCore
        {
            get;
            set;
        }
    }
    public interface ILocator<T> where T : ISkillTarget
    {
        bool Locate(List<T> targets, ISkill srcSkill, ISkillPlayer caster);
        List<T> Locate(ISkill srcSkill, ISkillPlayer caster);
    }
    public interface IPlayerSeeker
    {
        EnumSeekJoinType JoinType
        {
            get;
            set;
        }
        List<ISkillPlayer> Seek(ISkill srcSkill, ISkillPlayer caster);
    }
    public interface IManagerSkillSeeker
    {
        List<ISkill> Seek(ISkillManager dstManager);
    }
    public interface IPlayerSkillSeeker
    {
        List<ISkill> Seek(List<ISkillPlayer> dstPlayers);
    }
    public interface IManagerFilter
    {
        bool Check(ISkillManager srcManager, ISkillManager dstManager);
    }
    public interface IPlayerFilter
    {
        bool Check(ISkillManager srcManager, ISkillPlayer srcPlayer, ISkillPlayer dstPlayer);
    }
   
    public interface ITrigger
    {
        bool Repeat
        {
            get;
        }
        bool Recycle
        {
            get;
        }
        bool Delay
        {
            get;
        }
        bool Trigger(ISkill srcSkill, ISkillPlayer caster);
    }
    public interface IEffector
    {
        int InnerId
        {
            get;
        }
        IEffect MainEffect
        {
            get;
        }
        List<IEffect> Effects
        {
            get;
        }
        bool MainFlag
        {
            get;
        }
        bool RedoFlag
        {
            get;
        }
        bool UndoFlag
        {
            get;
        }
        short UndoState
        {
            get;
        }
        EnumSkillFlag SkillFlag
        {
            get;
        }
        SkillClipSetting ClipSetting
        {
            get;
        }
        void FitCasters(Dictionary<int, int> dic);
        void GetCasterFlag(Dictionary<int, int> dic);
        bool ReEffect(ISkill srcSkill);
        bool UnEffect(ISkill srcSkill);
        bool Revoke(ISkill srcSkill);
        bool Effect(ISkill srcSkill, ISkillPlayer caster);
        bool ReEffect(ISkill srcSkill, ISkillPlayer caster);
        bool UnEffect(ISkill srcSkill, ISkillPlayer caster);
        bool AddEffect(IEffect effect);
        bool RemoveEffect(IEffect effect);
        void AddShowClip(ISkill srcSkill, ISkillPlayer caster);
        IEffector Clone(ISkill srcSkill);
    }
   
}
