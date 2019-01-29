/********************************************************************************
 * 文件名：IModelStatus
 * 创建人：
 * 创建时间：5/3/2010 10:21:49 AM
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

namespace Games.NB.Match.Base.Interface.Player.Status {

    /// <summary>
    /// Represents the status of the player's model.
    /// 表示了球员的模型状态
    /// </summary>
    public interface IModelStatus {

        /// <summary>
        /// Represents the model id.
        /// 表示了模型id
        /// </summary>
        byte Mid { get; set; }

        /// <summary>
        /// Represents the model's remain time.(Round)
        /// 表示了模型的剩余时间(Round)
        /// </summary>
        int RemainTime { get; set; }

        /// <summary>
        /// Represents whether the model will effects while the player is holding ball.
        /// 表示了该模型是否是持球时就保持
        /// </summary>
        bool IsHoldBall { get; set; }
    }
}
