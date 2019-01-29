using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NB.Match.Base.Model
{

    /// <summary>
    /// Represents the mapping between the player property's name and the id.
    /// 表示了球员的属性名称和属性id的对应关系
    /// </summary>
    public struct PlayerProperty
    {

        #region Prop
        /// <summary>
        /// Speed
        /// 速度
        /// </summary>
        public const byte Speed = 0;

        /// <summary>
        /// Shooting
        /// 射门
        /// </summary>
        public const byte Shooting = 1;

        /// <summary>
        /// Free Kick
        /// 任意球
        /// </summary>
        public const byte FreeKick = 2;

        /// <summary>
        /// Balance
        /// 控制
        /// </summary>
        public const byte Balance = 3;

        /// <summary>
        /// Starmina
        /// 体能
        /// </summary>
        public const byte Stamina = 4;

        /// <summary>
        /// Bounce
        /// 弹跳
        /// </summary>
        public const byte Bounce = 5;

        /// <summary>
        /// Aggression
        /// 侵略性
        /// </summary>
        public const byte Aggression = 6;

        /// <summary>
        /// Disturb
        /// 干扰
        /// </summary>
        public const byte Disturb = 7;

        /// <summary>
        /// Interception
        /// 抢断
        /// </summary>
        public const byte Interception = 8;

        /// <summary>
        /// Dribble
        /// 控球
        /// </summary>
        public const byte Dribble = 9;

        /// <summary>
        /// Passing
        /// 传球
        /// </summary>
        public const byte Passing = 10;

        /// <summary>
        /// Mentality
        /// 意识
        /// </summary>
        public const byte Mentality = 11;

        /// <summary>
        /// Reflexes
        /// 反应
        /// </summary>
        public const byte Reflexes = 12;

        /// <summary>
        /// Positioning
        /// 位置感
        /// </summary>
        public const byte Positioning = 13;

        /// <summary>
        /// Handling
        /// 手控球
        /// </summary>
        public const byte Handling = 14;

        /// <summary>
        /// Acceleration
        /// 加速度
        /// </summary>
        public const byte Acceleration = 15;
        /// <summary>
        /// 力量
        /// </summary>
        public const byte Strength = 16;
        #endregion

        #region Rate 2000-
        /// <summary>
        /// 传球选择率
        /// </summary>
        public const int PassChooseRate = 2020;
        /// <summary>
        /// 带球选择率
        /// </summary>
        public const int DribbleChooseRate = 2021;
        /// <summary>
        /// 射门选择率
        /// </summary>
        public const int ShootChooseRate = 2022;
        /// <summary>
        /// 抢断选择率
        /// </summary>
        public const int StealChooseRate = 2023;
        /// <summary>
        /// 传球成功率
        /// </summary>
        public const int PassSuccRate = 2024;
        /// <summary>
        /// 带球成功率
        /// </summary>
        public const int DribbleSuccRate = 2025;
        /// <summary>
        /// 射门成功率
        /// </summary>
        public const int ShootSuccRate = 2026;
        /// <summary>
        /// 抢断成功率
        /// </summary>
        public const int StealSuccRate = 2027;
        /// <summary>
        /// 扑救成功率
        /// </summary>
        public const int DiveSuccRate = 2028;
        /// <summary>
        /// 反抢概率
        /// </summary>
        public const int TurnStealRate = 2029;
        #endregion

        #region Range 3000-
        /// <summary>
        /// 射门范围
        /// </summary>
        public const int ShootRange = 3030;
        /// <summary>
        /// 防守半径
        /// </summary>
        public const int DefenceRange = 3031;
        /// <summary>
        /// 干扰半径
        /// </summary>
        public const int DisturbRange = 3032;
        /// <summary>
        /// 抢断半径
        /// </summary>
        public const int StealRange = 3033;
        #endregion


        #region 统计专用
        public const int ShootingDist = 9000;
        #endregion
    }
}
