/********************************************************************************
 * 文件名：SoccerPitch.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
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

    //倍率转换
    void                        ReSize();

public:

    vector<SoccerManager*>&     Managers() { return m_Managers; }

private:

    void                        Attach();
    void                        AttachParser();

    int                         GetMaxRound();
    int                         GetParserMaxRound();

    //初始化变量
    void                        InitVariables();

public:    //球场的原始数据

    // 球场的长宽
    int             m_Ori_PitchWidth;
    int             m_Ori_PitchHeight;

    //两队球门的中心点
    Coordinate      m_Ori_HomeGoal;
    Coordinate      m_Ori_AwayGoal;

    //主客队禁区
    Region          m_Ori_Home_Penalty_Area;
    Region          m_Ori_Away_Penalty_Area;

    //球门的长宽
    int             m_Ori_GoalWidth;
    int             m_Ori_GoalHeight;

    //球门的区域
    Region          m_Ori_HomeGoalRegion;
    Region          m_Ori_AwayGoalRegion;

public:    // 转换后的数据

    // 球场的长宽
    int             m_PitchWidth;
    int             m_PitchHeight;

    //球场的区域
    Region          m_PitchRegion;

    //两队球门的中心点
    Coordinate      m_HomeGoal;
    Coordinate      m_AwayGoal;

    //主客队禁区
    Region          m_Home_Penalty_Area;
    Region          m_Away_Penalty_Area;

    //球门的长宽
    int             m_GoalWidth;
    int             m_GoalHeight;

    //球门的区域
    Region          m_HomeGoalRegion;
    Region          m_AwayGoalRegion;

private:

    //当前的回合数目
    int                     m_Round;

    //最大回合数目
    int                     m_MaxRound;

    IMatch*                 m_Match;

    //读取数据的比赛
    ParseMatchEntity*       m_ParseMatch;

    //////////////////////////////////////////////////////////////////////////
    vector<SoccerManager*>  m_Managers;
    SoccerFootball*         m_Football;
    SoccerMatch*            m_SoccerMatch;
};



#endif //__SOCCERPITCH_H__

