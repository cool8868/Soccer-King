using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Games.NB.Match.BLL.Facade;
using Games.NB.Match.BLL.Model;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.Base.Model.TranOut;
using Games.NB.Match.Base.Util;
using Games.NB.Match.Common.Random;
using Games.NB.Match.Emulator.WPF.Controls;
using Games.NB.Match.Emulator.WPF.Entity;
using Games.NB.Match.Emulator.WPF.Entity.Batch;
using Games.NB.Match.Emulator.WPF.Entity.Statistics;
using Games.NB.Match.Emulator.WPF.Mgrs;
using Games.NBall.Bll.Share;
using Games.NBall.Common;

namespace Games.NB.Match.Emulator.WPF
{
    /// <summary>
    /// Batchtest.xaml 的交互逻辑
    /// </summary>
    public partial class Batchtest : Window
    {
        public Batchtest()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            MatchFacade.Boot();
            miControl1.SetName(LocalHelper.EmulatorHomeId);
            miControl2.SetName(LocalHelper.EmulatorAwayId);
        }

        
        private void btnStartTest_Click(object sender, RoutedEventArgs e)
        {
            int count = ConvertHelper.ConvertToInt(txtTestCount.Text);
            int time = ConvertHelper.ConvertToInt(txtTime.Text);
            if (count < 1)
            {
                MessageBox.Show("场次不能小于1");
                return;
            }
            else if (count > 10000)
            {
                MessageBox.Show("场次不能大于10000");
                return;
            }
            if (time < 60 || time > 720 || time % 60 != 0)
            {
                MessageBox.Show("比赛时长取值范围 60-720，必须为60的倍数.");
                return;
            }

            _time = time;

            if (!miControl1.Check())
            {
                return;
            }
            if (!miControl2.Check())
            {
                return;
            }
            var transfer = LocalHelper.GetLocalMatchInput(miControl1.ManagerId,miControl1.IsNpc ,miControl2.ManagerId,miControl2.IsNpc, _time);
            if (transfer == null)
            {
                MessageBox.Show("获取经理信息失败，请检查!");
                return;
            }
            StartTest(count, transfer);
        }

        #region StartTest

        private bool _needStatistics = false;
        private int _matchTimes;
        private int _winTimes;
        private int _drawTimes;
        private string _folderName;
        private int _time;
        /// <summary>
        /// 比赛结束时处于投篮状态
        /// </summary>
        private int _shootEndCount;
        private int _selectMatchId = 0;

        private List<BatchMatchEntity> _testMatchList;

        private Dictionary<int, MatchReport> _testMatchEntityDic;
        private Dictionary<int, StatisticsMatchEntity> _testStatisticsDic;

        private BatchManagerEntity _homeStatistics;
        private BatchManagerEntity _awayStatistics;

        void StartTest(int matchTimes, MatchInput transfer)
        {
            if (matchTimes < 0)
                matchTimes = 1;
            if (matchTimes > 10000)
                matchTimes = 10000;

            if (chkStatistics.IsChecked.HasValue && chkStatistics.IsChecked == true)
            {
                if (matchTimes > 100)
                {
                    MessageBox.Show("【统计详细数据】会占用大量内存，当前运行场次大于100场，不能使用【统计详细数据】");
                    return;
                }
                _needStatistics = true;
            }
            else
            {
                _needStatistics = false;
            }

            _matchTimes = matchTimes;

            _selectMatchId = 0;
            _shootEndCount = 0;
            _testMatchList = new List<BatchMatchEntity>(_matchTimes);
            _testStatisticsDic = new Dictionary<int, StatisticsMatchEntity>(_matchTimes);
            _testMatchEntityDic = new Dictionary<int, MatchReport>();
            _folderName = DateTime.Now.ToString("yyyyMMdd");
            ProgressBar1.Maximum = _matchTimes;
            ProgressBar1.Value = 0;
            lblProcess.Content = string.Format("{0}/{1}", 0, _matchTimes);
            StartLoop(transfer);
        }

        private Thread _thread;
        public delegate void CreateDelegate(Guid matchId, int count, long cost, MatchReport matchResult);

        public delegate void FinishDelegate();


        public void StartLoop(MatchInput transfer)
        {
            _thread = new Thread(() => CreateMatch(transfer, _matchTimes, _needStatistics, CreateCallback, FinishCallback));
            _thread.IsBackground = true;
            _thread.Start();
        }

        public void CreateCallback(Guid matchId, int count, long cost, MatchReport matchResult)
        {
            var entity = new BatchMatchEntity(matchId, count, cost, matchResult);
            _testMatchList.Add(entity);
            EmulatorHelper.SaveMatchToFile(matchResult, matchId, _folderName);
            EmulatorHelper.LoadTestProcess(_folderName, matchId);
            //LogHelper.Insert("matchId:"+matchId,LogType.Info);
            ProgressBar1.Value = count;
            lblProcess.Content = string.Format("{0}/{1}", count, _matchTimes);

        }

