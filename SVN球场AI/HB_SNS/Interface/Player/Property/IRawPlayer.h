/********************************************************************************
 * 文件名：IRawPlayer.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IRAWPLAYER_H__
#define __IRAWPLAYER_H__

#include "../../../common/common.h"
#include "../../../common/Structs/Coordinate.h"

#include "IPlayerAttribute.h"

class IRawPlayer 
{
public:

    virtual ~IRawPlayer() {}

public:

    virtual unsigned int        Id() = 0;
    virtual void                SetId(unsigned int id) = 0;

    virtual Coordinate          GetDefault() = 0;
    virtual void                SetDefault(Coordinate corrd) = 0;

    virtual IPlayerAttribute*   BuildinAttribute() = 0;
    virtual void                SetBuildinAttribute(IPlayerAttribute* pAttr) = 0;

    virtual MapInt_Double&      RawProperty() = 0;
    virtual void                SetRawProperty(MapInt_Double property) = 0;

    virtual vector<string>&     Skills() = 0;
};

#endif //__IRAWPLAYER_H__
