using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Util;
using Games.NB.Match.Base.Model.TranIn;

namespace Games.NB.Match.Base.Model.TranOut
{
    [Serializable,DataContract]
    [XmlRoot(ElementName = "Match")]
    public class MatchReport:IBinIO
    {
        #region ctor
        public MatchReport()
        {
            this.BallResults = new List<BallMoveReport>();
        }
        public MatchReport(MatchInput input)
            : this()
        {
            this.ZipNo = ReportAsset.RPTZipNo;
            this.VerNo = ReportAsset.RPTVerNo;
            this.MatchType = input.MatchType;
            this.MapId = input.MapId;
        }
        #endregion

        #region Data
        /// <summary>
        /// 压缩标记
        /// </summary>
        [DataMember]
        [XmlAttribute("ZipNo")]
        public int ZipNo
        {
            get;
            set;
        }
        /// <summary>
        /// 版本号
        /// </summary>
        [DataMember]
        [XmlAttribute("VerNo")]
        public int VerNo
        {
            get;
            set;
        }
        /// <summary>
        /// 比赛类型
        /// </summary>
        [DataMember]
        [XmlAttribute("MatchType")]
        public int MatchType
        {
            get;
            set;
        }
        /// <summary>
        /// 地图编号
        /// </summary>
        [DataMember]
        [XmlAttribute("MapId")]
        public int MapId
        {
            get;
            set;
        }
        /// <summary>
        /// 主队比分
        /// </summary>
        [DataMember]
        [XmlAttribute("HomeScore")]
        public int HomeScore
        {
            get;
            set;
        }
        /// <summary>
        /// 客队比分
        /// </summary>
        [DataMember]
        [XmlAttribute("AwayScore")]
        public int AwayScore
        {
            get;
            set;
        }
        /// <summary>
        /// 主队经理
        /// </summary>
        [DataMember]
        [XmlElement("HomeManager")]
        public ManagerReport HomeManager
        {
            get;
            set;
        }
        /// <summary>
        /// 客队经理
        /// </summary>
        [DataMember]
        [XmlElement("AwayManager")]
        public ManagerReport AwayManager
        {
            get;
            set;
        }
        /// <summary>
        /// 移动记录
        /// </summary>
        [DataMember]
        [XmlArray("BallResults")]
        public List<BallMoveReport> BallResults
        {
            get;
            private set;
        }
        public int CntBallResults
        {
            get
            {
                return BallResults.Count;
            }
        }
        #endregion

        #region IBinIO
        public void BinWrite(BinaryWriter writer, int verNo)
        {
            var stream = writer.BaseStream;
            stream.Position = 0;
            if (ZipNo == 0)
            {
                writer.Write((byte)ZipNo);
                InnerBinWrite(writer, verNo);
                return;
            }
            InnerBinWrite(writer, verNo);
            var bytes = new byte[stream.Position];
            stream.Position = 0;
            stream.Read(bytes, 0, bytes.Length);
            bytes = IOUtil.DeflateCompress(bytes);
            stream.Position = 0;
            writer.Write((byte)ZipNo);
            writer.Write(bytes);
            stream.SetLength(stream.Position);
        }
        public void BinRead(BinaryReader reader, int verNo)
        {
            this.ZipNo = reader.ReadByte();
            if (ZipNo == 0)
            {
                InnerBinRead(reader, verNo);
                return;
            }
            var bytes = reader.ReadBytes((int)(reader.BaseStream.Length - 1));
            bytes = IOUtil.DeflateDecompress(bytes);
            using (var ms = new MemoryStream(bytes))
            {
                using (var reader2 = new BinaryReader(ms))
                {
                    InnerBinRead(reader2, verNo);
                }
            }
        }
        void InnerBinWrite(BinaryWriter writer, int verNo)
        {
            verNo = this.VerNo;
            writer.Write((byte)verNo);
            writer.Write((byte)MatchType);
            writer.Write((byte)MapId);
            writer.Write((byte)HomeScore);
            writer.Write((byte)AwayScore);
            HomeManager.BinWrite(writer, verNo);
            AwayManager.BinWrite(writer, verNo);
            int cnt = CntBallResults;
            writer.Write((ushort)cnt);
            if (cnt > 0)
            {
                foreach (var item in BallResults)
                {
                    item.BinWrite(writer, verNo);
                }
            }
        }
        void InnerBinRead(BinaryReader reader, int verNo)
        {
            this.VerNo = reader.ReadByte();
            verNo = this.VerNo;
            this.MatchType = reader.ReadByte();
            this.MapId = reader.ReadByte();
            this.HomeScore = reader.ReadByte();
            this.AwayScore = reader.ReadByte();
            this.HomeManager = IOUtil.BinRead<ManagerReport>(reader, verNo);
            this.AwayManager = IOUtil.BinRead<ManagerReport>(reader, verNo);
            int cnt = reader.ReadUInt16();
            this.BallResults = new List<BallMoveReport>(cnt);
            for (int i = 0; i < cnt; ++i)
            {
                this.BallResults.Add(IOUtil.BinRead<BallMoveReport>(reader, verNo));
            }
        }
        #endregion
      
    }
}
