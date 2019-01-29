/********************************************************************************
 * 文件名：IAddDebuff
 * 创建人：
 * 创建时间：2010-2-22 13:57:12
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

namespace Games.NB.Match.Base.Interface.Player
{
    /// <summary>
    /// Represents the methods that to add buffs to player.
    /// </summary>
    public interface IAddBuff
    {
        /// <summary>
        /// 射门
        /// </summary>
        void AddFinishingBuff(int last);
        /// <summary>
        /// 干扰
        /// </summary>
        void AddDisturbBuff(int last, int percent);
        /// <summary>
        /// 倒地
        /// </summary>
        void AddFallDownBuff(int last);
        /// <summary>
        /// 惯性
        /// </summary>
        void AddInertiaBuff(int last);
        /// <summary>
        /// 沉默
        /// </summary>
        /// <param name="last"></param>
        void AddSilenceBuff(int last);
        /// <summary>
        /// 强制状态
        /// </summary>
        /// <param name="last"></param>
        void AddForceStateBuff(int forceState, int last, int rate = 100);
    }

    
}
