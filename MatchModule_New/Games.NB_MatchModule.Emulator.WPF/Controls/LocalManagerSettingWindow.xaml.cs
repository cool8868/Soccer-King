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
using Games.NBall.Common;

namespace Games.NB.Match.Emulator.WPF.Controls
{
    /// <summary>
    /// LocalManagerSettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LocalManagerSettingWindow : Window
    {
        public LocalManagerSettingWindow()
        {
            InitializeComponent();
        }

        private LocalTransferManagerEntity _localTransfer;
        private bool _isNew;
        public LocalManagerSettingWindow(LocalTransferManagerEntity entity, bool isNew)
        {
            InitializeComponent();
            _localTransfer = entity;
            _isNew = isNew;
            Init();

        }

        void Init()
        {
            LocalHelper.BindManagerTree(cmbTalent);
            LocalHelper.BindWill(cmbManagerWill);
            LocalHelper.BindSuit(cmbSuit);
            LocalHelper.BindFormation(cmbFormation);
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            txtManagerName.Text = _localTransfer.Name;
            ComboBoxHelper.SetSelectItem(cmbManagerWill, _localTransfer.WillId);
            ComboBoxHelper.SetSelectItem(cmbTalent, _localTransfer.TalentId);
            ComboBoxHelper.SetSelectItem(cmbSuit, _localTransfer.SuitId);
            ComboBoxHelper.SetSelectItem(cmbFormation, _localTransfer.FormationId);
            txtFormationLevel.Text = _localTransfer.FormationLevel.ToString();
            datagridPlayer.ItemsSource = _localTransfer.Players;
            lblKpi.Content = _localTransfer.Kpi;
        }

        private void btnSettingPlayer_Click(object sender, RoutedEventArgs e)
        {
            var item = datagridPlayer.SelectedItem as LocalTransferPlayerEntity;
            if (item != null)
            {
                LocalPlayerSettingWindow window = new LocalPlayerSettingWindow(_localTransfer,item.Index);
                window.ShowDialog();
            }
            datagridPlayer.Items.Refresh();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = txtManagerName.Text;
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请取一个有意义的经理名.");
                return;
            }
            var check = LocalHelper.ManagerList.Managers.Exists(d => d.Name == name && d.Id != _localTransfer.Id);
            if (check)
            {
                MessageBox.Show("该经理名已经存在，请换一个.");
                return;
            }

            _localTransfer.TalentId = ComboBoxHelper.GetSelectValueInt(cmbTalent);
            _localTransfer.WillId = ComboBoxHelper.GetSelectValueInt(cmbManagerWill);
            _localTransfer.SuitId = ComboBoxHelper.GetSelectValueInt(cmbSuit);
            _localTransfer.FormationId = ComboBoxHelper.GetSelectValueInt(cmbFormation);
            _localTransfer.FormationLevel = ConvertHelper.ConvertToInt(txtFormationLevel.Text);

            _localTransfer.Name = name;
            LocalManagerHelper.SaveBefore(_localTransfer);
            if (_isNew)
                LocalHelper.ManagerList.Managers.Add(_localTransfer);
            lblKpi.Content = _localTransfer.Kpi;
            LocalHelper.SaveLocalTransferManager();
            this.DialogResult = true;
            this.Close();
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
