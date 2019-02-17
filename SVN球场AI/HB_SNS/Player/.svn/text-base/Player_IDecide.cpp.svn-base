/********************************************************************************
 * 文件名：Player_IDecide.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "Player.h"

/// Player to action.
/// including the pass action, shoot action, defence action and so on.
void Player::Action()
{
    if (!m_Status->Enable())
    {
        return;
    }

    if (m_Status->GetDebuffStatus()->GetDisableDebuff() != NULL)
    {
        m_Status->GetDebuffStatus()->GetDisableDebuff()->Action(this);
        return;
    }

    m_Status->State()->Action(this);
}

/// 这个为旧接口，使用QuickDecide大幅度提高了性能
void Player::Decide()
{            
    if (!m_Status->Enable())
    {
        return;
    }

    foreach (AbsBuff* debuff, m_DebuffBar)
    {
        if (debuff->Type() == BuffType_Disable)
        {
            LogHelper::Insert("[Debuff]" + debuff->Type(), Color_Red);
            return;
        }
    }
    LogHelper::Insert("[Decide]Start decide. current:" + m_Status->State()->ToString(), Color_Red);

    if (m_Status->GetActionStatus()->ActionLast() >= m_Status->State()->TimeLast())
    {
        IState* preview = m_Status->State();
        int loop = 0;

        do
        {
            // LogHelper.Insert("[Decide]Start decide. current:" + _status.State.ToString() + ", preview:" + preview.ToString(), Color.Gray);
            IState* state = m_Status->State();

            m_Status->SetState(state->Decide(this, preview));

            preview = state;

            // LogHelper.Insert("[Decide]Decide end. current:" + _status.State.ToString());

            if (++loop > 100)
            {
                //throw new ApplicationException(String.Format("Player decide infinite. Player:{0}, State:{1}", this, _status.State));
            }
        } while (!m_Status->State()->Stopable());

        LogHelper::Insert("[Decide]Decide end. current:" + m_Status->State()->ToString());
        DecideEnd();

        m_Status->GetActionStatus()->SetActionLast(0);
    }
    else
    {
        m_Status->GetActionStatus()->IncActionLast();
    }

    // Skill Trigger
    if (m_Status->State()->IsInState(DefenceState::Instance()))
    {
        if (m_Status->GetDefenceStatus()->DefenceTarget()->Status()->Hasball())
        {
            m_Status->GetDefenceStatus()->DefenceTarget()->Status()->SetState(BreakThroughState::Instance());
            ValidateSkillTriggered(m_Status->GetDefenceStatus()->DefenceTarget());
        }
    }

    ValidateSkillTriggered(this);
}

/// Quick Decide.
void Player::QuickDecide()
{
    if (!m_Status->Enable())
    {
        return;
    }

    //LogHelper.Insert("[Decide]Start decide. current:" + _status.State.ToString(), Color.Blue);

    if (m_Status->GetActionStatus()->ActionLast() >= m_Status->State()->TimeLast())
    {
        IState* preview = m_Status->State();
        int loop = 0;

        do
        {
            IState* state = m_Status->State();

            state->Enter(this);
            m_Status->SetState(state->QuickDecide(this, preview));

            preview = state;

            if (++loop > 100)
            {
                //throw new ApplicationException(String.Format("Player decide infinite. Player:{0}, State:{1}", this, _status.State));
            }
        } while (!m_Status->State()->Stopable());

        //LogHelper.Insert("[Decide]Decide end. current:" + _status.State.ToString(), Color.Blue);
        DecideEnd();

        m_Status->GetActionStatus()->SetActionLast(0);
    }
    else
    {
        m_Status->GetActionStatus()->IncActionLast();
    }
}

/// Decide is over.
void Player::DecideEnd()
{
    m_Status->DecideEnd();
}

static void Player::ValidateSkillTriggered(IPlayer* player)
{            
    if (player->Status()->State()->IsInState(DefenceState::Instance()))
    {
        // 球员和他防守目标的距离
        double distance = player->Current().Distance(player->Status()->GetDefenceStatus()->DefenceTarget()->Current());

        if (distance > Defines_Player.INTERRUPTION_RANGE)
        {
            return;
        }
    }

    MapIState_VectorIActionSkill& skills = player->Skills();

    if (skills.find(player->Status()->State()) != skills.end())
    {
        vector<IActionSkill*>& skillList = RandomHelper::GetRandomSortList<IActionSkill*>(skills[player->Status()->State()]);

        foreach (IActionSkill* skill, skillList)
        {
            if (skill->CoolDown() == 0 && SkillFacade::Instance()->IsActionSkillTriggered(player, skill))
            {
                SkillTriggered(player, skill);
                return;
            }
        }
    }
}

/// While a action type skill is triggered, it will invokes this method.
/// This method will refill the skill's cool down, and insert a <see cref="ISkillResult"/> to <see cref="IPlayer"/> entity.
static void Player::SkillTriggered(IPlayer* player, IActionSkill* skill)
{
    skill->SkillTriggered();
    
    ISkillResult* result = new SkillResult();

    result->SetSkillId(skill->SkillId());
    result->SetRound(player->GetMatch()->Status()->Round());
    result->SetSkillName(skill->SkillName());
    result->SetSkillTargets(skill->GetSkillTargetsToString(player));

    MapInt_ISkillResult& skillResults = player->SkillResults();
    if (skillResults.find(result->Round()) == skillResults.end())
    {
        skillResults[result->Round()]      = result;
        SkillFacade::Instance()->ActionSkillEffect(player, skill);
    }

    LogHelper::Insert("[Skill]Skill Triggerd. SkillId:" + skill->SkillId() + ", SkillName:" + skill->SkillName(), Color_Red);
}
