/********************************************************************************
 * 文件名：State
 * 创建人：
 * 创建时间：2009-12-30 13:48:50
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the abstract state entity.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranOut;
using Games.NB.Match.Base.Structs;


namespace Games.NB.Match.AI.States
{

    /// <summary>
    /// Represents the abstract state entity.
    /// </summary>
    public abstract class State : IState
    {
        /// <summary>
        /// Represents the state's id.
        /// </summary>
        public int Id { get { return _id; } }

        /// <summary>
        /// Represents a state's client id.
        /// </summary>
        public byte ClientId { get; set; }

        /// <summary>
        /// Represents whether the current state is a stopable state.
        /// </summary>
        public bool Stopable
        {
            get { return _stopable; }
            protected set { _stopable = value; }
        }

        /// <summary>
        /// Represents the action lasting time.
        /// 表示了该动作的持续时间
        /// </summary>
        public int TimeLast { get; protected set; }

        /// <summary>
        /// Initialize current <see cref="IState"/>.
        /// 初始化
        /// </summary>
        public abstract void Initialize();

       

        /// <summary>
        /// Decides the next <see cref="State"/>.
        /// </summary>
        /// <param name="player">Represents the current <see cref="IPlayer"/>.</param>
        /// <param name="preview">Represents the preview <see cref="IState"/>.</param>
        /// <returns></returns>
        /// <remarks>
        /// 1. Find the next states from the state chain.
        /// 2. Find the next state that pass the condition.
        /// </remarks>
        public IState Decide(IPlayer player, IState preview)
        {
            this.Enter(player);

            if(this._stateChain.Count == 0 || this._stateCondition.Count == 0)
            {
                throw new StateChainNotInitializeException(this.ToString());
            }

            try
            {
                foreach(var next in this._stateChain)
                {
                    if(!this._stateCondition.Keys.Contains(next))
                    {
                        throw new StateConditionNotFoundException(String.Format("Current:{0}, Next:{1}", this, next));
                    }

                    if(_stateCondition[next](player, preview))
                    {
                        return next;
                    }
                }

                throw new StateDecideInfiniteException(this.ToString());
            }
            finally
            {
                this.Exit(player);
            }
        }

        /// <summary>
        /// Decides the nex <see cref="IState"/>
        /// </summary>
        /// <param name="player">Represents the current <see cref="IPlayer"/>.</param>
        /// <param name="preview">Represents the preview <see cref="IState"/></param>
        /// <returns></returns>
        public abstract IState QuickDecide(IPlayer player, IState preview);

        /// <summary>
        /// Invoked while enter current state.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        public virtual void Enter(IPlayer player)
        {
            // LogHelper.Insert(String.Format("{0} Enter.", this));
        }

        /// <summary>
        /// Invoked while exit current state.
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        public virtual void Exit(IPlayer player)
        {
            // LogHelper.Insert(String.Format("{0} Exit.", this));
        }

        /// <summary>
        /// Current State to make a action.
        /// </summary>
        /// <param name="player">Current <see cref="IPlayer"/>.</param>
        public virtual void Action(IPlayer player)
        {
            throw new NotSupportedException(this.ToString());
        }

        public PlayerMoveReport SaveRpt(IPlayer player)
        {
            var state = CreateStateRpt(player);
            FillStateRpt(player, state);
            var rpt = new PlayerMoveReport();
            rpt.AsRound = player.Match.Status.Round;
            rpt.StateData = state;
            return rpt;
        }
        protected virtual PlayerStateReport CreateStateRpt(IPlayer player)
        {
            if (ReportAsset.RPTVerNo <= 1)
                return new PlayerStateReport();
            if (player.Input.AsPosition == Position.Goalkeeper)
                return new GkPlayerStateReportV2();
            return new PlayerStateReportV2();
        }
        protected void FillStateRpt(IPlayer player, PlayerStateReport rpt)
        {
            rpt.Current = player.Current;
            rpt.Angle = GetPlayerAngleIndex(player.Angle);
            rpt.State = player.Status.ClientState;
            rpt.IsBackward = Convert.ToByte(player.Status.IsBackward);
            rpt.IsStantUp = Convert.ToByte(player.Status.IsStantUp);
            rpt.HasBall = Convert.ToByte(player.Status.Hasball);
            rpt.HoldBall = Convert.ToByte(player.Status.Holdball);
            if (player.Status.IsAttackSide)
            {
                rpt.NameVisible = player.Status.Hasball ? (byte)1 : (byte)0;
            }
            else
            {
                rpt.NameVisible = (player.Status.DefenceStatus.DefenceTarget == player.Match.Status.BallHandler) ? (byte)1 : (byte)0;
            }
            //rpt.FoulLevel = player.Status.FoulStatus.FoulLevel;
            rpt.FoulLevel = player.SkillFoulState;
            rpt.ModelId = player.Status.ModelStatus.Mid;
        }
        static byte GetPlayerAngleIndex(int angle)
        {
            return (byte)((angle + 22.5) / 45 % 8);
        }
       
      

        /// <summary>
        /// Override the equals.
        /// </summary>
        /// <param name="obj">The input object.</param>
        /// <returns>Is equal?</returns>
        public override bool Equals(object obj)
        {
            var o = obj as State;
            if(o == null) return false;
            return o.Id == this.Id;
        }

        /// <summary>
        /// Used the guid for the hashcode.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.Id;
        }

        #region encapsulation

        private readonly int _id = Guid.NewGuid().GetHashCode();
        private bool _stopable;

        private readonly List<IState> _stateChain = new List<IState>(5);
        private readonly Dictionary<IState, ConditionDelegate> _stateCondition = new Dictionary<IState, ConditionDelegate>(5);

        /// <summary>
        /// Represents the state chain.
        /// </summary>
        protected List<IState> StateChain
        {
            get { return _stateChain; }
        }

        /// <summary>
        /// Represents the next states's conditions.
        /// </summary>
        protected Dictionary<IState, ConditionDelegate> StateCondition
        {
            get { return _stateCondition; }
        }

        #endregion
    }

    /// <summary>
    /// Condition Delegate.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="preview"></param>
    /// <returns></returns>
    public delegate bool ConditionDelegate(IPlayer player, IState preview);

    /// <summary>
    /// State chain not initialized exception.
    /// </summary>
    [Serializable]
    public class StateChainNotInitializeException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        /// <summary>
        /// State chain not initialized exception.
        /// </summary>
        public StateChainNotInitializeException()
        {
        }

        /// <summary>
        /// State chain not initialized exception.
        /// </summary>
        /// <param name="message"></param>
        public StateChainNotInitializeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// State chain not initialized exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public StateChainNotInitializeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// State chain not initialized exception.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected StateChainNotInitializeException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// State condition not found exception.
    /// </summary>
    [Serializable]
    public class StateConditionNotFoundException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        /// <summary>
        /// State condition not found exception.
        /// </summary>
        public StateConditionNotFoundException()
        {
        }

        /// <summary>
        /// State condition not found exception.
        /// </summary>
        /// <param name="message"></param>
        public StateConditionNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// State condition not found exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public StateConditionNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// State condition not found exception.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected StateConditionNotFoundException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// State decide infinite exception.
    /// </summary>
    [Serializable]
    public class StateDecideInfiniteException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        /// <summary>
        /// State decide infinite exception.
        /// </summary>
        public StateDecideInfiniteException()
        {
        }

        /// <summary>
        /// State decide infinite exception.
        /// </summary>
        /// <param name="message"></param>
        public StateDecideInfiniteException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// State decide infinite exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public StateDecideInfiniteException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// State decide infinite exception.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected StateDecideInfiniteException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
