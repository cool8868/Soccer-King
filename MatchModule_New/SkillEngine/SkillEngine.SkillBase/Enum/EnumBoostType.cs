using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.SkillBase.Enum
{
    public enum EnumBoostType
    {
        /// <summary>
        /// 加强持续时间
        /// </summary>
        AmpLast = 1,
        /// <summary>
        /// 加强效果概率
        /// </summary>
        AmpRate = 2,
        /// <summary>
        /// 加强效果值
        /// </summary>
        AmpValue = 3,
        /// <summary>
        /// 减弱持续时间
        /// </summary>
        EaseLast = 4,
        /// <summary>
        /// 减弱效果概率
        /// </summary>
        EaseRate = 5,
        /// <summary>
        /// 减弱效果值
        /// </summary>
        EaseValue = 6,
        /// <summary>
        /// 免疫概率
        /// </summary>
        AntiRate = 7,
        /// <summary>
        /// 技能CD
        /// </summary>
        SkillCD = 11,
        /// <summary>
        /// 技能触发率
        /// </summary>
        SkillRate = 12,
        /// <summary>
        /// 无视免疫概率
        /// </summary>
        PureBuff=91,
        /// <summary>
        /// 免疫Debuff概率
        /// </summary>
        AntiDebuff = 92,
    }

    public enum EnumBoostRootType
    {
        #region BoostBuff 9000-
        /// <summary>
        /// 增益属性持续时间
        /// </summary>
        PropBuffLast = 9101,
        /// <summary>
        /// 减益属性持续时间
        /// </summary>
        PropDebuffLast = 9102,
        /// <summary>
        /// 异常状态持续时间
        /// </summary>
        BlurLast = 9103,
        //Rate:9200-
        /// <summary>
        /// 异常状态概率
        /// </summary>
        BlurRate = 9203,
        /// <summary>
        /// 犯规概率
        /// </summary>
        FoulRate = 9204,
        //Value:9300-
        /// <summary>
        /// 增益属性效果值
        /// </summary>
        PropBuffValue = 9301,
        /// <summary>
        /// 减益属性效果值
        /// </summary>
        PropDebuffValue = 9302,
        /// <summary>
        /// 犯规等级
        /// </summary>
        FoulValue=9303,
        //AntiRate:9900-
        /// <summary>
        /// 免疫减益属性
        /// </summary>
        AntiPropDebuff = 9901,
        /// <summary>
        /// 免疫异常状态
        /// </summary>
        AntiBlur = 9903,
        /// <summary>
        /// 免疫犯规
        /// </summary>
        AntiFoul = 9904,
        /// <summary>
        /// 反弹犯规
        /// </summary>
        TurnFoul = 9905,
        #endregion
        
    }
}
