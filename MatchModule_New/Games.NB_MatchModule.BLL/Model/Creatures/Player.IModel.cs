/********************************************************************************
 * 文件名：Player
 * 创建人：
 * 创建时间：5/5/2010 10:58:36 AM
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

namespace Games.NB.Match.BLL.Model.Creatures
{
    /// <summary>
    /// Represents the player entity.
    /// This partial class implemented the interface of the <see cref="IModel"/>.
    /// </summary>
    public sealed partial class Player
    {
        /// <summary>
        /// Adds a model to a player.
        /// 为一个球员添加一个模型
        /// </summary>
        /// <param name="model">Represents the model id.</param>
        /// <param name="last">Represents the lasting time.</param>
        /// <param name="isHoldBall">Represents whether the model is lasting until player lose the ball.</param>
        public void AddModel(byte model, int last, bool isHoldBall)
        {
            this._status.ModelStatus.IsHoldBall = isHoldBall;
            this._status.ModelStatus.Mid = model;
            this._status.ModelStatus.RemainTime = last;
        }

        /// <summary>
        /// Adds a model to a player.
        /// 为一个球员添加一个模型
        /// </summary>
        /// <param name="model">Represents the model id.</param>
        /// <param name="last">Represents the lasting time.</param>        
        public void AddModel(byte model, int last)
        {
            this._status.ModelStatus.IsHoldBall = false;
            this._status.ModelStatus.Mid = model;
            this._status.ModelStatus.RemainTime = last;
        }
    }
}
