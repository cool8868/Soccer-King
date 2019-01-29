using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase.Enum;

namespace SkillEngine.SkillBase
{
    public interface IAddBuff
    {
        bool AddBuff(IBuff inBuff);
    }
    public interface IWaitBuff
    {
        bool WaitingFlag
        {
            get;
        }
        bool SetWaitBuffEnd(EnumBuffLast lastType);
    }
    public interface IPickBuff
    {
        bool TryPickBuff(int buffId, out IBuff buff);
        bool SetPickBuffEnd(int buffId, int skillId);
    }

    public interface ISyncBuff
    {
        bool SyncFlag
        {
            get;
        }
        short NextSyncTime
        {
            get;
        }
        short LastSyncTime
        {
            get;
        }
        bool SyncBuff(bool forceFlag);
    }
}
