using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Games.NB.Match.BLL.Facade;

namespace Games.NB.Match.Emulator.WPF.Controls
{
    /// <summary>
    /// PveTestWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PveTestWindow : Window
    {
        public PveTestWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            MatchFacade.Boot();
        }
    }
}
