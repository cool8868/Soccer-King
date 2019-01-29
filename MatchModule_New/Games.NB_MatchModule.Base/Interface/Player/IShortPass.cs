/********************************************************************************
 * 文件名：IShortPass
 * 创建人：
 * 创建时间：2009-12-7 17:31:37
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using Games.NB.Match.Base.Structs;
namespace Games.NB.Match.Base.Interface.Player
{

    /// <summary>
    /// Represents the action of the player to pass ball to other.
    /// </summary>
    public interface IShortPass
    {
        /// <summary>
        /// Decide a target to pass.
        /// </summary>
        void DecidePassTarget();
        IPlayer DecideShortPassTarget();
        /// <summary>
        /// Decide a target to Cross.
        /// </summary>
        IPlayer DecideCrossTarget();
        /// <summary>
        /// Player to pass the ball to passtarget.
        /// </summary>       
        void ShortPass();

        /// <summary>
        /// Validate a <see cref="Coordinate"/> enable to pass.
        /// 判定一个坐标是否能传球
        /// </summary>
        /// <param name="c"><see cref="Coordinate"/></param>
        /// <returns>Enable to pass.</returns>
        bool IsCoordinateEnablePass(Coordinate c);
    }
}
