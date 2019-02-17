/********************************************************************************
 * 文件名：MatchStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
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

    //初始化变量
    void            InitVariables();

protected:

    /// 表示了当前的回合数
    int m_Round;

    /// 表示了当前的游戏时间
    int m_Time;

    /// 表示了是否是上半场
    bool m_IsFirstHalf;

    /// 表示了是否需要显示过场动画
    bool m_Interruption;

    /// 表示了该回合是否犯规
    bool m_Foul;

    /// 表示了当前回合是否是中断回合（不显示切场动画）
    bool m_Break;

    /// 表示了当前的开球方
    OpenballSide m_OpenballSide;

    /// 表示了上半场的开球方
    OpenballSide m_HalfOpenballSide;

    /// 表示了当前的持球人
    IPlayer* m_BallHandler;

    /// 表示了当前回合是否有无球人
    bool m_IsNoBallHandler;

    /// 表示了当前回合是否进球
    bool m_IsGoal;

    /// 表示了当前回合是否需要取消更新状态（防止清除了下回合使用的开球状态)
    bool m_CancelUpdateStatus;

    /// 表示了主队的Opener技能
    string m_HomeOpener;

    /// 表示了客队的Opener技能
    string m_AwayOpener;

    /// 表示了该回合是否需要保存比赛回合数据
    bool m_NeedSave;
};


#endif  //__MATCHSTATUS_H__
