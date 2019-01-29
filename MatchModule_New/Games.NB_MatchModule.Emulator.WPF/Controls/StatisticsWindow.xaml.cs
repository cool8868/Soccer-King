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
using Games.NB.Match.Emulator.WPF.Entity.Statistics;
using Games.NB.Match.Emulator.WPF.Mgrs;
using Games.NBall.Common;

namespace Games.NB.Match.Emulator.WPF.Controls
{
    /// <summary>
    /// StatisticsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow()
        {
            InitializeComponent();
        }

        private StatisticsMatchEntity _statisticsMatch;
        private int _round = 0;
        public StatisticsWindow(StatisticsMatchEntity statisticsMatch)
        {
            InitializeComponent();
            Init(statisticsMatch);
        }

        private string _scoreContent = "";
        public void Init(StatisticsMatchEntity statisticsMatch)
        {
            _statisticsMatch = statisticsMatch;
            _scoreContent = string.Format("最终比分({0}:{1}) 总回合数:{2}  ", statisticsMatch.HomeScore, statisticsMatch.AwayScore, statisticsMatch.TotalRound);
            lblScore.Content = _scoreContent + "当前播放回合 0 ";
            ManagerControl1.SetData(_statisticsMatch.HomeManager);
            ManagerControl2.SetData(_statisticsMatch.AwayManager);
            _emulatorRound = _statisticsMatch.HomeManager.MinRound;
            DataGridMatch.ItemsSource = statisticsMatch.StatisticsBallResults;
            txtRound.Text = "";
        }

        

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int round = ConvertHelper.ConvertToInt(txtRound.Text);
            if (round > 0)
            {
                SetStatistics(round);
            }
        }

        private void btnSyncRound_Click(object sender, RoutedEventArgs e)
        {
            int round = _emulatorRound;
            if (round > 0)
            {
                SetStatistics(round);
            }
        }

        private int _emulatorRound = 0;
        public void SetEmulatorRound(int round)
        {
            _emulatorRound = round;
            bool sync = chkSync.IsChecked.HasValue && chkSync.IsChecked.Value;
            if(sync)
                SetStatistics(round);
        }

        void SetStatistics(int round)
        {
            _round = round;
            if (round <= 1)
                btnPrev.IsEnabled = false;
            else
            {
                btnPrev.IsEnabled = true;
            }
            if (round >= _statisticsMatch.TotalRound)
                btnNext.IsEnabled = false;
            else
            {
                btnNext.IsEnabled = true;
            }
            txtRound.Text = round.ToString();
            lblScore.Content = _scoreContent + "当前播放回合 " + round;
            ManagerControl1.SetRoundData(round);
            ManagerControl2.SetRoundData(round);
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            SetStatistics(_round - 1);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            SetStatistics(_round + 1);
        }
    }
}
