/********************************************************************************
 * 文件名：MatchProcess.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MATCHPROCESS_H__
#define __MATCHPROCESS_H__

#include "Process.h"

/// 表示了比赛的回合信息
class MatchProcess : public Process, public IMatchProcess
{
public:

    MatchProcess();

public:

    string  Score() { return m_Score; }
    void    SetScore(string score) { m_Score = score; }

    bool    IsGoal() { return m_Goal; }
    void    SetIsGoal(bool isGoal) { m_Goal = isGoal; }

    bool    GetInterruption() { return m_Interruption; }
    void    SetInterruption(bool interruption) { m_Interruption = interruption; }

    //////////////////////////////////////////////////////////////////////////
    int     Round() { return Process::Round(); }
    void    SetRound(int round) { Process::SetRound(round); }
    void    IncRound() { Process::IncRound(); }

    //////////////////////////////////////////////////////////////////////////
    int     HomeScore() { return m_HomeScore; }
    void    SetHomeScore(int score) { m_HomeScore = score; }

    int     AwayScore() { return m_AwayScore; }
    void    SetAwayScore(int score) { m_AwayScore = score; }

    bool    Foul() { return m_Foul; }
    void    SetFoul(bool flag) { m_Foul = flag; }

    bool    Break() { return m_Break; }
    void    SetBreak(bool flag) { m_Break = flag; }

    void    Output(CUtlBuffer& writer);
    void    Output(ofstream& writer);

    void    Read(ifstream& reader);
    void    Read(CUtlBuffer& reader);

private:

    //初始化变量
    void    InitVariables();

protected:

    /// 表示了该回合的比分
    string m_Score;

    /// 表示了该回合是否进球
    bool m_Goal;

    /// 表示了该回合是否中断比赛(客户端使用这个标记重置位置)
    bool m_Interruption;

    //////////////////////////////////////////////////////////////////////////
    const int m_Id;

    /// 表示了主队的比分
    int m_HomeScore;

    /// 表示了客队的比分
    int m_AwayScore;

    /// 表示了当前回合是否犯规
    bool m_Foul;

    /// 表示了当前回合是否是中断回合（不显示切场动画）
    bool m_Break;
};

#endif //__MATCHPROCESS_H__
