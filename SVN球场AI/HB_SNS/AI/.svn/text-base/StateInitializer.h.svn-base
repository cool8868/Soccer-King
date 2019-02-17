/********************************************************************************
 * 文件名：ActionState.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "StateSelecter.h"

class StateInitializer 
{
public:

    static void Initialize() 
    {
        if (m_HasInitialized)
        {
            return;
        }

        StateSelecter::Instance()->Initialize();

        foreach (MapString_IState::value_type& state, StateSelecter::Instance()->States()) 
        {
            state.second->Initialize();
        }

        LogHelper::Insert("State initialized.", LogType.Info);

        m_HasInitialized = true;
    }

    static bool m_HasInitialized;
};

bool StateInitializer::m_HasInitialized = false;
