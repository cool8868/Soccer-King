/*****************************************************************************
 * 文件名：Manager
 * 
 * 创建人：
 * 
 * 创建时间：2009-11-5 13:31:25
 * 
 * 功能说明：代表了经理实体对象
 *  
 * 历史修改记录：
 * 
 * ***************************************************************************/
using System;
using System.Collections.Generic;
using Games.NB.Match.Base;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Interface.Manager;
using Games.NB.Match.BLL.Rules.FreeKickRules;
using Games.NB.Match.Common;
using Games.NB.Match.Common.Random;
using Games.NB.Match.Log;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.Base.Model.TranOut;
using Games.NB.Match.Frame;
using Games.NB.Match.AI.States;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.SkillCore;
using SkillEngine.SkillImpl;
using SkillEngine.SkillImpl.Football;

namespace Games.NB.Match.BLL.Model.Creatures
{

    public sealed partial class Manager : Creature, IManager
    {

        #region Cache
        private Side _side;
        private readonly IMatch _match;
        private readonly ManagerInput _input;
        private readonly ManagerReport _report;
        private readonly IManagerStatus _status;
        private readonly Dictionary<Position, List<IPlayer>> _playerHash;
        private readonly List<IPlayer> _players = new List<IPlayer>(Defines.Match.MAX_PLAYER_COUNT);
        #endregion

