/********************************************************************************
 * 文件名：Region
 * 创建人：
 * 创建时间：2009-12-15 14:35:57
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents a rectangle region.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *  2010-1-24 10:36:25  1.1 Add comments.
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;

namespace Games.NB.Match.Base.Structs
{

    /// <summary>
    /// Represents a rectangle region.
    /// </summary>
    [Serializable]
    public struct Region
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Region"/>.
        /// </summary>
        /// <param name="x1">X1</param>
        /// <param name="y1">Y1</param>
        /// <param name="x2">X2</param>
        /// <param name="y2">Y2</param>
        public Region(int x1, int y1, int x2, int y2)
        {
            this.Start = new Coordinate(x1, y1);
            this.End = new Coordinate(x2, y2);
        }

        public Region(double x1, double y1, double x2, double y2)
        {
            this.Start = new Coordinate(x1, y1);
            this.End = new Coordinate(x2, y2);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Region"/>.
        /// </summary>
        /// <param name="start">Left-Top <see cref="Coordinate"/></param>
        /// <param name="end">Right-Down <see cref="Coordinate"/></param>
        public Region(Coordinate start, Coordinate end)
        {
            this.Start = start;
            this.End = end;
        }

        /// <summary>
        /// Left-Top <see cref="Coordinate"/>.
        /// </summary>
        public Coordinate Start;

        /// <summary>
        /// Right-Down <see cref="Coordinate"/>.
        /// </summary>
        public Coordinate End;

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("Region[Start:{0}, End:{1}]", Start, End);
        }

        /// <summary>
        /// Parse a string to <see cref="Region"/>.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Region ParseByStr(string str)
        {
            string[] strs = str.Split(',');

            if (strs.Length != 4)
            {
                throw new FormatException("Invokes Region class's ParseByStr method with errror arguments. Please recheck your incoming string's format(x1,y1,x2,y2).");
            }

            var points = new int[4];

            for (var i = 0; i < strs.Length; i++)
            {
                if (!int.TryParse(strs[i], out points[i]))
                {
                    throw new FormatException("Invokes Region class's ParseByStr method with error arguments. Plaase recheck your incoming string is composed by numbers(ex:1,1,2,2).");
                }
            }

            return new Region(points[0], points[1], points[2], points[3]);
        }

        /// <summary>
        /// Whether the first region is equals to the other one.
        /// </summary>
        /// <param name="region1">First <see cref="Region"/>.</param>
        /// <param name="region2">Another <see cref="Region"/>.</param>
        /// <returns>Whether they are eqaul.</returns>
        public static bool operator ==(Region region1, Region region2)
        {
            return (region1.Start == region2.Start) && (region2.Start == region2.End);
        }

        /// <summary>
        /// Whether the first region is not equals to the other one.
        /// </summary>
        /// <param name="region1">First <see cref="Region"/>.</param>
        /// <param name="region2">Another <see cref="Region"/>.</param>
        /// <returns>Whether they are not eaual</returns>
        public static bool operator !=(Region region1, Region region2)
        {
            return (region1.Start != region2.Start) || (region2.Start != region2.End);
        }

        /// <summary>
        /// Whether the current <see cref="Region"/> is equals to the incoming one.
        /// </summary>
        /// <param name="obj">Represents the incoming <see cref="Region"/>.</param>
        /// <returns></returns>
        public bool Equals(Region obj)
        {
            return obj.Start.Equals(Start) && obj.End.Equals(End);
        }

        /// <summary>
        /// Whether the current <see cref="Region"/> is equals to the incoming one.
        /// </summary>
        /// <param name="obj">Represents the incoming <see cref="Region"/>.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Region)) return false;
            return Equals((Region)obj);
        }

        /// <summary>
        /// Gets Hash Code
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Centrosymmetry a Region.
        /// </summary>
        /// <returns>The <see cref="Region"/> that be centrosymetryed.</returns>
        public Region Mirror()
        {
            return new Region(End.Mirror(), Start.Mirror());
        }

        /// <summary>
        /// Get a random point from the region.
        /// </summary>
        /// <returns>A random point in the region.</returns>
        public Coordinate GetRandomPoint(IRandom random)
        {
            return new Coordinate(random.RandomInt32((int)Start.X, (int)End.X), random.RandomInt32((int)Start.Y, (int)End.Y));
        }

        /// <summary>
        /// Validate the <see cref="Coordinate"/> is in the <see cref="Region"/>.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public bool IsCoordinateInRegion(Coordinate coordinate)
        {
            if (coordinate.X < Start.X)
            {
                return false;
            }

            if (coordinate.X > End.X)
            {
                return false;
            }

            if (coordinate.Y < Start.Y)
            {
                return false;
            }

            if (coordinate.Y > End.Y)
            {
                return false;
            }

            return true;
        }
    }
}
