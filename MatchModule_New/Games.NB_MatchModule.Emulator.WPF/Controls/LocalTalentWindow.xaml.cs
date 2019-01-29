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
    /// LocalTalentWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LocalTalentWindow : Window
    {
        private LocalTransferTalentEntity _talentEntity;
        private bool _isNew = false;
        public LocalTalentWindow()
        {
            InitializeComponent();
        }

        public LocalTalentWindow(LocalTransferTalentEntity talentEntity, bool isNew)
        {
            InitializeComponent();
            _talentEntity = talentEntity;
            _isNew = isNew;
        }

        #region Event
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            txtTalent.Text = _talentEntity.Name;
            List<LocalTalentEntity> list = new List<LocalTalentEntity>();
            var cacheLit = LocalHelper.LocalCache.Talents;
            foreach (var dicTalentEntity in cacheLit)
            {
                var entity = new LocalTalentEntity();
                entity.Id = dicTalentEntity.Id;
                entity.Name = dicTalentEntity.Name;
                entity.DriveType = dicTalentEntity.DriveType;
                entity.DriveTypeStr = GetDriveTypeStr(entity.DriveType);
                var talentData = _talentEntity.Talentdatas.Find(d => d.Id == entity.Id);
                if (talentData != null)
                {
                    entity.IsSelect = true;
                }
                list.Add(entity);
            }
            DataGridTalent.ItemsSource = list;
        }

        string GetDriveTypeStr(int drive)
        {
            if (drive == 1)
                return "主动";
            else if (drive == 2)
                return "混合";
            else
            {
                return "被动";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = txtTalent.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请取一个带感的名称!");
                return;
            }
            var check = LocalHelper.TalentList.Talents.Exists(d => d.Name == name && d.Id != _talentEntity.Id);
            if (check)
            {
                MessageBox.Show("该名称已经存在，请换一个.");
                return;
            }
            _talentEntity.Talentdatas.Clear();
            foreach (var item in DataGridTalent.ItemsSource)
            {
                var entity = item as LocalTalentEntity;
                
                if (entity.IsSelect)
                {
                    var data = new LocalTransferTalentDataEntity();
                    data.Id = entity.Id;
                    _talentEntity.Talentdatas.Add(data);
                }
            }
            _talentEntity.Name = name;
            if(_isNew)
                LocalHelper.TalentList.Talents.Add(_talentEntity);
            LocalHelper.SaveLocalTransferTalent();
            this.DialogResult = true;
            this.Close();
        }
        #endregion

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
