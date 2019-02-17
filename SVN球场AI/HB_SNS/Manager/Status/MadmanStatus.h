/********************************************************************************
 * 文件名：MadmanStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MADMANSTATUS_H__
#define __MADMANSTATUS_H__

/// 狂人技能的相关状态，当对方进球后，对方的属性下降，本方的属性提升
class MadmanStatus : public IMadmanStatus 
{
public:

    MadmanStatus()
    {
        InitVariables();
    }

    int     SelfUpgradeRate() { return m_SelfUpgradeRate; }
    void    SetSelfUpgradeRate(int vl) { m_SelfUpgradeRate = vl; }

    int     OppDecreaseRate() { return m_OppDecreaseRate; }
    void    SetOppDecreaseRate(int vl) { m_OppDecreaseRate = vl; }

protected:

    /// 表示了本方的提升幅度
    int     m_SelfUpgradeRate;

    /// 表示了对方的降低幅度
    int     m_OppDecreaseRate;

private:

    void InitVariables()
    {
        m_SelfUpgradeRate = 0.0;
        m_OppDecreaseRate = 0.0;
    }
};

#endif //__MADMANSTATUS_H__
