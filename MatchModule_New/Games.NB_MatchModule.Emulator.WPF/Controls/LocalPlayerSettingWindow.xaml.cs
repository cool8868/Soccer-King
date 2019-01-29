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
using Games.NB.Match.Emulator.WPF.Entity;
using Games.NB.Match.Emulator.WPF.Entity.LocalData;
using Games.NB.Match.Emulator.WPF.Mgrs;
using Games.NBall.Entity;

namespace Games.NB.Match.Emulator.WPF.Controls
{
    /// <summary>
    /// LocalPlayerSettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LocalPlayerSettingWindow : Window
    {
        public LocalPlayerSettingWindow()
        {
            InitializeComponent();
        }
        
        private LocalTransferPlayerEntity _player;
        private LocalTransferManagerEntity _manager;
        private List<LocalDicPlayer> _playerCache; 
        public LocalPlayerSettingWindow(LocalTransferManagerEntity entity,int index)
        {
            InitializeComponent();
            _manager = entity;
            _player = entity.Players[index];
            _playerCache = LocalHelper.LocalCache.Players;
            _dicPlayer = _playerCache.Find(d => d.Idx == _player.PlayerId);
        }

        private bool _isLoad = false;
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (_isLoad)
                return;
            BindStarSkill(cmbStarSkill);
            BindComboBox();
            lblPlayerName.Content = _player.Name;
            txtSpeed.Text = _player.Speed.ToString("f2");
            txtShooting.Text = _player.Shooting.ToString("f2");
            txtFreeKick.Text = _player.FreeKick.ToString("f2");
            txtBalance.Text = _player.Balance.ToString("f2");
            txtStamina.Text = _player.Stamina.ToString("f2");
            txtStrength.Text = _player.Strength.ToString("f2");
            txtAggression.Text = _player.Aggression.ToString("f2");
            txtDisturb.Text = _player.Disturb.ToString("f2");
            txtInterception.Text = _player.Interception.ToString("f2");
            txtDribble.Text = _player.Dribble.ToString("f2");
            txtPassing.Text = _player.Passing.ToString("f2");
            txtMentality.Text = _player.Mentality.ToString("f2");
            txtReflexes.Text = _player.Reflexes.ToString("f2");
            txtPositioning.Text = _player.Positioning.ToString("f2");
            txtHandling.Text = _player.Handling.ToString("f2");
            txtAcceleration.Text = _player.Acceleration.ToString("f2");

            playerSkillControl1.Init("技能1", _player.Skill);
            playerSkillControl2.Init("技能2", _player.Skill2);

            if (!string.IsNullOrEmpty(_player.StarSkill))
            {
                ComboBoxHelper.SetSelectItem(cmbStarSkill, _player.StarSkill);
            }
            _isLoad = true;
        }

        void BindComboBox()
        {
            var list = new List<KeyValueComboBoxItem>();
            list.Add(new KeyValueComboBoxItem(0, "请选择"));
            list.Add(new KeyValueComboBoxItem(1, "金"));
            list.Add(new KeyValueComboBoxItem(2, "橙"));
            list.Add(new KeyValueComboBoxItem(3, "紫"));
            list.Add(new KeyValueComboBoxItem(4, "蓝"));
            list.Add(new KeyValueComboBoxItem(5, "绿"));
            
            ComboBoxHelper.BindComboBox(cmbItemSubType,list);
            cmbItemSubType.SelectedIndex = 0;

            var list2 = new List<KeyValueComboBoxItem>();
            list2.Add(new KeyValueComboBoxItem(0, "所有"));
            list2.Add(new KeyValueComboBoxItem(1, "英超联赛"));
            list2.Add(new KeyValueComboBoxItem(2, "西甲联赛"));
            list2.Add(new KeyValueComboBoxItem(3, "德甲联赛"));
            list2.Add(new KeyValueComboBoxItem(4, "意甲联赛"));
            list2.Add(new KeyValueComboBoxItem(5, "法甲联赛"));
            ComboBoxHelper.BindComboBox(cmbItemThirdType, list2);
            cmbItemThirdType.SelectedIndex = 0;
            BindItem();
        }

