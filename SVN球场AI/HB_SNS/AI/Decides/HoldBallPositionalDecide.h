/********************************************************************************
 * 文件名：HoldBallPositionalDecide.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __HOLDBALLPOSITIONALDECIDE_H__
#define __HOLDBALLPOSITIONALDECIDE_H__

#include "../../common/common.h"
#include "../../common/utils.h"
#include "../../common/Enum/PlayerProperty.h"

/// 封装了持球者选择行动方向的逻辑
class HoldBallPositionalDecide
{
public:

    class HoldBallPositionalDecideLogic;

    static Coordinate GetTarget(IPlayer* player)
    {
        return HoldBallPositionalDecideLogic().SetPlayer(player).Start();
    }

    class HoldBallPositionalDecideLogic
    {
    private:

        enum Duty
        {
            Duty_Forward = 0,
            Duty_Middle = 1,
            Duty_Fullback = 2
        };

        enum PitchPosition
        {
            PitchPosition_Middle = 0,
            PitchPosition_Up = 1,
            PitchPosition_Down = 2
        };

        /// Represents the player's attribute.
        struct Attribute
        {
            Attribute()
                : AttackMax(Defines_Player_PositionalConfig.ATTACK_MAX)
                , AttackAward(Defines_Player_PositionalConfig.ATTACK_AWARD)
                , ForwardAward(Defines_Player_PositionalConfig.ForwardAward)
                , BackPenelty(Defines_Player_PositionalConfig.BackPenelty)
            {
                AttackAverage   = 0.0;
                DefenceAverage  = 0.0;

                EnemyDistrict   = 0;
                SelfDirection   = 0;
            }

            Attribute& operator = (const Attribute& attr)
            {
                AttackAverage           = attr.AttackAverage;
                DefenceAverage          = attr.DefenceAverage;
                xDuty                   = attr.xDuty;
                CurrentPosition         = attr.CurrentPosition;
                EnemyDistrict           = attr.EnemyDistrict;
                SelfDirection           = attr.SelfDirection;

                return *this;
            }

            /// 平均的进攻值
            double AttackAverage;

            /// 平均的防守值
            double DefenceAverage;

            /// 进攻的最大值
            const double AttackMax;

            /// 进攻的奖励值
            const double AttackAward;

            /// 职责
            Duty xDuty;

            /// 当前的位置
            PitchPosition CurrentPosition;

            /// 防守者的坐标
            int EnemyDistrict;

            /// 面朝方向
            int SelfDirection;

            /// 进攻奖励
            const double ForwardAward;

            /// 后退惩罚
            const double BackPenelty;
        };

        /// 移动的8方向象限
        struct District
        {
            District()
                : L0(0.0f)
                , L1(0.0f)
                , L2(0.0f)
                , L3(0.0f)
                , L4(0.0f)
                , L5(0.0f)
                , L6(0.0f)
                , L7(0.0f)
                , L8(0.0f)
                , L9(0.0f)
                , L10(0.0f)
                , L11(0.0f)
            {

            }

            /// 重载了+操作符
            District& operator +=(District distric)
            {
                L0 += distric.L0;
                L1 += distric.L1;
                L2 += distric.L2;
                L3 += distric.L3;
                L4 += distric.L4;
                L5 += distric.L5;
                L6 += distric.L6;
                L7 += distric.L7;
                L8 += distric.L8;
                L9 += distric.L9;
                L10 += distric.L10;
                L11 += distric.L11;

                return *this;
            }

            double L0;
            double L1;
            double L2;
            double L3;
            double L4;
            double L5;
            double L6;
            double L7;
            double L8;
            double L9;
            double L10;
            double L11;

            double Sum()
            {
                if (L0 < 0) L0 = 0;
                if (L1 < 0) L1 = 0;
                if (L2 < 0) L2 = 0;
                if (L3 < 0) L3 = 0;
                if (L4 < 0) L4 = 0;
                if (L5 < 0) L5 = 0;
                if (L6 < 0) L6 = 0;
                if (L7 < 0) L7 = 0;
                if (L8 < 0) L8 = 0;
                if (L9 < 0) L9 = 0;
                if (L10 < 0) L10 = 0;
                if (L11 < 0) L11 = 0;

                return L0 + L1 + L2 + L3 + L4 + L5 + L6 + L7 + L8 + L9 + L10 + L11;
            }
        };

    private:

        IPlayer*    m_player;
        Attribute   m_attribute;

    public:
        
        HoldBallPositionalDecideLogic& SetPlayer(IPlayer* player)
        {
            m_player        = player;
            m_attribute     = GetPlayerAttribute();

            return *this;
        }

        Coordinate Start()
        {
            District district;

            district += CalcAttackPoint();
            district += CalcDefencePoint();
            district += CalcAttackAward();
            district += CalcMentalityAward();
            district += CalcPositionAward();                
            district += CalcSelfDirection();
            district += CalcEnemyPenalty(district);

            double total = district.Sum();
            district.L0 = district.L0 / total;
            district.L1 = district.L1 / total;
            district.L2 = district.L2 / total;
            district.L3 = district.L3 / total;
            district.L4 = district.L4 / total;
            district.L5 = district.L5 / total;
            district.L6 = district.L6 / total;
            district.L7 = district.L7 / total;
            district.L8 = district.L8 / total;
            district.L9 = district.L9 / total;
            district.L10 = district.L10 / total;
            district.L11 = district.L11 / total;

            district.L1 = district.L0 + district.L1;
            district.L2 = district.L1 + district.L2;
            district.L3 = district.L2 + district.L3;
            district.L4 = district.L3 + district.L4;
            district.L5 = district.L4 + district.L5;
            district.L6 = district.L5 + district.L6;
            district.L7 = district.L6 + district.L7;
            district.L8 = district.L7 + district.L8;
            district.L9 = district.L8 + district.L9;
            district.L10 = district.L9 + district.L10;
            district.L11 = district.L10 + district.L11;

            int index = -255;
            double rand = (double)RandomHelper::GetInt32(0, 10000) / 100;
            if (rand <= district.L0 * 100)
            {
                index = 0;
            }
            else if (rand > district.L0 * 100 && rand <= district.L1 * 100)
            {
                index = 1;
            }
            else if (rand > district.L1 * 100 && rand <= district.L2 * 100)
            {
                index = 2;
            }
            else if (rand > district.L2 * 100 && rand <= district.L3 * 100)
            {
                index = 3;
            }
            else if (rand > district.L3 * 100 && rand <= district.L4 * 100)
            {
                index = 4;
            }
            else if (rand > district.L4 * 100 && rand <= district.L5 * 100)
            {
                index = 5;
            }
            else if (rand > district.L5 * 100 && rand <= district.L6 * 100)
            {
                index = 6;
            }
            else if (rand > district.L6 * 100 && rand <= district.L7 * 100)
            {
                index = 7;
            }
            else if (rand > district.L7 * 100 && rand <= district.L8 * 100)
            {
                index = 8;
            }
            else if (rand > district.L8 * 100 && rand <= district.L9 * 100)
            {
                index = 9;
            }
            else if (rand > district.L9 * 100 && rand <= district.L10 * 100)
            {
                index = 10;
            }
            else if (rand > district.L10 * 100 && rand <= 100)
            {
                index = 11;
            }

            if (index == -255)
            {
                //+++ tony
                throw ApplicationException("Random index application in HoldBallPositionalDecideLogic class, Start method.");
            }

            int angle = 0;
            int t = index * 30 - 90;
            if (t >= 0)
            {
                angle = RandomHelper::GetInt32(t, t + 30 - 1);
            }
            else
            {
                angle = RandomHelper::GetInt32(t + 360, t + 389);
            }

            double x = 0.0f;
            double y = 0.0f;

            if (m_player->GetSide() == Side_Home)
            {
                x = m_player->Current().X + 20 * cos(angle * MATH::PI / 180);
                y = m_player->Current().Y + 20 * sin(angle * MATH::PI / 180);
            }
            else
            {
                x = m_player->Current().Mirror().X + 20 * cos(angle * MATH::PI / 180);
                y = m_player->Current().Mirror().Y + 20 * sin(angle * MATH::PI / 180);
            }

            Coordinate target = Coordinate(x, y);

            return (m_player->GetSide() == Side_Home) ? target : target.Mirror();
        }

    private:

        Attribute GetPlayerAttribute()
        {
            Attribute attr;

            //平均进攻和平均防守
            attr.AttackAverage = (m_player->CurrProperty()[PlayerProperty_Shooting] +
                m_player->CurrProperty()[PlayerProperty_Strength] +
                m_player->CurrProperty()[PlayerProperty_Dribble] +
                m_player->CurrProperty()[PlayerProperty_Aggression]) / 4;

            attr.DefenceAverage = (m_player->CurrProperty()[PlayerProperty_Disturb] + 
                m_player->CurrProperty()[PlayerProperty_Interception]) / 2;

            //职责
            if (m_player->BuildinAttribute()->GetPosition() == Position_Forward)
            {
                attr.xDuty = Duty_Forward;
            }

            if (m_player->BuildinAttribute()->GetPosition() == Position_Midfielder)
            {
                attr.xDuty = Duty_Middle;
            }

            if (m_player->BuildinAttribute()->GetPosition() == Position_Fullback)
            {
                attr.xDuty = Duty_Fullback;
            }

            Coordinate current = (m_player->GetSide() == Side_Home) ? m_player->Current() : m_player->Current().Mirror();

            //场上位置
            attr.CurrentPosition = PitchPosition_Middle;
            if (current.Y < 28)
            {
                attr.CurrentPosition = PitchPosition_Up;
            }

            if (current.Y > 108)
            {
                attr.CurrentPosition = PitchPosition_Down;
            }

            //朝向象限
            int angle = (m_player->GetSide() == Side_Home)? m_player->Status()->Angle() : m_player->Status()->Angle() + 180;
            if (angle > 360)
            {
                angle -= 360;
            }
            attr.SelfDirection = (int) (((angle + 90) % 360) / 30);

            //防守者所在的象限
            if (m_player->Status()->GetDefenceStatus()->ClosestDefender() == NULL)
            {
                attr.EnemyDistrict = 9; // 身后不判断
            }
            else
            {
                if (m_player->Current().Distance(m_player->Status()->GetDefenceStatus()->ClosestDefender()->Current()) > 10)
                {
                    attr.EnemyDistrict = 9;
                }
                else
                {

                    Coordinate enemy = (m_player->GetSide() == Side_Home) ?
                        m_player->Status()->GetDefenceStatus()->ClosestDefender()->Current() :
                        m_player->Status()->GetDefenceStatus()->ClosestDefender()->Current().Mirror();

                    double dx = enemy.X - current.X;
                    double dy = enemy.Y - current.Y;

                    if (dx > 0 && dy <= 0) // 1区间
                    {
                        double value = fabs(dy / dx);
                        if (value >= 1.73) // >= 3^0.5
                        {
                            attr.EnemyDistrict = 0;
                        }

                        if (value >= 0.57 && value < 1.73)  // 3^0.5 / 3 ~ 3^0.5
                        {
                            attr.EnemyDistrict = 1;
                        }

                        if (value >= 0.0 && value < 0.57) // 0 ~ 3^0.5 / 3
                        {
                            attr.EnemyDistrict = 2;
                        }
                    }

                    if (dx >= 0.0 && dy > 0.0) // 2区间
                    {
                        if (MATH::FloatEqual(dx, 0.0))
                        {
                            attr.EnemyDistrict = 5;
                        }
                        else
                        {
                            double value = fabs(dy / dx);
                            if (value > 0 && value <= 0.57) // 0 ~ 3^0.5 / 3
                            {
                                attr.EnemyDistrict = 3;
                            }

                            if (value > 0.57 && value <= 1.73) // 3^0.5 / 3 ~ 3^0.5
                            {
                                attr.EnemyDistrict = 4;
                            }

                            if (value > 1.73)
                            {
                                attr.EnemyDistrict = 5;
                            }
                        }
                    }

                    if (dx < 0 && dy >= 0) // 3区间
                    {
                        double value = fabs(dy / dx);
                        if (value >= 1.73) // >= 3^0.5
                        {
                            attr.EnemyDistrict = 6;
                        }

                        if (value >= 0.57 && value < 1.73) // 3^0.5 / 3 ~ 3^0.5
                        {
                            attr.EnemyDistrict = 7;
                        }

                        if (value >= 0 && value < 0.57) // 0 ~ 3^0.5 / 3
                        {
                            attr.EnemyDistrict = 8;
                        }
                    }

                    if (dx <= 0 && dy < 0)
                    {
                        if (MATH::FloatEqual(dx, 0.0))
                        {
                            attr.EnemyDistrict = 11;
                        }
                        else
                        {
                            double value = fabs(dy / dx);
                            if (value > 0 && value <= 0.57) // 0 ~ 3^0.5 / 3
                            {
                                attr.EnemyDistrict = 9;
                            }

                            if (value > 0.57 && value <= 1.73) // 3^0.5 / 3  ~ 3^0.5
                            {
                                attr.EnemyDistrict = 10;
                            }

                            if (value > 1.73) // > 3^0.5
                            {
                                attr.EnemyDistrict = 11;
                            }
                        }
                    }
                }
            }

            return attr;
        }

        /// 计算进攻奖励
        District CalcAttackPoint()
        {
            District district;

            double attackPoint = m_attribute.AttackAverage;
            district.L0 = attackPoint;
            district.L1 = attackPoint;
            district.L2 = attackPoint;
            district.L3 = attackPoint;
            district.L4 = attackPoint;
            district.L5 = attackPoint;

            double backPoint = m_attribute.AttackMax - m_attribute.AttackAverage;
            district.L6 = backPoint;
            district.L7 = backPoint;
            district.L8 = backPoint;
            district.L9 = backPoint;
            district.L10 = backPoint;
            district.L11 = backPoint;

            return district;
        }

        /// 计算防守奖励
        District CalcDefencePoint()
        {
            District district;
            district.L6 = m_attribute.DefenceAverage;
            district.L7 = m_attribute.DefenceAverage;
            district.L8 = m_attribute.DefenceAverage / 2;
            district.L9 = district.L8;
            district.L10 = m_attribute.DefenceAverage;
            district.L11 = m_attribute.DefenceAverage;

            return district;
        }

        /// 前场进攻奖励
        District CalcAttackAward()
        {
            District district;

            district.L0 = m_attribute.AttackAward / 3;
            district.L1 = m_attribute.AttackAward * 2 / 3;
            district.L2 = m_attribute.AttackAward;
            district.L3 = district.L1;
            district.L4 = district.L0;

            return district;
        }

        /// 计算意识奖励
        District CalcMentalityAward()
        {
            District district;
            double mentality = m_player->CurrProperty()[PlayerProperty_Mentality];

            if (m_attribute.xDuty == Duty_Forward)
            {
                double douMentality = mentality * mentality;
                district.L0 = 0;// douMentality / 4;
                district.L1 = douMentality / 2;
                district.L2 = douMentality;
                district.L3 = district.L2;
                district.L4 = district.L1;
                district.L5 = district.L0;

                district.L6 = -mentality / 2;
                district.L7 = district.L6;
                district.L8 = -mentality;
                district.L9 = district.L8;
                district.L10 = district.L7;
                district.L11 = district.L6;
            }

            if (m_attribute.xDuty == Duty_Middle)
            {
                double douMentality = mentality * mentality;
                district.L0 = douMentality / 4;
                district.L1 = douMentality / 2;
                district.L2 = douMentality;
                district.L3 = district.L2;
                district.L4 = district.L1;
                district.L5 = district.L0;

                district.L6 = district.L5;
                district.L7 = -mentality / 2;
                district.L8 = -mentality;
                district.L9 = district.L8;
                district.L10 = district.L7;
                district.L11 = district.L6;
            }

            if (m_attribute.xDuty == Duty_Fullback)
            {
                double douMentality = mentality * mentality;
                district.L0 = douMentality / 4;
                district.L1 = douMentality / 2;
                district.L2 = douMentality / 2;
                district.L3 = district.L2;
                district.L4 = district.L1;
                district.L5 = district.L0;

                district.L6 = douMentality / 8;
                //district.L7 = 0;
                //district.L8 = 0;
                //district.L9 = 0;
                //district.L10 = 0;
                district.L11 = district.L6;
            }

            return district;
        }

        /// 计算场上位置奖励
        District CalcPositionAward()
        {
            District district;

            if (m_attribute.CurrentPosition == PitchPosition_Middle)
            {
                double douAttack = m_attribute.AttackAverage * m_attribute.AttackAverage;
                district.L0 = 0;// douAttack / 4;
                district.L1 = douAttack / 2;
                district.L2 = douAttack;
                district.L3 = district.L2;
                district.L4 = district.L1;
                district.L5 = district.L0;

                district.L6 = 0;
                district.L7 = -m_attribute.AttackAverage / 2;
                district.L8 = -m_attribute.AttackAverage;
                district.L9 = district.L8;
                district.L10 = district.L7;
                district.L11 = district.L6;
            }

            if (m_attribute.CurrentPosition == PitchPosition_Up)
            {
                double douAccleration = m_player->CurrProperty()[PlayerProperty_Acceleration] * m_player->CurrProperty()[PlayerProperty_Acceleration];
                double douBalance = m_player->CurrProperty()[PlayerProperty_Balance] * m_player->CurrProperty()[PlayerProperty_Balance];
                double sumAccBal = m_player->CurrProperty()[PlayerProperty_Acceleration] + m_player->CurrProperty()[PlayerProperty_Balance];
                district.L0 = -douBalance;
                district.L1 = -douBalance;// douAccleration / 4;
                district.L2 = douAccleration;// douAccleration / 2;
                district.L3 = douAccleration*2;//sumAccBal * sumAccBal / 4;
                district.L4 = 0;// sumAccBal* sumAccBal; //district.L3;
                district.L5 = -douBalance;//douBalance / 2;

                district.L6 = 0;//douBalance / 4;
                district.L7 = 0;
                district.L8 = -(m_player->CurrProperty()[PlayerProperty_Balance]) / 2;
                district.L9 = -sumAccBal / 2;
                district.L10 = district.L9;
                district.L11 = -m_player->CurrProperty()[PlayerProperty_Acceleration] / 2;
            }

            if (m_attribute.CurrentPosition == PitchPosition_Down)
            {
                double douAccleration = m_player->CurrProperty()[PlayerProperty_Acceleration] * m_player->CurrProperty()[PlayerProperty_Acceleration];
                double douBalance = m_player->CurrProperty()[PlayerProperty_Balance] * m_player->CurrProperty()[PlayerProperty_Balance];
                double sumAccBal = m_player->CurrProperty()[PlayerProperty_Acceleration] + m_player->CurrProperty()[PlayerProperty_Balance];
                district.L0 = -douBalance;//douBalance / 2;
                district.L1 = 0;// sumAccBal* sumAccBal;//sumAccBal * sumAccBal / 4;
                district.L2 = douAccleration*2;//district.L1;
                district.L3 = douAccleration;//douAccleration / 2;
                district.L4 = -douBalance;//douAccleration / 4;
                district.L5 = -douBalance;//0;

                district.L6 = -m_player->CurrProperty()[PlayerProperty_Acceleration] / 2;
                district.L7 = -sumAccBal / 2;
                district.L8 = district.L7;
                district.L9 = -(m_player->CurrProperty()[PlayerProperty_Balance] / 2);
                district.L10 = 0;
                district.L11 = douBalance / 4;
            }

            return district;
        }

        /// 计算防守人惩罚
        District CalcEnemyPenalty(District current)
        {
            District result;
            double D = m_player->CurrProperty()[PlayerProperty_Dribble];
            double targetValue = D * D / 2;
            double allocation = 0.0f;

            switch (m_attribute.EnemyDistrict)
            {
                case 2:
                    allocation = current.L2 - targetValue;
                    result.L0 = 0;//allocation / 6;
                    result.L1 = allocation / 2;//allocation / 3;
                    result.L2 = -allocation;
                    result.L3 = allocation / 2;//allocation / 3;
                    result.L4 = 0;//allocation / 6;
                    result.L5 = 0;
                    result.L6 = 0;
                    result.L7 = 0;
                    result.L8 = 0;
                    result.L9 = 0;
                    result.L10 = 0;
                    result.L11 = 0;
                    break;
                case 3:
                    allocation = current.L3 - targetValue;
                    result.L0 = 0;
                    result.L1 = 0;//allocation / 6;
                    result.L2 = allocation / 2;//allocation / 3;
                    result.L3 = -allocation;
                    result.L4 = allocation / 2;//allocation / 3;
                    result.L5 = 0;//allocation / 6;
                    result.L6 = 0;
                    result.L7 = 0;
                    result.L8 = 0;
                    result.L9 = 0;
                    result.L10 = 0;
                    result.L11 = 0;
                    break;
                case 1:
                    allocation = current.L1 - targetValue;
                    result.L0 = allocation / 2;//allocation / 3;
                    result.L1 = -allocation;
                    result.L2 = allocation / 2;//allocation / 3;
                    result.L3 = 0;//allocation / 6;
                    result.L4 = 0;
                    result.L5 = 0;
                    result.L6 = 0;
                    result.L7 = 0;
                    result.L8 = 0;
                    result.L9 = 0;
                    result.L10 = 0;
                    result.L11 = 0;//allocation / 6;
                    break;
                case 4:
                    allocation = current.L4 - targetValue;
                    result.L0 = 0;
                    result.L1 = 0;
                    result.L2 = 0;//allocation / 6;
                    result.L3 = allocation / 2;//allocation / 3;
                    result.L4 = -allocation;
                    result.L5 = allocation / 2;//allocation / 3;
                    result.L6 = 0;//allocation / 6;
                    result.L7 = 0;
                    result.L8 = 0;
                    result.L9 = 0;
                    result.L10 = 0;
                    result.L11 = 0;
                    break;
                case 0:
                    allocation = current.L0 - targetValue;
                    result.L0 = -allocation;
                    result.L1 = allocation / 2;//allocation / 3;
                    result.L2 = 0;//allocation / 6;
                    result.L3 = 0;
                    result.L4 = 0;
                    result.L5 = 0;
                    result.L6 = 0;
                    result.L7 = 0;
                    result.L8 = 0;
                    result.L9 = 0;
                    result.L10 = 0;//allocation / 6;
                    result.L11 = allocation / 2;//allocation / 3;
                    break;
                case 5:
                    allocation = current.L5 - targetValue;
                    result.L0 = 0;
                    result.L1 = 0;
                    result.L2 = 0;
                    result.L3 = 0;//allocation / 6;
                    result.L4 = allocation / 2;//allocation / 3;
                    result.L5 = -allocation;
                    result.L6 = allocation / 2;//allocation / 3;
                    result.L7 = 0;//allocation / 6;
                    result.L8 = 0;
                    result.L9 = 0;
                    result.L10 = 0;
                    result.L11 = 0;
                    break;                    
            }

            return result;
        }

        /// 计算球员自我的朝向的影响
        District CalcSelfDirection()
        {
            District result;
            int selfDirection = m_attribute.SelfDirection;                
            double A = (m_player->CurrProperty()[PlayerProperty_Aggression] * m_player->CurrProperty()[PlayerProperty_Aggression]);
            double B = (m_player->CurrProperty()[PlayerProperty_Balance] * m_player->CurrProperty()[PlayerProperty_Balance]);
            double forwardAward = A;
            double forwardAward1 = ((A * 2 / 3) + (B / 3)) / 2;
            double forwardAward2 = 0;// ((A / 3) + (B * 2 / 3)) / 2;

            double backPenelty = m_attribute.BackPenelty;
            switch (selfDirection)
            {
                case 0:
                    result.L0 = forwardAward;
                    result.L1 = forwardAward1;
                    result.L2 = forwardAward2;
                    result.L3 = 0;
                    result.L4 = -backPenelty;
                    result.L5 = -backPenelty;
                    result.L6 = -backPenelty;
                    result.L7 = -backPenelty;
                    result.L8 = -backPenelty;
                    result.L9 = 0;
                    result.L10 = forwardAward2;
                    result.L11 = forwardAward1;
                    break;
                case 1:
                    result.L0 = forwardAward1;
                    result.L1 = forwardAward;
                    result.L2 = forwardAward1;
                    result.L3 = forwardAward2;
                    result.L4 = 0;
                    result.L5 = -backPenelty;
                    result.L6 = -backPenelty;
                    result.L7 = -backPenelty;
                    result.L8 = -backPenelty;
                    result.L9 = -backPenelty;
                    result.L10 = 0;
                    result.L11 = forwardAward2;
                    break;
                case 2:
                    result.L0 = forwardAward2;
                    result.L1 = forwardAward1;
                    result.L2 = forwardAward;
                    result.L3 = forwardAward1;
                    result.L4 = forwardAward2;
                    result.L5 = 0;
                    result.L6 = -backPenelty;
                    result.L7 = -backPenelty;
                    result.L8 = -backPenelty;
                    result.L9 = -backPenelty;
                    result.L10 = -backPenelty;
                    result.L11 = 0;
                    break;
                case 3:
                    result.L0 = 0;
                    result.L1 = forwardAward2;
                    result.L2 = forwardAward1;
                    result.L3 = forwardAward;
                    result.L4 = forwardAward1;
                    result.L5 = forwardAward2;
                    result.L6 = 0;
                    result.L7 = -backPenelty;
                    result.L8 = -backPenelty;
                    result.L9 = -backPenelty;
                    result.L10 = -backPenelty;
                    result.L11 = -backPenelty;
                    break;
                case 4:
                    result.L0 = -backPenelty;
                    result.L1 = 0;
                    result.L2 = forwardAward2;
                    result.L3 = forwardAward1;
                    result.L4 = forwardAward;
                    result.L5 = forwardAward1;
                    result.L6 = forwardAward2;
                    result.L7 = 0;
                    result.L8 = -backPenelty;
                    result.L9 = -backPenelty;
                    result.L10 = -backPenelty;
                    result.L11 = -backPenelty;
                    break;
                case 5:
                    result.L0 = -backPenelty;
                    result.L1 = -backPenelty;
                    result.L2 = 0;
                    result.L3 = forwardAward2;
                    result.L4 = forwardAward1;
                    result.L5 = forwardAward;
                    result.L6 = forwardAward1;
                    result.L7 = forwardAward2;
                    result.L8 = 0;
                    result.L9 = -backPenelty;
                    result.L10 = -backPenelty;
                    result.L11 = -backPenelty;
                    break;
            }
            return result;
        }
    };
};

#endif //__HOLDBALLPOSITIONALDECIDE_H__
