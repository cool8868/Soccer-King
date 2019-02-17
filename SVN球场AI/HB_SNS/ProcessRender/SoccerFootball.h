/********************************************************************************
 * �ļ�����SoccerFootball.h
 * �����ˣ��α�
 * ����ʱ�䣺2011-6-10
 * �汾��1.0
 * ���ļ��汾�ţ�1.0
 * �����£�
 * ����˵����
 * ��ʷ�޸ļ�¼��
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

    //��ָ��
    SoccerPitch*        m_SoccerPitch;

    // ����İ뾶
    double              m_dBoundingRadius;

    int                 m_Round;

    IFootball*          m_Football;

    vector<Process*>    m_FootballProcess;
};


#endif //__SOCCERFOOTBALL_H__
