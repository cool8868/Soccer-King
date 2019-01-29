/********************************************************************************
 * 文件名：DefaultDribbleState
 * 创建人：
 * 创建时间：2009-12-14 17:06:17
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.AI.States.Dribble
{

    /// <summary>
    /// Represents a state that defines the default dribble state.
    /// </summary>
    [Singleton]
    public class DefaultDribbleState : DribbleState
    {

        /// <summary>
        /// Represents the instance of the <see cref="DefaultDribbleState"/>.
        /// </summary>
        public new static DefaultDribbleState Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Initializes the new instance of the <see cref="DefaultDribbleState"/> class.
        /// </summary>
        public override void Initialize()
        {
            StateChain.Add(this);
            StateChain.Add(DribbleState.Instance);
            StateCondition.Add(DribbleState.Instance, ValidateDefaultDribbleToDribble);
            StateCondition.Add(this, ValidateDefaultDribbleToDefaultDribble);
        }

        /// <summary>
        /// Outputs the <see cref="DefaultDribbleState"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "DefaultDribbleState";
        }

        /// <summary>
        /// Enters the <see cref="DefaultDribbleState"/>.
        /// </summary>
        /// <param name="player"></param>
        public override void Enter(IPlayer player)
        {
        }

        /// <summary>
        /// Decides the nex <see cref="IState"/>
        /// </summary>
        /// <param name="player">Represents the current <see cref="IPlayer"/>.</param>
        /// <param name="preview">Represents the preview <see cref="IState"/></param>
        /// <returns></returns>
        public override IState QuickDecide(IPlayer player, IState preview)
        {
            if (player.Status.Hasball == false)
            {
                return OffBallState.Instance;
            }
            else
            {
                return HoldBallState.Instance;
            }
        }

        #region encapsulation

        private readonly static DefaultDribbleState _instance = new DefaultDribbleState();

        private DefaultDribbleState()
        {
            this.Stopable = true;
        }

        private static bool ValidateDefaultDribbleToDribble(IPlayer player, IState preview)
        {
            return true;
        }

        private static bool ValidateDefaultDribbleToDefaultDribble(IPlayer player, IState preview)
        {
            if (!player.Status.Hasball)
            {
                return false;
            }
            return (player.Status.NeedRedecide == false);
        }

        #endregion
    }
}
