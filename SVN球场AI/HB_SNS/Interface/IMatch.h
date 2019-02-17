/********************************************************************************
 * 文件名：IMatch.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MATCH_H__
#define __MATCH_H__

#include "../common/common.h"

#include "IOutputer.h"
#include "Manager/IManager.h"
#include "ICreateMatch.h"
#include "IPitch.h"
#include "IFootball.h"

#include "../Match/Status/MatchStatus.h"
#include "../MatchProcess/Process.h"

class IMatch: public IInitMatch, public IOutputer
{
public:

    virtual ~IMatch() {}

public:

    /// 根据主客队选择一个经理
    virtual IManager*           GetManagerBySide(Side side) = 0;

    /// 开球
    virtual void                Openball(IManager* openballManager) = 0;

    /// 进球
    virtual void                Goal(Side side) = 0;

    /// 射偏或者射飞
    virtual void                MissShoot() = 0;

    /// 传入的经理犯规
    virtual void                Foul(IManager* manager) = 0;

    /// 出界
    /// 表示了让球出界的经理
    virtual void                OutBorder(IManager* manager) = 0;

    /// 保存当前回合的状态
    virtual void                Save() = 0;

    /// 回合初始化
    virtual void                RoundInit() = 0;

    /// 保存触发了的经理Opener技能
    virtual void                SaveOpenerSkill(Side side, string skillId) = 0;

public:
    
    virtual int                 MatchType() = 0;
    virtual void                SetMatchType(int matchType) = 0;

    virtual int                 MapId() = 0;
    virtual void                SetMapId(int mapId) = 0;

    virtual int                 HomeScore() = 0;
    virtual void                SetHomeScore(int score) = 0;

    virtual int                 AwayScore() = 0;
    virtual void                SetAwayScore(int score) = 0;

    virtual int                 GetTotalRound() = 0;

    virtual IPitch*             GetPitch() = 0;

    virtual IFootball*          GetFootball() = 0;

    virtual vector<IManager*>&  Managers() = 0;

    virtual IManager*           HomeManager() = 0;

    virtual IManager*           AwayManager() = 0;

    virtual IManager*           OpenballManager() = 0;

    virtual MatchStatus*        Status() = 0;
};

#endif
