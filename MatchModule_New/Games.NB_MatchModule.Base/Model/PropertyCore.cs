using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model.TranIn;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;

namespace Games.NB.Match.Base.Model
{
    public class PropertyCore
    {
        #region Cache
        public static readonly int[] ALLPropertyIds = { PlayerProperty.Speed, PlayerProperty.Shooting, PlayerProperty.FreeKick, PlayerProperty.Balance,
                                          PlayerProperty.Stamina, PlayerProperty.Bounce, PlayerProperty.Aggression, PlayerProperty.Disturb,
                                          PlayerProperty.Interception, PlayerProperty.Dribble, PlayerProperty.Passing, PlayerProperty.Mentality,
                                          PlayerProperty.Reflexes, PlayerProperty.Positioning, PlayerProperty.Handling, PlayerProperty.Acceleration,PlayerProperty.Strength,
                                          PlayerProperty.PassChooseRate,PlayerProperty.DribbleChooseRate,PlayerProperty.ShootChooseRate,PlayerProperty.StealChooseRate,
                                          PlayerProperty.PassSuccRate,PlayerProperty.DribbleSuccRate,PlayerProperty.ShootSuccRate,PlayerProperty.StealSuccRate,PlayerProperty.DiveSuccRate,
                                          PlayerProperty.TurnStealRate,PlayerProperty.ShootRange,PlayerProperty.DefenceRange,PlayerProperty.DisturbRange,PlayerProperty.StealRange};
        public static readonly int[] BASEPropertyIds = {PlayerProperty.Speed, PlayerProperty.Shooting, PlayerProperty.FreeKick, PlayerProperty.Balance,
                                          PlayerProperty.Stamina, PlayerProperty.Bounce, PlayerProperty.Aggression, PlayerProperty.Disturb,
                                          PlayerProperty.Interception, PlayerProperty.Dribble, PlayerProperty.Passing, PlayerProperty.Mentality,
                                          PlayerProperty.Reflexes, PlayerProperty.Positioning, PlayerProperty.Handling, PlayerProperty.Acceleration,PlayerProperty.Strength };
        public static readonly int[] ExPropertyIds ={PlayerProperty.PassChooseRate,PlayerProperty.DribbleChooseRate,PlayerProperty.ShootChooseRate,PlayerProperty.StealChooseRate,
                                          PlayerProperty.PassSuccRate,PlayerProperty.DribbleSuccRate,PlayerProperty.ShootSuccRate,PlayerProperty.StealSuccRate,PlayerProperty.DiveSuccRate,
                                          PlayerProperty.TurnStealRate,PlayerProperty.ShootRange,PlayerProperty.DefenceRange,PlayerProperty.DisturbRange,PlayerProperty.StealRange};
        readonly IPlayer _player;
        readonly Dictionary<int, double> _rawProperty;
        readonly Dictionary<int, double> _curProperty;
        readonly Dictionary<int, double> _lostProperty;
        readonly PropertyTraceCore _traceCore;
        double _maxPropValue = 0;
        const int RESETPropValue = -99;
        const int MINPropValue = 5;
        public const int LIMITPropValue = 225;
        double _unitStaminaRate;
        double _lostStaminaRate;
        double _minSpeedValue = 5;
        double _minStrength = 5;
        #endregion

