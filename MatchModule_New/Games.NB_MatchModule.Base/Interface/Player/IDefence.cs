/********************************************************************************
 * 文件名：IDefence
 * 创建人：
 * 创建时间：5/10/2010 2:36:55 PM
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
namespace Games.NB.Match.Base.Interface.Player
{
    /// <summary>
    /// Represents the contracts of the player defence.
    /// 表示了球员进行防守的接口
    /// </summary>
    public interface IDefence
    {
        /// <summary>
        /// 头球争顶
        /// </summary>
        void HeadingBall();
        /// <summary>
        /// Interrupts a ball.
        /// 抢断一个球
        /// </summary>
        void InterruptionBall();

        /// <summary>
        /// Slides tackle a ball.
        /// 铲球
        /// </summary>
        void SlideTackleBall();
    }
}
