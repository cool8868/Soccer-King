/********************************************************************************
 * 文件名：SkillDisplacement.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
class SkillDisplacement : public IDisplacement 
{
public:

    /// Effect a displacement record.
    /// <param name="displacement"><see cref="Displacement"/></param>
    /// <param name="player"><see cref="IPlayer"/></param>
    void Effect(Displacement* displacement, IPlayer* player) 
    {
        vector<IPlayer*> targets = singleton_default<TargetSelector>::instance().GetTarget(displacement->Target(), displacement->TargetPosition(), player);

        if (targets.size() == 0) 
        {
            throw new SkillErrorException("Effect a skill displacement throw exceptions. Player finds none target.");
        }

        if (targets.size() > 1) 
        {
            throw new SkillErrorException("Effect a skill displacement throw exceptions. Player finds more than one targets.");
        }

        IPlayer* target = targets[0];
        double leftDistance = player->Current().SimpleDistance(Coordinate(target->Current().X - displacement->Distance(), target->Current().Y));
        double rightDistance = player->Current().SimpleDistance(Coordinate(target->Current().X + displacement->Distance(), target->Current().Y));

        if (leftDistance < rightDistance) 
        {
            player->MoveTo(Coordinate(target->Current().X - displacement->Distance(), target->Current().Y));
        }
        else 
        {
            player->MoveTo(Coordinate(target->Current().X + displacement->Distance(), target->Current().Y));
        }
    }
};
