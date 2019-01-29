using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Games.NB.Match.Log;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Structs;

namespace Games.NB.Match.Frame
{
    public sealed class MoveRegionCache
    {
        #region Cache
        static readonly Dictionary<string, Coordinate> s_dicX = new Dictionary<string, Coordinate>();
        static readonly Dictionary<string, Coordinate> s_dicY = new Dictionary<string, Coordinate>();
        #endregion

        #region .ctor
        static MoveRegionCache()
        {
            var _doc = XDocument.Load(string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, @"config\RegionConfig.xml"));
            s_dicX.Clear();
            s_dicY.Clear();
            string type = string.Empty;
            string key = string.Empty;
            string point = string.Empty;
            foreach (var xer in _doc.Root.Elements("positions"))
            {
                type = xer.Attribute("type").Value;
                foreach (var xe in xer.Elements("add"))
                {
                    key = xe.Attribute("key").Value;
                    point = xe.Attribute("value").Value;
                    if (type == "x")
                    {
                        s_dicX[key] = Coordinate.Parse(point);
                    }
                    else if (type == "y")
                    {
                        s_dicY[key] = Coordinate.Parse(point);
                    }
                }
            }
            LogHelper.Insert("MoveRegionCache booted.", LogType.Info);
        }
        #endregion

        #region Facade
        public static void Boot()
        { }
        public static Region GetMoveRegion(Coordinate position)
        {

            Coordinate xPos, yPos;
            if (!s_dicX.TryGetValue(position.X.ToString(), out xPos)
                || !s_dicY.TryGetValue(position.Y.ToString(), out yPos))
                throw new Exception(String.Format("Can't find MoveRegion:{0}", position));
            var start = new Coordinate(xPos.X, yPos.X);
            var end = new Coordinate(xPos.Y, yPos.Y);
            return new Region(start, end);
        }
        #endregion
    }
}
