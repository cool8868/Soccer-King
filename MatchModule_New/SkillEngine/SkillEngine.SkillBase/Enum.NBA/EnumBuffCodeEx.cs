using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase.Enum.NBA
{
    public enum EnumBlurBuffCode
    {
        None=0,
        #region BanMan
        /// <summary>
        /// 致残
        /// </summary>
        //Disable = 5101,
        /// <summary>
        /// 致伤
        /// </summary>
        //Injure = 5102,
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
        //Puzzle = 5203,
        /// <summary>
        /// 晕眩
        /// </summary>
        Stun = 5204,
        /// <summary>
        /// 惯性
        /// </summary>
        //Inertia = 5205,
        /// <summary>
        /// 迷惑（叛乱）
        /// </summary>
        //Rebel = 5206,
        #endregion

        #region LockSkill
        /// <summary>
        /// 沉默
        /// </summary>
        Silence = 5301,
        #endregion
    }
    public enum EnumFoulBuffCode
    {
        /// <summary>
        /// 普通犯规
        /// </summary>
        FoulNormal = 6101,
        /// <summary>
        /// 罚球
        /// </summary>
        FoulShot = 6102,
        /// <summary>
        /// 技能罚球（天赋：上帝眷顾）
        /// </summary>
        SkillFoulShot = 6103,
    }
}
