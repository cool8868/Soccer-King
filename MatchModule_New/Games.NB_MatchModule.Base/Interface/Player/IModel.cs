/********************************************************************************
 * 文件名：IModel
 * 创建人：
 * 创建时间：2010-5-5 9:57:54
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
    /// Represents the interface of the player model.
    /// 表示了模型相关的接口
    /// </summary>
    public interface IModel {

        /// <summary>
        /// Adds a model to a player.
        /// 为一个球员添加一个模型
        /// </summary>
        /// <param name="model">Represents the model id.</param>
        /// <param name="last">Represents the lasting time.</param>
        /// <param name="isHoldBall">Represents whether the model is lasting until player lose the ball.</param>
        void AddModel(byte model, int last, bool isHoldBall);

        /// <summary>
        /// Adds a model to a player.
        /// 为一个球员添加一个模型
        /// </summary>
        /// <param name="model">Represents the model id.</param>
        /// <param name="last">Represents the lasting time.</param>        
        void AddModel(byte model, int last);
    }
}
