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
    /// LocalSuitWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LocalSuitWindow : Window
    {
        private LocalTransferSuitEntity _suitEntity;
        private bool _isNew = false;
        public LocalSuitWindow()
        {
            InitializeComponent();
        }

        public LocalSuitWindow(LocalTransferSuitEntity suitEntity, bool isNew)
        {
            InitializeComponent();
            _suitEntity = suitEntity;
            _isNew = isNew;
        }

        #region Event
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            txtSuit.Text = _suitEntity.Name;
            List<LocalSuitEntity> list = new List<LocalSuitEntity>();
            var cacheLit = LocalHelper.LocalCache.Suits;
            foreach (var dicSuitEntity in cacheLit)
            {
                var entity = new LocalSuitEntity();
                entity.Id = dicSuitEntity.Id;
                entity.Name = dicSuitEntity.Name;
                entity.SuitType = dicSuitEntity.SuitType;
                entity.SuitTypeStr = GetDriveTypeStr(entity.SuitType);
                var suitData = _suitEntity.Suitdatas.Find(d => d.Id == entity.Id);
                if (suitData != null)
                {
                    entity.IsSelect = true;
                }
                list.Add(entity);
            }
            DataGridSuit.ItemsSource = list;
        }

        string GetDriveTypeStr(int drive)
        {
            if (drive == 1)
                return "7件套";
            else if (drive == 2)
                return "5件套";
            else
            {
                return "3件套";
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = txtSuit.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("请取一个带感的名称!");
                return;
            }
            var check = LocalHelper.SuitList.Suits.Exists(d => d.Name == name && d.Id != _suitEntity.Id);
            if (check)
            {
                MessageBox.Show("该名称已经存在，请换一个.");
                return;
            }
            _suitEntity.Suitdatas.Clear();
            foreach (var item in DataGridSuit.ItemsSource)
            {
                var entity = item as LocalSuitEntity;
                
                if (entity.IsSelect)
                {
                    var data = new LocalTransferSuitDataEntity();
                    data.Id = entity.Id;
                    _suitEntity.Suitdatas.Add(data);
                }
            }
            _suitEntity.Name = name;
            if(_isNew)
                LocalHelper.SuitList.Suits.Add(_suitEntity);
            LocalHelper.SaveLocalTransferSuit();
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
