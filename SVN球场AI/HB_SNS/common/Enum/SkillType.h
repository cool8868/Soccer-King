/********************************************************************************
 * 文件名：SkillType.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SKILLTYPE_H__
#define __SKILLTYPE_H__

/// 表示了技能的类型
enum SkillType 
{
    /// <summary>
    /// Action
    /// Action技能
    /// </summary>
    SkillType_Action,

    /// <summary>
    /// Opener
    /// Opener技能
    /// </summary>
    SkillType_Opener,

    /// <summary>
    /// Passive
    /// 被动技能
    /// </summary>
    SkillType_Passive,

    /// <summary>
    /// Will
    /// 球员意志
    /// </summary>
    SkillType_Will
};

#endif //__SKILLTYPE_H__
