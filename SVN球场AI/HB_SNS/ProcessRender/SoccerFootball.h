/********************************************************************************
 * 文件名：SoccerFootball.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SOCCERFOOTBALL_H__
#define __SOCCERFOOTBALL_H__

#include "SoccerPlayer.h"
#include "../Interface/Manager/IManager.h"
#include "../Interface/IFootball.h"
#include "../ProcessParser/ParseEntity.h"

class SoccerPitch;

class SoccerFootball
{
public:

    SoccerFootball(SoccerPitch* sp);

    virtual ~SoccerFootball();

public:

    void            Attach(IMatch* match);
    void            Attach(ParseMatchEntity* match);

    void            Render(size_t round);
    void            RenderParser(size_t round);
    void            Render();

private:

    void InitVariables();

private:

    //球场指针
    SoccerPitch*        m_SoccerPitch;

    // 足球的半径
    double              m_dBoundingRadius;

    int                 m_Round;

    IFootball*          m_Football;

    vector<Process*>    m_FootballProcess;
};


#endif //__SOCCERFOOTBALL_H__
