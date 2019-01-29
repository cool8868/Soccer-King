/********************************************************************************
 * 文件名：ModelMgr
 * 创建人：
 * 创建时间：5/5/2010 2:16:58 PM
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
using Games.NB.Match.Base.Enum;
using Games.NBall.Common;

namespace Games.NB.Match.Emulator.WPF.Mgrs
{
    static class ModelMgr
    {
        private static readonly Dictionary<string, string> _modelmapping = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> _skillmapping = new Dictionary<string, string>();
        static ModelMgr()
        {
            try
            {
                XDocument doc = XDocument.Load(System.AppDomain.CurrentDomain.BaseDirectory + @"\SkillConfig\SkillClient.xml");
                var elements = from item in doc.Descendants("Model")
                               select item;

                foreach (var item in elements)
                {
                    if (!_modelmapping.ContainsKey(item.Attribute("ModelId").Value))
                        _modelmapping.Add(item.Attribute("ModelId").Value, item.Attribute("SkillName").Value);
                }

                var elements2 = from item in doc.Descendants("Skill")
                                select item;

                foreach (var item in elements2)
                {
                    if (!_skillmapping.ContainsKey(item.Attribute("SkillId").Value))
                        _skillmapping.Add(item.Attribute("SkillId").Value, item.Attribute("SkillName").Value);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }

        }

        public static string GetModelStr(string key)
        {
            if (_modelmapping.ContainsKey(key))
                return _modelmapping[key];
            else
            {
                return "";
            }
        }

        public static string GetSkillStr(string key)
        {
            if (_skillmapping.ContainsKey(key))
                return _skillmapping[key];
            else
            {
                return "";
            }
        }
    }
}
