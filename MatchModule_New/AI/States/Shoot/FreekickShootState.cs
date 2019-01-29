/********************************************************************************
 * 文件名：FreekickShootState
 * 创建人：
 * 创建时间：4/24/2010 3:28:49 PM
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
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.AI.States.Shoot
{

    /// <summary>
    /// Represents the free kick direct shoot state.
    /// 表示了任意球直接射门状态
    /// </summary>
    public class FreekickShootState : ShootState
    {

        /// <summary>
        /// Initializes the data of the <see cref="VolleyShootState"/> class.
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(ShootState.Instance);
            this.StateCondition.Add(ShootState.Instance, ValidateFreekickShootToShoot);
        }

        /// <summary>
        /// Actions the VolleyShootState class.
        /// 触发一次任意球直接射门
        /// </summary>
        /// <param name="player"></param>
        public override void Action(IPlayer player)
        {
            player.FreeKickShoot();
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "FreekickShootState";
        }

        /// <summary>
        /// Represents the instance of the <see cref="VolleyShootState"/> class.
        /// </summary>
        public new static FreekickShootState Instance
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

        private readonly static FreekickShootState _instance = new FreekickShootState();

        private FreekickShootState()
        {
            this.Stopable = true;
        }

        private static bool ValidateFreekickShootToShoot(IPlayer player, IState preview)
        {
            return true;
        }

        #endregion
    }
}
