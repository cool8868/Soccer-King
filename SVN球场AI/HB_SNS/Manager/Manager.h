/********************************************************************************
 * �ļ�����Manager.h
 * �����ˣ��α�
 * ����ʱ�䣺2011-6-10
 * �汾��1.0
 * ���ļ��汾�ţ�1.0
 * �����£�
 * ����˵����
 * ��ʷ�޸ļ�¼��
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MANAGER_H__
#define __MANAGER_H__

#include "../Interface/Manager/IManager.h"
#include "../Interface/Manager/IManagerData.h"
#include "../common/Enum/Position.h"

class Manager: public IManager, public Creature
{
public:

    /// ����ǹ��ڵĽӿ�,ֻ��ģ������ʹ��
    Manager(unsigned int id, Side side, IMatch* match);

    Manager(Side side, IMatch* match, TransferManagerEntity* entity);

    virtual ~Manager();

public:

    /// ����
    void                    Goal();

    /// �µĻغϵ���ʱ��ʼ��
    void                    RoundInit();

    /// �µķ��ӿ�ʼʱ��ʼ��
    void                    MinuteInit();

    /// �°볡��ʼ
    void                    SecondHalfStart();

    /// ��ʾ�˵�ǰ�������Ա����
    void                    Foul();

    /// ���������Ա�������쳣״̬
    void                    ClearDisable();

    /// ����Ա��������
    void                    GkOpenball();

    /// Initializes current entity.
    void                    Initialize();

    vector<IPlayer*>        GetPlayersByPosition(Position position);

    void                    TriggerOpenerSkill(bool isFirstHalf);

public:

    unsigned int            Id() { return m_Id; }
    string                  Name() { return m_Name; }

    string                  SpellName() { return m_SpellName; }

    int                     FormationId() { return m_FormationId; }

    string                  Logo() { return m_Logo; }
    void                    SetLogo(string logo) { m_Logo = logo; }

    vector<IPlayer*>&       Players() { return m_Players; };

    IMatch*                 GetMatch() { return m_Match; }

    IManager*               Opponent();

    Side                    GetSide() { return m_Side; }

    vector<string>&         PassiveSkills() { return m_PassiveSkills; }
    void                    SetPassiveSkills(vector<string>& vl) { m_PassiveSkills = vl; }

    vector<IOpenerSkill*>&  OpenerSkills() { return m_OpenerSkills; }
    void                    SetOpenerSkills(vector<IOpenerSkill*>& vl) { m_OpenerSkills = vl; }

    vector<IWill*>&         Wills() { return m_Wills; }
    void                    SetWills(vector<IWill*>& vl) { m_Wills = vl; }

    IManagerStatus*         Status() { return m_Status; }

protected:

    /// ��ʾ�˾����ID
    unsigned int m_Id;

    /// ��ʾ�˾����������
    string m_Name;

    /// ��ʾ�˾����Ӣ����
    string m_SpellName;

    /// ��ʾ������id
    int m_FormationId;

    /// ��ʾ�˾����Logo
    string m_Logo;

    /// ��ʾ����Ա�ļ���
    vector<IPlayer*> m_Players;

    /// ��ʾ�˶���������������
    IMatch* m_Match;

    /// ��ʾ�˶��־���
    IManager* m_Opponent;

    /// ��ʾ�˾��������ӻ�Ͷ�
    Side m_Side;

    /// ��ʾ�˸þ���ı�������
    vector<string> m_PassiveSkills;

    /// ��ʾ�˸þ��������������
    vector<IOpenerSkill*> m_OpenerSkills;

    /// ��ʾ�˸þ������־
    vector<IWill*>  m_Wills;

    /// ��ʾ�˾���ĵ�ǰ״̬
    IManagerStatus* m_Status;

private:

    vector<IPlayer*>            InternalGetPlayerByPosition(Position position);

    /// ˢ�±�������ǰ��һ����Ա
    void                        RefreshHeadMostPlayer();

    /// ˢ��ÿ����Ա�ķ���Ŀ��
    void                        RefreshDefenceStatus();

    /// �ҳ�һ����Ա������ķ�����
    static IPlayer*             GetClosestDefender(IPlayer* target, vector<IPlayer*>& players);

    static vector<IPlayer*>     GetNoneDefenceTargetPlayer(vector<IPlayer*>& players);

    //��ʼ������
    void                        InitVariables();

private:

    //���ڱ���ָ�룬��������
    IManagerData*   m_Entity;
};

#endif //__MANAGER_H__