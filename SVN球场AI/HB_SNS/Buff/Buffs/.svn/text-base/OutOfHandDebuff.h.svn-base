/********************************************************************************
 * 文件名：OutOfHandDebuff.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __OUTOFHANDDEBUFF_H__
#define __OUTOFHANDDEBUFF_H__

class OutOfHandDebuff : public AbnormalDebuff 
{        
public:

    /// Initialize a new abnormal debuff.
    OutOfHandDebuff(int triggerId, int time, int level)
        : AbnormalDebuff(triggerId, time, DebuffType_OutOfHandDebuff) 
    {
            m_Level = level;
    }

    /// Represents the debuff's level.
    int     Level() { return m_Level; }

protected:

    int m_Level;
};

#endif //__OUTOFHANDDEBUFF_H__
