using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase.Enum.Football
{
    public enum EnumBlurBuffCode
    {
        None = 0,
        #region BanMan
        /// <summary>
        /// 致残
        /// </summary>
        Disable = 5101,
        /// <summary>
        /// 致伤
        /// </summary>
        Injure = 5102,
        /// <summary>
        /// 伤残后复活
        /// </summary>
        Reborn = 5103,
        /// <summary>
        /// 倒地后致残
        /// </summary>
        FalldownThenInjure = 5104,
        #endregion

        #region LockMotion
        /// <summary>
        /// 静止
        /// </summary>
        Stand = 5201,
        /// <summary>
        /// 倒地
        /// </summary>
        Falldown = 5202,
        /// <summary>
        /// 困惑
        /// </summary>
        Puzzle = 5203,
        /// <summary>
        /// 晕眩
        /// </summary>
        Stun = 5204,
        /// <summary>
        /// 惯性
        /// </summary>
        Inertia = 5205,
        #endregion

        #region LockSkill
        /// <summary>
        /// 沉默
        /// </summary>
        Silence = 5301,
        #endregion

        #region SpecMotion
        /// <summary>
        /// 脱手
        /// </summary>
        OutHand = 5401,
        /// <summary>
        /// 迷惑（叛乱）
        /// </summary>
        Rebel = 5402,
        #endregion
    }
    public enum EnumFoulBuffCode
    {
        /// <summary>
        /// 普通犯规
        /// </summary>
        FoulNormal = 6601,
        /// <summary>
        /// 黄牌
        /// </summary>
        FoulYellow = 6602,
        /// <summary>
        /// 红牌
        /// </summary>
        FoulRed = 6603,
    }
}
