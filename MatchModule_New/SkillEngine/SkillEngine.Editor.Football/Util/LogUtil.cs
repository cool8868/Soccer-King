using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace SkillEngine.Editor.Football.Util
{
    public class LogUtil
    {
        static readonly ILog s_log;
        static LogUtil()
        {
            XmlConfigurator.Configure();
            s_log = LogManager.GetLogger("root");

        }
        public static void Error(string msg, Exception ex)
        {
            s_log.Error(msg, ex);
            if (null != ex && null != ex.InnerException)
            {
                s_log.Error(msg, ex.InnerException);
            }
        }
        public static void Info(string msg)
        {
            s_log.Info(msg);
        }
        public static void InfoFormat(string msg, params object[] args)
        {
            s_log.Info(string.Format(msg, args));
        }
        public static void Debug(string msg)
        {
            s_log.Debug(msg);
        }
        public static void DebugFormat(string msg, params object[] args)
        {
            s_log.Debug(string.Format(msg, args));
        }
    }
}
