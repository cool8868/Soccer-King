/********************************************************************************
 * 文件名：Interruption.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __INTERRUPTION_H__
#define __INTERRUPTION_H__

/// Represents the type of the interruption in a match
enum Interruption 
{
    /// <summary>
    /// None
    /// </summary>
    Interruption_None,

    /// <summary>
    /// Open ball
    /// 进球
    /// </summary>
    Interruption_Openball,

    /// <summary>
    /// Free kick
    /// 任意球
    /// </summary>
    Interruption_FreeKick
};

#endif //__INTERRUPTION_H__
