using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Model.TranOut;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Emulator.WPF.Entity.Batch;
using Games.NB.Match.Emulator.WPF.Mgrs;

namespace Games.NB.Match.Emulator.WPF.Entity
{
    public class BatchManagerEntity
    {
        private bool _isHome;
        private MatchReport _match;
        private ManagerReport _homeReport;
        private ManagerReport _awayReport;
//        你每一回合结束前 调用
//Player.PropCore[Games.NB.Match.Base.Model.PlayerProperty] 数据保存起来就行了
//这个PlayerProperty里有现在所有的BuffId

        #region .ctor
        public BatchManagerEntity()
        {

            this.TotalShoot=new BatchShootEntity();
            this.Shoot = new BatchShootEntity();
            this.BicycleShot = new BatchShootEntity();
            this.RebelShoot = new BatchShootEntity();
            this.VolleyShoot = new BatchShootEntity();

            this.TotalPass = new BatchRateEntity();
            this.LongPass = new BatchRateEntity();
            this.ShortPass = new BatchRateEntity();
            this.HeadPass = new BatchRateEntity();

            this.Foul = new BatchFoulEntity();
            this.GoalKeep = new BatchRateEntity();
            this.Breakthrough = new BatchRateEntity();
            this.Steal = new BatchRateEntity();
        }

        public BatchManagerEntity(MatchReport match,int round, bool isHome)
            :this()
        {
            TotalRound = round;
            _isHome = isHome;
            _match = match;
            if (_isHome)
            {
                Score = match.HomeScore;
                TotalRound = round;
                _homeReport = match.HomeManager;
                _awayReport = match.AwayManager;
            }
            else
            {
                Score = match.AwayScore;
                _homeReport = match.AwayManager;
                _awayReport = match.HomeManager;
            }
            BuildData();
        }
        #endregion

        #region Statistics
        public void CalAvg(int totalCount)
        {
            this.TotalRound = TotalRound/totalCount;
            this.Score = Score/totalCount;
            this.ControlRound = ControlRound / totalCount;

            this.TotalShoot.CalAvg(totalCount);
            this.Shoot.CalAvg(totalCount);
            this.BicycleShot.CalAvg(totalCount);
            this.RebelShoot.CalAvg(totalCount);
            this.VolleyShoot.CalAvg(totalCount);

            this.TotalPass.CalAvg(totalCount);
            this.LongPass.CalAvg(totalCount);
            this.ShortPass.CalAvg(totalCount);
            this.HeadPass.CalAvg(totalCount);

            this.Foul.CalAvg(totalCount);
            this.GoalKeep.CalAvg(totalCount);
            this.InjuredTimes = InjuredTimes/totalCount;
            this.Breakthrough.CalAvg(totalCount);
            this.Steal.CalAvg(totalCount);
        }

        public void AddData(BatchManagerEntity manager)
        {
            this.TotalRound += manager.TotalRound;
            this.Score += manager.Score;
            this.ControlRound +=manager.ControlRound;

            this.TotalShoot.AddData(manager.TotalShoot);
            this.Shoot.AddData(manager.Shoot);
            this.BicycleShot.AddData(manager.BicycleShot);
            this.RebelShoot.AddData(manager.RebelShoot);
            this.VolleyShoot.AddData(manager.VolleyShoot);

            this.TotalPass.AddData(manager.TotalPass);
            this.LongPass.AddData(manager.LongPass);
            this.ShortPass.AddData(manager.ShortPass);
            this.HeadPass.AddData(manager.HeadPass);

            this.Foul.AddData(manager.Foul);
            this.GoalKeep.AddData(manager.GoalKeep);
            this.InjuredTimes += manager.InjuredTimes;
            this.Breakthrough.AddData(manager.Breakthrough);
            this.Steal.AddData(manager.Steal);
        }
        #endregion

        #region Fields
        public int TotalRound { get; set; }

        public int Score { get; set; }

        /// <summary>
        /// 1
        /// </summary>
        public int ControlRound { get; set; }

        /// <summary>
        /// 总射门2
        /// </summary>
        public BatchShootEntity TotalShoot { get; set; }

