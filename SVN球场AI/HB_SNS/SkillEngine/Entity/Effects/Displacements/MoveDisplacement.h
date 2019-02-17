/********************************************************************************
 * 文件名：MoveDisplacement.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MOVEDISPLACEMENT_H__
#define __MOVEDISPLACEMENT_H__

/// Represents the type-0 displacement effector.
class MoveDisplacement : public IDisplacement 
{
public:
    
    /// Effect a displacement record.
    void Effect(Displacement* displacement, IPlayer* player) 
    {
        double x = cos(displacement->Angle() * MATH::PI / 180) * displacement->Distance() + player->Current().X;
        double y = sin(displacement->Angle() * MATH::PI / 180) * displacement->Distance() + player->Current().Y;

        vector<IPlayer*> targets = singleton_default<TargetSelector>::Instance().GetTarget(displacement->Target(), displacement->TargetPosition(), player);

        foreach (IPlayer* target, targets) 
        {
            if (target->Status()->Hasball()) 
            {
                target->GetMatch()->GetFootball()->MoveTo(Coordinate(x, y));
            }

            target->MoveTo(Coordinate(x, y));
        }            
    }
};

#endif //__MOVEDISPLACEMENT_H__
