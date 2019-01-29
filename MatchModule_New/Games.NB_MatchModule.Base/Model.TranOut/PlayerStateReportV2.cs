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
    public class PlayerStateReportV2 : PlayerStateReport
    {
        #region SpecIO
        protected override void WriteSpec(BinaryWriter writer, int verNo)
        {
            int modelFlag = ModelFlag;
            //writer.Write((byte)(HoldBall << 7 | HasBall << 6 | IsStantUp << 5 | IsBackward << 4 | FoulLevel << 2 | NameVisible << 1 | modelFlag));
            writer.Write((byte)(HoldBall << 7 | HasBall << 6 | FoulLevel << 2 | NameVisible << 1 | modelFlag));
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
            //this.IsBackward = (byte)(n >> 4 & 0x1);
            //this.IsStantUp = (byte)(n >> 5 & 0x1);
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

    #region GkDefault
    [Serializable, DataContract]
    public class GkPlayerStateReportV2 : PlayerStateReport
    {
        #region Data
        public override int ClassId
        {
            get
            {
                return ReportAsset.PlayerStateAsset.CLASSIdGkDefaultV2;
            }
        }
        #endregion

        #region IBinIO
        protected override void WriteSpec(BinaryWriter writer, int verNo)
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
        protected override void ReadSpec(BinaryReader reader, int verNo)
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
    public class PlayerDiveStateReportV2 : PlayerStateReport
    {
        #region Data
        public override int ClassId
        {
            get
            {
                return ReportAsset.PlayerStateAsset.CLASSIdDiveV2;
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
    public class PlayerShootStateReportV2 : PlayerStateReport
    {
        #region Data
        public override int ClassId
        {
            get
            {
                return ReportAsset.PlayerStateAsset.CLASSIdShootV2;
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
        [DataMember]
        [XmlAttribute("SuccFlag")]
        public int SuccFlag
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("RawSuccRate")]
        public int RawSuccRate
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("NewSuccRate")]
        public int NewSuccRate
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
            writer.Write((byte)(SuccFlag << 7 | RawSuccRate & 0x7f));
            writer.Write((byte)NewSuccRate);
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
            n = reader.ReadByte();
            this.SuccFlag = n >> 7;
            this.RawSuccRate = n & 0x7f;
            this.NewSuccRate = reader.ReadByte();
        }
        #endregion
    }
    #endregion

    #region Steal
    [Serializable, DataContract]
    public class PlayerStealStateReportV2 : PlayerStateReport
    {
        #region Data
        public override int ClassId
        {
            get
            {
                return ReportAsset.PlayerStateAsset.CLASSIdStealV2;
            }
        }
        [DataMember]
        [XmlAttribute("SuccFlag")]
        public int SuccFlag
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("RawSuccRate")]
        public int RawSuccRate
        {
            get;
            set;
        }
        [DataMember]
        [XmlAttribute("NewSuccRate")]
        public int NewSuccRate
        {
            get;
            set;
        }
        #endregion

        #region IBinIO
        protected override void WriteSpec(BinaryWriter writer, int verNo)
        {
            int modelFlag = ModelFlag;
            writer.Write((byte)(HoldBall << 7 | HasBall << 6 | FoulLevel << 2 | NameVisible << 1 | modelFlag));
            if (modelFlag > 0)
            {
                if (ReportAsset.IsShortModel(verNo))
                    writer.Write((byte)ModelId);
                else
                    writer.Write((ushort)ModelId);
            }
            writer.Write((byte)(SuccFlag << 7 | RawSuccRate & 0x7f));
            writer.Write((byte)NewSuccRate);
        }
        protected override void ReadSpec(BinaryReader reader, int verNo)
        {
            int n = reader.ReadByte();
            int modelFlag = n & 0x1;
            this.NameVisible = (byte)(n >> 1 & 0x1);
            this.FoulLevel = (byte)(n >> 2 & 0x3);
            this.HasBall = (byte)(n >> 6 & 0x1);
            this.HoldBall = (byte)(n >> 7 & 0x1);
            if (modelFlag > 0)
            {
                if (ReportAsset.IsShortModel(verNo))
                    this.ModelId = reader.ReadByte();
                else
                    this.ModelId = reader.ReadUInt16();
            }
            n = reader.ReadByte();
            this.SuccFlag = n >> 7;
            this.RawSuccRate = n & 0x7f;
            this.NewSuccRate = reader.ReadByte();
        }
        #endregion
    }
    #endregion

}
 