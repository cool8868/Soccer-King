/********************************************************************************
 * 文件名：IBinaryRead.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IBINARYREAD_H__
#define __IBINARYREAD_H__

#include "../common/common.h"
#include "../common/Package/packetbase.h"

class IBinaryReadIn
{
    /// 将当前对象的内容填充入二进制流中
    virtual void Read(ifstream& reader) = 0;
};

#endif //__IBINARYREAD_H__
