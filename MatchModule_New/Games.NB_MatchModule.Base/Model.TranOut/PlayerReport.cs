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
    public class PlayerReport : IBinIO
    {

        #region .ctor
        public PlayerReport()
        {
            this.SkillResults = new List<SkillReport>(256);
            this.MoveResults = new List<PlayerMoveReport>(512);
        }
        public PlayerReport(PlayerInput input, byte clientId)
            : this()
        {
            this.ClientId = clientId;
            this.Pid = input.Pid;
            this.Plus = input.Plus;
            this.Level = input.Level;
            this.Position = input.Position;
            this.Name = input.FamilyName;
        }
        #endregion
        
        #region Data
        /// <summary>
        /// 球员Id
        /// </summary>
        [DataMember]
        [XmlAttribute("Pid")]
        public int Pid
        {
            get;
            set;
        }
        /// <summary>
        /// 强化等级
        /// </summary>
        [DataMember]
        [XmlAttribute("Plus")]
        public int Plus
        {
            get;
            set;
        }
        /// <summary>
        /// 球员等级
        /// </summary>
        [DataMember]
        [XmlAttribute("Level")]
        public int Level
        {
            get;
            set;
        }
        /// <summary>
        /// 客户端Id
        /// </summary>
        [DataMember]
        [XmlAttribute("ClientId")]
        public int ClientId
        {
            get;
            set;
        }
        /// <summary>
        /// 场上位置
        /// </summary>
        [DataMember]
        [XmlAttribute("Position")]
        public int Position
        {
            get;
            set;
        }
        /// <summary>
        /// 球员名字
        /// </summary>
        [DataMember]
        [XmlAttribute("Name")]
        public string Name
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
        /// 移动记录
        /// </summary>
        [DataMember]
        [XmlArray("MoveResults")]
        public List<PlayerMoveReport> MoveResults
        {
            get;
            set;
        }
        public int CntSkillResults
        {
            get
            {
                return SkillResults.Count;
               
            }
        }
        public int CntMoveResults
        {
            get
            {
                return MoveResults.Count;
           }
        }
        #endregion

        #region IBinIO
        public void BinWrite(BinaryWriter writer, int verNo)
        {
            writer.Write((int)Pid);
            writer.Write((byte)Plus);
            writer.Write((byte)Level);
            int cnt = CntSkillResults;
            writer.Write((ushort)cnt);
            if (cnt > 0)
            {
                foreach (var item in SkillResults)
                {
                    item.BinWrite(writer, verNo);
                }
            }
            cnt = CntMoveResults;
            writer.Write((ushort)cnt);
            if (cnt > 0)
            {
                foreach (var item in MoveResults)
                {
                    item.BinWrite(writer, verNo);
                }
            }
        }
        public void BinRead(BinaryReader reader, int verNo)
        {
            this.Pid = reader.ReadInt32();
            this.Plus = reader.ReadByte();
            this.Level = reader.ReadByte();
            int cnt = reader.ReadUInt16();
            this.SkillResults = new List<SkillReport>(cnt);
            for (int i = 0; i < cnt; ++i)
            {
                this.SkillResults.Add(IOUtil.BinRead<SkillReport>(reader, verNo));
            }
            cnt = reader.ReadUInt16();
            this.MoveResults = new List<PlayerMoveReport>(cnt);
            for (int i = 0; i < cnt; ++i)
            {
                this.MoveResults.Add(IOUtil.BinRead<PlayerMoveReport>(reader, verNo));
            }
        }
        #endregion


    }
}