        #region .ctor
        public Manager(ManagerInput input, IMatch match, Side side)
        {
            if (input == null)
            {
                throw new ApplicationException("Initializes a new manager by null TransferManagerEntity.");
            }
            if (input.Players.Count == 0)
            {
                throw new ApplicationException("The manager has no team members. Manager name:" + input.Name);
            }
            if (input.Players.Count != Defines.Match.MAX_PLAYER_COUNT)
            {
                throw new ApplicationException("The manager's team members count is not 11. Manager name:" + input.Name + ", Team member count:" + input.Players.Count);
            }
            this._input = input;
            this._match = match;
            this._side = side;
            this._status = new ManagerStatus();
            this._report = new ManagerReport(input);
            this._playerHash = new Dictionary<Position, List<IPlayer>>(4);
            _playerHash.Add(Position.Goalkeeper, new List<IPlayer>(8));
            _playerHash.Add(Position.Fullback, new List<IPlayer>(8));
            _playerHash.Add(Position.Midfielder, new List<IPlayer>(8));
            _playerHash.Add(Position.Forward, new List<IPlayer>(8));
            int formId = input.FormId;
            var formCfg = FormationCache.GetFormation(formId);
            int clientId = (_side == Side.Home) ? 0 : Defines.Match.MAX_PLAYER_COUNT;

            #region ISkill
            this.boostCore = new BoostCore(match);
            this.buffCore = new BuffCore(match);
            this.specBuffCoe = new SpecBuffCore();
            this.rootSkill = new Skill(_match, this, string.Empty);
            #endregion

            PlayerInput pInput = null;
            Player player = null;
            for (var i = 0; i < input.Players.Count; i++)
            {
                pInput = input.Players[i];
                var pForm = formCfg[i];
                pInput.Position = (byte)pForm.Position;
                player = new Player(pInput, this, (byte)(i + clientId), pForm.Default, pForm.HalfDefault);
                _players.Add(player);
                _playerHash[pForm.Position].Add(player);
            }
            this.SkillPlayerList = new List<SkillEngine.SkillBase.Xtern.ISkillPlayer>(_players.Count);
            this.SkillPlayerList.AddRange(_players);

            #region Buff
            int last = _match.RoundPerMinute * 90;
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
                    this.AddBuff(CreatePropBuff(last, point, percent, item.BuffId));
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
                    this.AddBoost(CreateBoostBuff(item.BoostType, last, point, percent, item.BuffId));
                }
            }
            #endregion
        }
        #endregion

        #region Data
        public Side Side
        {
            get { return this._side; }
        }
        public IMatch Match
        {
            get { return this._match; }
        }
        public IManager Opponent
        {
            get
            {
                if (this == _match.HomeManager)
                    return _match.AwayManager;
                return _match.HomeManager;
                //if (_side == Base.Enum.Side.Home)
                //{
                //    return _match.AwayManager;
                //}
                //else
                //{
                //    return _match.HomeManager;
                //}
            }
        }
        public IManagerStatus Status
        {
            get { return _status; }
        }
        public ManagerInput Input
        {
            get { return this._input; }
        }
        public ManagerReport Report
        {
            get { return this._report; }
        }
        public List<IPlayer> Players
        {
            get { return this._players; }
        }
        public IPlayer this[int index]
        {
            get { return this._players[index] as Player; }
        }
        public bool IsAttackSide
        {
            get
            {
                var ballHandler = _match.Status.BallHandler;
                if (null == ballHandler)
                    return false;
                return _side == ballHandler.Manager.Side;
            }
        }
        public int GoalScore { get; set; }
        #endregion

        #region RoundInit
        public void ChangeSide()
        {
            this._side = _side == Side.Home ? Side.Away : Side.Home;
            foreach (var player in _players)
            {
                player.Status.ChangeSide();
            }
        }
        public void SectionInit(int sectionNo)
        {
            if (sectionNo > 0)
            {
                this.ClearDisable();
                this.ChangeSide();
            }
            foreach (var player in _players)
            {
                player.SectionInit(sectionNo);
            }
            List<string> skills = null;
            if (null == _input.Skills)
                skills = new List<string>(2);
            else
            {
                skills = new List<string>(_input.Skills.Count + 2);
                skills.AddRange(_input.Skills);
            }
            if (null != _input.SubSkills && sectionNo < _input.SubSkills.Length)
            {
                var talent = _input.SubSkills[sectionNo];
                if (!string.IsNullOrEmpty(talent))
                    skills.Add(talent);
            }
            SkillFacade.LoadManagerSkills(_match, this, skills);
            if (sectionNo == 0)
                SkillFacade.LoadHideSkills(_match, this);
            
        }
        public void MinuteInit()
        {
            foreach (var player in _players)
            {
                player.MinuteInit();
            }
        }
        public void RoundInit()
        {
            RefreshManagerState();
            RefreshPlayerEdge();
            foreach (var player in _players)
            {
                player.RoundInit();
            }
        }
        void RefreshManagerState()
        {
            if (this.Match.Status.BallHandler.Side == this.Side)
            {
                this._status.StatStatus.DribbleRound++;
                this._status.StatStatus.SingleAttackDribbleRound++;
            }
            else
            {
                this._status.StatStatus.SingleAttackDribbleRound = 0;
            }
        }
        unsafe void RefreshPlayerEdge()
        {
            double* array = stackalloc double[_players.Count];
            int ex = -1;
            for (int i = 0; i < _players.Count; i++)
            {
                array[i] = ex;
                if (_players[i].Disable)
                    continue;
                if (_players[i].Input.AsPosition == Position.Goalkeeper)
                    continue;
                array[i] = _players[i].Current.X;
            }
            int idxMax = 0;
            int idxMin = 0;
            double max = 0;
            double min = 1000d;
            for (int i = 0; i < _players.Count; i++)
            {
                if (array[i] == ex)
                    continue;
                if (array[i] > max)
                {
                    max = array[i];
                    idxMax = i;
                }
                if (array[i] < min)
                {
                    min = array[i];
                    idxMin = i;
                }
            }
            if (_side == Base.Enum.Side.Home)
            {
                _status.HeadMostPlayer = _players[idxMax];
                _status.LastPlayer = _players[idxMin];
            }
            else
            {
                _status.HeadMostPlayer = _players[idxMin];
                _status.LastPlayer = _players[idxMax];
            }
        }
        #endregion

        #region Func
        public void ClearDisable()
        {
            foreach (var p in this._players)
            {
                if (p.Disable)
                    continue;
                p.RemoveBuff((int)EnumBlurType.LockMotion, 0);
                p.RemoveBuff((int)EnumBlurType.SpecMotion, 0);
                if (p.SkillCore.ModelId >= (short)EnumSkillModel.Stand)
                    p.SkillCore.AddShowModel(null, 0, 0);
            }
        }
        public void SetDoneState()
        {
            foreach (var p in this._players)
            {
                if (p.Disable)
                    continue;
                p.Status.SetDoneState(p.Status.PreState, SkillEngine.Extern.Enum.EnumDoneStateFlag.None); 
            }
        }
        public void SetState(IState state)
        {
            foreach (var p in this._players)
            {
                if (p.Disable)
                    continue;
                p.Status.State = state;
            }
        }
        public void Reset()
        {
            foreach (var p in this._players)
            {
                p.Reset();
            }
        }
        public List<IPlayer> GetPlayersByPosition(Position position)
        {
            return _playerHash[position];
        }
        #endregion

        #region Buff
        public IBuff CreatePropBuff(int last, int point, int percent, params int[] buffIds)
        {
            if (last > 0)
                last += _match.Status.Round;
            return new FootballPropBuff(RootSkill, buffIds)
            {
                Rate = SkillDefines.MAXStorePercent,
                Point = point,
                Percent = percent,
                TimeEnd = (short)last,
                Times = 1,
            };
        }
        public IBuff CreateBlurBuff(EnumBlurType blurType, EnumBlurBuffCode blurCode, int last)
        {
            if (last > 0)
                last += _match.Status.Round;
            return new FootballBlurBuff(RootSkill, blurType, blurCode)
            {
                Rate = SkillDefines.MAXStorePercent,
                TimeEnd = (short)last,
            };
        }
        public IBoostEffect CreateBoostBuff(int boostType, int last, int point, int percent, params int[] buffIds)
        {
            var boost = new BoostEffect(RootSkill, buffIds, (EnumBoostType)boostType, false, false, false);
            boost.Point = point;
            boost.Percent = percent;
            boost.Last = last;
            boost.TimeEnd = (short)last;
            boost.Repeat = 0;
            boost.Recycle = false;
            return boost;
        }
        #endregion

    }
}
