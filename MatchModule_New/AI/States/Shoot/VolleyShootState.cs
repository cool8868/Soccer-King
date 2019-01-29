/********************************************************************************
 * 文件名：VolleyShootState
 * 创建人：
 * 创建时间：2010-2-28 10:09:05
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
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Common.Collections;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.AI.Actions;

namespace Games.NB.Match.AI.States.Shoot
{

    /// <summary>
    /// Represents the volley shoot state.
    /// 表示了大力抽射的状态
    /// </summary>
    [Singleton]
    public sealed class VolleyShootState : ShootState
    {

        /// <summary>
        /// Initializes the data of the <see cref="VolleyShootState"/> class.
        /// </summary>
        public override void Initialize()
        {
            this.StateChain.Add(ShootState.Instance);

            this.StateCondition.Add(ShootState.Instance, ValidateVolleyShootToShoot);
        }

        /// <summary>
        /// Actions the VolleyShootState class.
        /// 触发一次大力射门
        /// </summary>
        /// <param name="player"></param>
        public override void Action(IPlayer player)
        {
            player.VolleyShoot();
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "VolleyShootState";
        }

        /// <summary>
        /// Represents the instance of the <see cref="VolleyShootState"/> class.
        /// </summary>
        public new static VolleyShootState Instance
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

        private readonly static VolleyShootState _instance = new VolleyShootState();

        private VolleyShootState()
        {
            this.Stopable = true;
        }

        private static bool ValidateVolleyShootToShoot(IPlayer player, IState preview)
        {
            return true;
        }

        #endregion
    }
}
