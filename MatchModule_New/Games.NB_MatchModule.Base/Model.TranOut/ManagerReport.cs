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
    [Serializable, DataContract]
    public class ManagerReport : IBinIO
    {
        #region .ctor
        public ManagerReport()
        {
            this.SkillResults = new List<SkillReport>();
            this.Players = new List<PlayerReport>(Defines.Match.MAX_PLAYER_COUNT);
        }
        public ManagerReport(ManagerInput input)
            : this()
        {
            this.ManagerId = input.Mid;
            this.ManagerName = input.Name;
            this.ManagerLogo = input.Logo;
            this.ClothId = input.ClothId;
            this.FormId = input.FormId;
        }
        #endregion

        #region Data
        /// <summary>
        /// 经理Id
        /// </summary>
        [DataMember]
        [XmlAttribute("ManagerId")]
        public Guid ManagerId
        {
            get;
            set;
        }
        /// <summary>
        /// 经理名称
        /// </summary>
        [DataMember]
        [XmlAttribute("ManagerName")]
        public string ManagerName
        {
            get;
            set;
        }
        /// <summary>
        /// 经理Logo
        /// </summary>
        [DataMember]
        [XmlAttribute("ManagerLogo")]
        public string ManagerLogo
        {
            get;
            set;
        }
        /// <summary>
        /// 球衣Id
        /// </summary>
        [DataMember]
        [XmlAttribute("ClothId")]
        public int ClothId
        {
            get;
            set;
        }
        /// <summary>
        /// 阵型Id
        /// </summary>
        [DataMember]
        [XmlAttribute("FormId")]
        public int FormId
        {
            get;
            set;
        }
        /// <summary>
        /// 技能记录
        /// </summary>
        [DataMember]
        [XmlArray("SkillResults")]
        public List<SkillReport> SkillResults
        {
            get;
            set;
        }
        /// <summary>
        /// 球员列表
        /// </summary>
        [DataMember]
        [XmlArray("Players")]
        public List<PlayerReport> Players
        {
            get;
            private set;
        }
        public int CntSkillResults
        {
            get
            {
                return SkillResults.Count;
              
            }
        }
        public int CntPlayers
        {
            get
            {
                return Players.Count;
            }
        }
        #endregion

        #region IBinIO
        public void BinWrite(BinaryWriter writer, int verNo)
        {
            IOUtil.BinWriteGuid(writer, this.ManagerId);
            IOUtil.BinWriteShortUTF8(writer, this.ManagerName);
            IOUtil.BinWriteShortUTF8(writer, this.ManagerLogo);
            writer.Write((byte)ClothId);
            writer.Write((byte)FormId);
            int cnt = CntSkillResults;
            writer.Write((ushort)cnt);
            if (cnt > 0)
            {
                foreach (var item in SkillResults)
                {
                    item.BinWrite(writer, verNo);
                }
            }
            cnt = CntPlayers;
            writer.Write((byte)cnt);
            if (cnt > 0)
            {
                foreach (var item in Players)
                {
                    item.BinWrite(writer, verNo);
                }
            }
        }
        public void BinRead(BinaryReader reader, int verNo)
        {
            this.ManagerId = IOUtil.BinReadGuid(reader);
            this.ManagerName = IOUtil.BinReadShortUTF8(reader);
            this.ManagerLogo = IOUtil.BinReadShortUTF8(reader);
            this.ClothId = reader.ReadByte();
            this.FormId = reader.ReadByte();
            int cnt = reader.ReadUInt16();
            this.SkillResults = new List<SkillReport>(cnt);
            for (int i = 0; i < cnt; ++i)
            {
                this.SkillResults.Add(IOUtil.BinRead<SkillReport>(reader, verNo));
            }
            cnt = reader.ReadByte();
            this.Players = new List<PlayerReport>(cnt);
            for (int i = 0; i < cnt; ++i)
            {
                this.Players.Add(IOUtil.BinRead<PlayerReport>(reader, verNo));
            }
        }
        #endregion
    }
}
