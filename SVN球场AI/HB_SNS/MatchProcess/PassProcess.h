/********************************************************************************
 * 文件名：PassProcess.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PASSPROCESS_H__
#define __PASSPROCESS_H__

#include "PlayerProcess.h"

/// 表示了传球回合的结果数据
class PassProcess : public PlayerProcess, public IPassProcess
{
public:

    PassProcess();

public:

    bool        IsPassFail() { return m_IsPassFail; }
    void        SetIsPassFail(bool flag) { m_IsPassFail = flag; }

    //////////////////////////////////////////////////////////////////////////
    int         Angle() { return PlayerProcess::Angle(); }
    void        SetAngle(int angle) { PlayerProcess::SetAngle(angle); }

    int         GetState() { return PlayerProcess::GetState(); }
    void        SetState(int state) { PlayerProcess::SetState(state); }

    bool        NameVisible() { return PlayerProcess::NameVisible(); }
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

    /// 是否是传球失误
    bool m_IsPassFail;
};

#endif //__PASSPROCESS_H__
