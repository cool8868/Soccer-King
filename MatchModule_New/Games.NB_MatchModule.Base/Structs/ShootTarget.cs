/********************************************************************************
 * 文件名：ShootTarget
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

namespace Games.NB.Match.Base.Structs
{

    /// <summary>
    /// Represents a shoot's target.
    /// </summary>
    public struct ShootTarget
    {

        /// <summary>
        /// X
        /// </summary>
        public int X;

        /// <summary>
        /// Y
        /// </summary>
        public int Y;

        /// <summary>
        /// 是否是门框
        /// </summary>
        public bool IsFrame;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShootTarget"/> class.
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public ShootTarget(int x, int y)
        {
            X = x;
            Y = y;
            IsFrame = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShootTarget"/> class with whether it is the door frame.
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="isFrame">Whether it is the door frame.</param>
        public ShootTarget(int x, int y, bool isFrame)
        {
            X = x;
            Y = y;
            IsFrame = isFrame;
        }

        /// <summary>
        /// To String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "[ShootTarget]" + Output();
        }

        /// <summary>
        /// Output the <see cref="ShootTarget"/> for the client side.
        /// </summary>
        /// <returns>The <see cref="String"/> that represents the <see cref="ShootTarget"/> class.</returns>
        public string Output()
        {
            if (IsFrame)
            {
                return "F" + X;
            }

            return X + "," + Y;
        }
    }
}
