/********************************************************************************
 * 文件名：Coordinate
 * 创建人：
 * 创建时间：2009-11-24 17:53:48
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using Games.NB.Match.Common;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Common.Collections;
using System.IO;
using System.Xml.Serialization;

namespace Games.NB.Match.Base.Structs
{

    /// <summary>
    /// Represents a coordiante a the pitch.
    /// </summary>
    [Serializable]
    public struct Coordinate
    {

        /// <summary>
        /// Initialize a new <see cref="Coordinate"/>.
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public Coordinate(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// X
        /// </summary>
        public double X;

        /// <summary>
        /// Y
        /// </summary>
        public double Y;

        /// <summary>
        /// Return the x,y
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return String.Format("{0},{1}", X, Y);
        }

        /// <summary>
        /// Return the x,y, and the x,y is int32 formate.
        /// </summary>
        /// <returns>string.</returns>
        public string Output()
        {
            return String.Format("{0},{1}", Convert.ToInt32(X), Convert.ToInt32(Y));
        }

        /// <summary>
        /// Current coordiante with a x-mirror.
        /// </summary>
        /// <returns>new <see cref="Coordinate"/></returns>
        public Coordinate XMirror()
        {
            int middle = Defines.Pitch.MAX_WIDTH / 2;

            if (X == middle) return new Coordinate(X, Y);

            return new Coordinate(2 * middle - X, Y);
        }

        /// <summary>
        /// Current coordinate with a y-mirror.
        /// </summary>
        /// <returns>new <see cref="Coordinate"/></returns>
        public Coordinate YMirror()
        {
            int middle = Defines.Pitch.MAX_HEIGHT / 2;

            if (Y == middle) return new Coordinate(X, Y);

            return new Coordinate(X, 2 * middle - Y);
        }

        /// <summary>
        /// Current coordiante with a center star mirror.
        /// </summary>
        /// <returns>new <see cref="Coordinate"/></returns>
        public Coordinate Mirror()
        {
            return new Coordinate(this.XMirror().X, this.YMirror().Y);
        }

        /// <summary>
        /// Calculate the distance between current <see cref="Coordinate"/> 
        /// and the target <see cref="Coordinate"/>.
        /// 计算两点之间的距离
        /// </summary>
        /// <param name="target"><see cref="Coordinate"/></param>
        /// <returns>Represents the distance.</returns>
        public double Distance(Coordinate target)
        {
            //return new Core_Performance.Utility().distance(X, Y, target.X, target.Y);
            return Math.Sqrt(Math.Pow(this.X - target.X, 2) + Math.Pow(this.Y - target.Y, 2));
        }

        /// <summary>
        /// Calculate the distance between current <see cref="Coordinate"/>
        /// and the target <see cref="Coordinate"/> uses a simple way.
        /// 使用简单的方式获取之间两点之间的距离
        /// </summary>
        /// <param name="target">Target <see cref="Coordinate"/>.</param>
        /// <returns>Represents the distance.</returns>
        public double SimpleDistance(Coordinate target)
        {
            return Math.Pow(this.X - target.X, 2) + Math.Pow(this.Y - target.Y, 2);
        }

        /// <summary>
        /// ==
        /// </summary>
        /// <param name="coordinate1">First <see cref="Coordinate"/></param>
        /// <param name="coordinate2">Second <see cref="Coordinate"/></param>
        /// <returns>bool</returns>
        public static bool operator ==(Coordinate coordinate1, Coordinate coordinate2)
        {
            return coordinate1.X == coordinate2.X && coordinate1.Y == coordinate2.Y;
        }

        /// <summary>
        /// !=
        /// </summary>
        /// <param name="coordinate1">First <see cref="Coordinate"/></param>
        /// <param name="coordinate2">Second <see cref="Coordinate"/></param>
        /// <returns>bool</returns>
        public static bool operator !=(Coordinate coordinate1, Coordinate coordinate2)
        {
            return !(coordinate1.X == coordinate2.X && coordinate1.Y == coordinate2.Y);
        }

        /// <summary>
        /// Parse the string to <see cref="Coordinate"/>.
        /// </summary>
        /// <param name="c">string with {x,y} formatation</param>
        /// <returns><see cref="Coordinate"/></returns>
        public static Coordinate Parse(string c)
        {
            string[] points = c.Split(',');
            return new Coordinate(int.Parse(points[0]), int.Parse(points[1]));
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"><see cref="Coordinate"/></param>
        /// <returns>is equal</returns>
        public bool Equals(Coordinate obj)
        {
            return obj.X == X && obj.Y == Y;
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj">target.</param>
        /// <returns>is equal</returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Coordinate)) return false;
            return Equals((Coordinate)obj);
        }

        /// <summary>
        /// GetHashcode
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return String.Format("Coordiante_{0}_{1}", X, Y).GetHashCode();
        }

        static readonly int MINXy = 1;
        static readonly int MAXx = Base.Defines.Pitch.MAX_WIDTH - 1;
        static readonly int MAXy = Base.Defines.Pitch.MAX_HEIGHT - 1;
        public Coordinate Regulate()
        {
            this.X = Math.Max(MINXy, Math.Min(MAXx, this.X));
            this.Y = Math.Max(MINXy, Math.Min(MAXy, this.Y));
            return this;
        }
        public bool Regulate(out Coordinate newCoordinate)
        {
            newCoordinate = this;
            double x = Math.Max(MINXy, Math.Min(MAXx, this.X));
            double y = Math.Max(MINXy, Math.Min(MAXy, this.Y));
            if (x == this.X && y == this.Y)
                return false;
            newCoordinate.X = x;
            newCoordinate.Y = y;
            return true;
        }
    }
}
