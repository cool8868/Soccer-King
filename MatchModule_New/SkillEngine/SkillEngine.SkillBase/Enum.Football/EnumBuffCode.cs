using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase.Enum.Football
{
    public enum EnumBuffCode
    {
        None = 0,

        #region PropPlus 1000-
        /// <summary>
        /// Speed
        /// 速度
        /// </summary>
        Speed = 1000,

        /// <summary>
        /// Shooting
        /// 射门
        /// </summary>
        Shooting = 1001,

        /// <summary>
        /// Free Kick
        /// 任意球
        /// </summary>
        FreeKick = 1002,

        /// <summary>
        /// Balance
        /// 控制
        /// </summary>
        Balance = 1003,

        /// <summary>
        /// Starmina
        /// 体能
        /// </summary>
        Stamina = 1004,

        /// <summary>
        /// Bounce
        /// 弹跳
        /// </summary>
        Bounce = 1005,

        /// <summary>
        /// Aggression
        /// 侵略性
        /// </summary>
        Aggression = 1006,

        /// <summary>
        /// Disturb
        /// 干扰
        /// </summary>
        Disturb = 1007,

        /// <summary>
        /// Interception
        /// 抢断
        /// </summary>
        Interception = 1008,

        /// <summary>
        /// Dribble
        /// 控球
        /// </summary>
        Dribble = 1009,

        /// <summary>
        /// Passing
        /// 传球
        /// </summary>
        Passing = 1010,

        /// <summary>
        /// Mentality
        /// 意识
        /// </summary>
        Mentality = 1011,

        /// <summary>
        /// Reflexes
        /// 反应
        /// </summary>
        Reflexes = 1012,

        /// <summary>
        /// Positioning
        /// 位置感
        /// </summary>
        Positioning = 1013,

        /// <summary>
        /// Handling
        /// 手控球
        /// </summary>
        Handling = 1014,

        /// <summary>
        /// Acceleration
        /// 加速度
        /// </summary>
        Acceleration = 1015,
        /// <summary>
        /// 力量
        /// </summary>
        Strength=1016,
        #endregion

        #region ActionRate 2000-
        /// <summary>
        /// 传球选择率
        /// </summary>
        PassChooseRate = 2020,
        /// <summary>
        /// 带球选择率
        /// </summary>
        DribbleChooseRate = 2021,
        /// <summary>
        /// 射门选择率
        /// </summary>
        ShootChooseRate = 2022,
        /// <summary>
        /// 抢断选择率
        /// </summary>
        StealChooseRate = 2023,
        /// <summary>
        /// 传球成功率
        /// </summary>
        PassSuccRate = 2024,
        /// <summary>
        /// 带球成功率
        /// </summary>
        DribbleSuccRate = 2025,
        /// <summary>
        /// 射门成功率
        /// </summary>
        ShootSuccRate = 2026,
        /// <summary>
        /// 抢断成功率
        /// </summary>
        StealSuccRate = 2027,
        /// <summary>
        /// 扑救成功率
        /// </summary>
        DiveSuccRate = 2028,
        /// <summary>
        /// 反抢概率
        /// </summary>
        TurnStealRate = 2029,
        /// <summary>
        /// 争顶成功率
        /// </summary>
        HeadingDuelRate=2030,
        #endregion

        #region ActionRange 3000-
        /// <summary>
        /// 射门范围
        /// </summary>
        ShootRange = 3030,
        /// <summary>
        /// 防守半径
        /// </summary>
        DefenceRange = 3031,
        /// <summary>
        /// 干扰半径
        /// </summary>
        DisturbRange = 3032,
        /// <summary>
        /// 抢断半径
        /// </summary>
        StealRange = 3033,
        #endregion

        #region Blur 5000-
        #endregion

        #region Foul 6000-
        /// <summary>
        /// 犯规概率
        /// </summary>
        FoulRate = 6060,
        /// <summary>
        /// 体能流失速度
        /// </summary>
        StaminaLossSpeed = 6061,
        /// <summary>
        /// 体能流失下限
        /// </summary>
        StaminaLossFloor = 6062,
        /// <summary>
        /// 脱手概率
        /// </summary>
        OutHandRate = 6063,
        #endregion
    }
}
