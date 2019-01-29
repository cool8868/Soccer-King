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
using Games.NB.Match.Emulator.WPF.Entity.Statistics;

namespace Games.NB.Match.Emulator.WPF.Controls
{
    /// <summary>
    /// StatisticsManagerControl.xaml 的交互逻辑
    /// </summary>
    public partial class StatisticsManagerControl : UserControl
    {
        public StatisticsManagerControl()
        {
            InitializeComponent();
        }

        private StatisticsManagerEntity _managerEntity;
        public void SetData(StatisticsManagerEntity managerEntity)
        {
            _managerEntity = managerEntity;
            lblName.Content = _managerEntity.Name;
            BuildInfo();
            
            SetRoundData(_managerEntity.MinRound);
        }

        public void SetRoundData(int round)
        {
            if (_managerEntity.Players.ContainsKey(round))
            {
                //PlayerDatagrid.ItemsSource = _managerEntity.Players[round];
                var list = _managerEntity.Players[round];
                spc1.SetRoundData(list[0]);
                spc2.SetRoundData(list[1]);
                spc3.SetRoundData(list[2]);
                spc4.SetRoundData(list[3]);
                spc5.SetRoundData(list[4]);
                spc6.SetRoundData(list[5]);
                spc7.SetRoundData(list[6]);
                spc8.SetRoundData(list[7]);
                spc9.SetRoundData(list[8]);
                spc10.SetRoundData(list[9]);
                spc11.SetRoundData(list[10]);
                
            }
            else
            {
                PlayerDatagrid.ItemsSource = null;
            }
        }

        void BuildInfo()
        {
            lblInfo.Content = string.Format("[射门:{0},射进:{1}]   [扑救:{2},成功:{3}]   [传球:{4},成功:{5}]   [乌龙:{6},进球:{7}]   ",
                _managerEntity.ShootTimes, _managerEntity.GoalTimes, _managerEntity.DiveTimes, _managerEntity.DiveSuccTimes, _managerEntity.PassTimes, _managerEntity.PassSuccTimes
                ,_managerEntity.RebelShootTimes,_managerEntity.RebelSuccTimes);
        }
    }
}
