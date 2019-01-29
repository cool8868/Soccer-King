using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NB.Match.Base.Interface.Manager
{
    public interface IManagerStatStatus
    {
        #region 控球
        /// <summary>
        /// 球队控球回合数
        /// </summary>
        int DribbleRound { get; set; }

        /// <summary>
        /// 单次进攻的控球时间
        /// </summary>
        int SingleAttackDribbleRound { get; set; }
        #endregion

        #region 射门
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
