/********************************************************************************
 * 文件名：BuffType.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __BUFFTYPE_H__
#define __BUFFTYPE_H__

/// 表示了BUFF的类型
enum BuffType 
{
    /// <summary>
    /// Disturb
    /// 表示一个干扰类型的BUFF
    /// </summary>
    BuffType_Disturb = 0,

    /// <summary>
    /// Skill
    /// 表示一个技能类型的BUFF
    /// </summary>
    BuffType_Skill,

    /// <summary>
    /// Represents a gradient type buff.
    /// 表示一个DOT类型的BUFF
    /// </summary>
    BuffType_Dot,

    /// <summary>
    /// Represents the disable type buff.
    /// <example>
    /// 倒地，困惑，晕眩，惯性
    /// </example>
    /// </summary>
    BuffType_Disable,

    /// <summary>        
    /// Represents the finishing type buff.
    /// 表示了临门一脚BUFF
    /// </summary>
    BuffType_Finishing,

    /// <summary>
    /// Represents a buff that effect while the player is hold ball.
    /// 表示了持球才有效的Buff
    /// </summary>
    BuffType_HoldBall
};

#endif //__BUFFTYPE_H__
