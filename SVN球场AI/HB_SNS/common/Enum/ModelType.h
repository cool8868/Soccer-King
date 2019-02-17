/********************************************************************************
 * 文件名：ModelType.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MODELTYPE_H__
#define __MODELTYPE_H__

/// 表示了模型的类型枚举
enum ModelType 
{
    /// <summary>
    /// Normal Model.
    /// 正常
    /// </summary>
    ModelType_Normal = 0,

    /// <summary>
    /// Disappear Model.
    /// 消失模型
    /// </summary>
    ModelType_Disappear = 1,

    /// <summary>
    /// 假动作过人
    /// </summary>
    ModelType_FakeAction = 2,

    /// <summary>
    /// 加速过人
    /// </summary>
    ModelType_Accelerate = 3,

    /// <summary>
    /// 穿档过人
    /// </summary>
    ModelType_Nutmeg = 4,

    /// <summary>
    /// 挑球过人
    /// </summary>
    ModelType_ToeLift = 5,

    /// <summary>
    /// 踩单车
    /// </summary>
    ModelType_BicycleKicks = 6,

    /// <summary>
    /// 幻影突破
    /// </summary>
    ModelType_ShadowThrough = 7,

    /// <summary>
    /// 贴身防守
    /// </summary>
    ModelType_TightMarking = 8,

    /// <summary>
    /// 怒吼特效
    /// </summary>
    ModelType_RoarEffect1 = 9,

    /// <summary>
    /// 咆哮特效
    /// </summary>
    ModelType_RoalEffect2 = 10,

    /// <summary>
    /// 倒地效果
    /// </summary>
    ModelType_DownEffect = 11,

    /// <summary>
    /// 困惑效果
    /// </summary>
    ModelType_PuzzleEffect = 12,

    /// <summary>
    /// 眩晕
    /// </summary>
    ModelType_StunEffect = 13,

    /// <summary>
    /// 惯性
    /// </summary>
    ModelType_InertiaEffect = 14,

    /// <summary>
    /// 静止状态
    /// </summary>
    ModelType_FreezeEffect = 15,

    /// <summary>
    /// 外星人效果
    /// </summary>
    ModelType_UFO = 16,

    /// <summary>
    /// 葫芦
    /// </summary>
    ModelType_Calabash = 17,

    /// <summary>
    /// 幽灵
    /// </summary>
    ModelType_Ghost = 18,

    /// <summary>
    /// 小罗牛尾巴
    /// </summary>
    ModelType_Elastico = 19,

    /// <summary>
    /// 魔术盘带
    /// </summary>
    ModelType_MagicDribble = 20,

    /// <summary>
    /// 优雅蝴蝶
    /// </summary>
    ModelType_BeautyButterfly = 21,

    /// <summary>
    /// 金童
    /// </summary>
    ModelType_GoldenChild = 22,

    /// <summary>
    /// 炸弹效果
    /// </summary>
    ModelType_Bomb = 23,

    /// <summary>
    /// 燃烧状态
    /// </summary>
    ModelType_Burn = 24,

    /// <summary>
    /// 装弹效果
    /// </summary>
    ModelType_Reload = 25,

    /// <summary>
    /// 门神怒吼特效
    /// </summary>
    ModelType_RoalEffect3 = 26,

    /// <summary>
    /// 吸魂效果
    /// </summary>
    ModelType_AbsorbSoul = 27,

    /// <summary>
    /// 胶水
    /// </summary>
    ModelType_Gluewater
};

#endif //__MODELTYPE_H__
