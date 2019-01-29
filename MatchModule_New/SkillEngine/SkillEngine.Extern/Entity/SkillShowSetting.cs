using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern
{
    public class SkillClipSetting
    {
        public ushort ClipId
        {
            get;
            set;
        }
        /// <summary>
        /// 0-无目标球员；1-有目标球员
        /// </summary>
        public byte ClipType
        {
            get;
            set;
        }
        public short ClipLast
        {
            get;
            set;
        }
    }
    public class SkillModelSetting
    {
        public short ModelId
        {
            get;
            set;
        }
        public short ModelLast
        {
            get;
            set;
        }
    }
}
