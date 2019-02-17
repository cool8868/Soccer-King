/********************************************************************************
 * 文件名：GoalkeeperProcess.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __GOALKEEPERPROCESS_H__
#define __GOALKEEPERPROCESS_H__

#include "PlayerProcess.h"

/// 表示了守门员的回合数据
class GoalkeeperProcess : public PlayerProcess, public IGoalkeeperProcess
{
public:

    GoalkeeperProcess();

public:

    bool        IsBackward() { return m_IsBackward; }
    void        SetIsBackward(bool flag) { m_IsBackward = flag; }

    bool        IsStandUp() { return m_IsStandUp; }
    void        SetIsStandUp(bool flag) { m_IsStandUp = flag; }

    int         DiveDirection() { return m_DiveDirection; }
    void        SetDiveDirection(int dir) { m_DiveDirection = dir; }

    //////////////////////////////////////////////////////////////////////////
    int         Angle() { return PlayerProcess::Angle(); }
    void        SetAngle(int angle) { PlayerProcess::SetAngle(angle); }

    int         GetState() { return PlayerProcess::GetState(); }
    void        SetState(int state) { PlayerProcess::SetState(state); }

    bool        NameVisible() {  return PlayerProcess::NameVisible(); }
    void        SetNameVisible(bool visible) { PlayerProcess::SetNameVisible(visible); }

    bool        HasBall() { return PlayerProcess::HasBall(); }
    void        SetHasBall(bool flag) { PlayerProcess::SetHasBall(flag); }

    int         FoulLevel() { return PlayerProcess::FoulLevel(); }
    void        SetFoulLevel(int level) { PlayerProcess::SetFoulLevel(level); }

    //////////////////////////////////////////////////////////////////////////
    string      GetCoordinate() { return PlayerProcess::GetCoordinate(); }
    void        SetCoordinate(string coord) { PlayerProcess::SetCoordinate(coord); }

    Coordinate  GetCoordinate2() { return PlayerProcess::GetCoordinate2(); }
    void        SetCoordinate2(Coordinate coord) { PlayerProcess::SetCoordinate2(coord); }

    int         Acceleration() { return PlayerProcess::Acceleration(); }
    void        SetAcceleration(int acc) { PlayerProcess::SetAcceleration(acc); }

    void        Output(CUtlBuffer& writer);
    void        Output(ofstream& writer);

    void        Read(ifstream& reader);
    void        Read(CUtlBuffer& reader);

private:

    //初始化变量
    void        InitVariables();

protected:

    /// 表示了守门员是否是后退行动
    bool m_IsBackward;

    /// 表示了守门员是否是站立状态
    bool m_IsStandUp;

    /// 表示了扑球的方向
    /// 0 - 左
    /// 1 - 中
    /// 2 - 右
    int m_DiveDirection;
};

#endif //__GOALKEEPERPROCESS_H__
