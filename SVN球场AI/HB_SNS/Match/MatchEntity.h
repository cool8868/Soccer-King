/********************************************************************************
 * �ļ�����MatchEntity.h
 * �����ˣ��α�
 * ����ʱ�䣺2011-6-10
 * �汾��1.0
 * ���ļ��汾�ţ�1.0
 * �����£�
 * ����˵����
 * ��ʷ�޸ļ�¼��
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
    /// �������Ͷ�ѡ��һ������
    IManager*           GetManagerBySide(Side side);
    IManager*           GetManagerBySide(OpenballSide side);

    IPlayer*            GetPlayerByCoordiante(Coordinate c);

    /// ����
    void                Openball(IManager* openballManager);

    /// ����
    void                Goal(Side side);

    /// ��ƫ�������
    void                MissShoot();

    /// ����ľ�����
    void                Foul(IManager* manager);

    /// ����
    /// ��ʾ���������ľ���
    void                OutBorder(IManager* manager);

    /// Save the triggered opener skill.        
    void                SaveOpenerSkill(Side side, string skillId);

    /// ���浱ǰ�غϵ�״̬
    void                Save();

    /// �غϳ�ʼ��
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

    /// ��ʾ�˱���������
    int                 m_MatchType;

    /// ��ʾ�˵�ͼ���
    int                 m_MapId;

    /// ��ʾ����
    IPitch*             m_Pitch;

    /// ��ʾ��һ������
    IFootball*          m_Football;

    /// ��ʾ�����ӵľ���
    vector<IManager*>   m_Managers;

    /// ��ʾ�����Ӿ���
    IManager*           m_HomeManager;

    /// ��ʾ�˿ͶӾ���
    IManager*           m_AwayManager;

    /// ��ʾ�����ӱȷ�
    int                 m_HomeScore;

    /// ��ʾ�˿Ͷӱȷ�
    int                 m_AwayScore;

    /// ��ʾ�˿���ľ���
    IManager*           m_OpenballManager;

    /// ��ʾ�˱����ĵ�ǰ״̬
    MatchStatus*        m_Status;

    /// ��ʾ��һ���������ܻغ���
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

    /// ˢ�³�����
    void                RefreshBallHandler();

    //��ʼ������
    void                InitVariables();
};

#endif  //__MATCHENTITY_H__
