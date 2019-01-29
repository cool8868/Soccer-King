/********************************************************************************
 * 文件名：ReflectCreater
 * 创建人：
 * 创建时间：2009-12-12 16:03:07
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
using System.Reflection;
using System.Diagnostics;

namespace Games.NB.Match.Common.Reflector {
    public class ReflectCreater {
        public static T Create<T>(string type, string assembly) where T : class {            
            Assembly assemblyRef = Assembly.Load(new AssemblyName(assembly));           
            return assemblyRef.CreateInstance(type) as T;            
        }

        public static T Create<T>(string config) where T : class {
            string[] configs = config.Split(',');

            if (configs.Length != 2) {
                throw new ArgumentException("传入的配置文件不足，请检查配置是否满足[Type,Assembly]的格式");
            }

            configs[0] = configs[0].Trim();
            configs[1] = configs[1].Trim();

            if (configs[0].Length == 0) {
                throw new ArgumentNullException("Type为空，请检查配置是否满足[Type,Assembly]的格式");
            }

            if (configs[1].Length == 1) {
                throw new ArgumentNullException("Assembly为空，请检查配置是否满足[Type,Assembly]的格式");
            }

            return Create<T>(configs[0], configs[1]);
        }
    }
}
