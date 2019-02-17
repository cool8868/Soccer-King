/********************************************************************************
 * 文件名：FormationFacade.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __FORMATIONFACADE_H__
#define __FORMATIONFACADE_H__

#include "../../common/common.h"
#include "../../common/Pugixml/pugixml.hpp"

#include "FormationEntity.h"

class xmlFormation
{
public:

    xmlFormation();

    xml_document& GetDoc() { return m_doc; }

private:

    xml_document m_doc;
};

using boost::details::pool::singleton_default;
typedef singleton_default<xmlFormation> singleton_Formation;

class FormationFacade 
{
public:

    static vector<FormationEntity> GetFormation(int formationId);
};


#endif //__FORMATIONFACADE_H__
