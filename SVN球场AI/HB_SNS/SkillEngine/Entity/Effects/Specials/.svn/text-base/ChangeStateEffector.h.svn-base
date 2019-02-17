/********************************************************************************
 * 文件名：ChangeStateEffector.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __CHANGESTATEEFFECTOR_H__
#define __CHANGESTATEEFFECTOR_H__

class ChangeStateEffector : public ISpecialEffector
{
public:

    Effect(IPlayer* player, Special* skill)
    {
        player->Status()->SetState(StateSelecter::Instance()->States[skill->Value()]);
    }

    string ToString()
    {
        return "ChangeStateEffector";
    }
};

#endif //__CHANGESTATEEFFECTOR_H__