        public void FinishCallback()
        {
            DataGridMatchList.ItemsSource = _testMatchList;

            #region CalAvg
            _homeStatistics = new BatchManagerEntity();
            _awayStatistics = new BatchManagerEntity();
            _winTimes = 0;
            _drawTimes = 0;
            foreach (var entity in _testMatchList)
            {
                _homeStatistics.AddData(entity.HomeManager);
                _awayStatistics.AddData(entity.AwayManager);
                if (entity.HomeScore > entity.AwayScore)
                    _winTimes++;
                else if (entity.HomeScore == entity.AwayScore)
                    _drawTimes++;
            }
            _homeStatistics.CalAvg(_matchTimes);
            _awayStatistics.CalAvg(_matchTimes);
            #endregion
            ShowStatisticsData(Guid.Empty, 0, _homeStatistics, _awayStatistics);
            btnViewAvg.IsEnabled = true;

            int totalTimes = _matchTimes - _drawTimes;
            double winRate = 0;
            if (totalTimes != 0)
                winRate = _winTimes * 100 / totalTimes;

            lblResult.Content = string.Format("场次:{0} 主队胜场:{3}  主队胜率:{1:f0}%  平局:{2} ", _matchTimes, winRate, _drawTimes, _winTimes);
        }

        public void StatisticsAddProcess()
        {
            if(statisticsMatch!=null)
                statisticsMatch.AddProcess();
        }
        StatisticsMatchEntity statisticsMatch = null;
        void CreateMatch(MatchInput transfer, int matchTimes, bool needStatistics, CreateDelegate createDelegate, FinishDelegate finishDelegate)
        {
            int i = 1;
            var hmid = transfer.HomeManager.Mid;
            var amid = transfer.AwayManager.Mid;
            while (i <= matchTimes)
            {
                if (transfer != null)
                {
                    var watch = new System.Diagnostics.Stopwatch();
                    watch.Start();
                    transfer.MatchId = ShareUtil.GenerateComb();

                    try
                    {
                        IMatch match = new MatchEntity(transfer);
                        
                        if (needStatistics)
                        {
                            statisticsMatch = new StatisticsMatchEntity(match);
                        }
                        EmulatorHelper.CreateMatch(match,statisticsMatch);
                        var result =match.Report;
                        long cost = watch.ElapsedMilliseconds;

                        _testStatisticsDic.Add(i, statisticsMatch);
                        _testMatchEntityDic.Add(i, result);
                        Dispatcher.Invoke((Action)delegate { createDelegate(transfer.MatchId, i, cost, result); });
                    }
                    catch (Exception ex)
                    {
                        LogHelper.Insert(ex);
                    }
                }
                i++;
            }
            Dispatcher.Invoke((Action)delegate { finishDelegate(); });
        }

        
        #endregion

        private void CalScore(IMatch match, out int homeScore, out int awayScore)
        {
            homeScore = 0;
            awayScore = 0;
            
        }

        private void DataGridMatchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = DataGridMatchList.SelectedIndex;
            if (index >= 0)
            {
                var match = DataGridMatchList.SelectedItem as BatchMatchEntity;
                ShowStatisticsData(match);
            }
        }


        void ShowStatisticsData(BatchMatchEntity match)
        {
            ShowStatisticsData(match.MatchId, match.Id, match.HomeManager, match.AwayManager);
        }

        void ShowStatisticsData(Guid matchId, int matchCount, BatchManagerEntity home, BatchManagerEntity away)
        {
            string title = "所有场次平均";
            if (matchCount > 0)
            {
                title = "场次: " + matchCount;
                if (_testStatisticsDic.ContainsKey(_selectMatchId))
                {
                    btnViewStatistics.IsEnabled = true;
                }
                else
                {
                    btnViewStatistics.IsEnabled = false;
                }
                btnViewEmulator.IsEnabled = true;
                btnViewAvg.IsEnabled = true;
                lblMatchId.Content = "比赛id: " + matchId;
            }
            else
            {
                btnViewStatistics.IsEnabled = false;
                btnViewEmulator.IsEnabled = false;
                btnViewAvg.IsEnabled = false;
                lblMatchId.Content = title;
            }
            _selectMatchId = matchCount;
            lblMatchCount.Content = title;
            BatchDataControl1.SetData(home);
            BatchDataControl2.SetData(away);
        }

        private void btnViewAvg_Click(object sender, RoutedEventArgs e)
        {
            if (_homeStatistics == null)
                return;
            else
            {
                ShowStatisticsData(Guid.Empty, 0, _homeStatistics, _awayStatistics);
            }
        }

        private void btnViewStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (_selectMatchId <= 0)
            {
                MessageBox.Show("请先选择对应的比赛!");
                return;
            }
            if (!_testStatisticsDic.ContainsKey(_selectMatchId))
            {
                MessageBox.Show("该比赛没有对应的统计数据!");
                return;
            }
            StatisticsWindow demo = new StatisticsWindow(_testStatisticsDic[_selectMatchId]);
            demo.Show();
        }

        private void btnViewEmulator_Click(object sender, RoutedEventArgs e)
        {
            if (_selectMatchId <= 0)
            {
                MessageBox.Show("请先选择对应的比赛!");
                return;
            }
            Main demo = new Main(_testMatchEntityDic[_selectMatchId], _testStatisticsDic[_selectMatchId]);
            demo.Show();

        }

        private void DataGridMatchList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = DataGridMatchList.SelectedIndex;
            if (index >= 0)
            {
                var match = DataGridMatchList.SelectedItem as BatchMatchEntity;
                Main demo = new Main(_testMatchEntityDic[match.Id], _testStatisticsDic[match.Id]);
                demo.Show();
            }
        }
    }
}
