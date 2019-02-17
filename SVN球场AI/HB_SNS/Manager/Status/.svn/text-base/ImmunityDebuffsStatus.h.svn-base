/********************************************************************************
 * 文件名：ImmunityDebuffsStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IMMUNITYDEBUFFSSTATUS_H__
#define __IMMUNITYDEBUFFSSTATUS_H__

/// Represents a player's status that defines all the debuff and its immunities property.    
class ImmunityDebuffsStatus : public IImmunityDebuffsStatus 
{
public:

    ImmunityDebuffsStatus() 
    {
        InitVariables();
    }

public:

    MapInt_Double&  DebuffProperty() { return m_DebuffProperty; }
    void            SetDebuffProperty(MapInt_Double& vl) { m_DebuffProperty = vl; }

    MapInt_Double&  RawDebuffProperty() { return m_RawDebuffProperty; }
    void            SetRawDebuffProperty(MapInt_Double& vl) { m_RawDebuffProperty = vl; }

protected:

    /// Represents the debuffs and its immunities property.
    MapInt_Double   m_DebuffProperty;

    /// Represents the debuffs and its immunities property.        
    MapInt_Double   m_RawDebuffProperty;

private:

    void InitVariables()
    {
        m_DebuffProperty.clear();
        m_RawDebuffProperty.clear();

        m_DebuffProperty[DebuffType_DownDebuff]         = 0;
        m_DebuffProperty[DebuffType_PuzzleDebuff]       = 0;
        m_DebuffProperty[DebuffType_OutOfHandDebuff]    = 0;
        m_DebuffProperty[DebuffType_StunDebuff]         = 0;
        m_DebuffProperty[DebuffType_InertiaDebuff]      = 0;
        m_DebuffProperty[DebuffType_FreezeDebuff]       = 0;

        m_RawDebuffProperty[DebuffType_DownDebuff]      = 0;
        m_RawDebuffProperty[DebuffType_PuzzleDebuff]    = 0;
        m_RawDebuffProperty[DebuffType_OutOfHandDebuff] = 0;
        m_RawDebuffProperty[DebuffType_StunDebuff]      = 0;
        m_RawDebuffProperty[DebuffType_InertiaDebuff]   = 0;
        m_RawDebuffProperty[DebuffType_FreezeDebuff]    = 0;
    }
};
