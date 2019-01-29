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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Games.NB.Match.Emulator.WPF.Controls
{
    /// <summary>
    /// MenuControl.xaml 的交互逻辑
    /// </summary>
    public partial class MenuControl : UserControl
    {
        public MenuControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 平衡测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemBalanceTest_Click(object sender, RoutedEventArgs e)
        {
            Batchtest win = new Batchtest();
            win.Show();
        }
        private void MenuItemEmulator_Click(object sender, RoutedEventArgs e)
        {
            Main demo = new Main();
            demo.Show();
        }

        private void MenuItemSetData_Click(object sender, RoutedEventArgs e)
        {
            LocalDataSettingWindow setting = new LocalDataSettingWindow();
            setting.ShowDialog();
        }

        private void MenuItemPVETest_Click(object sender, RoutedEventArgs e)
        {
            PveTestWindow win = new PveTestWindow();
            win.Show();
        }

        public string CurName { get; set; }

        private void MenuControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            switch (CurName)
            {
                case "Batch":
                    MenuItemBalanceTest.IsEnabled = false;
                    break;
                case "Emulator":
                    MenuItemEmulator.IsEnabled = false;
                    break;
                case "PVE":
                    MenuItemPVETest.IsEnabled = false;
                    break;
                case "SetData":
                    MenuItemSetData.IsEnabled = false;
                    break;
            }
        }
    }
}
