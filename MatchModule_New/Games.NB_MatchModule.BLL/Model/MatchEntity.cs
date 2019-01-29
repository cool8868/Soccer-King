/*****************************************************************************
 * 文件名：MatchEntity
 * 
 * 创建人：
 * 
 * 创建时间：2009-11-5 13:17:01
 * 
 * 功能说明：比赛实体对象
 *  
 * 历史修改记录：
 * 
 * ***************************************************************************/
using System;
using System.Collections.Generic;
using Games.NB.Match.AI.States;
using Games.NB.Match.Base;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.BLL.Model.Creatures;
using Games.NB.Match.BLL.Rules;
using Games.NB.Match.BLL.Rules.FreeKickRules;
using Games.NB.Match.Common.Random;
using Games.NB.Match.Log;
using Games.NB.Match.Common;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.Base.Model.TranOut;
using Games.NB.Match.AI.MatchStates;
using SkillEngine.SkillImpl;

namespace Games.NB.Match.BLL.Model
{
    public delegate void StatisticsCallback();

    public sealed partial class MatchEntity : IMatch
    {
      
        #region Cache
        private readonly IPitch _pitch = Pitchs.Pitch.New();
        private readonly IFootball _football;
        private readonly  IManager _homeManager;
        private readonly IManager _awayManager;
        private readonly MatchStatus _status = new MatchStatus();
        private readonly MatchInput _input;
        private readonly MatchReport _report;
        private Side _openSide;
        private StatisticsCallback _statisticsCallback;
        #endregion

        #region .ctor
        public MatchEntity(MatchInput input, StatisticsCallback statisticsCallback)
            : this(input)
        {
            this._statisticsCallback = statisticsCallback;
        }
        public MatchEntity(MatchInput input)
        {
            _status.Round = -1;
            _status.Minute = -1;
            //_status.TotalRound = Convert.ToInt32(input.TranTime / Defines.Match.ROUND_TIME);
            _status.TotalRound = 2*Convert.ToInt32(input.TranTime / Defines.Match.ROUND_TIME);
            input.HomeManager.BalanceProp();
            input.AwayManager.BalanceProp();
            this._input = input;
            this._football = new Football(this);
            this._homeManager = new Manager(input.HomeManager, this, Side.Home);
            this._awayManager = new Manager(input.AwayManager, this, Side.Away);
            this._openSide = this.RandomBool() ? Side.Home : Side.Away;
            this._report = new MatchReport(input);
        }
        #endregion

        #region Data
        public IPitch Pitch
        {
            get { return this._pitch; }
        }
        public IFootball Football
        {
            get { return this._football; }
        }
        public IManager[] Managers
        {
            get { return new[] { this._homeManager, this._awayManager }; }
        }
        public IManager HomeManager
        {
            get { return this._homeManager; }
        }
        public IManager AwayManager
        {
            get { return this._awayManager; }
        }
        public MatchStatus Status
        {
            get { return this._status; }
        }
        public MatchInput Input
        {
            get { return this._input; }
        }
        public MatchReport Report
        {
            get { return this._report; }
        }
        public int HomeScore
        {
            get { return _homeManager.GoalScore; }
        }
        public int AwayScore
        {
            get { return _awayManager.GoalScore; }
        }
        #endregion

        #region RoundInit
        public void SessionInit()
        {
            double max = 0;
            foreach (var m in this.Managers)
            {
                foreach (var p in m.Players)
                {
                    if (max < p.PropCore.MaxPropValue)
                        max = p.PropCore.MaxPropValue;
                }
            }
            foreach (var m in this.Managers)
            {
                foreach (var p in m.Players)
                {
                    p.PropCore.BalanceProp(max);
                    p.Init();
                }
            }
        }
        public void RoundInit()
        {
            _status.MatchState = _football.IsInAir ? AirBallState.Instance : DefaultBallState.Instance;
            _status.GoalState = false;
            _status.FoulState = EnumMatchFoulState.None;
            _status.FoulPlayer = null;
            _status.Break(EnumMatchBreakState.None);
            _football.TurnFlag = false;
            short round = _status.Round;
            int minute = MatchRule.ConvertRound2Minute(round, _status.TotalRound);
            int sectionNo = -1;
            bool minuteFlag = false;
            if (round == 0)
            {
                sectionNo = 0;
                minuteFlag = true;
            }
            else if (_status.Minute != minute)
            {
                minuteFlag = true;
                if (minute == 46)
                    sectionNo = 1;
            }
            if (sectionNo >= 0)
            {
                _status.SectionNo = sectionNo;
                HomeManager.SectionInit(sectionNo);
                AwayManager.SectionInit(sectionNo);
                SkillFacade.TriggerManagerSkills(HomeManager, 0);
                SkillFacade.TriggerManagerSkills(AwayManager, 0);
                foreach (var player in HomeManager.Players)
                {
                    SkillFacade.TriggerPlayerSkills(player, 0);
                }
                foreach (var player in AwayManager.Players)
                {
                    SkillFacade.TriggerPlayerSkills(player, 0);
                }
            }
            if (minuteFlag)
            {
                _status.Minute = (short)minute;
                HomeManager.MinuteInit();
                AwayManager.MinuteInit();
            }
            if (sectionNo >= 0)
            {
                _openSide = _openSide == Side.Home ? Side.Away : Side.Home;
                _status.Break(EnumMatchBreakState.SectionOpen);
                Openball(_openSide == Side.Home ? _homeManager : _awayManager);
            }
            if (null != _status.BallHandler && _status.BallHandler.Disable)
                _status.IsNoBallHandler = true;
            if (_status.IsNoBallHandler)
                RefreshBallHandler();
            if (HomeManager.IsAttackSide)
            {
                AwayManager.RoundInit();
                HomeManager.RoundInit();
            }
            else
            {
                HomeManager.RoundInit();
                AwayManager.RoundInit();
            }
        }
        unsafe void RefreshBallHandler()
        {
            double* distances = stackalloc double[22];
            List<IPlayer> parray = new List<IPlayer>(22);
            foreach (IManager m in Managers)
            {
                foreach (IPlayer p in m.Players)
                {
                    if (!p.SkillEnable)
                        continue;
                    distances[parray.Count] = p.Current.SimpleDistance(Football.Current);
                    parray.Add(p);
                }
            }
            if (parray.Count == 0)
                return;
            IPlayer ballhandler = parray[Utility.GetDoubleMinIndexQuick(distances, parray.Count)];
            ballhandler.Status.Hasball = true;
            if (ballhandler.Status.Holdball)
                _status.IsNoBallHandler = false;
        }
        #endregion

