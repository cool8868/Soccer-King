using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;


namespace Games.NB.Match.Base.Model.TranOut
{
    [Serializable, DataContract]
    [KnownType(typeof(AirBallStateReport))]
    [KnownType(typeof(GoalStateReport))]
    [XmlInclude(typeof(AirBallStateReport))]
    [XmlInclude(typeof(GoalStateReport))]
    public class BallMoveReport:IBinIO
    {
        #region Data
        public int ClassId
        {
            get
            {
                if (null == this.StateData)
                    return 0;
                return StateData.ClassId;
            }
        }
        [DataMember]
        [XmlAttribute("Round")]
        public int Round
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("AsRound")]
        public int AsRound
        {
            get;
            set;
        }
        [DataMember]
        [XmlElement("StateData")]
        public BallStateReport StateData
        {
            get;
            set;
        }
        public int RoundFlag
        {
            get { return this.Round > 0 ? 1 : 0; }
        }
        #endregion

        #region IBinIO
        public void BinWrite(BinaryWriter writer, int verNo)
        {
            writer.Write(Convert.ToByte(ClassId << 5 | RoundFlag << 4 | Round >> 8));
            if (RoundFlag > 0)
                writer.Write((byte)Round);
            if (null == StateData)
                return;
            StateData.BinWrite(writer, verNo);
        }
        public void BinRead(BinaryReader reader, int verNo)
        {
            int n = reader.ReadByte();
            int classId = n >> 5;
            int roundFlag = n >> 4 & 0x1;
            if (roundFlag > 0)
                this.Round = (n & 0x0f) << 8 | reader.ReadByte();
            this.StateData = CreateBallProcess(classId);
            if (null == StateData)
                return;
            StateData.BinRead(reader, verNo);
        }
        #endregion


        public static BallStateReport CreateBallProcess(int classId)
        {
            switch (classId)
            {
                case ReportAsset.MatchStateAsset.CLASSIdDefault:
                    return new BallStateReport();
                case ReportAsset.MatchStateAsset.CLASSIDAir:
                    return new AirBallStateReport();
                case ReportAsset.MatchStateAsset.CLASSIdGoal:
                    return new GoalStateReport();
                default:
                    return null;
            }
        }
    }
}
