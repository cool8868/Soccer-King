using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NB.Match.Base.Interface.Player.Status
{
    public interface IPlayerStatStatus
    {
        #region 射门
        byte Goals
        {
            get;
            set;
        }
        int ShootTimes
        {
            get;
            set;
        }
        #endregion

        #region 传球
        int PassTimes
        {
            get;
            set;
        }
        int SuccPassTimes
        {
            get;
            set;
        }
        #endregion

        #region 过人
        int ThroughTimes
        {
            get;
            set;
        }

        int SuccThroughTimes
        {
            get;
            set;
        }
        #endregion

        #region 抢断
        int StealTimes
        {
            get;
            set;
        }
        int SuccStealTimes
        {
            get;
            set;
        }
        #endregion

        #region 扑救
        int DiveTimes
        {
            get;
            set;
        }
        int SuccDiveTimes
        {
            get;
            set;
        }
        #endregion
    }
}
