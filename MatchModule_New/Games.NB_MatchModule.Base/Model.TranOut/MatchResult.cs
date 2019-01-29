using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Games.NB.Match.Base.Model.TranOut
{
    [DataContract]
    [Serializable]
    public class MatchResult
    {
        #region Data
        [DataMember]
        public int MessageCode
        {
            get;
            set;
        }

        [DataMember]
        public string MatchId
        {
            get;
            set;
        }

        [DataMember]
        public int HomeScore
        {
            get;
            set;
        }

        [DataMember]
        public int AwayScore
        {
            get;
            set;
        }

        [DataMember]
        public byte[] Process
        {
            get;
            set;
        }
        #endregion
    }
}
