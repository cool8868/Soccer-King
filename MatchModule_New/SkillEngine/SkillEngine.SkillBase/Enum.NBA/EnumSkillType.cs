using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase.Enum.NBA
{
    public enum EnumSkillSrcType : byte
    {
        /// <summary>
        /// 球员动作
        /// </summary>
        PlayerAction = 0,
        /// <summary>
        /// 球星技能
        /// </summary>
        PlayerStar = 1,
        /// <summary>
        /// 球员组合
        /// </summary>
        PlayerSuit = 2,
        /// <summary>
        /// 装备签名
        /// </summary>
        EquipSign = 3,
        /// <summary>
        /// 装备洗炼
        /// </summary>
        EquipWash = 4,
        /// <summary>
        /// 球魂
        /// </summary>
        EquipSoul = 5,
        /// <summary>
        /// 套装
        /// </summary>
        EquipSuit = 6,
        /// <summary>
        /// 经理天赋
        /// </summary>
        ManagerTalent = 7,
        /// <summary>
        /// 意志
        /// </summary>
        Will = 8,
        /// <summary>
        /// 教练
        /// </summary>
        Coach = 9,
    }

    public enum EnumSkillActType : byte
    {
        None = 0,
        /// <summary>
        /// 进攻
        /// </summary>
        Attack = 1,
        /// <summary>
        /// 防守
        /// </summary>
        Defence = 2,
        /// <summary>
        /// 组织
        /// </summary>
        Organize = 3,
    }
}
