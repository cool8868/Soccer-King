/********************************************************************************
 * 文件名：PositionalDecide
 * 创建人：
 * 创建时间：2009-12-21 15:04:01
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
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base;

namespace Games.NB.Match.AI.Decides.Goalkeeper
{
    /// <summary>
    /// 守门员移动时逻辑
    /// </summary>
    [Singleton]
    public sealed class PositionalDecide : Decides.PositionalDecide
    {
        /// <summary>
        /// 本方进攻时的逻辑
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public override Coordinate OffenseSideDecide(IPlayer player)
        {            
            player.Rotate(player.Match.Football.Current);
            return player.Status.Default;
        }

        /// <summary>
        /// 防守时的逻辑
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public override Coordinate DefenceSideDecide(IPlayer player)
        {
            if (player.Status.IsAttackSide != false)
            {
                Region region = (player.Side == Side.Home) ?
                    player.Match.Pitch.HomePenaltyRegion :
                    player.Match.Pitch.AwayPenaltyRegion;

                if (region.IsCoordinateInRegion(player.Match.Football.Current))
                {
                    return player.Match.Football.Current;
                }
            }

            player.Rotate(player.Match.Football.Current);
            if (player.Match.Status.BallHandler.Status.Zone == Zone.OpposingHalf)
            {

                // 当对方进攻球员比较近时，守门员应该蹲下并且退回至门线上
                player.Status.IsStantUp = false;
                Coordinate target = new Coordinate();
                target.X = player.Status.Default.X;
                target.Y = player.Match.Football.Current.Y;

                if (target.Y < player.Status.Default.Y - Defines.Player.GK_MOVE_RANGE) target.Y = player.Status.Default.Y - Defines.Player.GK_MOVE_RANGE;
                if (target.Y > player.Status.Default.Y + Defines.Player.GK_MOVE_RANGE) target.Y = player.Status.Default.Y + Defines.Player.GK_MOVE_RANGE;
                return target;
            }

            player.Status.IsStantUp = true;
            return player.Match.Football.Current;
        }
    }
}
