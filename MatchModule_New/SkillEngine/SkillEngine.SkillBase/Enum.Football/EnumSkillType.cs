using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase.Enum.Football
{
    public enum EnumSkillSrcType
    {
        /// <summary>
        /// 通用技能
        /// </summary>
        PlayerAction = 0,
        /// <summary>
        /// 球星技能
        /// </summary>
        PlayerStar = 1,
        /// <summary>
        /// 球星封印
        /// </summary>
        EquipWash = 2,
        /// <summary>
        /// 教练
        /// </summary>
        Coach = 3,
        /// <summary>
        /// 装备
        /// </summary>
        Equip = 4,
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
        /// 组合
        /// </summary>
        PlayerSuit = 9,
        /// <summary>
        /// 阵型
        /// </summary>
        Formation = 10,
        /// <summary>
        /// 球员觉醒
        /// </summary>
        StarWakeup=12,
        /// <summary>
        /// 星座技能
        /// </summary>
        StarSign=13,
        /// <summary>
        /// 公会技能
        /// </summary>
        GuildSkill=90,
        /// <summary>
        /// 经理隐藏
        /// </summary>
        ManagerHide=100,

    }

    public enum EnumSkillActType
    {
        None = 0,
        /// <summary>
        /// 射门
        /// </summary>
        Shoot = 1,
        /// <summary>
        /// 防守
        /// </summary>
        Defence = 2,
        /// <summary>
        /// 组织
        /// </summary>
        Organize = 3,
        /// <summary>
        /// 过人
        /// </summary>
        BreakThrough = 4,
        /// <summary>
        /// 守门
        /// </summary>
        GoalKeeping = 5,
       
    }
}
