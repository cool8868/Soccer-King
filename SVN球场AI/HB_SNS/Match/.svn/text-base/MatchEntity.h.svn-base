/********************************************************************************
 * 文件名：MatchEntity.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MATCHENTITY_H__
#define __MATCHENTITY_H__

#include "../common/common.h"

#include "../Interface/IMatch.h"
#include "../Interface/IPitch.h"
#include "../Interface/IFootball.h"

#include "../Manager/Manager.h"
#include "../MatchProcess/Process.h"
#include "../MatchProcess/MatchProcess.h"

#include "Status/MatchStatus.h"

class MatchEntity: public IMatch
{
public:

    MatchEntity();

    ~MatchEntity();

    void Release();

public:

    void                InitMatch(int homeId, int awayId, int matchType, double time);
    void                InitMatch(TransferMatchEntity* entity);

    int                 HalfMatchRoundCount() { return m_HalfMatchRoundCount; }

    vector<Process*>&   Processes() { return m_Processes; }

    //////////////////////////////////////////////////////////////////////////
    /// 根据主客队选择一个经理
    IManager*           GetManagerBySide(Side side);
    IManager*           GetManagerBySide(OpenballSide side);

    IPlayer*            GetPlayerByCoordiante(Coordinate c);

    /// 开球
    void                Openball(IManager* openballManager);

    /// 进球
    void                Goal(Side side);

    /// 射偏或者射飞
    void                MissShoot();

    /// 传入的经理犯规
    void                Foul(IManager* manager);

    /// 出界
    /// 表示了让球出界的经理
    void                OutBorder(IManager* manager);

    /// Save the triggered opener skill.        
    void                SaveOpenerSkill(Side side, string skillId);

    /// 保存当前回合的状态
    void                Save();

    /// 回合初始化
    void                RoundInit();

    //////////////////////////////////////////////////////////////////////////
    int                 MatchType() { return m_MatchType; }
    void                SetMatchType(int matchType) { m_MatchType = matchType; }

    int                 MapId() { return m_MapId; }
    void                SetMapId(int mapId) { m_MapId = mapId; }

    int                 HomeScore() { return m_HomeScore; }
    void                SetHomeScore(int score) { m_HomeScore = score; }

    int                 AwayScore() { return m_AwayScore; }
    void                SetAwayScore(int score) { m_AwayScore = score; }

    int                 GetTotalRound() { return m_TotalRound; }

    IPitch*             GetPitch() { return m_Pitch; }

    IFootball*          GetFootball() { return m_Football; }

    vector<IManager*>&  Managers() { return m_Managers; }

    IManager*           HomeManager() { return m_HomeManager; }

    IManager*           AwayManager() { return m_AwayManager; }

    IManager*           OpenballManager();

    MatchStatus*        Status() { return m_Status; }

protected:

    /// 表示了比赛的类型
    int                 m_MatchType;

    /// 表示了地图编号
    int                 m_MapId;

    /// 表示了球场
    IPitch*             m_Pitch;

    /// 表示了一个足球
    IFootball*          m_Football;

    /// 表示了两队的经理
    vector<IManager*>   m_Managers;

    /// 表示了主队经理
    IManager*           m_HomeManager;

    /// 表示了客队经理
    IManager*           m_AwayManager;

    /// 表示了主队比分
    int                 m_HomeScore;

    /// 表示了客队比分
    int                 m_AwayScore;

    /// 表示了开球的经理
    IManager*           m_OpenballManager;

    /// 表示了比赛的当前状态
    MatchStatus*        m_Status;

    /// 表示了一场比赛的总回合数
    int                 m_TotalRound;

    //////////////////////////////////////////////////////////////////////////
    int                 m_HalfMatchRoundCount;

    vector<Process*>    m_Processes;

private:

    void                SetHalfMatchRoundCount(int count) { m_HalfMatchRoundCount = count; }
    int                 CalculateGameTime();

    void                InitBasicInfo(int matchType, double time);
    void                InitPlayerPropertyBalance();

    void                InternalSave();

    /// 刷新持球人
    void                RefreshBallHandler();

    //初始化变量
    void                InitVariables();
};

#endif  //__MATCHENTITY_H__
