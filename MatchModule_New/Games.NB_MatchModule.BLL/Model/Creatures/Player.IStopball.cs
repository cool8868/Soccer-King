/********************************************************************************
 * 文件名：PlayerStopball
 * 创建人：
 * 创建时间：2009-12-18 22:38:02
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：This partial class implemented the interface of the <see cref="IStopball"/>.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Interface.Player;
using Games.NB.Match.Log;
using Games.NB.Match.Base;

namespace Games.NB.Match.BLL.Model.Creatures {

    /// <summary>
    /// Represents the player entity.
    /// This partial class implemented the interface of the <see cref="IStopball"/>.
    /// </summary>
    public sealed partial class Player {
        #region IStopball Members

        public void Stopball()
        {
            if (this._status.Hasball == false || this.Status.BallDistance > Defines.Player.DEFENCE_RANGE)
            {
                throw new NotSupportedException("[停球]球在不在这个球员身上：" + this);
            }

            LogHelper.Insert(String.Format("[停球]{0} 停球", this));

            this.Manager.Match.Football.Kick(this.Manager.Match.Football.Current, 0, this);
        }

        #endregion
    }
}