        #region .ctor
        public PropertyCore(IPlayer player)
        {
            _player = player;
            _rawProperty = CreatePropDic();
            _curProperty = CreatePropDic();
            _lostProperty = CreatePropDic();
            this.InitProp(player.Input);
            if (PropertyTraceCore.TRACEBuffFlag)
                _traceCore = new PropertyTraceCore(_player);
        }
        void InitProp(PlayerInput srcInput)
        {
            _rawProperty[PlayerProperty.Speed] = srcInput.Speed;
            _rawProperty[PlayerProperty.Shooting] = srcInput.Shooting;
            _rawProperty[PlayerProperty.FreeKick] = srcInput.FreeKick;
            _rawProperty[PlayerProperty.Balance] = srcInput.Balance;
            _rawProperty[PlayerProperty.Stamina] = srcInput.Stamina;
            _rawProperty[PlayerProperty.Bounce] = srcInput.Strength;
            _rawProperty[PlayerProperty.Aggression] = srcInput.Aggression;
            _rawProperty[PlayerProperty.Disturb] = srcInput.Disturb;
            _rawProperty[PlayerProperty.Interception] = srcInput.Interception;
            _rawProperty[PlayerProperty.Dribble] = srcInput.Dribble;
            _rawProperty[PlayerProperty.Passing] = srcInput.Passing;
            _rawProperty[PlayerProperty.Mentality] = srcInput.Mentality;
            _rawProperty[PlayerProperty.Reflexes] = srcInput.Reflexes;
            _rawProperty[PlayerProperty.Positioning] = srcInput.Positioning;
            _rawProperty[PlayerProperty.Handling] = srcInput.Handling;
            _rawProperty[PlayerProperty.Acceleration] = srcInput.Acceleration;
            _rawProperty[PlayerProperty.Strength] = srcInput.Stamina;
            switch (srcInput.AsPosition)
            {
                case Position.Fullback:
                    _rawProperty[PlayerProperty.PassChooseRate] = Defines.Player.Fullback.PASS_PROBABILITY;
                    break;
                case Position.Midfielder:
                    _rawProperty[PlayerProperty.PassChooseRate] = Defines.Player.Midfielder.PASS_PROBABILITY;
                    break;
                case Position.Forward:
                    _rawProperty[PlayerProperty.PassChooseRate] = Defines.Player.Forward.PASS_PROBABILITY;
                    break;
            }
            _rawProperty[PlayerProperty.DribbleChooseRate] = 100 - _rawProperty[PlayerProperty.PassChooseRate];
            _rawProperty[PlayerProperty.DefenceRange] = Defines.Player.DEFENCE_RANGE;
            _rawProperty[PlayerProperty.DisturbRange] = Defines.Player.DISTURB_RANGE;
            _rawProperty[PlayerProperty.StealRange] = Defines.Player.STEAL_RANGE;
            double maxVal = 0;
            double val = 0;
            foreach (var propId in BASEPropertyIds)
            {
                val = _rawProperty[propId];
                _curProperty[propId] = val;
                _lostProperty[propId] = val;
                if (maxVal < val)
                    maxVal = val;
            }
            this._maxPropValue = maxVal;
        }
        #endregion

        #region Facade
        public void SectionInit(int sectionNo)
        {
            this.sectionInit4AbsLost(sectionNo);
        }
        public void ElapseMinute()
        {
            this.elapseMinute4AbsLost();
        }
        public double MaxPropValue
        {
            get
            {
                return this._maxPropValue;
            }
        }
        public void BalanceProp(double max)
        {
            double val = 0;
            foreach (var propId in BASEPropertyIds)
            {
                val = _rawProperty[propId] / max * LIMITPropValue;
                _rawProperty[propId] = val;
                _curProperty[propId] = val;
            }
            _minSpeedValue = _rawProperty[PlayerProperty.Speed] * 0.8;
            _minStrength = _rawProperty[PlayerProperty.Strength] * 0.8;
        }
        public static void BalanceProp(params PropertyCore[] props)
        {
            if (null == props || props.Length == 0)
                return;
            double max = 0;
            foreach (var item in props)
            {
                if (max < item.MaxPropValue)
                    max = item.MaxPropValue;
            }
            if (max <= LIMITPropValue)
                return;
            foreach (var item in props)
            {
                item.BalanceProp(max);
            }
        }
        public PropertyTraceCore TraceCore
        {
            get { return _traceCore; }
        }
        #endregion

        #region Retrive
        public double GetPropValue(EnumBuffCode buffCode, bool buffFlag)
        {
            return this[(int)buffCode, 0, 0, buffFlag];
        }
        public double GetActionRate(EnumBuffCode buffCode, double rawVal)
        {
            return this[(int)buffCode, rawVal, -1, true];
        }
        public double GetActionRange(EnumBuffCode buffCode)
        {
            return this[(int)buffCode, 0, -1, true];
        }
        public double GetBuffValue(EnumBuffCode buffCode, double rawVal = 0, double curVal = 0)
        {
            return this[(int)buffCode, rawVal, curVal, true];
        }
        public double this[int propId, double rawVal = 0, double curVal = 0, bool buffFlag = true]
        {
            get
            {
                return getValue4AbsLost(propId, rawVal, curVal, buffFlag);
            }
        }
        #endregion

