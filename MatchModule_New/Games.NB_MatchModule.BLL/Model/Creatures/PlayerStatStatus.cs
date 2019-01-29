using System;
using System.Collections.Generic;
using Games.NB.Match.AI;
using Games.NB.Match.AI.States;
using Games.NB.Match.Base;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Interface.Player.Status;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Log;
using Games.NB.Match.BLL.Rules;
using Games.NB.Match.Frame;
using SkillEngine.Extern.Enum.Football;

namespace Games.NB.Match.BLL.Model.Creatures
{
    class PlayerStatStatus : IPlayerStatStatus
    {
        #region 射门
        public byte Goals
        {
            get;
            set;
        }
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
