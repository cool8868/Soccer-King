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
    [KnownType(typeof(PlayerStateReport))]
    [KnownType(typeof(PlayerDiveStateReport))]
    [KnownType(typeof(PlayerShootStateReport))]
    [KnownType(typeof(PlayerStateReportV2))]
    [KnownType(typeof(GkPlayerStateReportV2))]
    [KnownType(typeof(PlayerDiveStateReportV2))]
    [KnownType(typeof(PlayerShootStateReportV2))]
    [KnownType(typeof(PlayerStealStateReportV2))]
    [XmlInclude(typeof(PlayerStateReport))]
    [XmlInclude(typeof(PlayerDiveStateReport))]
    [XmlInclude(typeof(PlayerShootStateReport))]
    [XmlInclude(typeof(PlayerStateReportV2))]
    [XmlInclude(typeof(GkPlayerStateReportV2))]
    [XmlInclude(typeof(PlayerDiveStateReportV2))]
    [XmlInclude(typeof(PlayerShootStateReportV2))]
    [XmlInclude(typeof(PlayerStealStateReportV2))]
    public class PlayerMoveReport : IBinIO
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
        public PlayerStateReport StateData
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
            this.StateData = CreatePlayerProcess(verNo, classId);
            if (null == StateData)
                return;
            StateData.BinRead(reader, verNo);
        }
        #endregion

        public static PlayerStateReport CreatePlayerProcess(int verNo, int classId)
        {
            if (verNo <= 1)
            {
                switch (classId)
                {
                    case ReportAsset.PlayerStateAsset.CLASSIdDefault:
                        return new PlayerStateReport();
                    case ReportAsset.PlayerStateAsset.CLASSIdDive:
                        return new PlayerDiveStateReport();
                    case ReportAsset.PlayerStateAsset.CLASSIdShoot:
                        return new PlayerShootStateReport();
                }
                return null;
            }
            switch (classId)
            {
                case ReportAsset.PlayerStateAsset.CLASSIdDefault:
                    return new PlayerStateReportV2();
                case ReportAsset.PlayerStateAsset.CLASSIdGkDefaultV2:
                    return new GkPlayerStateReportV2();
                case ReportAsset.PlayerStateAsset.CLASSIdDiveV2:
                    return new PlayerDiveStateReportV2();
                case ReportAsset.PlayerStateAsset.CLASSIdShootV2:
                    return new PlayerShootStateReportV2();
                case ReportAsset.PlayerStateAsset.CLASSIdStealV2:
                    return new PlayerStealStateReportV2();
            }
            return null;
        }
    }
}
 