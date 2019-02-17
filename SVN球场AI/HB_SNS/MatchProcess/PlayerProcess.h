/********************************************************************************
 * 文件名：PlayerProcess.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PLAYERPROCESS_H__
#define __PLAYERPROCESS_H__

#include "../Interface/IProcess.h"
#include "MoveableProcess.h"

/// 表示了球员的每回合状态
class PlayerProcess : public MoveableProcess, public IPlayerProcess
{
public:

    PlayerProcess(int id = 0);

public:

    int     Angle() { return m_Angle; }
    void    SetAngle(int angle) { m_Angle = angle; }

    int     GetState() { return m_State; }
    void    SetState(int state) { m_State = state; }

    bool    NameVisible() { return m_NameVisible; }
    void    SetNameVisible(bool visible) { m_NameVisible = visible; }

    bool    HasBall() { return m_HasBall; }
    void    SetHasBall(bool flag) { m_HasBall = flag; }

    int     FoulLevel() { return m_FoulLevel; }
    void    SetFoulLevel(int level) { m_FoulLevel = level; }

    int     Model() { return m_Model; }
    void    SetModel(int vl) { m_Model = vl; }
    //////////////////////////////////////////////////////////////////////////
    string      GetCoordinate() { return MoveableProcess::GetCoordinate(); }
    void        SetCoordinate(string coord) { MoveableProcess::SetCoordinate(coord); }

    Coordinate  GetCoordinate2() { return MoveableProcess::GetCoordinate2(); }
    void        SetCoordinate2(Coordinate coord) { MoveableProcess::SetCoordinate2(coord); }

    int         Acceleration() { return MoveableProcess::Acceleration(); }
    void        SetAcceleration(int acc) { MoveableProcess::SetAcceleration(acc); }

    //////////////////////////////////////////////////////////////////////////
    string ToString();

    /// Get the index of the angle.
    static int GetPlayerAngleIndex(int angle);

    /// Get the client state index.
    static int GetClientStateIndex(int state);

    void        Output(CUtlBuffer& writer);
    void        Output(ofstream& writer);
    void        Read(ifstream& reader);
    void        Read(CUtlBuffer& reader);

protected:

    void        Output(CUtlBuffer& writer, int id);
    void        Output(ofstream& writer, int id);

    void        ReadInvoke(ifstream& reader);
    void        ReadInvoke(CUtlBuffer& reader);

private:

    //初始化变量
    void        InitVariables();

protected:
    
    const int m_Id;

    /// 表示了球员的朝向（朝右为0°，朝左为180°）
    int m_Angle;

    /// 球员的状态编号
    /// 0 - ActionState
    /// 1 - ChaceState
    /// 2 - DefenceState
    /// 3 - DiveBallState
    /// 4 - DribbleState
    /// 5 - HoldBallState
    /// 6 - IdleState
    /// 7 - OffBallState
    /// 8 - PassState
    /// 9 - PositionalState
    /// 10- ShootState
    /// 11- StopballState
    /// 12- InterruptionState
    /// 13- SlideTackleState
    /// 14- DefaultDribbleState
    /// 15- LongPassState
    /// 16- ShortPassState
    /// 17- DefaultShootState
    /// 18- VolleyShootState
    int m_State;

    /// <summary>
    /// Represents whether to show the player's name in the flash.
    /// 客户端是否显示球员的名称
    /// </summary>
    bool m_NameVisible;

    /// <summary>
    /// Represents whether current player is the ball handler.
    /// 表示了当前球员是否是持球人
    /// </summary>
    bool m_HasBall;

    /// 表示了球员的犯规等级
    /// 1 - 黄牌
    /// 2 - 红牌（离场）
    /// 3 - 受伤（离场）
    int m_FoulLevel;

    /// 表示了球员的模型.
    int m_Model;
};

#endif //__PLAYERPROCESS_H__
