using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.Base.Model.TranOut
{
    public class SkillReport : IBinIO
    {
        #region Cache
        [XmlAttribute(AttributeName = "Round")]
        public int Round { get; set; }

        [XmlAttribute(AttributeName = "SkillId")]
        public ushort SkillId { get; set; }

        [XmlArray("Targets")]
        public byte[] SkillTargets { get; set; }
        #endregion

        #region IBinIO
        public void BinWrite(BinaryWriter writer, int version)
        {
            int cnt = 0;
            if (null != SkillTargets && SkillTargets.Length > 0)
                cnt = SkillTargets.Length;
            writer.Write(Convert.ToByte(cnt << 4 | Round >> 8));
            writer.Write((byte)Round);
            writer.Write((ushort)SkillId);
            for (int i = 0; i < cnt; i++)
            {
                writer.Write((byte)SkillTargets[i]);
            }
        }
        public void BinRead(BinaryReader reader, int version)
        {
            int n = reader.ReadByte();
            int cnt = (n >> 4) & 0x0f;
            this.Round = (n & 0x0f) << 8 | reader.ReadByte();
            this.SkillId = reader.ReadUInt16();
            this.SkillTargets = new byte[cnt];
            for (int i = 0; i < cnt; i++)
            {
                this.SkillTargets[i] = reader.ReadByte();
            }
        }
        #endregion
    }
}
