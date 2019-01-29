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
using Games.NB.Match.Emulator.WPF.Entity.LocalData;
using Games.NB.Match.Emulator.WPF.Mgrs;

namespace Games.NB.Match.Emulator.WPF.Controls
{
    /// <summary>
    /// ManagerSelectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ManagerSelectWindow : Window
    {
        public ManagerSelectWindow()
        {
            InitializeComponent();
        }

        private void CmbType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isLoad)
            {
                BindData();
            }
        }

        private bool _isLoad;
        private void ManagerSelectWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_isLoad)
                return;
            ComboBoxHelper.BindComboBox(cmbType,LocalHelper.NpcTypes);
            cmbType.SelectedIndex = 0;
            BindData();
            _isLoad = true;
        }

        void BindData()
        {
            var type = ComboBoxHelper.GetSelectValueInt(cmbType);
            if (type == 99)
            {
                var list = new List<LocalNpcEntity>();
                foreach (var manager in LocalHelper.LocalManagerDic)
                {
                    LocalNpcEntity entity = new LocalNpcEntity(){Kpi = manager.Value.Kpi,Name = manager.Value.Name
                    ,NpcId = manager.Key};
                    list.Add(entity);
                }
                datagridList.ItemsSource = list;
            }
            else
            {
                datagridList.ItemsSource = LocalHelper.LocalCache.Npcs.FindAll(d => d.Type == type);
            }
            
        }

        private void BtnSelect_OnClick(object sender, RoutedEventArgs e)
        {
            var item = datagridList.SelectedItem as LocalNpcEntity;
            if (item != null)
            {
                LocalHelper.CurrentNpc = item;
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