        #region Func
        public void Openball(IManager manager)
        {
            OpenballRule.Start(manager);
        }
        public void GKOpenball(IManager manager)
        {
            GkOpenballRule.Start(manager);
        }
        public void Goal(IManager manager)
        {
            manager.GoalScore++;
            _status.MatchState = GoalState.Instance;
            _status.GoalState = true;
            _status.Break(EnumMatchBreakState.ShootInto);
            this.SaveRpt();
            _homeManager.ClearDisable();
            _awayManager.ClearDisable();
            _status.Round++;
            this.RoundInit();
            //经理技能
            _homeManager.SetState(IdleState.Instance);
            _awayManager.SetState(IdleState.Instance);
            SkillEngine.SkillImpl.SkillFacade.TriggerManagerSkills(_homeManager, 0);
            SkillEngine.SkillImpl.SkillFacade.TriggerManagerSkills(_awayManager, 0);
            OpenballRule.Start(manager.Opponent);
        }
        public void MissGoal(IManager manager,bool openFlag)
        {
            _homeManager.ClearDisable();
            _awayManager.ClearDisable();
            if (!openFlag)
            {
                SkillEngine.SkillImpl.SkillFacade.TriggerManagerSkills(_homeManager, 0);
                SkillEngine.SkillImpl.SkillFacade.TriggerManagerSkills(_awayManager, 0);
                return;
            }
            this.SaveRpt();
            _status.Round++;
            this.RoundInit();
            //经理技能
            _homeManager.SetState(IdleState.Instance);
            _awayManager.SetState(IdleState.Instance);
            SkillEngine.SkillImpl.SkillFacade.TriggerManagerSkills(_homeManager, 0);
            SkillEngine.SkillImpl.SkillFacade.TriggerManagerSkills(_awayManager, 0);
            GkOpenballRule.Start(manager.Opponent);
        }
        public void Foul(IPlayer player, bool openFlag)
        {
            _status.FoulState = EnumMatchFoulState.Foul;
            _status.FoulPlayer = player;
            _status.Break(EnumMatchBreakState.Fouled);
            if (openFlag)
                this.SkillSkip();
        }
        public void OutBorder(IManager manager)
        {
            this.SaveRpt();
            FreeKickRuleFactory.Instance.Start(manager.Opponent, _football.Current);
        }
        public bool PassOutside(IPlayer player)
        {
            if (_football.IsInAir || !player.PropCore.CanSteal())
                return false;
            IPlayer opp = player.OppSkillPlayer as IPlayer;
            var ballHandler = _status.BallHandler;
            if (null != ballHandler && ballHandler != player)
                ballHandler.Status.ForceState(AI.States.IdleState.Instance);
            player.Status.Hasball = true;
            player.Rotate(_football.Current);
            player.MoveTo(_football.Current);
            this.SaveRpt();
            _status.Round++;
            this.RoundInit();
            player.Status.State = AI.States.Pass.LongPassState.Instance;
            var x = player.Current.X;
            var y = (player.Current.Y < Defines.Pitch.MID_HEIGHT) ? -20 : Defines.Pitch.MAX_HEIGHT + 20;
            _football.Kick(new Coordinate(x, y), 50, 45, player);
            this.SaveRpt();
            foreach (var p in player.Manager.Opponent.Players)
            {
                if (!p.SkillEnable)
                    continue;
                p.Status.ForceState(AI.States.IdleState.Instance);
                if (p.Input.AsPosition == Position.Goalkeeper)
                    continue;
                if (null == opp)
                    opp = p;
            }
            foreach (var p in player.Manager.Players)
            {
                if (!p.SkillEnable)
                    continue;
                if (p.ClientId != player.ClientId)
                    p.Status.ForceState(AI.States.IdleState.Instance);
            }
            opp.Status.Hasball = true;
            return true;
        }
        #endregion
     
    }
}
