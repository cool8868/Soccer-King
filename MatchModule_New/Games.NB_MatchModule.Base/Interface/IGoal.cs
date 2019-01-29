/********************************************************************************
 * 文件名：IGoal
 * 创建人：
 * 创建时间：2010-3-1 14:31:25
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents a goal.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Structs;
using SkillEngine.Extern;

namespace Games.NB.Match.Base.Interface {

    /// <summary>
    /// Represents a goal.
    /// 表示了一个球门
    /// </summary>
    public interface IGoal {

        /// <summary>
        /// Represents a goal's all door frame.
        /// 表示了一个球门的多个门框区域
        /// </summary>
        List<ShootTarget> DoorFrame { get; }

        /// <summary>
        /// Get a <see cref="ShootTarget"/> by index.
        /// 通过index获取一个射门目标
        /// </summary>
        /// <param name="index">Represents the shoot index.</param>
        /// <returns><see cref="ShootTarget"/></returns>
        ShootTarget GetShootTargetByIndex(IRandom random, int index);

        /// <summary>
        /// Get a random door frame index.
        /// 获取一个随机门框
        /// </summary>
        /// <returns><see cref="ShootTarget"/></returns>
        ShootTarget GetRandomDoorFrame(IRandom random);
    }
}
