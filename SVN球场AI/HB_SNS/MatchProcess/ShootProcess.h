/********************************************************************************
 * 文件名：ShootProcess.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SHOOTPROCESS_H__
#define __SHOOTPROCESS_H__

#include "PlayerProcess.h"
#include "../common/Structs/ShootTarget.h"

/// 表示了射门回合的数据
class ShootProcess : public PlayerProcess, public IShootProcess
{
public:

    ShootProcess();

public:

    string      GetShootTarget() { return m_ShootTarget; }
    void        SetShootTarget(string target) { m_ShootTarget = target; }

    int         ShootTargetIndex() { return m_ShootTargetIndex; }
    void        SetShootTargetIndex(int index) { m_ShootTargetIndex = index; }

    int         GetShootSpeed() { return m_ShootSpeed; }
    void        SetShootSpeed(int speed) { m_ShootSpeed = speed; }

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

    //////////////////////////////////////////////////////////////////////////
    ShootTarget ShootTarget2() { return m_ShootTarget2; }
    void        SetShootTarget2(ShootTarget shootTarget) { m_ShootTarget2 = shootTarget; }

    void        Output(CUtlBuffer& writer);
    void        Output(ofstream& writer);

    void        Read(ifstream& reader);
    void        Read(CUtlBuffer& reader);

private:

    //初始化变量
    void        InitVariables();

protected:

    /// 表示了射门的目标
    string m_ShootTarget;

    /// 表示了射门目标的index.
    int m_ShootTargetIndex;

    /// 表示了射门时的球速
    int m_ShootSpeed;

    //////////////////////////////////////////////////////////////////////////
    ShootTarget m_ShootTarget2;
};

#endif //__SHOOTPROCESS_H__
