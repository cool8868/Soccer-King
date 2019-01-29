/********************************************************************************
 * 文件名：PlayerRotate
 * 创建人：
 * 创建时间：2009-12-18 22:39:29
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：This partial class implemented the interface of the <see cref="IRotate"/>.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface.Player;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Log;

namespace Games.NB.Match.BLL.Model.Creatures
{

    /// <summary>
    /// Represents the player entity.
    /// This partial class implemented the interface of the <see cref="IRotate"/>.
    /// </summary>
    public sealed partial class Player
    {
        #region IRotate Members

        /// <summary>
        /// Rotate player to toward the target.
        /// 让球员将自己的方向转为传递的目标
        /// </summary>
        /// <param name="target"><see cref="Coordinate"/></param>
        public void Rotate(Coordinate target)
        {
            // 守门员由于没有素材而不转向
            if (_input.AsPosition == Position.Goalkeeper)
            {
                target = _match.Football.Current;
            }

            // 当球员坐标与目标重合则转向查看足球
            if (_status.Current == target)
            {
                if (Current != _match.Football.Current)
                {
                    Rotate(_match.Football.Current);
                }
                else
                {
                    _status.Angle = (Side == Side.Home) ? 0 : 180;
                }

                return;
            }

            // 球员转向
            var x = this.Status.Current.X;
            var y = this.Status.Current.Y;

            var d = this.Current.Distance(target);
            if (d < 1) // 避免距离相差太小导致运算后角度超出int范围
            {
                return;
            }

            var angle = (target.X > x) ? Math.Asin((target.Y - y) / d) : Math.PI - Math.Asin((target.Y - y) / d);
            if (angle < 0)
            {
                angle = 2 * Math.PI + angle;
            }

            this.Status.Angle = Convert.ToInt32(angle * 180 / Math.PI);
        }

        #endregion
    }
}
