/********************************************************************************
 * 文件名：StopballAction
 * 创建人：
 * 创建时间：2009-12-10 14:19:02
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the action while player in the <see cref="StopballState"/>.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;

namespace Games.NB.Match.AI.Actions {

    /// <summary>
    /// Represents the action while player in the StopballState.
    /// </summary>
    [Singleton]
    public class StopballAction : IAction {        

        /// <summary>
        /// The player to stop the football.
        /// </summary>
        /// <param name="player">Represents the current player.</param>
        public void Action(IPlayer player) {            
            var x = player.Status.Width * Math.Cos(player.Status.Angle) + player.Current.X;
            var y = player.Status.Width * Math.Sin(player.Status.Angle) + player.Current.Y;

            player.Match.Football.MoveTo(new Coordinate(x, y));
            player.Status.Hasball = true;
            player.Status.Speed = player.Status.Speed / 2;
        }        
    }
}
