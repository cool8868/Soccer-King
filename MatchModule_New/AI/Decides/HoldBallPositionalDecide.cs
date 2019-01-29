/********************************************************************************
 * 文件名：HoldBallPositionalDecide
 * 创建人：
 * 创建时间：5/6/2010 1:43:55 PM
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：持球人决定行动方向
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base;
using Games.NB.Match.Common.Random;

namespace Games.NB.Match.AI.Decides
{
    /// <summary>
    /// Represents the encapsulated of the hold ball positional logic.
    /// 封装了持球者选择行动方向的逻辑
    /// </summary>
    static class HoldBallPositionalDecide
    {
        public static Coordinate GetTarget(IPlayer player)
        {
            return new HoldBallPositionalDecideLogic(player).Start();
        }

        class HoldBallPositionalDecideLogic
        {       
            private readonly IPlayer _player;
            private readonly Attribute _attribute;

            /// <summary>
            /// Initializes a new instance of the <see cref="HoldBallPositionalDecideLogic"/> class.
            /// </summary>
            /// <param name="player"><see cref="IPlayer"/></param>
            public HoldBallPositionalDecideLogic(IPlayer player)
            {
                this._player = player;
                this._attribute = GetPlayerAttribute();                
            }

            public Coordinate Start()
            {
                var match = _player.Manager.Match;
                //if (Utility.IfSolo(this._player))
                //    return Utility.GetSoloPosition(this._player);
                if (Utility.IfRush(this._player))
                    return Utility.GetRushPosition(this._player);
                District district = new District();
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
                double rand = (double)match.RandomInt32(0, 10000) / 100;
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
                    throw new ApplicationException("Random index application in HoldBallPositionalDecideLogic class, Start method.");
                }

                int angle = 0;
                int t = index * 30 - 90;
                if (t >= 0)
                {
                    angle = match.RandomInt32(t, t + 30 - 1);
                }
                else
                {
                    angle = match.RandomInt32(t + 360, t + 389);
                }

                double x = 0;
                double y = 0;

                if (_player.Side == Base.Enum.Side.Home)
                {
                    x = _player.Current.X + 20 * Math.Cos(angle * Math.PI / 180);
                    y = _player.Current.Y + 20 * Math.Sin(angle * Math.PI / 180);
                }
                else
                {
                    x = _player.Current.Mirror().X + 20 * Math.Cos(angle * Math.PI / 180);
                    y = _player.Current.Mirror().Y + 20 * Math.Sin(angle * Math.PI / 180);
                }

                Coordinate target = new Coordinate(x, y);
                return (_player.Side == Base.Enum.Side.Home) ? target : target.Mirror();
            }

            #region encapsulation
            private Attribute GetPlayerAttribute()
            {
                Attribute attr = new Attribute();

                #region 平均进攻和平均防守
                attr.AttackAverage = (_player.PropCore[PlayerProperty.Shooting] +
                    _player.PropCore[PlayerProperty.Strength] +
                    _player.PropCore[PlayerProperty.Dribble] +
                    _player.PropCore[PlayerProperty.Aggression]) / 4;

                attr.DefenceAverage = (_player.PropCore[PlayerProperty.Disturb] +
                    _player.PropCore[PlayerProperty.Interception]) / 2;
                #endregion                

                #region 职责
                if (_player.Input.AsPosition == Base.Enum.Position.Forward)
                {
                    attr.Duty = Duty.Forward;
                }

                if (_player.Input.AsPosition == Base.Enum.Position.Midfielder)
                {
                    attr.Duty = Duty.Middle;
                }

                if (_player.Input.AsPosition == Base.Enum.Position.Fullback)
                {
                    attr.Duty = Duty.Fullback;
                }
                #endregion

                Coordinate current = (_player.Side == Base.Enum.Side.Home) ? _player.Current : _player.Current.Mirror();

                #region 场上位置
                attr.CurrentPosition = PitchPosition.Middle;
                if (current.Y < 28)
                {
                    attr.CurrentPosition = PitchPosition.Up;
                }

                if (current.Y > 108)
                {
                    attr.CurrentPosition = PitchPosition.Down;
                }
                #endregion

                #region 朝向象限
                int angle = (_player.Side == Base.Enum.Side.Home)? _player.Status.Angle : _player.Status.Angle + 180;
                if (angle > 360)
                {
                    angle -= 360;
                }
                attr.SelfDirection = (int) (((angle + 90) % 360) / 30);
                #endregion

                #region 防守者所在的象限
                if (_player.Status.DefenceStatus.Defender == null)
                {
                    attr.EnemyDistrict = 9; // 身后不判断
                }
                else
                {
                    if (_player.Status.DefenceStatus.DefenderDistancePow> 100)
                    {
                        attr.EnemyDistrict = 9;
                    }
                    else
                    {

                        Coordinate enemy = (_player.Side == Base.Enum.Side.Home) ?
                            _player.Status.DefenceStatus.Defender.Current :
                            _player.Status.DefenceStatus.Defender.Current.Mirror();
                      
                        double dx = enemy.X - current.X;
                        double dy = enemy.Y - current.Y;

                        if (dx > 0 && dy <= 0) // 1区间
                        {
                            double value = Math.Abs(dy / dx);
                            if (value >= 1.73) // >= 3^0.5
                            {
                                attr.EnemyDistrict = 0;
                            }

                            if (value >= 0.57 && value < 1.73)  // 3^0.5 / 3 ~ 3^0.5
                            {
                                attr.EnemyDistrict = 1;
                            }

                            if (value >= 0 && value < 0.57) // 0 ~ 3^0.5 / 3
                            {
                                attr.EnemyDistrict = 2;
                            }
                        }

                        if (dx >= 0 && dy > 0) // 2区间
                        {
                            if (dx == 0)
                            {
                                attr.EnemyDistrict = 5;
                            }
                            else
                            {
                                double value = Math.Abs(dy / dx);
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
                            double value = Math.Abs(dy / dx);
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
                            if (dx == 0)
                            {
                                attr.EnemyDistrict = 11;
                            }
                            else
                            {
                                double value = Math.Abs(dy / dx);
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
                #endregion

                return attr;
            }

            /// <summary>
            /// 计算进攻奖励
            /// </summary>
            /// <returns></returns>
            private District CalcAttackPoint()
            {
                District district = new District();

                double attackPoint = _attribute.AttackAverage;
                district.L0 = attackPoint;
                district.L1 = attackPoint;
                district.L2 = attackPoint;
                district.L3 = attackPoint;
                district.L4 = attackPoint;
                district.L5 = attackPoint;

                double backPoint = Attribute.AttackMax - _attribute.AttackAverage;
                district.L6 = backPoint;
                district.L7 = backPoint;
                district.L8 = backPoint;
                district.L9 = backPoint;
                district.L10 = backPoint;
                district.L11 = backPoint;

                return district;
            }

            /// <summary>
            /// 计算防守奖励
            /// </summary>
            /// <returns></returns>
            private District CalcDefencePoint()
            {
                District district = new District();               
                district.L6 = _attribute.DefenceAverage;
                district.L7 = _attribute.DefenceAverage;
                district.L8 = _attribute.DefenceAverage / 2;
                district.L9 = district.L8;
                district.L10 = _attribute.DefenceAverage;
                district.L11 = _attribute.DefenceAverage;

                return district;
            }

            /// <summary>
            /// 前场进攻奖励
            /// </summary>
            /// <returns></returns>
            private District CalcAttackAward()
            {
                District district = new District();

                district.L0 = Attribute.AttackAward / 3;
                district.L1 = Attribute.AttackAward * 2 / 3;
                district.L2 = Attribute.AttackAward;
                district.L3 = district.L1;
                district.L4 = district.L0;

                return district;
            }

            /// <summary>
            /// 计算意识奖励
            /// </summary>
            /// <returns></returns>
            private District CalcMentalityAward()
            {
                District district = new District();
                double mentality = _player.PropCore[PlayerProperty.Mentality];

                if (_attribute.Duty == Duty.Forward)
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

                if (_attribute.Duty == Duty.Middle)
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

                if (_attribute.Duty == Duty.Fullback)
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

            /// <summary>
            /// 计算场上位置奖励
            /// </summary>
            /// <returns></returns>
            private District CalcPositionAward()
            {
                District district = new District();

                if (_attribute.CurrentPosition == PitchPosition.Middle)
                {
                    double douAttack = _attribute.AttackAverage * _attribute.AttackAverage;
                    district.L0 = 0;// douAttack / 4;
                    district.L1 = douAttack / 2;
                    district.L2 = douAttack;
                    district.L3 = district.L2;
                    district.L4 = district.L1;
                    district.L5 = district.L0;

                    district.L6 = 0;
                    district.L7 = -_attribute.AttackAverage / 2;
                    district.L8 = -_attribute.AttackAverage;
                    district.L9 = district.L8;
                    district.L10 = district.L7;
                    district.L11 = district.L6;
                }
                double accleration = _player.PropCore[PlayerProperty.Acceleration];
                double balance = _player.PropCore[PlayerProperty.Balance];
                if (_attribute.CurrentPosition == PitchPosition.Up)
                {
                    double douAccleration = accleration * accleration;
                    double douBalance = balance * balance;
                    double sumAccBal = accleration + balance;
                    district.L0 = -douBalance;
                    district.L1 = -douBalance;// douAccleration / 4;
                    district.L2 = douAccleration;// douAccleration / 2;
                    district.L3 = douAccleration*2;//sumAccBal * sumAccBal / 4;
                    district.L4 = 0;// sumAccBal* sumAccBal; //district.L3;
                    district.L5 = -douBalance;//douBalance / 2;

                    district.L6 = 0;//douBalance / 4;
                    district.L7 = 0;
                    district.L8 = -balance / 2;
                    district.L9 = -sumAccBal / 2;
                    district.L10 = district.L9;
                    district.L11 = -accleration / 2;
                }

                if (_attribute.CurrentPosition == PitchPosition.Down)
                {
                    double douAccleration = accleration * accleration;
                    double douBalance = balance * balance;
                    double sumAccBal = accleration + balance;
                    district.L0 = -douBalance;//douBalance / 2;
                    district.L1 = 0;// sumAccBal* sumAccBal;//sumAccBal * sumAccBal / 4;
                    district.L2 = douAccleration*2;//district.L1;
                    district.L3 = douAccleration;//douAccleration / 2;
                    district.L4 = -douBalance;//douAccleration / 4;
                    district.L5 = -douBalance;//0;

                    district.L6 = -accleration / 2;
                    district.L7 = -sumAccBal / 2;
                    district.L8 = district.L7;
                    district.L9 = -balance/ 2;
                    district.L10 = 0;
                    district.L11 = douBalance / 4;
                }

                return district;
            }

            /// <summary>
            /// 计算防守人惩罚
            /// </summary>
            /// <param name="current">当前的计算中间值</param>
            /// <returns></returns>
            private District CalcEnemyPenalty(District current)
            {
                District result = new District();
                double D = _player.PropCore[PlayerProperty.Dribble];
                double targetValue = D * D / 2;
                double allocation = 0;

                switch (_attribute.EnemyDistrict)
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

            /// <summary>
            /// 计算球员自我的朝向的影响
            /// </summary>
            /// <returns></returns>
            private District CalcSelfDirection()
            {
                District result = new District();
                int selfDirection = _attribute.SelfDirection;
                double aggression = _player.PropCore[PlayerProperty.Aggression];
                double balance = _player.PropCore[PlayerProperty.Balance];
                double A = aggression * aggression;
                double B = balance * balance;
                double forwardAward = A;
                double forwardAward1 = ((A * 2 / 3) + (B / 3)) / 2;
                double forwardAward2 = 0;// ((A / 3) + (B * 2 / 3)) / 2;

                double backPenelty = Attribute.BackPenelty;
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

            #endregion

            /// <summary>
            /// 移动的8方向象限
            /// </summary>
            private struct District
            {
                /// <summary>
                /// 重载了+操作符
                /// </summary>
                /// <param name="distric1"></param>
                /// <param name="distric2"></param>
                /// <returns></returns>
                public static District operator +(District distric1, District distric2)
                {
                    District d = new District();
                    d.L0 = distric1.L0 + distric2.L0;
                    d.L1 = distric1.L1 + distric2.L1;
                    d.L2 = distric1.L2 + distric2.L2;
                    d.L3 = distric1.L3 + distric2.L3;
                    d.L4 = distric1.L4 + distric2.L4;
                    d.L5 = distric1.L5 + distric2.L5;
                    d.L6 = distric1.L6 + distric2.L6;
                    d.L7 = distric1.L7 + distric2.L7;
                    d.L8 = distric1.L8 + distric2.L8;
                    d.L9 = distric1.L9 + distric2.L9;
                    d.L10 = distric1.L10 + distric2.L10;
                    d.L11 = distric1.L11 + distric2.L11;

                    return d;
                }

                public double L0;
                public double L1;
                public double L2;
                public double L3;
                public double L4;
                public double L5;
                public double L6;
                public double L7;
                public double L8;
                public double L9;
                public double L10;
                public double L11;

                public double Sum()
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
            }

            /// <summary>
            /// Represents the player's attribute.
            /// </summary>
            private struct Attribute
            {
                /// <summary>
                /// 平均的进攻值
                /// </summary>
                public double AttackAverage;

                /// <summary>
                /// 平均的防守值
                /// </summary>
                public double DefenceAverage;

                /// <summary>
                /// 进攻的最大值
                /// </summary>
                public const double AttackMax = Defines.Player.PositionalConfig.ATTACK_MAX;

                /// <summary>
                /// 进攻的奖励值
                /// </summary>
                public const double AttackAward = Defines.Player.PositionalConfig.ATTACK_AWARD;

                /// <summary>
                /// 职责
                /// </summary>
                public Duty Duty;

                /// <summary>
                /// 当前的位置
                /// </summary>
                public PitchPosition CurrentPosition;

                /// <summary>
                /// 防守者的坐标
                /// </summary>
                public int EnemyDistrict;

                /// <summary>
                /// 面朝方向
                /// </summary>
                public int SelfDirection;

                /// <summary>
                /// 进攻奖励
                /// </summary>
                public const double ForwardAward = Defines.Player.PositionalConfig.ForwardAward;

                /// <summary>
                /// 后退惩罚
                /// </summary>
                public const double BackPenelty = Defines.Player.PositionalConfig.BackPenelty;
            }
        }

        enum Duty
        {
            Forward = 0,
            Middle = 1,
            Fullback = 2
        }

        enum PitchPosition
        {
            Middle = 0,
            Up = 1,
            Down = 2
        }
    }
}
