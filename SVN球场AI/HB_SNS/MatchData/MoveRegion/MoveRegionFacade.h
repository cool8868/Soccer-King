/********************************************************************************
 * 文件名：MoveRegionFacade.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MOVEREGIONFACADE_H__
#define __MOVEREGIONFACADE_H__

#include "../../common/common.h"
#include "../../common/Pugixml/pugixml.hpp"
#include "../../common/Structs/Region.h"

class xmlMoveRegion
{
public:
    xmlMoveRegion();

    xml_document& GetDoc() { return m_doc; }

private:
    xml_document m_doc;
};

using boost::details::pool::singleton_default;
typedef singleton_default< xmlMoveRegion > singleton_xmlMoveRegion;

class MoveRegionFacade 
{
public:

  static Region GetMoveRegion(Coordinate position);
};

#endif //__MOVEREGIONFACADE_H__
