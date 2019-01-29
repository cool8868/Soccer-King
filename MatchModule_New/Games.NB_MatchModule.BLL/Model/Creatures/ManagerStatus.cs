/********************************************************************************
 * 文件名：ManagerStatus
 * 创建人：
 * 创建时间：4/17/2010 5:56:28 PM
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Interface.Manager;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.Base.Model.TranOut;

namespace Games.NB.Match.BLL.Model.Creatures
{
    /// <summary>
    /// Represents a <see cref="IManager"/>'s status.
    /// </summary>
    sealed class ManagerStatus : IManagerStatus
    {

        #region Cache
        private readonly IManagerStatStatus _statStatus = new ManagerStatStatus();
        #endregion

        #region .ctor
        public ManagerStatus()
        {
            HelpDefenseRate = 50;
        }
        #endregion

        #region Data
        public int HelpDefenseRate { get; set; }

        public IPlayer HeadMostPlayer { get; set; }

        public IPlayer LastPlayer { get; set; }

        public IManagerStatStatus StatStatus
        {
            get { return this._statStatus; }
        }
        #endregion
    }
    class ManagerStatStatus : IManagerStatStatus
    {
        #region 控球
        public int DribbleRound
        {
            get;
            set;
        }

        public int SingleAttackDribbleRound
        {
            get;
            set;
        }
        #endregion

        #region 射门
        public int ShootTimes
        {
            get;
            set;
        }
        #endregion

        #region 传球
        public int PassTimes
        {
            get;
            set;
        }
        public int SuccPassTimes
        {
            get;
            set;
        }
        #endregion

        #region 过人
        public int ThroughTimes
        {
            get;
            set;
        }

        public int SuccThroughTimes
        {
            get;
            set;
        }
        #endregion

        #region 抢断
        public int StealTimes
        {
            get;
            set;
        }
        public int SuccStealTimes
        {
            get;
            set;
        }
        #endregion

        #region 扑救
        public int DiveTimes
        {
            get;
            set;
        }
        public int SuccDiveTimes
        {
            get;
            set;
        }
        #endregion
    }
}