        /// <summary>
        /// 射门3
        /// </summary>
        public BatchShootEntity Shoot { get; set; }
        /// <summary>
        /// 大力射门4
        /// </summary>
        public BatchShootEntity VolleyShoot { get; set; }
        /// <summary>
        /// 乌龙射门5
        /// </summary>
        public BatchShootEntity RebelShoot { get; set; }
        /// <summary>
        /// 倒挂金钩射门6
        /// </summary>
        public BatchShootEntity BicycleShot { get; set; }
        /// <summary>
        /// 扑救7
        /// </summary>
        public BatchRateEntity GoalKeep { get; set; }

        /// <summary>
        /// 总传球8
        /// </summary>
        public BatchRateEntity TotalPass { get; set; }
        /// <summary>
        /// 短传9
        /// </summary>
        public BatchRateEntity ShortPass { get; set; }
        /// <summary>
        /// 长传10
        /// </summary>
        public BatchRateEntity LongPass { get; set; }
        /// <summary>
        /// 头球11
        /// </summary>
        public BatchRateEntity HeadPass { get; set; }
        /// <summary>
        /// 抢断12
        /// </summary>
        public BatchRateEntity Steal { get; set; }
        /// <summary>
        /// 过人13
        /// </summary>
        public BatchRateEntity Breakthrough { get; set; }
        /// <summary>
        /// 犯规14
        /// </summary>
        public BatchFoulEntity Foul { get; set; }

        /// <summary>
        /// 受伤次数15
        /// </summary>
        public int InjuredTimes { get; set; }
        #endregion

        #region BuildData
        private List<PlayerMoveReport> _moveResults; 
        private int _curMoveResultsIndex;
        private PlayerMoveReport _curMoveResult;
        void BuildData()
        {
            foreach (var playerReport in _homeReport.Players)
            {
                _curMoveResultsIndex = 0;
                _moveResults = playerReport.MoveResults;
                for (; _curMoveResultsIndex < _moveResults.Count; _curMoveResultsIndex++)
                {
                    _curMoveResult = _moveResults[_curMoveResultsIndex];
                    var stateReport = _curMoveResult.StateData;
                    if (stateReport.HasBall == 1)
                    {
                        ControlRound++;
                    }
                    if (stateReport.State == 10)
                    {
                        int a = 0;
                        string s = a.ToString();
                    }
                    switch (stateReport.ClassId)
                    {
                        case (int)ReportAsset.PlayerStateAsset.CLASSIdShoot:
                            BuildShoot(stateReport);
                            break;
                        default:
                            BuildDefault(stateReport);
                            break;
                    }
                    BuildFoul(stateReport.FoulLevel);
                }
            }
        }
        #endregion

        #region BuildShoot
        void BuildShoot(PlayerStateReport stateReport)
        {
            var newReport = stateReport as PlayerShootStateReport;
            if(newReport==null)
                return;
            BatchShootEntity shootEntity = null;
            switch (newReport.State)
            {
                case (int)EnumClientState.Shoot:
                    shootEntity = Shoot;
                    break;
                case (int)EnumClientState.RebelShoot:
                    BuildRebelShoot(newReport);
                    break;
                case (int)EnumClientState.VolleyShoot:
                    shootEntity = VolleyShoot;
                    break;
                case (int)EnumClientState.BicycleShot:
                    shootEntity = BicycleShot;
                    break;
            }
            if (shootEntity != null)
            {
                TotalShoot.ShootTimes++;
                shootEntity.ShootTimes++;
                shootEntity.ShootRate += 0;
                TotalShoot.ShootRate += 0;
                if (newReport.GoalIndex == 0)
                {
                    TotalShoot.DoorFrame++;
                    shootEntity.DoorFrame++;
                }
                else if (newReport.GoalIndex < 4 && CheckGoal())
                {
                    TotalShoot.GoalTimes++;
                    shootEntity.GoalTimes++;
                }
            }
        }

        void BuildRebelShoot(PlayerShootStateReport stateReport)
        {
            RebelShoot.ShootTimes++;
            if (stateReport.GoalIndex == 0)
            {
                RebelShoot.DoorFrame++;
            }
            else if (stateReport.GoalIndex < 4 && CheckGoal())
            {
                RebelShoot.GoalTimes++;
            }
        }
        #endregion

