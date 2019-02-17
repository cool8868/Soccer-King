/********************************************************************************
 * �ļ�����IMatch.h
 * �����ˣ��α�
 * ����ʱ�䣺2011-6-10
 * �汾��1.0
 * ���ļ��汾�ţ�1.0
 * �����£�
 * ����˵����
 * ��ʷ�޸ļ�¼��
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

    /// �������Ͷ�ѡ��һ������
    virtual IManager*           GetManagerBySide(Side side) = 0;

    /// ����
    virtual void                Openball(IManager* openballManager) = 0;

    /// ����
    virtual void                Goal(Side side) = 0;

    /// ��ƫ�������
    virtual void                MissShoot() = 0;

    /// ����ľ�����
    virtual void                Foul(IManager* manager) = 0;

    /// ����
    /// ��ʾ���������ľ���
    virtual void                OutBorder(IManager* manager) = 0;

    /// ���浱ǰ�غϵ�״̬
    virtual void                Save() = 0;

    /// �غϳ�ʼ��
    virtual void                RoundInit() = 0;

    /// ���津���˵ľ���Opener����
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
