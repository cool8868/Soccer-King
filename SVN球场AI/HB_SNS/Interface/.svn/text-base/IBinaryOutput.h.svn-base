/********************************************************************************
 * 文件名：IBinaryOutput.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IBINARYOUTPUT_H__
#define __IBINARYOUTPUT_H__

#include "../common/common.h"
#include "../common/Package/packetbase.h"

class IBinaryOutput 
{
public:

    virtual ~IBinaryOutput() {}

public:

    /// 将当前对象的内容填充入二进制流中
    virtual void Output(CUtlBuffer& writer) = 0;
    virtual void Output(ofstream& writer) = 0;
};

#endif //__IBINARYOUTPUT_H__
