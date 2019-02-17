/********************************************************************************
 * 文件名：SoccerPlayer.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SOCCERPLAYER_H__
#define __SOCCERPLAYER_H__

#include "../Interface/Player/IPlayer.h"
#include "../common/Structs/Coordinate.h"
#include "../common/DisplayUtility.h"
#include "../common/Utility.h"
#include "../ProcessParser/ParseEntity.h"

class SoccerPitch;

class SoccerPlayer
{
public:

    SoccerPlayer(SoccerPitch* sp);

    virtual ~SoccerPlayer();

public:

    void                    Attach(IPlayer* player);
    void                    Attach(ParsePlayerEntity* player);

    void                    Render();
    void                    Render(size_t round);
    void                    RenderParser(size_t round);

private:

    //初始化变量
    void                    InitVariables();

private:

    //实体对象的边界半径的大小
    double                  m_dBoundingRadius;

    //干扰半径
    double                  m_DefenceRangeRadius;

    //抢断半径
    double                  m_InterRuptionRangeRadius;

    //视野半径
    double                  m_SightRangeRadius;

private:

    //球场指针
    SoccerPitch*            m_SoccerPitch;

    //当前的回合数
    int                     m_Round;

    IPlayer*                m_Player;

    ParsePlayerEntity*      m_ParsePlayer;
};

#endif //__SOCCERPLAYER_H__
