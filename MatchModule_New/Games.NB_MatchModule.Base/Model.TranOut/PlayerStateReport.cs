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
    public class PlayerStateReport : IBinIO
    {
        #region Data
        public virtual int ClassId
        {
            get { return ReportAsset.PlayerStateAsset.CLASSIdDefault; }
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
        [DataMember]
        [XmlAttribute("Angle")]
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
        [XmlAttribute("HoldBall")]
        public byte HoldBall
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("HasBall")]
        public byte HasBall
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("IsBackward")]
        public byte IsBackward
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("IsStantUp")]
        public byte IsStantUp
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("NameVisible")]
        public byte NameVisible
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("FoulLevel")]
        public byte FoulLevel
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
        public bool Disable
        {
            get { return FoulLevel >= Defines.FoulLevel.RED_CARD; }
        }
        #endregion

        #region IBinIO
        public virtual void BinWrite(BinaryWriter writer, int verNo)
        {
            writer.Write((byte)Current.X);
            writer.Write((byte)Current.Y);
            writer.Write(Convert.ToByte(State << 3 | Angle));
            WriteSpec(writer, verNo);
        }
        public virtual void BinRead(BinaryReader reader, int verNo)
        {
            int n = reader.ReadByte();
            int n2 = reader.ReadByte();
            this.Current = new Coordinate(n, n2);
            n = reader.ReadByte();
            this.State = (byte)(n >> 3);
            this.Angle = (byte)(n & 0x07);
            ReadSpec(reader, verNo);
        }
        #endregion

        #region SpecIO
        protected virtual void WriteSpec(BinaryWriter writer, int verNo)
        {
            int modelFlag = ModelFlag;
            writer.Write((byte)(HoldBall << 7 | HasBall << 6 | IsStantUp << 5 | IsBackward << 4 | FoulLevel << 2 | NameVisible << 1 | modelFlag));
            if (modelFlag > 0)
            {
                if (ReportAsset.IsShortModel(verNo))
                    writer.Write((byte)ModelId);
                else
                    writer.Write((ushort)ModelId);
            }
        }
        protected virtual void ReadSpec(BinaryReader reader, int verNo)
        {
            int n = reader.ReadByte();
            int modelFlag = n & 0x1;
            this.NameVisible = (byte)(n >> 1 & 0x1);
            this.FoulLevel = (byte)(n >> 2 & 0x3);
            this.IsBackward = (byte)(n >> 4 & 0x1);
            this.IsStantUp = (byte)(n >> 5 & 0x1);
            this.HasBall = (byte)(n >> 6 & 0x1);
            this.HoldBall = (byte)(n >> 7 & 0x1);
            if (modelFlag > 0)
            {
                if (ReportAsset.IsShortModel(verNo))
                    this.ModelId = reader.ReadByte();
                else
                    this.ModelId = reader.ReadUInt16();
            }
        }
        #endregion

    }
    #endregion

    #region Dive
    [Serializable, DataContract]
    public class PlayerDiveStateReport : PlayerStateReport
    {
        #region Data
        public override int ClassId
        {
            get
            {
                return ReportAsset.PlayerStateAsset.CLASSIdDive;
            }
        }
        [DataMember]
        [XmlAttribute("DiveDirection")]
        public byte DiveDirection
        {
            get;
            set;
        }
        #endregion

        #region IBinIO
        protected override void WriteSpec(BinaryWriter writer, int verNo)
        {
            int modelFlag = ModelFlag;
            writer.Write((byte)(DiveDirection << 6 | IsStantUp << 5 | IsBackward << 4 | FoulLevel << 2 | NameVisible << 1 | modelFlag));
            if (modelFlag > 0)
            {
                if (ReportAsset.IsShortModel(verNo))
                    writer.Write((byte)ModelId);
                else
                    writer.Write((ushort)ModelId);
            }
        }
        protected override void ReadSpec(BinaryReader reader, int verNo)
        {
            int n = reader.ReadByte();
            int modelFlag = n & 0x1;
            this.NameVisible = (byte)(n >> 1 & 0x1);
            this.FoulLevel = (byte)(n >> 2 & 0x3);
            this.IsBackward = (byte)(n >> 4 & 0x1);
            this.IsStantUp = (byte)(n >> 5 & 0x1);
            this.DiveDirection = (byte)(n >> 6 & 0x3);
            if (modelFlag > 0)
            {
                if (ReportAsset.IsShortModel(verNo))
                    this.ModelId = reader.ReadByte();
                else
                    this.ModelId = reader.ReadUInt16();
            }
        }
        #endregion
    }
    #endregion

    #region Shoot
    [Serializable, DataContract]
    public class PlayerShootStateReport : PlayerStateReport
    {
        #region Data
        public override int ClassId
        {
            get
            {
                return ReportAsset.PlayerStateAsset.CLASSIdShoot;
            }
        }
        [DataMember]
        [XmlAttribute("GoalIndex")]
        public int GoalIndex
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("GoalX")]
        public int GoalX
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("GoalY")]
        public int GoalY
        {
            get;
            set;
        }
        public bool IsFrame
        {
            get { return GoalIndex == 0; }
        }
        public bool IsMiss
        {
            get { return GoalIndex == 0 || GoalIndex == 4; }
        }
        #endregion

        #region BinIO
        protected override void WriteSpec(BinaryWriter writer, int verNo)
        {
            int modelFlag = ModelFlag;
            writer.Write((byte)(GoalIndex << 4 | FoulLevel << 2 | NameVisible << 1 | modelFlag));
            if (modelFlag > 0)
            {
                if (ReportAsset.IsShortModel(verNo))
                    writer.Write((byte)ModelId);
                else
                    writer.Write((ushort)ModelId);
            }
            writer.Write((byte)GoalX);
            if (!IsFrame)
                writer.Write((byte)GoalY);
        }
        protected override void ReadSpec(BinaryReader reader, int verNo)
        {
            int n = reader.ReadByte();
            int modelFlag = n & 0x1;
            this.NameVisible = (byte)(n >> 1 & 0x1);
            this.FoulLevel = (byte)(n >> 2 & 0x3);
            this.GoalIndex = n >> 4 & 0xf;
            if (modelFlag > 0)
            {
                if (ReportAsset.IsShortModel(verNo))
                    this.ModelId = reader.ReadByte();
                else
                    this.ModelId = reader.ReadUInt16();
            }
            this.GoalX = reader.ReadByte();
            if (IsFrame)
                this.GoalY = 0;
            else
                this.GoalY = reader.ReadByte();
        }
        #endregion
    }
    #endregion

}
 