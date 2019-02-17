/********************************************************************************
 * 文件名：IGetSkillTarget.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IGETSKILLTARGET_H__
#define __IGETSKILLTARGET_H__

class IPlayer;
class ISkillPart;

/// 表示了一个包含了获取技能目标函数的接口
class IGetSkillTarget 
{
public:

    virtual ~IGetSkillTarget() {}

public:

    /// 获取技能的目标
    /// <param name="player">Represents the trigger man. 表示了技能的发动人</param>
    /// <param name="part">Represents the <see cref="ISkillPart"/>. 表示了发动的技能需要解析的一部分</param>
    /// <returns>The effected players.表示了被影响的球员</returns>
    virtual vector<IPlayer*> GetSkillTargets(IPlayer* player, ISkillPart* part) = 0;
};

#endif //__IGETSKILLTARGET_H__
