/********************************************************************************
 * 文件名：SoccerMatch.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SOCCERMATCH_H__
#define __SOCCERMATCH_H__

#include "SoccerPlayer.h"
#include "../Interface/Manager/IManager.h"
#include "../Interface/IFootball.h"
#include "../ProcessParser/ParseEntity.h"

class SoccerPitch;

class SoccerMatch
{
public:

    SoccerMatch(SoccerPitch* sp);

    virtual ~SoccerMatch();

public:

    void            Attach(IMatch* match);
    void            Attach(ParseMatchEntity* match);

    void            Render(size_t round);
    void            RenderParser(size_t round);
    void            Render();

    //重新开始
    void            Restart();

private:

    void InitVariables();

private:

    //球场指针
    SoccerPitch*        m_SoccerPitch;

    //主队进球数
    int                 m_HomeScore;

    //客队进球数
    int                 m_AwayScore;

    //回合数
    int                 m_Round;

    IMatch*             m_Match;

    vector<Process*>    m_MatchProcess;
};


#endif //__SOCCERMATCH_H__
