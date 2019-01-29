using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;

namespace SkillEngine.SkillBase
{
    public interface IBuffCore : IWaitBuff, IAddBuff, IPickBuff
    {
        bool TryGetBuff(int buffId, ref IBuff buff);
        bool RemoveBuff(int buffId, int skillId);
        bool ForceSyncBuff(int buffId, bool forceFlag);
    }
    public interface IBoostCore : IWaitBuff
    {
        bool TryGetSkillCD(ref IBoostBuff buff);   
        bool TryGetSkillRate(ref IBoostBuff buff);
        bool TryGetPureBuff(ref IBoostBuff buff);
        bool TryGetAntiDebuff(ref IBoostBuff buff);
        bool TryGetAmpLast(ref IBoostBuff buff, params int[] buffIds);
        bool TryGetAmpRate(ref IBoostBuff buff, params int[] buffIds);
        bool TryGetAmpValue(ref IBoostBuff buff, params int[] buffIds);
        bool TryGetEaseLast(ref IBoostBuff buff, params int[] buffIds);
        bool TryGetEaseRate(ref IBoostBuff buff, params int[] buffIds);
        bool TryGetEaseValue(ref IBoostBuff buff, params int[] buffIds);
        bool TryGetAntiRate(ref IBoostBuff buff, params int[] buffIds);
        bool AddBoost(IBoostEffect inBoost);
        bool RemoveBoost(IBoostEffect inBoost);
        bool AddBoostToSkill(ISkill inSkill);
        bool ForceSyncBoost(bool forceFlag);
    }
    public interface ISpecBuffCore
    {
        bool AddSpecBuff(ISpecEffect inSpec);
        bool TryPickSpecBuff(int inTiming, out ISpecEffect outSpec);
    }
}
