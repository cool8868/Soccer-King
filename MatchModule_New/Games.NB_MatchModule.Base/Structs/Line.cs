/********************************************************************************
 * 文件名：Line
 * 创建人：
 * 创建时间：2009-12-16 9:49:15
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
using SkillEngine.Extern;

namespace Games.NB.Match.Base.Structs
{

    /// <summary>
    /// Represents a <see cref="Line"/>.
    /// </summary>
    public struct Line
    {
        /// <summary>
        /// Creates the instance of the <see cref="Line"/> class.
        /// </summary>
        /// <param name="start">Start Coordinate</param>
        /// <param name="end">End Coordinate</param>
        public Line(Coordinate start, Coordinate end)
        {
            this.Start = start;
            this.End = end;
        }

        /// <summary>
        /// Creates a line with coordinates
        /// </summary>
        /// <param name="x1">x1</param>
        /// <param name="y1">y1</param>
        /// <param name="x2">x2</param>
        /// <param name="y2">y2</param>
        public Line(int x1, int y1, int x2, int y2)
        {
            this.Start = new Coordinate(x1, y1);
            this.End = new Coordinate(x2, y2);
        }

        /// <summary>
        /// Start <see cref="Coordinate"/>
        /// </summary>
        public Coordinate Start;

        /// <summary>
        /// End <see cref="Coordinate"/>
        /// </summary>
        public Coordinate End;

        /// <summary>
        /// Gets the random point from the random.
        /// </summary>
        /// <returns></returns>
        public Coordinate GetRandomPoint(IRandom random)
        {
            if (this.Start.X == this.End.X)
            {
                return new Coordinate(this.Start.X, random.RandomInt32((int)this.Start.Y, (int)this.End.Y));
            }

            if (this.Start.Y == this.End.Y)
            {
                return GetCoordinateByX(random.RandomInt32((int)this.Start.X, (int)this.End.X));
            }

            if (random.RandomBool())
            {
                return GetCoordinateByX(random.RandomInt32((int)this.Start.X, (int)this.End.X));
            }
            else
            {
                return GetCoordinateByY(random.RandomInt32((int)this.Start.Y, (int)this.End.Y));
            }
        }

        /// <summary>
        /// Parse <see cref="Line"/> by string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Line ParseByStr(string str)
        {
            string[] strs = str.Split(',');

            if (strs.Length != 4)
            {
                throw new FormatException(String.Format("Line.ParseByStr接受了错误的参数，请检查传入的字符串是否满足x1,y1,x2,y2的格式, 原字符串:{0}", str));
            }

            var points = new int[4];

            for (var i = 0; i < strs.Length; i++)
            {
                if (!int.TryParse(strs[i], out points[i]))
                {
                    throw new FormatException(String.Format("Line.ParseByStr接受了错误的参数，请检查传入的字符串中是否是由数字组成, 原字符串:{0}", str));
                }
            }

            return new Line(points[0], points[1], points[2], points[3]);
        }

        #region encapsulation

        private Coordinate GetCoordinateByX(int x)
        {
            var y = (End.Y - Start.Y) / (End.X - Start.X) * x;
            return new Coordinate(x, y);
        }

        private Coordinate GetCoordinateByY(int y)
        {
            var x = y / ((End.Y - Start.Y) / (End.X - Start.X));
            return new Coordinate(x, y);
        }

        #endregion

    }
}
