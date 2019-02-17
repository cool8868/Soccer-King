/********************************************************************************
 * 文件名：StateSelecter.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "../common/common.h"
#include "../Interface/IState.h"

typedef map<string, IState*> MapString_IState;

class StateSelecter 
{
public:

    static StateSelecter* Instance();

public:

    void                Initialize();

    IState*             GetStateByString(string state);
    
    MapString_IState&   States() { return m_States; }

private:

    MapString_IState m_States;
};
