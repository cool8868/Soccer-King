/********************************************************************************
 * 文件名：DebuffType.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DEBUFFTYPE_H__
#define __DEBUFFTYPE_H__

/// <summary>
/// Represents the debuff's type.
/// 表示了异常状态的类型
/// </summary>
enum DebuffType 
{
    /// <summary>
    /// Represents player has no debuff.
    /// 无异常状态
    /// </summary>
    DebuffType_None = 0,

    /// <summary>
    /// Represents the down debuff.
    /// 倒地状态
    /// </summary>
    DebuffType_DownDebuff = 1,

    /// <summary>
    /// Represents the puzzle debuff.
    /// 困惑
    /// </summary>
    DebuffType_PuzzleDebuff = 2,

    /// <summary>
    /// Represents the out of hand debuff.
    /// 脱手
    /// </summary>
    DebuffType_OutOfHandDebuff = 3,

    /// <summary>
    /// Represents the stun debuff.
    /// 晕眩
    /// </summary>
    DebuffType_StunDebuff = 4,

    /// <summary>
    /// Represents the inertia type debuff.
    /// 惯性
    /// </summary>
    DebuffType_InertiaDebuff = 5,

    /// <summary>
    /// Represents the freeze type debuff.
    /// 静止
    /// </summary>
    DebuffType_FreezeDebuff
};

#endif //__DEBUFFTYPE_H__
