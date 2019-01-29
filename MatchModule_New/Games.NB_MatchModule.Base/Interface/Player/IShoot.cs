/********************************************************************************
 * 文件名：IShoot
 * 创建人：
 * 创建时间：2009-12-19 10:28:49
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

namespace Games.NB.Match.Base.Interface.Player {

    /// <summary>
    /// Represents the interface of the player shoot.
    /// 表示了球员的射门动作
    /// </summary>
    public interface IShoot {

        /// <summary>
        /// Player to shoot.
        /// 射门
        /// </summary>
        void Shoot();
        /// <summary>
        /// 乌龙射门
        /// </summary>
        void RebelShoot();
        /// <summary>
        /// Player to action a volley shoot.
        /// 球员发动一次大力抽射
        /// </summary>
        void VolleyShoot();

        /// <summary>
        /// Player to action a Direct free kick shoot.
        /// 球员发动一次直接任意球射门
        /// </summary>
        void FreeKickShoot();
    }
}
