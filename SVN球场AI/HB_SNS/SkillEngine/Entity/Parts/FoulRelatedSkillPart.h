/********************************************************************************
 * 文件名：FoulRelatedSkillPart.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __FOULRELATEDSKILLPART_H__
#define __FOULRELATEDSKILLPART_H__

/// Represents a part of skill that defines the info related the foul.
class FoulRelatedSkillPart : public ISkillPart
{
public:

    FoulRelatedSkillPart()
    {
        InitVariables();
    }

    FoulRelatedSkillPart(FoulRelatedSkillPart& rf)
    {
        InitVariables();

        foreach (Foul* f, rf.Fouls())
        {
            m_Fouls.push_back(f->Clone());
        }

        foreach (FoulChange* f, rf.FoulChanges())
        {
            m_FoulChanges.push_back(f->Clone());
        }
    }

    ~FoulRelatedSkillPart()
    {
        foreach (Foul* f, m_Fouls)
        {
            delete f;
        }

        m_Fouls.clear();

        foreach (FoulChange* f, m_FoulChanges)
        {
            delete f;
        }

        m_FoulChanges.clear()
    }

public:

    vector<Foul*>&          Fouls() { return m_Fouls; }

    vector<FoulChange*>&    FoulChanges() { return m_FoulChanges; }

    FoulRelatedSkillPart*   Clone() { return new FoulRelatedSkillPart(*this); }

private:

    /// Represetns the list of the <see cref="Foul"/>
    vector<Foul*>           m_Fouls;

    /// Represetns the list of the <see cref="FoulChange"/>.
    vector<FoulChange*>     m_FoulChanges;

private:

    void    InitVariables()
    {
        m_Fouls.reserve(4);
        m_Fouls.clear();

        m_FoulChanges.reserve(4);
        m_FoulChanges.clear();
    }
};

/// Represents the foul information.
class Foul
{
public:

    Foul()
    {
        InitVariables();
    }

    Foul(Foul& rf)
    {
        InitVariables();

        m_Type          = rf.Type();
        m_Target        = rf.Target();
        m_Probability   = rf.Probability();
    }

    virtual ~Foul() {} 

public:

    int     Type() { return m_Type; }
    void    SetType(int vl) { m_Type = vl; }

    int     Target() { return m_Target; }
    void    SetTarget(int vl) { m_Target = vl; }

    double  Probability() { return m_Probability; }
    void    SetProbability(double vl) { m_Probability = vl; }

    Foul*   Clone() {return new Foul(*this); }

protected:

    /// Represents the foul type.
    int     m_Type;

    /// Represents the foul's target.
    int     m_Target;

    /// Represetns the foul's trigger probability.
    double  m_Probability;

private:

    void    InitVariables()
    {
        m_Type          = 0;
        m_Target        = 0;
        m_Probability   = 0.0;
    }
};

/// Represents the foul change information.
class FoulChange
{
public:

    FoulChange()
    {
        InitVariables();
    }

    FoulChange(FoulChange& rf)
    {
        m_Type      = rf.Type();
        m_Target    = rf.Target();
        m_Lv        = rf.Lv();
    }

    virtual ~FoulChange() {}

public:

    int         Type() { return m_Type; }
    void        SetType(int vl) { m_Type = vl; }

    int         Target() { return m_Target; }
    void        SetTarget(int vl) { m_Target = vl; }

    int         Lv() { return m_Lv; }
    void        SetLv(int vl) { m_Lv = vl; }

    FoulChange* Clone() { return new FoulChange(*this); }

protected:

    /// Represents the foul change type.
    int     m_Type;

    /// Represents the target.
    int     m_Target;

    /// Represents the level of the foul.
    int     m_Lv;

private:

    void        InitVariables()
    {
        m_Type      = 0;
        m_Target    = 0;
        m_Lv        = 0;
    }
};

#endif //__FOULRELATEDSKILLPART_H__
