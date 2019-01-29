/********************************************************************************
 * 文件名：Player
 * 创建人：
 * 创建时间：2009-11-5 13:34:10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *  1.1 2009-12-8 9:59:33  Reflector the player entity.
 *  1.2 2010-1-23 15:26:31 Reflector the player's propertys
 * and the player's buildin attributes.
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using Games.NB.Match.AI.States;
using Games.NB.Match.Base;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Interface.Player;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.Base.Model.TranOut;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.BLL.Rules;
using Games.NB.Match.Common.Random;
using Games.NB.Match.Log;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.SkillCore;

namespace Games.NB.Match.BLL.Model.Creatures
{

    public sealed partial class Player : Creature, IPlayer
    {
        #region Cache
        private readonly byte _clientId;
        private readonly IManager _manager;
        private readonly IMatch _match;
        private readonly PropertyCore _propCore;
        private readonly IPlayerStatus _status;
        private readonly PlayerInput _input;
        private readonly PlayerReport _report;
        private readonly List<IPlayer> _passTargets = new List<IPlayer>(10);
        private string _toString = null;
        #endregion

        #region .ctor
        public Player(PlayerInput input, IManager manager, byte clientId, Coordinate curPoint, Coordinate halfDefault)
        {
            this._input = input;
            this._clientId = clientId;
            this._manager = manager;
            this._match = manager.Match;
            this._propCore = new PropertyCore(this);
            this._status = new PlayerStatus(this, curPoint, halfDefault);
            this._report = new PlayerReport(input, clientId);
            _report.ClientId = clientId;
            _report.Position = input.Position;
            _report.Name = input.FamilyName;

            #region ISkill
            this.boostCore = new BoostCore(this._match);
            this.buffCore = new BuffCore(this._match);
            this.specBuffCoe = new SpecBuffCore();
            #endregion

            #region Buff
            int last=_match.RoundPerMinute*90;
            int point = 0;
            int percent = 0;
            if (null != input.PropList)
            {
                foreach (var item in input.PropList)
                {
                    if (null == item.BuffId)
                        continue;
                    point = (int)(item.Point * 100);
                    percent = (int)(item.Percent * 10000);
                    this.AddBuff(_manager.CreatePropBuff(last, point, percent, item.BuffId));
                }
            }
            if (null != input.BoostList)
            {
                foreach (var item in input.BoostList)
                {
                    if (null == item.BuffId)
                        continue;
                    point = (int)(item.Point * 100);
                    percent = (int)(item.Percent * 10000);
                    this.AddBoost(_manager.CreateBoostBuff(item.BoostType, last, point, percent, item.BuffId));
                }
            }
            #endregion
        }
        public override string ToString()
        {
            if (string.IsNullOrEmpty(_toString))
                _toString = string.Format("[ClientId:{0},Team:{1},Position:{2}]", _clientId, this.Side, this._input.AsPosition);
            return _toString;
        }
        #endregion

        #region Data
        public Side Side
        {
            get { return this._manager.Side; }
        }
        public Guid Tid
        {
            get { return this._input.Tid; }
        }
        public byte ClientId
        {
            get { return _clientId; }
        }
        public IManager Manager
        {
            get { return this._manager; }
        }
        public IMatch Match
        {
            get { return this._match; }
        }
        public PropertyCore PropCore
        {
            get { return this._propCore; }
        }
        public IPlayerStatus Status
        {
            get { return this._status; }
        }
        public PlayerInput Input
        {
            get { return this._input; }
        }
        public PlayerReport Report
        {
            get { return this._report; }
        }
        public List<IPlayer> PassTargets
        {
            get { return _passTargets; }
        }
        #endregion

        #region RoundInit
        public void Init()
        {
            PlayerRule.InitPassTarget(this);
        }
        public void SectionInit(int sectionNo)
        {
            if (sectionNo <= 0)
                SkillEngine.SkillImpl.SkillFacade.LoadPlayerSkills(_match, this, _input.Skills);
            this._propCore.SectionInit(sectionNo);
        }
        public void MinuteInit()
        {
            this._propCore.ElapseMinute();
        }
        public void RoundInit()
        {
            if (DisableState == (int)SkillEngine.SkillBase.Enum.Football.EnumBlurBuffCode.Disable)
            {
                if (_status.Hasball)
                    _status.DefenceStatus.RefreshDefenders();
                return;
            }
            _status.RoundInit();
            RefreshEnableDecide();
            RefreshDisturbBuff();
        }
        private void RefreshEnableDecide()
        {
            if (this._status.NeedRedecide)
                return;
            if (_input.AsPosition == Position.Goalkeeper)
            {
                _status.Redecide();
                return;
            }
            if (_status.DestinationDistancePow <= 1) // each player has move to his destination will redecide.
            {
                _status.Redecide();
                return;
            }
            if (_status.IsAttackSide == false)
            {
                if (_status.BallDistancePow <= 6400)
                    _status.Redecide();
                return;
            }
            if (!_status.Hasball)
                return;
            if (_status.State is DefenceState||!_status.Holdball)
            {
                _status.Redecide();
                return;
            }
            var region = (Side == Side.Home) ? _match.Pitch.AwayShootRegion : _match.Pitch.HomeShootRegion;
            if (region.IsCoordinateInRegion(_status.Current))
            {
                _status.Redecide();
            }
        }
        private void RefreshDisturbBuff()
        {
            var defender = _status.DefenceStatus.Defender;
            if (!this.Status.Hasball || null == defender)
                return;
            double buffPer = 0;
            if (defender.SkillEnable && _status.DefenceStatus.DefenderDistancePow <= Math.Pow(defender.PropCore.GetActionRange(EnumBuffCode.DisturbRange), 2))
                buffPer += -defender.PropCore[PlayerProperty.Disturb] * 15;
            var defender2 = _status.DefenceStatus.HelpDefender;
            if (null != defender2 && defender != defender2 && defender2.SkillEnable)
            {
                if (_status.DefenceStatus.HelpDefenderDistancePow <= Math.Pow(defender2.PropCore.GetActionRange(EnumBuffCode.DisturbRange), 2))
                    buffPer += -defender2.PropCore[PlayerProperty.Disturb] * 15;
            }
            if (buffPer >= 0)
                return;
            double maxVal = buffPer * 0.5;
            double minVal = -6000;
            buffPer += this.PropCore[PlayerProperty.Balance] * 20;
            if (buffPer >= 0)
                return;
            if (buffPer > maxVal)
                buffPer = maxVal;
            if (buffPer < minVal)
                buffPer = minVal;
            this.AddDisturbBuff(1, (int)buffPer);
        }
        #region Old
        //private void RefreshDisturbBuff()
        //{
        //    var defender = _status.DefenceStatus.Defender;
        //    if (!this.Status.Hasball || null == defender)
        //        return;
        //    double buffPer = 0;
        //    if (defender.SkillEnable && _status.DefenceStatus.DefenderDistancePow <= Math.Pow(defender.PropCore.GetActionRange(EnumBuffCode.DisturbRange), 2))
        //        buffPer += -defender.PropCore[PlayerProperty.Disturb] * 25;
        //    var defender2 = _status.DefenceStatus.HelpDefender;
        //    if (null != defender2 && defender != defender2 && defender2.SkillEnable)
        //    {
        //        if (_status.DefenceStatus.HelpDefenderDistancePow <= Math.Pow(defender2.PropCore.GetActionRange(EnumBuffCode.DisturbRange), 2))
        //            buffPer += -defender2.PropCore[PlayerProperty.Disturb] * 5;
        //    }
        //    if (buffPer >= 0)
        //        return;
        //    buffPer += this.PropCore[PlayerProperty.Balance] * 20;
        //    if (buffPer >= 0)
        //        return;
        //    this.AddDisturbBuff(1, (int)buffPer);
        //}
        #endregion
        #endregion

        #region SetTarget
        public Zone GetTargetZone(Coordinate target)
        {
            if (target.X > (double)Defines.Pitch.MAX_WIDTH / 2)
            {
                return (this._manager.Side == Side.Home) ? Zone.OpposingHalf : Zone.OwnHalf;
            }
            else
            {
                return (this._manager.Side == Side.Home) ? Zone.OwnHalf : Zone.OpposingHalf;
            }
        }
        public void SetTarget(Coordinate target)
        {
            SetTarget(target.X, target.Y);
        }
        public void SetTarget(double x, double y)
        {
            _status.Destination = new Coordinate(x, y);
            DecideEnd();
        }
        public void SetTargetBall(bool isCurrent)
        {
            var football = (isCurrent) ? Match.Football.Current : Match.Football.Destination;
            SetTarget(football.X, football.Y);
        }
        public void SetHomeSideCoordinate(Coordinate target)
        {
            if (_manager.Side == Side.Away)
            {
                target = target.Mirror();
            }
            this._status.Current = target;
        }
        public bool IsCoordinateInPlayer(Coordinate coordinate)
        {
            return (_status.Current.SimpleDistance(coordinate) <= _status.Width * _status.Width);
        }
        #endregion

    }
}
