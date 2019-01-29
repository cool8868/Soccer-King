/********************************************************************************
 * 文件名：PlayerDribble
 * 创建人：
 * 创建时间：2009-12-18 22:42:36
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：This partial class implemented the <see cref="IDribble"/> interface.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Interface.Player;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.BLL.Rules;

namespace Games.NB.Match.BLL.Model.Creatures
{
    /// <summary>
    /// Represents the player entity.
    /// This partial class implemented the <see cref="IDribble"/> interface.
    /// </summary>
    public sealed partial class Player
    {
        #region IDribble Members

        /// <summary>
        /// Dribble ball.
        /// </summary>        
        public void DribbleBall()
        {
            Move();

            if (_status.Holdball)
            {
                double x = _match.Football.Current.X + _status.Width * Math.Cos(_status.Angle * Math.PI / 180);
                double y = _match.Football.Current.Y + _status.Width * Math.Sin(_status.Angle * Math.PI / 180);
                _match.Football.Kick(new Coordinate(x, y), 22, this);
                //_match.Football.Kick(new Coordinate(x, y), 25, this);
                //_match.Football.MoveTo(new Coordinate(x, y));
            }                
        }

        #endregion
    }
}
