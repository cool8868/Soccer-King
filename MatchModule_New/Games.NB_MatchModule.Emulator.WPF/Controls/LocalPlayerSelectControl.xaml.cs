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
using Games.NB.Match.Emulator.WPF.Entity.LocalData;
using Games.NB.Match.Emulator.WPF.Mgrs;

namespace Games.NB.Match.Emulator.WPF.Controls
{
    /// <summary>
    /// LocalPlayerSelectControl.xaml 的交互逻辑
    /// </summary>
    public partial class LocalPlayerSelectControl : UserControl
    {
        public LocalPlayerSelectControl()
        {
            InitializeComponent();
        }

        private LocalTransferManagerEntity _managerEntity;
        private int _index;
        public void SetData(LocalTransferManagerEntity managerEntity,int index)
        {
            _managerEntity = managerEntity;
            var entity = _managerEntity.Players[_index];
            _index = index;
            lblClientId.Content = _index;
            lblPlayerName.Content = entity.Name;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

        }

        private void btnSetProperty_Click(object sender, RoutedEventArgs e)
        {
            var x = new LocalPlayerSettingWindow(_managerEntity,_index);
            x.ShowDialog();
        }
    }
}
