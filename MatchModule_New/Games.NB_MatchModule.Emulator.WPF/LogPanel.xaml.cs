/*****************************************************************************
 * 文件名：LogPanel
 * 
 * 创建人：
 * 
 * 创建时间：2009-11-12 21:14:02
 * 
 * 功能说明：
 *  
 * 历史修改记录：
 * 
 * ***************************************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Games.NB.Match.Emulator.WPF {

    /// <summary>
    /// Represents the emulator's log panel.
    /// </summary>
    public partial class LogPanel : Window {        
        private readonly Queue<string> _queue = new Queue<string>(500);               

        /// <summary>
        /// Initializes a new Log Panel.
        /// </summary>
        public LogPanel() {
            InitializeComponent();                        
            var listener = new DebugTrace();
            listener.OnDebug += (o, e) => { ShowLog(e.Message); };
            Debug.Listeners.Add(listener);
        }        

        public void ClearAll() {
            this.logPanel.Children.Clear();
            this.logScroll.ScrollToEnd();
        }

        public void ScrollToTop() {
            this.logScroll.ScrollToTop();
        }

        public void ScrollToEnd() {
            this.logScroll.ScrollToEnd();
        }

        class DebugTrace : TraceListener {
            public EventHandler<TraceEventArgs> OnDebug { get; set; }

            public override void Write(string message) {
                WriteLine(message);
            }

            public override void WriteLine(string message) {
                if (OnDebug != null) {
                    OnDebug(this, new TraceEventArgs() { Message = message });
                }
            }
        }

        class TraceEventArgs : EventArgs {
            public string Message { get; set; }
        }

        private void btnClearAll_Click(object sender, RoutedEventArgs e) {
            ClearAll();
        }

        private void btnScrollTop_Click(object sender, RoutedEventArgs e) {
            ScrollToTop();
        }

        /// <summary>
        /// Get a log from the debug listener.
        /// </summary>
        /// <param name="log"></param>
        private void ShowLog(string log) {
            WriteLogDelegate method = WriteLog;
            logPanel.Dispatcher.Invoke(method, new object[] { log });
        }

        private void WriteLog(string log) {            
            var color = Colors.Black;
            if (log.Length > 0 && log[0] == '#') {
                var strColor = log.Substring(1, log.IndexOf('#', 1) - 1);
                var strColors = strColor.Split(',');
                color = new Color() { R = byte.Parse(strColors[0]), G = byte.Parse(strColors[1]), B = byte.Parse(strColors[2]), A = 255 };
                log = log.Remove(0, log.IndexOf('#', 1) + 1);
            }
            this.logPanel.Children.Add(new TextBlock { Text = log, Foreground = new SolidColorBrush(color) });             
        }

        private delegate void WriteLogDelegate(string log);
    }

    public class LogEventArgs : EventArgs {
        public string Log { get; set; }
    }
}