        #region StaminaLost
        void sectionInit4PerLost(int sectionNo)
        {
            double start;
            double end;
            switch (sectionNo)
            {
                case 0:
                    start = Defines.Match.STAMINA_FIRST_HALF_START;
                    end = 80 + _curProperty[PlayerProperty.Stamina] * 0.1;
                    this._lostStaminaRate = 0;
                    break;
                default:
                    start = Defines.Match.STAMINA_SECOND_HALF_START;
                    end = 70 + _curProperty[PlayerProperty.Stamina] * 0.125;
                    this._lostStaminaRate = 1 - start / 100;
                    break;
            }
            if (end > start)
                end = start;
            this._unitStaminaRate = (double)(start - end) / 4500;
        }
        void elapseMinute4PerLost()
        {
            double total = this[(int)EnumBuffCode.StaminaLossSpeed, _unitStaminaRate, -1, true];
            total += _lostStaminaRate;
            this._lostStaminaRate = Math.Max(0, Math.Min(0.9, total));
            foreach (var propId in BASEPropertyIds)
            {
                switch (propId)
                {
                    case PlayerProperty.Stamina:
                    case PlayerProperty.Acceleration:
                        break;
                    case PlayerProperty.Speed:
                        _curProperty[propId] = _rawProperty[propId] * (1 - _lostStaminaRate);
                        break;
                    default:
                        _curProperty[propId] = RESETPropValue;
                        break;
                }
            }
        }
        double getValue4PerLost(int propId, double rawVal = 0, double curVal = 0, bool buffFlag = true)
        {
            int buffId = CastProp2BuffId(propId);
            int cPropId = CastBuff2PropId(propId);
            if (rawVal < 0)
                rawVal = 0;
            else if (rawVal == 0)
                _rawProperty.TryGetValue(cPropId, out rawVal);
            if (curVal < 0)
                curVal = rawVal;
            else if (curVal == 0)
            {
                _curProperty.TryGetValue(cPropId, out curVal);
                if (curVal == RESETPropValue)
                {
                    curVal = rawVal * (1 - _lostStaminaRate);
                    _curProperty[cPropId] = curVal;
                }
            }
            IBuff buff = null;
            double finalVal = curVal;
            if (buffFlag && _player.TryGetBuff(buffId, ref buff))
                finalVal = curVal + rawVal * buff.AsPercent + buff.AsPoint;
            if (null != _traceCore && _player.Match.Status.Round > 0)
            {
                if (null == buff)
                    _traceCore.Trace(cPropId, finalVal, curVal, 0, 0);
                else
                    _traceCore.Trace(cPropId, finalVal, curVal, buff.AsPercent, buff.AsPoint);
            }
            return finalVal;
        }
        void sectionInit4AbsLost(int sectionNo)
        {
            double stamina = _curProperty[PlayerProperty.Stamina];
            //this._unitStaminaRate = 20 / stamina;
            this._unitStaminaRate = 20 / stamina + 0.5;
            double rawVal = 0;
            double curVal = 0;
            foreach (var propId in BASEPropertyIds)
            {
                if (sectionNo == 0)
                {
                    _lostProperty[propId] = _rawProperty[propId];
                    continue;
                }
                switch (propId)
                {
                    case PlayerProperty.Stamina:
                    case PlayerProperty.Acceleration:
                        break;
                    default:
                        rawVal = _rawProperty[propId];
                        curVal = Math.Max(MINPropValue, rawVal - _lostStaminaRate);
                        curVal+=(rawVal - curVal) * (stamina * 0.2 / 100);
                        _lostProperty[propId] = curVal;
                        _curProperty[propId] = curVal;
                        break;
                }
            }
            if (sectionNo == 0)
                this._lostStaminaRate = 0;
            else
                this._lostStaminaRate = -1;
            _rawProperty[PlayerProperty.ShootRange] = this.ShootRangeRaw();
        }
        void elapseMinute4AbsLost()
        {
            if (this._lostStaminaRate < 0)
            {
                this._lostStaminaRate = 0;
                return;
            }
            double stamina =getValue4AbsLost(PlayerProperty.Stamina,0,-1,true);
            //this._unitStaminaRate = 20 / stamina;
            this._unitStaminaRate = 20 / stamina + 0.5;
            _lostStaminaRate += this[(int)EnumBuffCode.StaminaLossSpeed, _unitStaminaRate, -1, true];
            foreach (var propId in BASEPropertyIds)
            {
                switch (propId)
                {
                    case PlayerProperty.Stamina:
                        _curProperty[propId] = stamina;
                        break;
                    case PlayerProperty.Speed:
                        //_curProperty[propId] = Math.Max(MINPropValue, _lostProperty[propId] - _lostStaminaRate);
                        double val = _curProperty[propId];
                        if (val < _minSpeedValue)
                            _curProperty[propId] = _minSpeedValue;
                        else if (val > _minSpeedValue)
                            _curProperty[propId] = Math.Max(_minSpeedValue, _lostProperty[propId] - _lostStaminaRate);
                        break;
                    case PlayerProperty.Acceleration:
                        break;
                    default:
                        _curProperty[propId] = RESETPropValue;
                        break;
                }
            }
        }
        double getValue4AbsLost(int propId, double rawVal = 0, double curVal = 0, bool buffFlag = true)
        {
            int buffId = CastProp2BuffId(propId);
            int cPropId = CastBuff2PropId(propId);
            if (rawVal < 0)
                rawVal = 0;
            else if (rawVal == 0)
                _rawProperty.TryGetValue(cPropId, out rawVal);
            if (curVal < 0)
                curVal = rawVal;
            else if (curVal == 0)
            {
                _curProperty.TryGetValue(cPropId, out curVal);
                if (curVal == RESETPropValue)
                {
                    _lostProperty.TryGetValue(cPropId, out curVal);
                    if (cPropId == PlayerProperty.Strength)
                        curVal = Math.Max(_minStrength, curVal - _lostStaminaRate);
                    else
                        curVal = Math.Max(MINPropValue, curVal - _lostStaminaRate);
                    _curProperty[cPropId] = curVal;
                }
            }
            IBuff buff = null;
            double finalVal = curVal;
            if (buffFlag && _player.TryGetBuff(buffId, ref buff))
                finalVal = curVal + rawVal * buff.AsPercent + buff.AsPoint;
            if (null != _traceCore && _player.Match.Status.Round > 0)
            {
                if (null == buff)
                    _traceCore.Trace(cPropId, finalVal, curVal, 0, 0);
                else
                    _traceCore.Trace(cPropId, finalVal, curVal, buff.AsPercent, buff.AsPoint);
            }
            return finalVal;
        }
        #endregion

