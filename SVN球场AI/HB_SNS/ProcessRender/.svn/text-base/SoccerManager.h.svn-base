/********************************************************************************
 * 文件名：SoccerManager.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SOCCERMANAGERS_H__
#define __SOCCERMANAGERS_H__

#include "SoccerPlayer.h"
#include "../Interface/Manager/IManager.h"
#include "../ProcessParser/ParseEntity.h"

class SoccerPitch;

class SoccerManager
{
public:

    SoccerManager(SoccerPitch* sp);

    virtual ~SoccerManager();

public:

    void                            Attach(IManager* manager);
    void                            Attach(ParseManagerEntity* manager);

    vector<SoccerPlayer*>&          Players() { return m_Players; }

    void                            Render();
    void                            Render(size_t round);
    void                            RenderParser(size_t round);

private:

    void                            Attach();
    void                            AttachParser();
    
    //初始化变量
    void                            InitVariables();

private:

    SoccerPitch*                    m_SoccerPitch;

    vector<SoccerPlayer*>           m_Players;
    IManager*                       m_Manager;
    ParseManagerEntity*             m_ParseManager;
};


#endif //__SOCCERMANAGERS_H__
