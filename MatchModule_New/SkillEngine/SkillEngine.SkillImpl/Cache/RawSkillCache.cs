using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.Configuration;
using System.IO;
using SkillEngine.Extern;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;


namespace SkillEngine.SkillImpl
{
    public class RawSkillCache
    {
        #region Define
        const string CFGFileName = "SkillConfig/SkillConfig.xml";
        const string CFGHideFileName = "SkillConfig/SkillConfigHide.xml";
        const string KEYType = "type";
        const string KEYAdd = "add";
        const string PREFIXCtor = "c.";
        const string PREFIXProperty = "p.";
        const char SPLITArray = ',';
        const int PREFIXLen = 2;
        #endregion

        #region Cache
        static readonly Dictionary<string, Type> s_dicTypes = new Dictionary<string, Type>();
        static readonly Dictionary<string, IRawSkill> s_dicSkills = new Dictionary<string, IRawSkill>();
        static readonly List<IRawSkill> s_lstHideSkills = new List<IRawSkill>();
        public readonly bool TRACEPlayerSkillFlag = false;
        #endregion

        #region Singleton
        static readonly object s_lockObj = new object();
        static RawSkillCache s_instnce = null;
        public readonly bool InitFlag = false;
        public static RawSkillCache Instance()
        {
            if (null == s_instnce || !s_instnce.InitFlag)
            {
                lock (s_lockObj)
                {
                    if (null == s_instnce || !s_instnce.InitFlag)
                    {
                        s_instnce = new RawSkillCache();
                    }
                }
            }
            return s_instnce;
        }
        #endregion

        #region .ctor
        private RawSkillCache()
        {
            try
            {
                this.InitFlag = false;
                s_dicTypes.Clear();
                s_dicSkills.Clear();
                s_lstHideSkills.Clear();
                var xd = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + CFGFileName);
                string typeKey, typeName, fileName;
                typeKey = typeName = fileName = string.Empty;
                foreach (var xe in xd.Root.Element("Types").Elements("Type"))
                {
                    typeKey = xe.Attribute("name").Value;
                    typeName = xe.Attribute("typeName").Value;
                    fileName = xe.Attribute("fileName").Value;
                    s_dicTypes[typeKey] = Assembly.Load(fileName).GetType(typeName, false, true);
                }
                IRawSkill skill = null;
                foreach (var xes in xd.Root.Elements("Skills"))
                {
                    foreach (var xe in xes.Elements("Skill"))
                    {
                        skill = BuildObj<IRawSkill>(xe);
                        if (null != skill)
                            s_dicSkills[skill.SkillCode] = skill;
                    }
                }
                string hideFile = AppDomain.CurrentDomain.BaseDirectory + CFGHideFileName;
                if (File.Exists(hideFile))
                {
                    xd = XDocument.Load(hideFile);
                    foreach (var xes in xd.Root.Elements("Skills"))
                    {
                        foreach (var xe in xes.Elements("Skill"))
                        {
                            skill = BuildObj<IRawSkill>(xe);
                            if (null != skill)
                                s_lstHideSkills.Add(skill);
                        }
                    }
                }
                string cfg = ConfigurationManager.AppSettings["TRACEPlayerSkillFlag"] ?? string.Empty;
                bool val;
                if (bool.TryParse(cfg, out val))
                    TRACEPlayerSkillFlag = val;
                this.InitFlag = true;
            }
            catch (Exception ex)
            {
                LogUtil.Error("RawSkillCache:Init", ex);
                this.InitFlag = false;
            }
        }
        #endregion

        #region Facade
        public void Init()
        { }
        public bool TryGetRawSkill(string skillCode, out IRawSkill rawSkill)
        {
            return s_dicSkills.TryGetValue(skillCode, out rawSkill);
        }
        public List<IRawSkill> GetHideSkills()
        {
            return s_lstHideSkills;
        }
        #endregion

