using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern
{
    public class SkillClipResult
    {
        /// <summary>
        /// 回合
        /// </summary>
        public short Round { get; set; }
        /// <summary>
        /// 技能存照Id
        /// </summary>
        public ushort ClipId { get; set; }
        /// <summary>
        /// 技能目标
        /// </summary>
        public Dictionary<byte, byte> Targets { get; set; }
    }
    public class SkillModelResult
    {
        public int SkillId { get; set; }
        /// <summary>
        /// 回合
        /// </summary>
        public short Round { get; set; }
        /// <summary>
        /// 模型Id
        /// </summary>
        public short ModelId { get; set; }
        /// <summary>
        /// 截止回合
        /// </summary>
        public short RoundEnd { get; set; }
    }
}
