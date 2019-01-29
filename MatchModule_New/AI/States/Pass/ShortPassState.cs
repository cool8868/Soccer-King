/********************************************************************************
 * 文件名：ShortPassState
 * 创建人：
 * 创建时间：2009-12-12 10:21:38
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.AI.Actions;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Common.Collections;
using Games.NB.Match.Base.Enum;

namespace Games.NB.Match.AI.States.Pass
{

    /// <summary>
    /// Represents the short pass state.
    /// This class implemented the singleton pattern.
    /// </summary>
    /// <example>ShortPassState.Instance</example>
    [Singleton]
    public class ShortPassState : PassState
    {
        /// <summary>
        /// Initializes the new instance of the <see cref="ShortPassState"/>.
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(PassState.Instance);
            this.StateCondition.Add(PassState.Instance, ValidateShortPassToPass);
        }

        /// <summary>
        /// Short pass action.
        /// </summary>
        /// <param name="player"></param>
        public override void Action(IPlayer player)
        {
            player.ShortPass();
        }

        /// <summary>
        /// Outputs the <see cref="ShortPassState"/>'s name.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "ShortPassState";
        }

        /// <summary>
        /// Represents the <see cref="ShortPassState"/>'s instance.
        /// </summary>
        public new static ShortPassState Instance
        {
            get { return _instance; }
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

        private static readonly ShortPassState _instance = new ShortPassState();

        private ShortPassState()
        {
            this.Stopable = true;
        }

        private static bool ValidateShortPassToPass(IPlayer player, IState preview)
        {
            return true;
        }

        #endregion
    }
}
