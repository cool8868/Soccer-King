/********************************************************************************
 * �ļ�����SoccerManager.h
 * �����ˣ��α�
 * ����ʱ�䣺2011-6-10
 * �汾��1.0
 * ���ļ��汾�ţ�1.0
 * �����£�
 * ����˵����
 * ��ʷ�޸ļ�¼��
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
    
    //��ʼ������
    void                            InitVariables();

private:

    SoccerPitch*                    m_SoccerPitch;

    vector<SoccerPlayer*>           m_Players;
    IManager*                       m_Manager;
    ParseManagerEntity*             m_ParseManager;
};


#endif //__SOCCERMANAGERS_H__
