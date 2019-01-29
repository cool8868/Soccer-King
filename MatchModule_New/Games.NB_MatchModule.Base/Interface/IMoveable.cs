/*****************************************************************************
 * 文件名：IMoveable
 * 
 * 创建人：
 * 
 * 创建时间：2009-11-9 17:27:59
 * 
 * 功能说明：Represents the moveable object's interface.
 *  
 * 历史修改记录：
 * 
 * ***************************************************************************/
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Interface;
using System.Collections.Generic;
using Games.NB.Match.Base.Structs;

namespace Games.NB.Match.Base.Interface {

    /// <summary>
    /// Represents the moveable object's interface.
    /// </summary>
    public interface IMoveable 
    {
        /// <summary>
        /// Represents the current <see cref="Coordinate"/>.
        /// 表示了物体的当前坐标
        /// </summary>
        Coordinate Current { get; }

        /// <summary>
        /// Represents the destination coordinate.
        /// 表示了物体的目标坐标
        /// </summary>
        Coordinate Destination { get; }

        /// <summary>
        /// Represents the object's speed.
        /// 表示了物体的速度
        /// </summary>
        double Speed { get; }

        /// <summary>
        /// 角度
        /// </summary>
        int Angle { get; }
        /// <summary>
        /// Represents the object's acceleration.
        /// 表示了物体的加速度
        /// </summary>
        double Acceleration { get; }        

        /// <summary>
        /// Creature moving.
        /// 物体根据自身的趋势移动
        /// </summary>
        void Move();

        /// <summary>
        /// Moves the object to target.
        /// 将物体强行移动至某处
        /// </summary>
        /// <param name="target"><see cref="Coordinate"/></param>
        void MoveTo(Coordinate target);

        /// <summary>
        /// Moves the object to the target.
        /// 将物体强行移动至某处
        /// </summary>
        /// <param name="x">Represents the X-axis coordinate.</param>
        /// <param name="y">Represents the Y-axis coordinate.</param>
        void MoveTo(double x, double y);

        /// <summary>
        /// Reset player to default position.
        /// 重置物体的当前状态
        /// </summary>
        void Reset();

        /// <summary>
        /// Decreases the player's speed.
        /// 将球员的速度降低为当前速度的一半
        /// </summary>
        void DecreaseSpeed();
      
       
    }
}
