/********************************************************************************
 * 文件名：Football.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __FOOTBALL_H__
#define __FOOTBALL_H__

#include "../Interface/IFootball.h"
#include "../Interface/IMatch.h"
#include "../MatchProcess/Process.h"

#include "../common/Structs/Coordinate.h"

class Football : public IFootball
{
public:

    Football();

    ~Football();

public:

    vector<Process*>&   Processes() { return m_Processes; }

    //////////////////////////////////////////////////////////////////////////
    bool                IsInAir() { return m_IsInAir; }
    void                SetIsInAir(bool flag) { m_IsInAir = flag; }

    bool                IsPassDestination() { return m_IsPassDestination; }
    void                SetPassDestination(bool flag) { m_IsPassDestination = flag; }

    bool                Visible();
    void                SetVisible(bool visible) { m_Visible = visible; }

    IPlayer*            LastTouchMan() { return m_LastTouchMan; }
    void                SetLastTouchMan(IPlayer* player) { m_LastTouchMan = player; }

    /// 把球踢往某一点
    void                Kick(Coordinate coordinate, double speed, IPlayer* lastMan);

    /// 将球踢往某一点（球是在空中的）
    void                Kick(Coordinate coordinate, double speed, int angle, IPlayer* lastMan);

    /// 将球隐藏一小段时间
    void                Hide(int round);

    /// 强迫下一次传球为高球
    void                ForceInAir();

    /// 在新一回合到来时初始化
    void                RoundInit();

    //////////////////////////////////////////////////////////////////////////
    Coordinate          Current() { return m_Current; }

    Coordinate          Destination() { return m_Destination; }

    double              Speed() { return m_Speed; }

    double              Acceleration() { return m_Acceleration; }

    /// 物体根据自身的趋势移动
    void                Move();

    /// 将物体强行移动至某处
    void                MoveTo(Coordinate target);

    /// 将物体强行移动至某处
    void                MoveTo(double x, double y);

    /// 保存物体的当前状态
    void                Save();

    /// 保存物体的当前状态
    void                Save(int round);

    /// 重置物体的当前状态
    void                Reset();

protected:

    /// 表示了球是否在空中
    bool m_IsInAir;

    /// 是否通过了目标点（惯性移动）
    bool m_IsPassDestination;

    /// 表示了球的可见性
    bool m_Visible;

    /// 表示了最后接触球的球员
    IPlayer* m_LastTouchMan;

    //////////////////////////////////////////////////////////////////////////
    bool m_ForceInAir;

    bool m_Recalculate;

    int  m_Angle;

    int m_HideTime;

    //////////////////////////////////////////////////////////////////////////
    /// 表示了物体的当前坐标
    Coordinate m_Current;

    /// 表示了物体的目标坐标
    Coordinate m_Destination;

    /// 表示了物体的速度
    double m_Speed;

    /// 表示了物体的加速度
    double m_Acceleration;

    //////////////////////////////////////////////////////////////////////////
    Coordinate m_Default;

    vector<Process*> m_Processes;

    //////////////////////////////////////////////////////////////////////////
    double   m_cosA;
    double   m_sinA;

private:

    /// 内部保存方法
    void    InternalSave(int round);

    /// 判断足球是否出了边界
    void    ValidateOutBorder();

    /// 守门员开门球
    void    GKOpenball(Side side);

    //初始化变量
    void   InitVariables(); 
};

#endif //__FOOTBALL_H__
