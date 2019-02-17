/********************************************************************************
 * 文件名：common.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __COMMON_H__
#define __COMMON_H__

#include <math.h>
#include <string>
#include <vector>
#include <list>
#include <algorithm>
#include <fstream>

#include <boost/lexical_cast.hpp>

#include <boost/format.hpp>

#include <boost/tokenizer.hpp>
#include <boost/foreach.hpp>
#include <boost/assign.hpp>

#include <boost/pool/detail/singleton.hpp>
#include <boost/algorithm/string/trim.hpp>
#include <boost/algorithm/string/erase.hpp>

#include "Pugixml/pugixml.hpp"
#include "Macros.h"

using namespace boost;
using namespace boost::assign;
using namespace pugi;

using boost::details::pool::singleton_default;

#endif//__COMMON_H__