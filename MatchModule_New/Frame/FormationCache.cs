/********************************************************************************
 * 文件名：FormationCache
 * 创建人：
 * 创建时间：2009-12-23 20:29:28
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
using System.Xml.Linq;
using Games.NB.Match.Log;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Common;
using Games.NB.Match.Frame.Model;

namespace Games.NB.Match.Frame
{
    public sealed class FormationCache 
    {
        #region Cache
        static Dictionary<int, List<FormationEntity>> s_dicForm = new Dictionary<int, List<FormationEntity>>();
        static Dictionary<double, double> s_dicHomeX = new Dictionary<double, double>();
        #endregion

        #region .ctor
        static FormationCache()
        {
            var _doc = XDocument.Load(string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, @"config\Formation.xml"));
            s_dicHomeX.Clear();
            s_dicForm.Clear();
            var xes = _doc.Root.Element("HomeXMap").Elements("HomeX");
            int globalX = 0;
            int homeX = 0;
            foreach (var x in xes)
            {
                globalX = Convert.ToInt32(x.Attribute("global").Value);
                homeX = Convert.ToInt32(x.Attribute("home").Value);
                s_dicHomeX[globalX] = homeX;
            }
            int formId = 0;
            var forms = _doc.Descendants("Formation");
            List<FormationEntity> list = null;
            foreach (var f in forms)
            {
                formId = Convert.ToInt32(f.Attribute("id").Value);
                if (s_dicForm.ContainsKey(formId))
                    continue;
                list = new List<FormationEntity>(Defines.Match.MAX_PLAYER_COUNT);
                foreach (var p in f.Elements("Player"))
                {
                    list.Add(new FormationEntity
                    {
                        Position = (Position)Convert.ToInt32(p.Attribute("type").Value),
                        Default = Coordinate.Parse(p.Attribute("coordinate").Value),
                    });
                }
                foreach (var item in list)
                {
                    item.HalfDefault = new Coordinate(CastHomeX(item.Default.X), item.Default.Y);
                }
                s_dicForm[formId] = list;
            }
            LogHelper.Insert("FormationCache booted.", LogType.Info);
        }
        static double CastHomeX(double globleX)
        {
            double val;
            if (!s_dicHomeX.TryGetValue(globleX, out val))
                return globleX;
            return val;
        }
        #endregion

        #region Facade
        public static void Boot()
        { }
        public static List<FormationEntity> GetFormation(int formationId)
        {
            List<FormationEntity> list;
            s_dicForm.TryGetValue(formationId, out list);
            return list;
        }
        public static FormationEntity GetFormationByPlayerIndex(int formationId, int playerIndex)
        {
            var obj = GetFormation(formationId);
            if (null == obj || playerIndex >= obj.Count)
                return null;
            return obj[playerIndex];
        }
        #endregion
    }
}
