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
    /// LocalDataSettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LocalDataSettingWindow : Window
    {
        public LocalDataSettingWindow()
        {
            InitializeComponent();
        }

        #region Event

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            LocalHelper.LoadLocalTransferManager();
            LocalHelper.LoadLocalTransferTalent();
            LocalHelper.LoadLocalTransferWill();
            DataGridManager.ItemsSource = LocalHelper.ManagerList.Managers;
            DataGridTalent.ItemsSource = LocalHelper.TalentList.Talents;
            DataGridWill.ItemsSource = LocalHelper.WillList.Wills;
        }

        private void btnGridSettingManager_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = DataGridManager.SelectedItem as LocalTransferManagerEntity;
            ShowManagerWindow(entity, false);
        }

        private void btnGridSettingTalent_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = DataGridTalent.SelectedItem as LocalTransferTalentEntity;
            ShowTalentWindow(entity, false);
        }

        private void btnNewTalent_Click(object sender, RoutedEventArgs e)
        {
            int id = LocalHelper.TalentNewId;
            var entity = CreateLocalTalent(id);
            ShowTalentWindow(entity, true);
        }

        private void btnGridSettingWill_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = DataGridWill.SelectedItem as LocalTransferWillEntity;
            ShowWillWindow(entity, false);
        }

        private void btnNewWill_Click(object sender, RoutedEventArgs e)
        {
            int id = LocalHelper.WillNewId;
            var entity = CreateLocalWill(id);
            ShowWillWindow(entity, true);
        }

        private void btnGridSettingSuit_OnClick(object sender, RoutedEventArgs e)
        {
            var entity = DataGridSuit.SelectedItem as LocalTransferSuitEntity;
            ShowSuitWindow(entity, false);
        }

        private void btnNewSuit_Click(object sender, RoutedEventArgs e)
        {
            int id = LocalHelper.SuitNewId;
            var entity = CreateLocalSuit(id);
            ShowSuitWindow(entity, true);
        }

        private void btnNewManager_Click(object sender, RoutedEventArgs e)
        {
            int id = LocalHelper.ManagerNewId;
            var entity = CreateLocalManager(id);
            ShowManagerWindow(entity, true);
        }

        #endregion


        #region encapsulation

        void ShowManagerWindow(LocalTransferManagerEntity entity, bool isNew)
        {
            LocalManagerSettingWindow window = new LocalManagerSettingWindow(entity, isNew);
            var result = window.ShowDialog();
            if (result.HasValue && result.Value)
            {
                DataGridManager.ItemsSource = null;
                DataGridManager.ItemsSource = LocalHelper.ManagerList.Managers;
            }
        }

        LocalTransferManagerEntity CreateLocalManager(int id)
        {
            var manager = new LocalTransferManagerEntity();
            manager.Id = id;
            manager.Name = "球队" + id;

            manager.FormationId = 1;
            manager.FormationLevel = 1;
            manager.TalentId = 0;
            manager.Players = new List<LocalTransferPlayerEntity>(11);
            var ss = "30437,30704,32356,32124,41299,30542,31976,32006,40813,20169,30273".Split(',');
            int i = 0;
            foreach (var s in ss)
            {
                manager.Players.Add(CreateLocalPlayer(Convert.ToInt32(s),i++));
            }
            LocalHelper.BuildPlayerPosition(manager);
            LocalManagerHelper.SaveBefore(manager);
            return manager;
        }

        LocalTransferPlayerEntity CreateLocalPlayer(int playerId,int index)
        {
            var dic = LocalHelper.LocalCache.Players.Find(d => d.Idx == playerId);
            var entity = new LocalTransferPlayerEntity();
            entity.Index = index;
            entity.Name = dic.Name;
            entity.PlayerId = dic.Idx;
            entity.Skill = "";
            entity.Skill2 = "";
            entity.StarSkill = "";

            entity.Speed = Convert.ToInt32(dic.Speed);
            entity.Shooting = Convert.ToInt32(dic.Shoot);
            entity.FreeKick = Convert.ToInt32(dic.FreeKick);
            entity.Balance = Convert.ToInt32(dic.Balance);
            entity.Stamina = Convert.ToInt32(dic.Physique);
            entity.Strength = Convert.ToInt32(dic.Bounce);
            entity.Aggression = Convert.ToInt32(dic.Aggression);
            entity.Disturb = Convert.ToInt32(dic.Disturb);
            entity.Interception = Convert.ToInt32(dic.Interception);
            entity.Dribble = Convert.ToInt32(dic.Dribble);
            entity.Passing = Convert.ToInt32(dic.Pass);
            entity.Mentality = Convert.ToInt32(dic.Mentality);
            entity.Reflexes = Convert.ToInt32(dic.Response);
            entity.Positioning = Convert.ToInt32(dic.Positioning);
            entity.Handling = Convert.ToInt32(dic.HandControl);
            entity.Acceleration = Convert.ToInt32(dic.Acceleration);

            return entity;
        }

        void ShowTalentWindow(LocalTransferTalentEntity entity, bool isNew)
        {
            LocalTalentWindow window = new LocalTalentWindow(entity, isNew);
            var result = window.ShowDialog();
            if (result.HasValue && result.Value)
            {
                DataGridTalent.ItemsSource = null;
                DataGridTalent.ItemsSource = LocalHelper.TalentList.Talents;
            }
        }

        LocalTransferTalentEntity CreateLocalTalent(int id)
        {
            var talent = new LocalTransferTalentEntity();
            talent.Id = id;
            talent.Name = "天赋" + id;
            talent.Talentdatas = new List<LocalTransferTalentDataEntity>();

            return talent;
        }


        void ShowWillWindow(LocalTransferWillEntity entity, bool isNew)
        {
            LocalWillWindow window = new LocalWillWindow(entity, isNew);
            var result = window.ShowDialog();
            if (result.HasValue && result.Value)
            {
                DataGridWill.ItemsSource = null;
                DataGridWill.ItemsSource = LocalHelper.WillList.Wills;
            }
        }

        LocalTransferWillEntity CreateLocalWill(int id)
        {
            var talent = new LocalTransferWillEntity();
            talent.Id = id;
            talent.Name = "意志" + id;
            talent.Willdatas = new List<LocalTransferWillDataEntity>();
            return talent;
        }

        void ShowSuitWindow(LocalTransferSuitEntity entity, bool isNew)
        {
            LocalSuitWindow window = new LocalSuitWindow(entity, isNew);
            var result = window.ShowDialog();
            if (result.HasValue && result.Value)
            {
                DataGridSuit.ItemsSource = null;
                DataGridSuit.ItemsSource = LocalHelper.SuitList.Suits;
            }
        }

        LocalTransferSuitEntity CreateLocalSuit(int id)
        {
            var talent = new LocalTransferSuitEntity();
            talent.Id = id;
            talent.Name = "套装" + id;
            talent.Suitdatas = new List<LocalTransferSuitDataEntity>();
            return talent;
        }
        #endregion
    }
}
