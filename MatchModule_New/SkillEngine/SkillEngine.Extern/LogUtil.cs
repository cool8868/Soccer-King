using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillEngine.Extern
{
    public static class LogUtil
    {
        public static void Error(string msg, Exception ex)
        {
            Games.NB.Match.Log.LogHelper.Insert(ex, msg);
        }

        public static void Info(string msg)
        {
            Games.NB.Match.Log.LogHelper.Insert(msg, Games.NB.Match.Log.LogType.Info);
        }

        public static void Info(string msg, params object[] args)
        {
            Games.NB.Match.Log.LogHelper.Insert(string.Format(msg, args), Games.NB.Match.Log.LogType.Info);
        }
    }
}
