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
    #region Default
   [Serializable, DataContract]
    public class BallStateReport : IBinIO
    {
        #region Data
        public virtual int ClassId
        {
            get { return ReportAsset.MatchStateAsset.CLASSIdDefault; }
        }
        [DataMember]
        [XmlIgnore]
        public Coordinate Current
        {
            get;
            set;
        }
        [XmlAttribute("Current")]
        public string CurrentStr
        {
            get { return Current.Output(); }
            set { }
        }
        [XmlIgnore]
        public byte Angle
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("State")]
        public byte State
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("TurnFlag")]
        public byte TurnFlag
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("FoulState")]
        public byte FoulState
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("ModelId")]
        public ushort ModelId
        {
            get;
            set;
        }
        public int ModelFlag
        {
            get { return this.ModelId > 0 ? 1 : 0; }
        }
        #endregion

        #region IBinIO
        public virtual void BinWrite(BinaryWriter writer, int verNo)
        {
            writer.Write((byte)Current.X);
            writer.Write((byte)Current.Y);
            int modelFlag = ModelFlag;
            writer.Write(Convert.ToByte(State << 5 | TurnFlag << 4 | FoulState << 1 | ModelFlag));
            if (modelFlag > 0)
            {
                if (ReportAsset.IsShortModel(verNo))
                    writer.Write((byte)ModelId);
                else
                    writer.Write((ushort)ModelId);
            }
            WriteSpec(writer, verNo);
        }
        public virtual void BinRead(BinaryReader reader, int verNo)
        {
            int n = reader.ReadByte();
            int n2 = reader.ReadByte();
            this.Current = new Coordinate(n, n2);
            n = reader.ReadByte();
            this.State = (byte)(n >> 5);
            this.TurnFlag = (byte)(n >> 4 & 0x1);
            this.FoulState = (byte)(n >> 1 & 0x3);
            int modelFlag = n & 0x1;
            if (modelFlag > 0)
            {
                if (ReportAsset.IsShortModel(verNo))
                    this.ModelId = reader.ReadByte();
                else
                    this.ModelId = reader.ReadUInt16();
            }
            ReadSpec(reader, verNo);
        }
        #endregion

        #region SpecIO
        protected virtual void WriteSpec(BinaryWriter writer, int verNo)
        {
        }
        protected virtual void ReadSpec(BinaryReader reader, int verNo)
        {
        }
        #endregion
    }
    #endregion

    #region Air
    [Serializable, DataContract]
    public class AirBallStateReport : BallStateReport
    {
        #region Data
        public override int ClassId
        {
            get
            {
                return ReportAsset.MatchStateAsset.CLASSIDAir;
            }
        }
        [DataMember]
        [XmlIgnore]
        public Coordinate Destination
        {
            get;
            set;
        }
        [XmlAttribute("Destination")]
        public string DestinationStr
        {
            get { return Destination.Output(); }
            set { }
        }
        #endregion

        #region IBinIO
        protected override void WriteSpec(BinaryWriter writer, int verNo)
        {
            writer.Write((byte)Destination.X);
            writer.Write((byte)Destination.Y);
        }
        protected override void ReadSpec(BinaryReader reader, int verNo)
        {
            int n = reader.ReadByte();
            int n2 = reader.ReadByte();
            this.Destination = new Coordinate(n, n2);
        }
        #endregion
    }
    #endregion

    #region Goal
    [Serializable, DataContract]
    public class GoalStateReport : BallStateReport
    {
        #region Data
        public override int ClassId
        {
            get
            {
                return ReportAsset.MatchStateAsset.CLASSIdGoal;
            }
        }
        [DataMember]
        [XmlAttribute("HomeScore")]
        public byte HomeScore
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("AwayScore")]
        public byte AwayScore
        {
            get;
            set;
        }
        #endregion

        #region IBinIO
        protected override void WriteSpec(BinaryWriter writer, int verNo)
        {
            writer.Write((byte)HomeScore);
            writer.Write((byte)AwayScore);
        }
        protected override void ReadSpec(BinaryReader reader, int verNo)
        {
            this.HomeScore = reader.ReadByte();
            this.AwayScore = reader.ReadByte();
        }
        #endregion
    }
    #endregion
}
