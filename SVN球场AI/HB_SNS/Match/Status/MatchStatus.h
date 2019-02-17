/********************************************************************************
 * �ļ�����MatchStatus.h
 * �����ˣ��α�
 * ����ʱ�䣺2011-6-10
 * �汾��1.0
 * ���ļ��汾�ţ�1.0
 * �����£�
 * ����˵����
 * ��ʷ�޸ļ�¼��
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MATCHSTATUS_H__
#define __MATCHSTATUS_H__

#include "../../common/Enum/OpenballSide.h"
#include "../../Interface/Player/IPlayer.h"

class MatchStatus
{
public:

    MatchStatus();

public:

    int             Round() { return m_Round; }
    void            SetRound(int round) { m_Round = round; }
    void            IncRound() { m_Round++; }

    int             Time() { return m_Time; }
    void            SetTime(int time) { m_Time = time; }

    bool            IsFirstHalf() { return m_IsFirstHalf; }
    void            SetIsFirstHalf(bool flag) { m_IsFirstHalf = flag; }

    bool            GetInterruption() { return m_Interruption; }
    void            SetInterruption(bool flag) { m_Interruption = flag; }

    bool            Foul() { return m_Foul; }
    void            SetFoul(bool flag) { m_Foul = flag; }

    bool            Break() { return m_Break; }
    void            SetBreak(bool flag) { m_Break = flag; }

    OpenballSide    GetOpenballSide() { return m_OpenballSide; }
    void            SetOpenballSide(OpenballSide side) { m_OpenballSide = side; }

    OpenballSide    HalfOpenballSide() { return m_HalfOpenballSide; }
    void            SetHalfOpenballSide(OpenballSide side) { m_HalfOpenballSide = side; }

    IPlayer*        BallHandler() { return m_BallHandler; }
    void            SetBallHandler(IPlayer* player) { m_BallHandler = player; }

    bool            IsNoBallHandler() { return m_IsNoBallHandler; }
    void            SetIsNoBallHandler(bool flag) { m_IsNoBallHandler = flag; }

    bool            IsGoal() { return m_IsGoal; }
    void            SetIsGoal(bool flag) { m_IsGoal = flag; }

    bool            CancelUpdateStatus() { return m_CancelUpdateStatus; }
    void            SetCancelUpdateStatus(bool flag) { m_CancelUpdateStatus = flag; }

    bool            NeedSave() { return m_NeedSave; }
    void            SetNeedSave(bool flag) { m_NeedSave = flag; }

    string          HomeOpener() { return m_HomeOpener; }
    void            SetHomeOpener(string vl) { m_HomeOpener = vl; }

    string          AwayOpener() { return m_AwayOpener; }
    void            SetAwayOpener(string vl) { m_AwayOpener = vl; }

private:

    //��ʼ������
    void            InitVariables();

protected:

    /// ��ʾ�˵�ǰ�Ļغ���
    int m_Round;

    /// ��ʾ�˵�ǰ����Ϸʱ��
    int m_Time;

    /// ��ʾ���Ƿ����ϰ볡
    bool m_IsFirstHalf;

    /// ��ʾ���Ƿ���Ҫ��ʾ��������
    bool m_Interruption;

    /// ��ʾ�˸ûغ��Ƿ񷸹�
    bool m_Foul;

    /// ��ʾ�˵�ǰ�غ��Ƿ����жϻغϣ�����ʾ�г�������
    bool m_Break;

    /// ��ʾ�˵�ǰ�Ŀ���
    OpenballSide m_OpenballSide;

    /// ��ʾ���ϰ볡�Ŀ���
    OpenballSide m_HalfOpenballSide;

    /// ��ʾ�˵�ǰ�ĳ�����
    IPlayer* m_BallHandler;

    /// ��ʾ�˵�ǰ�غ��Ƿ���������
    bool m_IsNoBallHandler;

    /// ��ʾ�˵�ǰ�غ��Ƿ����
    bool m_IsGoal;

    /// ��ʾ�˵�ǰ�غ��Ƿ���Ҫȡ������״̬����ֹ������»غ�ʹ�õĿ���״̬)
    bool m_CancelUpdateStatus;

    /// ��ʾ�����ӵ�Opener����
    string m_HomeOpener;

    /// ��ʾ�˿Ͷӵ�Opener����
    string m_AwayOpener;

    /// ��ʾ�˸ûغ��Ƿ���Ҫ��������غ�����
    bool m_NeedSave;
};


#endif  //__MATCHSTATUS_H__
