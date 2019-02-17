/********************************************************************************
 * 文件名：RawPlayer.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __RAWPLAYER_H__
#define __RAWPLAYER_H__

#include "../../Interface/Player/Property/IRawPlayer.h"

class RawPlayer: public IRawPlayer
{
public:

    RawPlayer();

    ~RawPlayer();

public:

    unsigned int            Id() { return m_Id; }
    void                    SetId(unsigned int id) { m_Id = id; }

    Coordinate              GetDefault() { return m_Default; }
    void                    SetDefault(Coordinate corrd) { m_Default = corrd; }

    IPlayerAttribute*       BuildinAttribute() { return m_BuildinAttribute; }
    void                    SetBuildinAttribute(IPlayerAttribute* pAttr) { m_BuildinAttribute = pAttr; }

    MapInt_Double&          RawProperty() { return m_RawProperty; }
    void                    SetRawProperty(MapInt_Double property) { m_RawProperty = property; }

    vector<string>&         Skills() { return m_Skills; }
    void                    SetSkills(vector<string>& vl) { m_Skills = vl; }

private:

    unsigned int            m_Id;

    Coordinate              m_Default;

    IPlayerAttribute*       m_BuildinAttribute;

    MapInt_Double           m_RawProperty;

    vector<string>          m_Skills;

private:

    void                InitVariables();
};

#endif //__RAWPLAYER_H__