        #region PropCompute
        public int ShootRangeRaw()
        {
            return (int)(25 + Math.Pow(this[PlayerProperty.Strength, 0, 0, false], 0.5));
        }
        public bool CanShoot(Side side)
        {
            if (_player.Current.Y <= 20 || _player.Current.Y >= Defines.Pitch.MID_HEIGHT - 20)
                return false;
            double x = _player.Current.X - Defines.Pitch.MID_WIDTH;
            x *= (side == Side.Home ? -1 : 1);
            int range = Defines.Pitch.MID_WIDTH - (int)GetActionRange(EnumBuffCode.ShootRange);
            return x >= range;
        }
        public bool CanHeadShoot(Side side)
        {
            double x = _player.Current.X - Defines.Pitch.MID_WIDTH;
            x *= (side == Side.Home ? -1 : 1);
            int range = Defines.Pitch.MID_WIDTH - 38;
            return x >= range;
        }
        public bool CanHeading(IPlayer target)
        {
            if (null == target)
                return false;
            double difBall = _player.Status.BallDistancePow;
            if (difBall > 20)
                return false;
            return target.Status.BallDistancePow <= 20;
        }
        public bool CanSteal()
        {
            double val = _player.Status.BallDistance;
            return val <= 25 && val < GetActionRange(EnumBuffCode.StealRange);
        }
        public double ShootChooseRate()
        {
            double rawVal = 10 + (1 - Math.Pow(1 - this[PlayerProperty.Shooting] / 250, 0.25)) * 100;
            return this[(int)EnumBuffCode.ShootChooseRate, rawVal, -1, true];
        }
        public double PassChooseRate(double rawVal)
        {
            return this[(int)EnumBuffCode.PassChooseRate, rawVal, -1, true];
        }
        public double DribbleChooseRate()
        {
            return this[(int)EnumBuffCode.DribbleChooseRate, 0, -1, true];
        }
        public double StealChooseRate()
        {
            double rawVal = 1d;
            switch (_player.Input.AsPosition)
            {
                case Position.Fullback:
                    rawVal = 1.25;
                    break;
                case Position.Forward:
                    rawVal = 0.25;
                    break;
            }
            rawVal *= this[PlayerProperty.Aggression] * 0.3;
            return this[(int)EnumBuffCode.StealChooseRate, rawVal, -1, true];
        }
        #endregion

        #region Native
        static Dictionary<int, double> CreatePropDic()
        {
            var dic = new Dictionary<int, double>(ALLPropertyIds.Length);
            foreach (var key in ALLPropertyIds)
            {
                dic[key] = 0;
            }
            return dic;
        }
        public static int CastProp2BuffId(int propId)
        {
            if (propId == PlayerProperty.Strength)
                return (int)EnumBuffCode.Stamina;
            return propId < 20 ? (propId + 1000) : propId;
        }
        public static int CastBuff2PropId(int buffId)
        {
            return buffId < 1020 ? buffId % 1000 : buffId;
        }
        #endregion

    }
}
