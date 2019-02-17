/********************************************************************************
 * �ļ�����SoccerPlayer.h
 * �����ˣ��α�
 * ����ʱ�䣺2011-6-10
 * �汾��1.0
 * ���ļ��汾�ţ�1.0
 * �����£�
 * ����˵����
 * ��ʷ�޸ļ�¼��
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

    //��ʼ������
    void                    InitVariables();

private:

    //ʵ�����ı߽�뾶�Ĵ�С
    double                  m_dBoundingRadius;

    //���Ű뾶
    double                  m_DefenceRangeRadius;

    //���ϰ뾶
    double                  m_InterRuptionRangeRadius;

    //��Ұ�뾶
    double                  m_SightRangeRadius;

private:

    //��ָ��
    SoccerPitch*            m_SoccerPitch;

    //��ǰ�Ļغ���
    int                     m_Round;

    IPlayer*                m_Player;

    ParsePlayerEntity*      m_ParsePlayer;
};

#endif //__SOCCERPLAYER_H__