        #region BuildObj
        static T BuildObj<T>(XElement xe)
        {
            try
            {
                var obj = BuildObj(typeof(T), xe);
                if (null == obj)
                    return default(T);
                return (T)obj;
            }
            catch (Exception ex)
            {
                LogUtil.Error(string.Concat("BuildObj:", typeof(T).FullName), ex);
                return default(T);
            }
        }
        static object BuildObj(Type t, object obj)
        {
            if (obj is string)
                return BuildObj(t, obj as string);
            else
                return BuildObj(t, obj as XElement);
        }
        static object BuildObj(Type t, XElement xe)
        {
            try
            {
                var txa = xe.Attribute(KEYType);
                if (null != txa)
                {
                    Type t2 = GetType(txa.Value);
                    if (null == t || null != t2 && t.IsAssignableFrom(t2))
                        t = t2;
                }
                if (t == typeof(String) || t.IsPrimitive)
                    return BuildObj(t, xe.Value);
                var dic = new Dictionary<string, object>(8);
                int cntCtor = 0;
                foreach (var xa in xe.Attributes())
                {
                    if (xa.Name.LocalName.Length < PREFIXLen)
                        continue;
                    if (xa.Name.LocalName.StartsWith(PREFIXCtor))
                        cntCtor++;
                    switch (xa.Name.LocalName.Substring(0, PREFIXLen))
                    {
                        case PREFIXCtor:
                        case PREFIXProperty:
                            dic[xa.Name.LocalName] = xa.Value;
                            break;
                    }
                }
                foreach (var inXe in xe.Elements())
                {
                    if (inXe.Name.LocalName.Length < PREFIXLen)
                        continue;
                    if (inXe.Name.LocalName.StartsWith(PREFIXCtor))
                        cntCtor++;
                    switch (inXe.Name.LocalName.Substring(0, PREFIXLen))
                    {
                        case PREFIXCtor:
                        case PREFIXProperty:
                            dic[inXe.Name.LocalName] = inXe;
                            break;
                    }
                }
                bool hitFlag = true;
                object obj = null;
                object tmp = null;
                foreach (var ctor in t.GetConstructors())
                {
                    var parms = ctor.GetParameters();
                    if (parms.Length != cntCtor)
                        continue;
                    hitFlag = true;
                    foreach (var parm in parms)
                    {
                        if (!dic.ContainsKey(string.Concat(PREFIXCtor, parm.Name)))
                        {
                            hitFlag = false;
                            break;
                        }
                    }
                    if (!hitFlag)
                        continue;
                    object[] args = new object[parms.Length];
                    for (int i = 0; i < parms.Length; i++)
                    {
                        if (dic.TryGetValue(string.Concat(PREFIXCtor, parms[i].Name), out tmp))
                            dic.Remove(string.Concat(PREFIXCtor, parms[i].Name));
                        args[i] = BuildObj(parms[i].ParameterType, tmp);
                    }
                    obj = ctor.Invoke(args);
                    break;
                }
                if (null == obj)
                    return obj;
                if (dic.Count > 0)
                {
                    foreach (var kvp in dic)
                    {
                        var prop = t.GetProperty(kvp.Key.Substring(PREFIXLen));
                        if (null == prop)
                            continue;
                        prop.SetValue(obj, BuildObj(prop.PropertyType, kvp.Value), null);
                    }
                    dic.Clear();
                }
                if (!typeof(IList).IsAssignableFrom(t))
                    return obj;
                var gt = t.GetGenericArguments();
                if (gt.Length != 1)
                    return obj;
                foreach (var inXe in xe.Elements(KEYAdd))
                {
                    tmp = BuildObj(gt[0], inXe);
                    if (null == tmp)
                        LogUtil.Info(string.Format("BuildObj:Null {0} {1}", gt[0].FullName, inXe.ToString()));
                    else
                        ((IList)obj).Add(tmp);
                }
                return obj;
            }
            catch (Exception ex)
            {
                LogUtil.Error(string.Concat("BuildObj:",t.FullName), ex);
                throw ex;
            }
        }
        static object BuildObj(Type t, string s)
        {
            if (t.IsArray)
            {
                if (string.IsNullOrEmpty(s))
                    return null;
                Type et = t.GetElementType();
                var splits = s.Split(SPLITArray);
                var array = Array.CreateInstance(et, splits.Length);
                for (int i = 0; i < splits.Length; i++)
                {
                    ((IList)array)[i] = BuildObj(et, splits[i]);
                }
                return array;
            }
            if (t == typeof(String))
                return s;
            if (t.IsEnum)
                return Enum.Parse(t, s);
            if (t.IsValueType)
            {
                var objs = new object[] { s, null };
                t.GetMethod("TryParse", new[] { typeof(string), t.MakeByRefType() }).Invoke(null, objs);
                return objs[1];
            }
            return null;
        }
        static Type GetType(string typeKey)
        {
            Type t;
            s_dicTypes.TryGetValue(typeKey, out t);
            return t;
        }
        #endregion
    }
}
