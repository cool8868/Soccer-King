/********************************************************************************
 * 文件名：IPlayer.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IPLAYER_H__
#define __IPLAYER_H__

#include "../IMoveable.h"
#include "IDecide.h"
#include "INotifyRedecide.h"
#include "IShortPass.h"
#include "IStopball.h"
#include "IRotate.h"
#include "IDribble.h"
#include "ILongPass.h"
#include "IShoot.h"
#include "ISight.h"
#include "IDisturb.h"
#include "IFoul.h"
#include "IDefence.h"
#include "IDiveBall.h"
#include "IAddDebuff.h"
#include "IModel.h"

#include "../../common/Enum/Zone.h"
#include "../../common/Enum/Side.h"

class IManager;
class IMatch;
class IPlayerStatus;
class IPlayerAttribute;

class IPlayer:
    public IMoveable,   public IDecide,     public IDecideEnd,  public INotifyRedecide, public IShortPass,  
    public IStopball,   public IRotate,     public IDribble,    public ILongPass,       public IShoot,      
    public ISight,      public IDisturb,    public IFoul,       public IAddDebuff,      public IAddBuff,
    public IModel,      public IDefence,    public IDiveBall
{
public:

    IPlayer() {}
    virtual ~IPlayer() {}

public:

    /// 球员初始化各项数据
    virtual void                            Init() = 0;

    /// 在新的一回合到来时执行初始化
    virtual void                            RoundInit() = 0;

    /// 在新的一分钟到来时执行初始化
    virtual void                            MinuteInit() = 0;

    /// 下半场开始时调用
    virtual void                            SecondHalfStart() = 0;

    /// 获取目标所在的区域
    virtual Zone                            GetTargetZone(Coordinate target) = 0;

    /// 设置球员的目标
    virtual void                            SetTarget(Coordinate target) = 0;

    /// 设置球员的目标
    virtual void                            SetTarget(double x, double y) = 0;

    /// 将球员的目标设置为球
    virtual void                            SetTargetBall(bool isCurrent) = 0;

    /// 设置一个坐标给球员。
    /// 如果球员为主队，那么不变；
    /// 如果球员为客队，将自动进行中心对称再赋值.
    virtual void                            SetHomeSideCoordinate(Coordinate target) = 0;

    /// 判断一个坐标是否在球员之内
    virtual bool                            IsCoordinateInPlayer(Coordinate coordinate) = 0;

    /// 将球员的速度降低为当前速度的一半    
    virtual void                            DecreaseSpeed() = 0;

public:

    /// 表示了球员的ID
    virtual unsigned int                    Id() = 0;
    
    /// 表示了客户端唯一标示符
    virtual int                             ClientId() = 0;

    /// 表示了球员的天生特性
    virtual IPlayerAttribute*               BuildinAttribute() = 0;
    
    /// 表示了球员的原属性
    virtual MapInt_Double&                  RawProperty() = 0;

    /// 表示了球员的当前属性
    virtual MapInt_Double&                  CurrProperty() = 0;

    /// 表示了对经理对象的引用
    virtual IManager*                       GetManager() = 0;

    /// 表示了对比赛对象的引用
    virtual IMatch*                         GetMatch() = 0;

    /// 表示了球员的当前是主队还是客队
    virtual Side                            GetSide() = 0;

    /// 表示了球员的当前状态
    virtual IPlayerStatus*                  Status() = 0;

    /// 表示了球员的传球目标列表
    virtual vector<IPlayer*>&               PassTargets()  = 0;

    /// 表示了球员的关注列表
    virtual vector<IPlayer*>&               CarePlayers()  = 0;

    /// 表示了球员的各个技能
    virtual MapIState_VectorIActionSkill&   Skills() = 0;

    /// 表示了球员Action技能的引用集合
    virtual vector<IActionSkill*>&          SkillReferences() = 0;

    /// 表示了球员的技能释放结果
    virtual MapInt_ISkillResult&            SkillResults() = 0;

};

#endif  //__IPLAYER_H__
