using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Reflection;
using SkillEngine.Editor.Football.Util;
using SkillEngine.SkillBase;

namespace SkillEngine.Editor.Football.Rules
{
    public static class CheckRules
    {
        #region Define
        const string KEYType = "type";
        const string KEYAdd = "add";
        const string PREFIXCtor = "c.";
        const string PREFIXProperty = "p.";
        const char SPLITArray = ',';
        const int PREFIXLen = 2;
        #endregion

        #region Facade
        public static bool CheckFile(string filePath)
        {
            try
            {
                var dicTypes = new Dictionary<string, Type>();
                var xd = XDocument.Load(filePath);
                string typeKey, typeName, fileName;
                typeKey = typeName = fileName = string.Empty;
                foreach (var xe in xd.Root.Element("Types").Elements("Type"))
                {
                    typeKey = xe.Attribute("name").Value;
                    typeName = xe.Attribute("typeName").Value;
                    fileName = xe.Attribute("fileName").Value;
                    dicTypes[typeKey] = Assembly.Load(fileName).GetType(typeName, false, true);
                }
                foreach (var xes in xd.Root.Elements("Skills"))
                {
                    foreach (var xe in xes.Elements("Skill"))
                    {
                        BuildObj<IRawSkill>(xe, dicTypes);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
 
        }
        #endregion

        #region BuildObj
        static T BuildObj<T>(XElement xe, Dictionary<string, Type> dicTypes)
        {
            try
            {
                var obj = BuildObj(typeof(T), xe, dicTypes);
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
        static object BuildObj(Type t, object obj, Dictionary<string, Type> dicTypes)
        {
            if (obj is string)
                return BuildObj(t, obj as string);
            else
                return BuildObj(t, obj as XElement, dicTypes);
        }
        static object BuildObj(Type t, XElement xe, Dictionary<string, Type> dicTypes)
        {
            try
            {
                var txa = xe.Attribute(KEYType);
                if (null != txa)
                {
                    Type t2 = null;
                    dicTypes.TryGetValue(txa.Value, out t2);
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
                        args[i] = BuildObj(parms[i].ParameterType, tmp, dicTypes);
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
                        prop.SetValue(obj, BuildObj(prop.PropertyType, kvp.Value, dicTypes), null);
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
                    tmp = BuildObj(gt[0], inXe, dicTypes);
                    if (null == tmp)
                        LogUtil.Info(string.Format("BuildObj:Null {0} {1}", gt[0].FullName, inXe.ToString()));
                    else
                        ((IList)obj).Add(tmp);
                }
                return obj;
            }
            catch (Exception ex)
            {
                LogUtil.Error(string.Concat("BuildObj:", t.FullName), ex);
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
        #endregion
    }
}
