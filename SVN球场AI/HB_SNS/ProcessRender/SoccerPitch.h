/********************************************************************************
 * �ļ�����SoccerPitch.h
 * �����ˣ��α�
 * ����ʱ�䣺2011-6-10
 * �汾��1.0
 * ���ļ��汾�ţ�1.0
 * �����£�
 * ����˵����
 * ��ʷ�޸ļ�¼��
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SOCCERPITCH_H__
#define __SOCCERPITCH_H__

#include "../common/common.h"
#include "../Interface/IMatch.h"
#include "SoccerManager.h"
#include "SoccerFootball.h"
#include "SoccerMatch.h"
#include "../ProcessParser/ParseEntity.h"

class SoccerPitch
{
public:

    SoccerPitch();

    ~SoccerPitch();

public:

    void                        Attach(IMatch* match);
    void                        Attach(ParseMatchEntity* match);

    void                        RenderByPlayer();
    void                        RenderByProcess();
    void                        RenderByParserProcess();

    void                        Update();

    void                        Restart();

    //����ת��
    void                        ReSize();

public:

    vector<SoccerManager*>&     Managers() { return m_Managers; }

private:

    void                        Attach();
    void                        AttachParser();

    int                         GetMaxRound();
    int                         GetParserMaxRound();

    //��ʼ������
    void                        InitVariables();

public:    //�򳡵�ԭʼ����

    // �򳡵ĳ���
    int             m_Ori_PitchWidth;
    int             m_Ori_PitchHeight;

    //�������ŵ����ĵ�
    Coordinate      m_Ori_HomeGoal;
    Coordinate      m_Ori_AwayGoal;

    //���Ͷӽ���
    Region          m_Ori_Home_Penalty_Area;
    Region          m_Ori_Away_Penalty_Area;

    //���ŵĳ���
    int             m_Ori_GoalWidth;
    int             m_Ori_GoalHeight;

    //���ŵ�����
    Region          m_Ori_HomeGoalRegion;
    Region          m_Ori_AwayGoalRegion;

public:    // ת���������

    // �򳡵ĳ���
    int             m_PitchWidth;
    int             m_PitchHeight;

    //�򳡵�����
    Region          m_PitchRegion;

    //�������ŵ����ĵ�
    Coordinate      m_HomeGoal;
    Coordinate      m_AwayGoal;

    //���Ͷӽ���
    Region          m_Home_Penalty_Area;
    Region          m_Away_Penalty_Area;

    //���ŵĳ���
    int             m_GoalWidth;
    int             m_GoalHeight;

    //���ŵ�����
    Region          m_HomeGoalRegion;
    Region          m_AwayGoalRegion;

private:

    //��ǰ�Ļغ���Ŀ
    int                     m_Round;

    //���غ���Ŀ
    int                     m_MaxRound;

    IMatch*                 m_Match;

    //��ȡ���ݵı���
    ParseMatchEntity*       m_ParseMatch;

    //////////////////////////////////////////////////////////////////////////
    vector<SoccerManager*>  m_Managers;
    SoccerFootball*         m_Football;
    SoccerMatch*            m_SoccerMatch;
};



#endif //__SOCCERPITCH_H__

