/********************************************************************************
 * 文件名：PlayerPropertyInitializer.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __PLAYERPROPERTYINITIALIZER_H__
#define __PLAYERPROPERTYINITIALIZER_H__

#include "../../common/common.h"

class PlayerPropertyInitializer
{
public:

    PlayerPropertyInitializer();

public:

    /// 初始化所有的球员属性
    static MapInt_Double        Initialize();

    /// 表示了所有的属性编号集合
    static vector<int>&         PropertyIds() { return m_PropertyIds; }

    /// 表示了所有的球员属性编号集合        
    static vector<int>&         PlayerPropertyIds() { return m_PlayerPropertyIds; }

private:

    static vector<int>  m_PropertyIds;
    static vector<int>  m_PlayerPropertyIds;
};

#endif //__PLAYERPROPERTYINITIALIZER_H__
