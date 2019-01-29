using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Games.NBall.Common;

namespace Games.NB.Match.Emulator.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("应用程序出现了未捕获的异常，{0}\n", e.Exception.Message);
            if (e.Exception.InnerException != null)
            {
                stringBuilder.AppendFormat("\n {0}", e.Exception.InnerException.Message);
            }
            stringBuilder.AppendFormat("\n {0}", e.Exception.StackTrace);
            string s = stringBuilder.ToString();
            stringBuilder.Clear();
            LogHelper.Insert(s, LogType.Error);
            MessageBox.Show(s);
            e.Handled = true;
        }  
    }
}
