/********************************************************************************
 * 文件名：
 * 创建人：
 * 创建时间：
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
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Common.Collections;

namespace Games.NB.Match.AI.Decides.Factory
{
    /// <summary>
    /// 寻位工厂
    /// </summary>
    public static class PositionalDecideFactory
    {
        /// <summary>
        /// 创建寻位对象
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static IPositionalDecide Create(Position position)
        {
            return _cache[position];
        }

        #region encapsulation

        private static readonly Dictionary<Position, IPositionalDecide> _cache = new Dictionary<Position, IPositionalDecide>(4);

        static PositionalDecideFactory()
        {
            _cache.Add(Position.Forward, Singleton<Forward.PositionalDecide>.Instance);
            _cache.Add(Position.Midfielder, Singleton<Midfielder.PositionalDecide>.Instance);
            _cache.Add(Position.Fullback, Singleton<Fullback.PositionalDecide>.Instance);
            _cache.Add(Position.Goalkeeper, Singleton<Goalkeeper.PositionalDecide>.Instance);
        }

        #endregion
    }
}
