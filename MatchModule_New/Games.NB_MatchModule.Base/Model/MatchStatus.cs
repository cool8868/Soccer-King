using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.Base.Model
{
    public class MatchStatus
    {
        const int TOTALMinutes = 90;
        private int _totalRound;
        private short _roundPerMinute;
        private short _roundPerSection;
        private int _breakCycle;
        private EnumMatchBreakState _breakState;
        private EnumMatchBreakStateV2 _breakStateV2;
        /// <summary>
        /// 总回合数
        /// </summary>
        public int TotalRound
        {
            get { return _totalRound; }
            set
            {
                int val = value;
                this._totalRound = val;
                this._roundPerMinute = (short)(val / TOTALMinutes);
                this._roundPerSection = (short)(val / 2);
            }
        }
        public short RoundPerMinute
        {
            get { return _roundPerMinute; }
        }
        public short RoundPerSection
        {
            get { return _roundPerSection; }
        }
        /// <summary>
        /// 表示上下半场
        /// </summary>
        public int SectionNo { get; set; }
        /// <summary>
        /// Represents whether is the first half.
        /// 表示了是否是上半场
        /// </summary>
        public bool IsFirstHalf
        {
            get { return SectionNo == 0; }
        }
        /// <summary>
        /// Represents the current round.
        /// 表示了当前的回合数
        /// </summary>
        public short Round { get; set; }
        /// <summary>
        /// Represents the current game time.
        /// 表示了当前的分钟数
        /// </summary>
        public short Minute { get; set; }

        public IMatchState MatchState { get; set; }
        public int CycleCount
        {
            get { return _breakCycle; }
        }
        public int BreakState
        {
            get
            {
                if (ReportAsset.RPTVerNo <= 1)
                    return (int)_breakState;
                else
                    return (int)_breakStateV2;
            }
        }

        public void Break(EnumMatchBreakState breakState)
        {
            if (ReportAsset.RPTVerNo <= 1)
                _breakState = CastBreakState(breakState);
            else
                _breakStateV2 = CastBreakStateV2(breakState);
            switch (breakState)
            {
                case EnumMatchBreakState.None:
                case EnumMatchBreakState.FreeKick:
                case EnumMatchBreakState.Shooted:
                    return;
                default:
                    _breakCycle++;
                    return;
            }
        }
        EnumMatchBreakState CastBreakState(EnumMatchBreakState breakState)
        {
            switch (breakState)
            {
                case EnumMatchBreakState.HandThrow:
                case EnumMatchBreakState.CornerKick:
                case EnumMatchBreakState.GoalKick:
                    return EnumMatchBreakState.OutBorder;
                case EnumMatchBreakState.ShootFly:
                case EnumMatchBreakState.ShootInto:
                    return EnumMatchBreakState.Shooted;
                case EnumMatchBreakState.IndirectKick:
                case EnumMatchBreakState.DirectKick:
                case EnumMatchBreakState.PenaltyKick:
                    return EnumMatchBreakState.FreeKick;
                default:
                    return breakState;
            }
 
        }
        EnumMatchBreakStateV2 CastBreakStateV2(EnumMatchBreakState breakState)
        {
            switch (breakState)
            {
                case EnumMatchBreakState.SectionOpen:
                    return EnumMatchBreakStateV2.SectionOpen;
                case EnumMatchBreakState.MFOpen:
                    return EnumMatchBreakStateV2.MFOpen;
                case EnumMatchBreakState.GKOpen:
                    return EnumMatchBreakStateV2.GKOpen;
                case EnumMatchBreakState.CornerKick:
                    return EnumMatchBreakStateV2.CornerKick;
                case EnumMatchBreakState.HandThrow:
                    return EnumMatchBreakStateV2.HandThrow;
                case EnumMatchBreakState.IndirectKick:
                case EnumMatchBreakState.DirectKick:
                    return EnumMatchBreakStateV2.FreeKick;
                case EnumMatchBreakState.PenaltyKick:
                    return EnumMatchBreakStateV2.PenaltyKick;
                default:
                    return EnumMatchBreakStateV2.None;

            }
        }
       
        /// <summary>
        /// Represents whether the current round has foul.
        /// 表示了该回合是否犯规
        /// </summary>
        public EnumMatchFoulState FoulState { get; set; }
        /// <summary>
        /// 犯规球员
        /// </summary>
        public IPlayer FoulPlayer { get; set; }
        /// <summary>
        /// Represents the current round is goal?
        /// 表示了当前回合是否进球
        /// </summary>
        public bool GoalState { get; set; }
        /// <summary>
        /// Represents the current ball handler.
        /// 表示了当前的持球人
        /// </summary>
        public IPlayer BallHandler { get; set; }
        /// <summary>
        /// Represents whether current round has ball handler.
        /// 表示了当前回合是否有无球人
        /// </summary>
        public bool IsNoBallHandler { get; set; }

       
    }
}
