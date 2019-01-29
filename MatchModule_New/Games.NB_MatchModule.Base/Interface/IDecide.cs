/*****************************************************************************
 * 文件名：IDecide
 * 
 * 创建人：
 * 
 * 创建时间：2009-11-18 11:03:34
 * 
 * 功能说明：Represents the interface of the player to decide.
 *  
 * 历史修改记录：
 * 
 * ***************************************************************************/
namespace Games.NB.Match.Base.Interface
{
    /// <summary>
    /// Represents the interface of the player to decide.
    /// </summary>
    public interface IDecide 
    {

        /// <summary>
        /// Player to action.
        /// including the pass action, shoot action, defence action and so on.
        /// </summary>
        void Action();

        /// <summary>
        /// Quick Decide.
        /// </summary>
        void QuickDecide();
    }
   
}
