/********************************************************************************
 * 文件名：DefaultShootState
 * 创建人：
 * 创建时间：2009-12-17 15:42:16
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.AI.Actions;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Common.Collections;

namespace Games.NB.Match.AI.States.Shoot
{

    /// <summary>
    /// Represents the normal shoot state.
    /// 表示了普通射门状态
    /// </summary>
    [Singleton]
    public class DefaultShootState : ShootState
    {
        /// <summary>
        /// Initializes the data of the <see cref="DefaultShootState"/> class.
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(ShootState.Instance);

            this.StateCondition.Add(ShootState.Instance, ValidateDefaultShootToShoot);
        }

        /// <summary>
        /// Normal Shoot actions.
        /// 普通射门行动
        /// </summary>
        /// <param name="player">Represents the shooter man.</param>
        public override void Action(IPlayer player)
        {
            player.Shoot();
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "DefaultShootState";
        }

        /// <summary>
        /// Represents the instance of the <see cref="DefaultShootState"/> class.
        /// </summary>
        public new static DefaultShootState Instance
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

        private static readonly DefaultShootState _instance = new DefaultShootState();

        protected DefaultShootState()
        {
            this.Stopable = true;
        }

        private static bool ValidateDefaultShootToShoot(IPlayer player, IState preview)
        {
            return true;
        }

        #endregion
    }
}
