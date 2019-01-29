/********************************************************************************
 * 文件名：BreakThroughState
 * 创建人：
 * 创建时间：2010-3-4 21:42:56
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.AI.States.Dribble;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.AI.States.Dribble
{

    /// <summary>
    /// Represents the player's break through <see cref="State"/>.
    /// 表示了球员的过人状态
    /// </summary>
    [Singleton]
    public class BreakThroughState : DribbleState
    {
        /// <summary>
        /// Intializes the new instance of the <see cref="BreakThroughState"/>
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(ActionState.Instance);
            this.StateCondition.Add(ActionState.Instance, ValidateBreakThroughToAction);
        }

        /// <summary>
        /// Represents the instance of the <see cref="BreakThroughState"/>.
        /// </summary>
        public new static BreakThroughState Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Enters the <see cref="BreakThroughState"/>.
        /// </summary>
        /// <param name="player"></param>
        public override void Enter(IPlayer player)
        {
        }

        /// <summary>
        /// Outputs the <see cref="BreakThroughState"/>'s name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "BreakThroughState";
        }

        /// <summary>
        /// Decides the nex <see cref="IState"/>
        /// </summary>
        /// <param name="player">Represents the current <see cref="IPlayer"/>.</param>
        /// <param name="preview">Represents the preview <see cref="IState"/></param>
        /// <returns></returns>
        public override IState QuickDecide(IPlayer player, IState preview)
        {
            if (player.Status.Hasball)
            {
                return HoldBallState.Instance;
            }
            else
            {
                return OffBallState.Instance;
            }
        }

        #region encapsulation

        private BreakThroughState()
        {
            this.Stopable = true;
        }

        private static readonly BreakThroughState _instance = new BreakThroughState();

        private static bool ValidateBreakThroughToAction(IPlayer player, IState preview)
        {
            return true;
        }

        #endregion
    }
}
