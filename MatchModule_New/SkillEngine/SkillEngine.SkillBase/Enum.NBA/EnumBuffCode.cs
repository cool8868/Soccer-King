using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SkillEngine.SkillBase.Enum.NBA
{
    public enum EnumBuffCode
    {
        None = 0,

        #region PropPlus 1000-
        /// <summary>
        /// 中投
        /// </summary>
        JumpShot = 1000,
        /// <summary>
        /// 三分
        /// </summary>
        ThreePoints = 1001,
        /// <summary>
        /// 封盖
        /// </summary>
        Rejection = 1002,
        /// <summary>
        /// 抢断
        /// </summary>
        Steals = 1003,
        /// <summary>
        /// 传球
        /// </summary>
        Pass = 1004,
        /// <summary>
        /// 控球
        /// </summary>
        Dribble = 1005,
        /// <summary>
        /// 扣篮
        /// </summary>
        Dunk = 1006,
        /// <summary>
        /// 篮板
        /// </summary>
        Rebound = 1007,
        /// <summary>
        /// 速度
        /// </summary>
        Speed = 1008,
        /// <summary>
        /// 体能
        /// </summary>
        Stamina = 1009,
        #endregion

        #region PropAlter 2000-
        #endregion

        #region ActionRate 3000-
        /// <summary>
        /// 进攻战术执行率
        /// </summary>
        AtkTacticExecRate = 3001,
        /// <summary>
        /// 防守战术执行率
        /// </summary>
        DefTacticExecRate = 3002,
        /// <summary>
        /// 进攻战术效果加成
        /// </summary>
        AtkTacticPropPlus = 3003,
        /// <summary>
        /// 防守战术效果加成
        /// </summary>
        DefTacticPropPlus = 3004,
        /// <summary>
        /// 中投选择率
        /// </summary>
        JumpShotPickRate = 3101,
        /// <summary>
        /// 三分选择率
        /// </summary>
        ThreePointPickRate = 3102,
        /// <summary>
        /// 扣篮选择率
        /// </summary>
        DunkPickRate = 3103,
        /// <summary>
        /// 进攻篮板补篮概率
        /// </summary>
        PutbackPickRate = 3104,
        /// <summary>
        /// 罚球成功率
        /// </summary>
        FoulShotSuccRate = 3110,
        /// <summary>
        /// 中投成功率
        /// </summary>
        JumpShotSuccRate = 3111,
        /// <summary>
        /// 三分成功率
        /// </summary>
        ThreePointSuccRate = 3112,
        /// <summary>
        /// 扣篮成功率
        /// </summary>
        DunkSuccRate = 3113,
        /// <summary>
        /// 传球成功率
        /// </summary>
        PassSuccRate = 3114,
        /// <summary>
        /// 突破成功率
        /// </summary>
        DribbleSuccRate = 3115,
        /// <summary>
        /// 传球拦截成功率
        /// </summary>
        PassStealSuccRate = 3116,
        /// <summary>
        /// 突破抢断成功率
        /// </summary>
        DribbleStealSuccRate = 3117,
        /// <summary>
        /// 封盖成功率
        /// </summary>
        RejectionSuccRate = 3118,
        /// <summary>
        /// 进攻篮板成功率
        /// </summary>
        AtkReboundSuccRate = 3119,
        /// <summary>
        /// 防守篮板成功率
        /// </summary>
        DefReboundSuccRate = 3120,
        /// <summary>
        /// 投篮失败率
        /// </summary>
        ShootMissRate = 3201,
        /// <summary>
        /// 无视封盖概率
        /// </summary>
        AntiRejectionRate = 3301,
        /// <summary>
        /// 无视防守概率
        /// </summary>
        AntiStealRate = 3302,
        #endregion

        #region ActionParm 4000-
        /// <summary>
        /// 扣篮半径
        /// </summary>
        DunkRange = 4001,
        /// <summary>
        /// 抢断半径
        /// </summary>
        StealRange = 4002,
        /// <summary>
        /// 封盖、防守半径
        /// </summary>
        DefenceRange = 4003,
        /// <summary>
        /// 体能流失速度
        /// </summary>
        StaminaLossSpeed = 4011,
        /// <summary>
        /// 体能流失下限
        /// </summary>
        StaminaLossFloor = 4012,
        #endregion

        #region Blur 5000-
        #endregion

        #region Foul 6000-
        /// <summary>
        /// 犯规概率
        /// </summary>
        FoulRate = 6000,
        #endregion

        #region AlterMotion 7000-
        #endregion

        #region BallAct 8000-
        #endregion

      
    }
  
}
