/********************************************************************************
 * 文件名：FormationEntity.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __FORMATIONENTITY_H__
#define __FORMATIONENTITY_H__

#include "../../common/Structs/Coordinate.h"
#include "../../common/Enum/Position.h"
#include "../../common/common.h"

class FormationEntity 
{
public:
    FormationEntity(string str_pos, string str_coord)
    {
        m_Position  = (Position)lexical_cast<int>(str_pos);
        m_Default   = str_coord;
    }

public:

    Position    GetPosition() { return m_Position; }
    Coordinate  GetDefault() { return m_Default; }

private:

    Position    m_Position;
    Coordinate  m_Default;
};

#endif //__FORMATIONENTITY_H__

