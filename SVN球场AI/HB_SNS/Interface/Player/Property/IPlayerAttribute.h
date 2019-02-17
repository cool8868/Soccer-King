/********************************************************************************
 * 文件名：IPlayerAttribute.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IPLAYERATTRIBUTE_H__
#define __IPLAYERATTRIBUTE_H__

#include "../../../common/Enum/Position.h"
#include "../../../common/Structs/Region.h"

/// 表示了球员的天生属性接口
class IPlayerAttribute
{
public:

    virtual ~IPlayerAttribute() {}

public:
    virtual unsigned int    PID() = 0;
    virtual void            SetPID(unsigned int pid) = 0;

    virtual int             CardLevel() = 0;
    virtual void            SetCardLevel(int cardLevel) = 0;

    virtual int             Strengthen() = 0;
    virtual void            SetStrengthen(int strength) = 0;

    virtual Position        GetPosition() = 0;
    virtual void            SetPosition(Position pos) = 0;

    virtual int             HeadStyle() = 0;
    virtual void            SetHeadStyle(int style) = 0;

    virtual int             BodyStyle() = 0;
    virtual void            SetBodyStyle(int style) = 0;

    virtual string          FirstName() = 0;
    virtual void            SetFirstName(string firstName) = 0;

    virtual string          FamilyName() = 0;
    virtual void            SetFamilyName(string familyName) = 0;

    virtual int             Width() = 0;
    virtual void            SetWidth(int width) = 0;

    virtual int             DefenceRange() = 0;
    virtual void            SetDefenceRange(int range) = 0;

    virtual int             SightRange() = 0;
    virtual void            SetSightRange(int range) = 0;

    virtual Region          MoveRegion() = 0;
    virtual void            SetMoveRegion(Region region) = 0;
};

#endif //__IPLAYERATTRIBUTE_H__
