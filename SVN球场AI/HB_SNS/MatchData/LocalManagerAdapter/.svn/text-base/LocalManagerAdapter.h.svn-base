/********************************************************************************
 * 文件名：LocalManagerAdapter.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __LOCALMANAGERADAPTER_H__
#define __LOCALMANAGERADAPTER_H__

#include "../Formation/FormationFacade.h"
#include "../MoveRegion/MoveRegionFacade.h"
#include "../../common/String/String.h"
#include "../../common/Enum/PlayerProperty.h"
#include "../../common/Enum/Side.h"

#include "../ManagerData/ManagerData.h"

class LocalManagerAdapter
{
public:

    LocalManagerAdapter(): m_PlayerIdCounter(-1){}

    virtual ~LocalManagerAdapter();
    
public:
    
    static LocalManagerAdapter* Instance();

    IManagerData* GetManagerById(int id, int type, Side side);

private:

    int m_PlayerIdCounter;

    //////////////////////////////////////////////////////////////////////////
    void            AddSkill(vector<string>& skills, IRawPlayer* player);
    vector<string>  InitPassiveSkills(string skillconfig);
    vector<string>  InitOpenerSkills(string skillconfig);

};

#endif //__LOCALMANAGERADAPTER_H__
