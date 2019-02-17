/********************************************************************************
 * 文件名：Position.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __POSITION_H__
#define __POSITION_H__

/// <summary>
/// Represents the position of the football player.
/// </summary>
enum Position 
{
    /// <summary>
    /// Goalkeeper
    /// </summary>
    Position_Goalkeeper = 0,

    /// <summary>
    /// Full back
    /// </summary>
    Position_Fullback,

    /// <summary>
    /// Midfielder
    /// </summary>
    Position_Midfielder,

    /// <summary>
    /// Forward
    /// </summary>
    Position_Forward
};

#endif //__POSITION_H__
