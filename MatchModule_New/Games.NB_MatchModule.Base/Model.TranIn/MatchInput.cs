using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Games.NB.Match.Base.Model.TranIn
{
    public enum EnumForceWinType
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 主场必胜
        /// </summary>
        HomeWin = 1,
        /// <summary>
        /// 非平局
        /// </summary>
        NoDraw = 2,
        /// <summary>
        /// 客场必胜
        /// </summary>
        AwayWin = 3,
    }
    [DataContract]
    [Serializable]
    public class MatchInput
    {
        #region Data
        /// <summary>
        /// 比赛Id
        /// </summary>
        [DataMember]
        public Guid MatchId
        {
            get;
            set;
        }
        /// <summary>
        /// 比赛类型
        /// </summary>
        [DataMember]
        public int MatchType
        {
            get;
            set;
        }
        /// <summary>
        /// 运算时间(秒)
        /// </summary>
        [DataMember]
        public int TranTime
        {
            get;
            set;
        }
        /// <summary>
        /// 地图Id
        /// </summary>
        [DataMember]
        public int MapId
        {
            get;
            set;
        }
        /// <summary>
        /// 强制类型
        /// </summary>
        [DataMember]
        public EnumForceWinType ForceType
        {
            get;
            set;
        }
        #endregion

        #region 经理
        /// <summary>
        /// 主队经理
        /// </summary>
        [DataMember]
        public ManagerInput HomeManager
        {
            get;
            set;
        }
        /// <summary>
        /// 客队经理
        /// </summary>
        [DataMember]
        public ManagerInput AwayManager
        {
            get;
            set;
        }
        #endregion

    }
}
