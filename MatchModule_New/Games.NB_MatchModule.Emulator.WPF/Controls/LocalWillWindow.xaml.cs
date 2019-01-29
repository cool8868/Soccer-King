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
    /// LocalWillWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LocalWillWindow : Window
    {
        private LocalTransferWillEntity _willEntity;
        private bool _isNew = false;
        public LocalWillWindow()
        {
            InitializeComponent();
        }

        public LocalWillWindow(LocalTransferWillEntity willEntity, bool isNew)
        {
            InitializeComponent();
            _willEntity = willEntity;
            _isNew = isNew;
        }
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            txtWill.Text = _willEntity.Name;
            List<LocalWillEntity> list = new List<LocalWillEntity>();
            var cacheLit = LocalHelper.LocalCache.Wills;
            foreach (var dicWillEntity in cacheLit)
            {
                var entity = new LocalWillEntity();
                entity.Id = dicWillEntity.Id;
                entity.Name = dicWillEntity.Name;
                entity.WillRank = dicWillEntity.WillRank;
                entity.WillRankStr = dicWillEntity.WillRank == 1 ? "组合" : "意志";
                var data = _willEntity.Willdatas.Find(d => d.Id == entity.Id);
                if (data != null)
                {
                    entity.IsSelect = true;
                }
                list.Add(entity);
            }
            DataGridWill.ItemsSource = list;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = txtWill.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请取一个带感的名称!");
                return;
            }
            var check = LocalHelper.WillList.Wills.Exists(d => d.Name == name && d.Id != _willEntity.Id);
            if (check)
            {
                MessageBox.Show("该名称已经存在，请换一个.");
                return;
            }
            _willEntity.Willdatas.Clear();
            foreach (var item in DataGridWill.ItemsSource)
            {
                var entity = item as LocalWillEntity;
                
                if (entity.IsSelect)
                {
                    var data = new LocalTransferWillDataEntity();
                    data.Id = entity.Id;
                    _willEntity.Willdatas.Add(data);
                }
            }
            _willEntity.Name = name;
            if(_isNew)
                LocalHelper.WillList.Wills.Add(_willEntity);
            LocalHelper.SaveLocalTransferWill();
            this.DialogResult = true;
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
