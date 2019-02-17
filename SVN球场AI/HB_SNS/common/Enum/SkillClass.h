/********************************************************************************
 * 文件名：SkillClass.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SKILLCLASS_H__
#define __SKILLCLASS_H__

/// 表示了技能的分类
enum SkillClass 
{
    /// <summary>
    /// Shooting class
    /// 射门类
    /// </summary>
    SkillClass_Shoot = 0,

    /// <summary>
    /// Defence class
    /// 防守类
    /// </summary>
    SkillClass_Defence,

    /// <summary>
    /// Organization class
    /// 组织类
    /// </summary>
    SkillClass_Organization,

    /// <summary>
    /// BreakThrough class
    /// 过人类
    /// </summary>
    SkillClass_BreakThrough,

    /// <summary>
    /// Goalkeep class
    /// 防守类
    /// </summary>
    SkillClass_Goalkeep
};

#endif //__SKILLCLASS_H__
