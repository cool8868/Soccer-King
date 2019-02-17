/********************************************************************************
 * 文件名：AbnormalDebuff.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "AbnormalDebuff.h"

AbnormalDebuff::AbnormalDebuff(int triggerId, int time, DebuffType type)
    : AbsBuff(triggerId, time, BuffType_Disable)
{
    m_Isbuff        = false;    // all the abnormal debuffs are debuff type.      
    m_DebuffType    = type;
}

//////////////////////////////////////////////////////////////////////////
DisableDebuff::DisableDebuff(int triggerId, int time, DebuffType type)
    : AbnormalDebuff(triggerId, time, type)
{

}