        #region BuildDefault

        private void BuildDefault(PlayerStateReport stateReport)
        {
            switch (stateReport.State)
            {
                case (int)EnumClientState.BreakThrough:
                    Breakthrough.Times++;
                    if (CheckHasball())
                    {
                        Breakthrough.SuccTimes++;
                    }
                    break;
                case (int)EnumClientState.Interruption:
                case (int)EnumClientState.SlideTackle:
                    Steal.Times++;
                    if (stateReport.HasBall==1)
                    {
                        Steal.SuccTimes++;
                    }
                    break;
                //case (int)EnumClientState.KeepBall:
                case (int)EnumClientState.DiveBall:
                    BuildDive(stateReport);
                    break;
                default:
                    BuildPass(stateReport);
                    break;
            }
        }

        #endregion

        #region BuildDive
        private void BuildDive(PlayerStateReport stateReport)
        {
            GoalKeep.Times++;
            if(!CheckGoal())
                GoalKeep.SuccTimes++;
        }
        #endregion

        #region BuildFoul
        void BuildFoul(int foulLevel)
        {
            switch (foulLevel)
            {
                case 1:
                    Foul.FoulTimes++;
                    Foul.YellowTimes++;
                    break;
                case 2:
                    Foul.FoulTimes++;
                    Foul.RedTimes++;
                    break;
                case 3:
                    InjuredTimes++;
                    break;
            }
        }
        #endregion

        #region BuildPass
        void BuildPass(PlayerStateReport stateReport)
        {
            BatchRateEntity passEntity = null;
            switch (stateReport.State)
            {
                case (int)EnumClientState.LongPass:
                    passEntity = LongPass;
                    break;
                case (int)EnumClientState.ShortPass:
                    passEntity = ShortPass;
                    break;
                case (int)EnumClientState.HeadPass:
                    passEntity = HeadPass;
                    break;
            }
            if (passEntity != null)
            {
                TotalPass.Times++;
                passEntity.Times++;

                var nextHasBall = GetNextHasballPlayer();
                var latelyHoldBall = GetLatelyHoldballPlayer();
                if (nextHasBall > 0 && nextHasBall == latelyHoldBall)
                {
                    TotalPass.SuccTimes++;
                    passEntity.SuccTimes++;
                }
            }
        }
        #endregion

        #region encapsulation
        int NextRound
        {
            get { return _curMoveResult.AsRound + 1; }
        }

        bool CheckGoal()
        {
            var round = _curMoveResult.AsRound;
            var ballReport = _match.BallResults.Find(d => d.AsRound == round);
            if (ballReport != null)
            {
                return ballReport.StateData.ClassId == 3;
            }
            return false;
        }

        int GetNextHasballPlayer()
        {
            foreach (var playerReport in _homeReport.Players)
            {
                if (playerReport.MoveResults.Exists(d => d.AsRound == NextRound && d.StateData.HasBall == 1))
                    return playerReport.Pid;
            }
            return 0;
        }

        int GetLatelyHoldballPlayer()
        {
            int compareRound = NextRound+200;
            int curPid = 0;

            foreach (var playerReport in _homeReport.Players)
            {
                var report = playerReport.MoveResults.Find(d => d.AsRound >= NextRound && d.AsRound<compareRound && d.StateData.HoldBall == 1);
                if (report != null)
                {
                    curPid = playerReport.Pid;
                    compareRound = report.AsRound;
                }
            }
            foreach (var playerReport in _awayReport.Players)
            {
                var report = playerReport.MoveResults.Find(d => d.AsRound >= NextRound && d.AsRound < compareRound && d.StateData.HoldBall == 1);
                if (report != null)
                {
                    curPid = playerReport.Pid;
                    compareRound = report.AsRound;
                }
            }
            return curPid;
        }

        bool CheckHasball()
        {
            int i = _curMoveResultsIndex + 1;
            if (_moveResults.Count <= i)
            {
                return false;
            }
            var next = _moveResults[i];
            if (next.AsRound != (_curMoveResult.AsRound + 1))
                return _curMoveResult.StateData.HoldBall == 1;
            return next.StateData.HoldBall == 1;
        }
        #endregion
    }
}
