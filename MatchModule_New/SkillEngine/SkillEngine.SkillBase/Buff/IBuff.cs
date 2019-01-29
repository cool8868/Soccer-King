using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase.Enum;

namespace SkillEngine.SkillBase
{
    public interface IBuff
    {
        #region Data
        bool DebuffFlag
        {
            get;
        }
        bool ValuedFlag
        {
            get;
        }
        bool InvalidFlag
        {
            get;
        }
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


        ISkill SrcSkill
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

        bool PlusBuff(IBuff srcBuff);
        bool CoverBuff(IBuff srcBuff);
        bool Clear();
        IBuff Clone();
        IBuff Clone(params int[] buffId);
    }

    public interface IBoostBuff
    {
        #region Data
        bool ValuedFlag
        {
            get;
        }
        double AsPoint
        {
            get;
        }
        double AsPercent
        {
            get;
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
        #endregion

        bool PlusBuff(IBoostBuff srcBuff);
        bool Clear();
        IBoostBuff Clone();
    }

    public interface IBoostAntiBuff : IBoostBuff
    {
        int[] AntiSkillType
        {
            get;
            set;
        }
    }

    public interface IDisableBuff
    {
        void ReEnable();
    }
}
