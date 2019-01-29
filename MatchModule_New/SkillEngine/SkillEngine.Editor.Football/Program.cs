using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SkillEngine.Editor.Football.Util;

namespace SkillEngine.Editor.Football
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SkillEngine.Editor.Football.UI.SkillListForm());
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            string msg=ex.Message+"\n"+ex.StackTrace;
            LogUtil.Error("AppError", ex);
            MessageBox.Show(msg, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var ex = e.Exception;
            string msg = ex.Message + "\n" + ex.StackTrace;
            LogUtil.Error("AppError", ex);
            MessageBox.Show(msg, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
