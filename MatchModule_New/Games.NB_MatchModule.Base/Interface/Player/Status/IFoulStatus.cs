/********************************************************************************
 * 文件名：IFoulStatus
 * 创建人：
 * 创建时间：2010-2-21 10:52:492
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents a player's foul informations.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NB.Match.Base.Interface.Player.Status
{

    /// <summary>
    /// Represents a player's foul informations.
    /// 表示了球员的犯规状态
    /// </summary>
    public interface IFoulStatus
    {

        /// <summary>
        /// Represents the player's foul level.
        /// 表示了球员的犯规等级
        /// <example>        
        ///  0 - None       
        ///  1 - Yellow Card
        ///  2 - Red Card
        ///  3 - Injured
        /// </example>
        /// </summary>
        byte FoulLevel { get; set; }

        /// <summary>
        /// Whether the player is yellow card.
        /// 是否黄牌
        /// </summary>
        bool IsYellowCard { get; }

        /// <summary>
        /// Whether the player is red card.
        /// 是否红牌
        /// </summary>
        bool IsRedCard { get; }

        /// <summary>
        /// Whether the player is injured.
        /// 是否受伤
        /// </summary>
        bool IsInjured { get; }
    }
}