        private void CmbItemSubType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isLoad)
            {
                BindItem();
            }
        }

        private void CmbItemThirdType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isLoad)
            {
                BindItem();
            }
        }

        private void CmbItemCode_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_isLoad)
            {
                var itemCode = ComboBoxHelper.GetSelectValueInt(cmbItemCode);
                if(itemCode<=0)
                    return;
                _dicPlayer = _playerCache.Find(d => d.Idx == itemCode);
                if(_dicPlayer==null)
                    return;
                lblPlayerName.Content = _dicPlayer.Name;
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private LocalDicPlayer _dicPlayer;
        private void btnSure_Click(object sender, RoutedEventArgs e)
        {
            if (_dicPlayer==null)
            {
                MessageBox.Show("请选择一个球员!");
                return;
            }
            var check = _manager.Players.Exists(d =>d.Index!=_player.Index && d.PlayerId == _dicPlayer.Idx);
            if (check)
            {
                MessageBox.Show("该球员已经存在，请换一个.");
                return;
            }

            try
            {
                _player.PlayerId = _dicPlayer.Idx;
                _player.Name = _dicPlayer.Name;
                _player.Speed = GetTxtValue(txtSpeed);
                _player.Shooting = GetTxtValue(txtShooting);
                _player.FreeKick = GetTxtValue(txtFreeKick);
                _player.Balance = GetTxtValue(txtBalance);
                _player.Stamina = GetTxtValue(txtStamina);
                _player.Strength = GetTxtValue(txtStrength);
                _player.Aggression = GetTxtValue(txtAggression);
                _player.Disturb = GetTxtValue(txtDisturb);
                _player.Interception = GetTxtValue(txtInterception);
                _player.Dribble = GetTxtValue(txtDribble);
                _player.Passing = GetTxtValue(txtPassing);
                _player.Mentality = GetTxtValue(txtMentality);
                _player.Reflexes = GetTxtValue(txtReflexes);
                _player.Positioning = GetTxtValue(txtPositioning);
                _player.Handling = GetTxtValue(txtHandling);
                _player.Acceleration = GetTxtValue(txtAcceleration);


                _player.Skill = playerSkillControl1.GetSkill();
                _player.Skill2 = playerSkillControl2.GetSkill();
                _player.StarSkill = ComboBoxHelper.GetSelectValue(cmbStarSkill);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            LocalManagerHelper.SaveBefore(_manager);
            lblPlayerKpi.Content = "Kpi:" + _player.Kpi;
            LocalHelper.SaveLocalTransferManager();
            this.DialogResult = true;
            this.Close();
        }

        private double GetTxtValue(TextBox control)
        {
            string x = control.Text;
            if(string.IsNullOrEmpty(x))
                throw new Exception("球员属性不能为空");
            var y = EmulatorHelper.ConvertToDouble(x);
            if (y < 1)
                y = 1;
            if (y > 5000)
                y = 5000;
            return y;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string x = txtBatch.Text;
            if (string.IsNullOrEmpty(x))
            {
                MessageBox.Show("球员属性不能为空");
                return;
            }
            var y = EmulatorHelper.ConvertToDouble(x);
            if (y < 1)
                y = 1;
            if (y > 5000)
                y = 5000;
            txtSpeed.Text = y.ToString("f2");
            txtShooting.Text = y.ToString("f2");
            txtFreeKick.Text = y.ToString("f2");
            txtBalance.Text = y.ToString("f2");
            txtStamina.Text = y.ToString("f2");
            txtStrength.Text = y.ToString("f2");
            txtAggression.Text = y.ToString("f2");
            txtDisturb.Text = y.ToString("f2");
            txtInterception.Text = y.ToString("f2");
            txtDribble.Text = y.ToString("f2");
            txtPassing.Text = y.ToString("f2");
            txtMentality.Text = y.ToString("f2");
            txtReflexes.Text = y.ToString("f2");
            txtPositioning.Text = y.ToString("f2");
            txtHandling.Text = y.ToString("f2");
            txtAcceleration.Text = y.ToString("f2");
                                                                                      
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (_dicPlayer == null)
            {
                MessageBox.Show("请先选择一个球员");
                return;
            }
            txtSpeed.Text = _dicPlayer.Speed;
            txtShooting.Text = _dicPlayer.Shoot;
            txtFreeKick.Text = _dicPlayer.FreeKick;
            txtBalance.Text = _dicPlayer.Balance;
            txtStamina.Text = _dicPlayer.Physique;
            txtStrength.Text = _dicPlayer.Bounce;
            txtAggression.Text = _dicPlayer.Aggression;
            txtDisturb.Text = _dicPlayer.Disturb;
            txtInterception.Text = _dicPlayer.Interception;
            txtDribble.Text = _dicPlayer.Dribble;
            txtPassing.Text = _dicPlayer.Pass;
            txtMentality.Text = _dicPlayer.Mentality;
            txtReflexes.Text = _dicPlayer.Response;
            txtPositioning.Text = _dicPlayer.Positioning;
            txtHandling.Text = _dicPlayer.HandControl;
            txtAcceleration.Text = _dicPlayer.Acceleration;
        }

        private int subType;
        private int thirdType;
        private int _subType;
        private int _thirdType;
        private void BindItem()
        {
            subType = ComboBoxHelper.GetSelectValueInt(cmbItemSubType);
            thirdType = ComboBoxHelper.GetSelectValueInt(cmbItemThirdType);
            if (_subType == subType && _thirdType == thirdType)
                return;
            _subType = subType;
            _thirdType = thirdType;

            var list = _playerCache.FindAll(d => d.CardLevel == subType && (thirdType == 0 || d.LeagueLevel == thirdType));
            var cmblist = new List<KeyValueComboBoxItem>(list.Count);
            if (list.Count > 0)
            {
                cmblist.Add(new KeyValueComboBoxItem(0, "请选择"));

                foreach (var dicItem in list)
                {
                    cmblist.Add(new KeyValueComboBoxItem(dicItem.Idx, dicItem.Name));
                }
            }
            else
            {
                cmblist.Add(new KeyValueComboBoxItem(0, "无"));
            }
            ComboBoxHelper.BindComboBox(cmbItemCode, cmblist);
            cmbItemCode.SelectedIndex = 0;
        }

        private void btnCopyProperty_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCopySkill_Click(object sender, RoutedEventArgs e)
        {

        }

        public void BindStarSkill(ComboBox cmb)
        {
            var allskill = LocalHelper.LocalCache.StarSkills;
            var list = new List<KeyValueComboBoxItem>();
            list.Add(new KeyValueComboBoxItem("", "无"));
            foreach (var item in allskill)
            {
                list.Add(new KeyValueComboBoxItem(item.SkillCode, item.SkillName));
            }
            cmb.ItemsSource = list;
            cmb.SelectedIndex = 0;
        }
    }
}
