/*****************************************************************************
 * 文件名：Main.xaml
 * 
 * 创建人：
 * 
 * 创建时间：2009-11-12 16:22:52
 * 
 * 功能说明：Represents the main window.
 *  
 * 历史修改记录：
 * 
 * ***************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Games.NB.Match.BLL.Model;
using Games.NB.Match.Emulator.WPF.Controls;
using Games.NB.Match.Emulator.WPF.Entity;
using Games.NB.Match.Emulator.WPF.Entity.Statistics;
using Games.NBall.Common;
using Microsoft.Win32;
using Games.NB.Match.AI;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.Base.Model.TranOut;
using Games.NB.Match.Base.Util;
using Games.NB.Match.Emulator.WPF.Mgrs;
using Games.NB.Match.BLL.Facade;
using Games.NBall.Entity.Enums;
using RandomHelper = Games.NB.Match.Common.Random.RandomHelper;

namespace Games.NB.Match.Emulator.WPF
{
    /// <summary>
    /// Represents the main window.
    /// </summary>
    public partial class Main : Window
    {
        private const int RECTANGLE_SIZE = 4;   // represents the rectange's size.
        private const int MANAGER_COUNT = 2;    // represents the manager's count.
        private readonly Guid HOME_MANAGER = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        private readonly Guid AWAY_MANAGER = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1);

        private int _round;
        private int _maxRound;
        private LogPanel _logDialog;
        private bool _openStatistics;

        private int _timerInterval = 150;
        private DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(150) };
        private DispatcherTimer _loadingTimer;
        private DispatcherTimer _waitTimer;

        private readonly System.Collections.Generic.Dictionary<string, Player> _playerCache = new System.Collections.Generic.Dictionary<string, Player>(Defines.Match.MAX_PLAYER_COUNT * 2);
        private readonly Ellipse _ball = new Ellipse();

        private Canvas _pitch;

        private int _isEndStart = 0;
        private MatchReport _match;

        private OpenFileDialog _dialog = new OpenFileDialog();
        readonly OpenFileDialog _diagOpenFile = new OpenFileDialog();
        readonly SaveFileDialog _diagSaveFile = new SaveFileDialog();

        private StatisticsMatchEntity _statisticsMatch;

        public Main(MatchReport match,StatisticsMatchEntity statistics)
            : this()
        {
            _match = match;
            _statisticsMatch = statistics;
            if (_match != null)
            {
                _timer.Stop();
                _round = 0;
                _maxRound = GetMaxRound();
                ResetPlayer();
                //ResetBall();
                btnNewGame.IsEnabled = false;
                PlayGame(this, new EventArgs());
            }

        }

        /// <summary>
        /// Initialize the main window.
        /// 初始化整个窗体
        /// </summary>
        public Main()
        {
            InitializeComponent();

            RandomHelper.Initialize();
            _timer.Tick += PlayGame;
            _dialog.Filter = "比赛战报文件(*.bin)|*.bin";
            _dialog.FileOk += OpenFileDialog_Ok;
            _dialog.Multiselect = false;

            string path=AppDomain.CurrentDomain.BaseDirectory + "Report";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            this._diagOpenFile.Multiselect = false;
            this._diagOpenFile.InitialDirectory = path;
            this._diagOpenFile.Filter = "比赛战报文件(*.bin)|*.bin";
            this._diagOpenFile.FileOk += _diagOpenFile_FileOk;
            this._diagSaveFile.InitialDirectory = path;
            this._diagSaveFile.Filter = "比赛战报文件(*.bin)|*.bin";
            this._diagSaveFile.FileOk += _diagSaveFile_FileOk;
            miControl1.SetName(LocalHelper.EmulatorHomeId);
            miControl2.SetName(LocalHelper.EmulatorAwayId);

            if (_loadingTimer == null)
            {
                _loadingTimer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(100) };
                _loadingTimer.Tick += LoadingTimer_Tick;
            }
            if (_waitTimer == null)
            {
                Interlocked.Exchange(ref _waitTimer, new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(100) });
                _waitTimer.Tick -= _waitTimer_Tick;
                _waitTimer.Tick += _waitTimer_Tick;
            }
            else
            {
                _waitTimer.Stop();
            }
        }


        void _diagOpenFile_FileOk(object sender, CancelEventArgs e)
        {
            string path = _diagOpenFile.FileName;
            if (String.IsNullOrEmpty(path))
            {
                MessageBox.Show("请先选择文件");
                return;
            }
            try
            {
                _match = IOUtil.BinRead<MatchReport>(path, 0);
                DataMgr.FillFormData(_match);
                DataMgr.FillRoundData(_match);
            }
            catch
            {
                MessageBox.Show("打开文件失败！");
                return;
            }
            _diagOpenFile.FileName = string.Empty;
            if (_match != null)
            {
                _timer.Stop();
                _round = 0;
                DataMgr.FillFormData(_match);
                DataMgr.FillRoundData(_match);
                this._maxRound = GetMaxRound();
                Interlocked.Exchange(ref this._isEndStart, 1);
                _waitTimer.Start();
                ResetPlayer();
                PlayGame(this, new EventArgs());
            }
        }
        void _diagSaveFile_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                if (_match == null)
                {
                    MessageBox.Show("请先生成比赛");
                    return;
                }
                _match.ZipNo = this.chk_Zip.IsChecked.Value ? ReportAsset.RPTZipNo4Deflate : 0;
                string binPath = _diagSaveFile.FileName;
                string xmlPath = binPath.Replace(".bin", ".xml");
                IOUtil.BinWrite(binPath, _match, 0);
                IOUtil.XmlWrite(xmlPath, _match);
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存文件失败:" + ex.Message + "\n" + ex.StackTrace);
            }
            _diagSaveFile.FileName = string.Empty;
        }
        #region Event Handler
        
        /// <summary>
        /// Invokes while the main window load.
        /// 窗体加载成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MatchFacade.Boot();

            Initialize();

            _logDialog = new LogPanel();
            // _logDialog.Show();
        }

        /// <summary>
        /// Invokes while the main window closed.
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if (_logDialog != null)
            {
                _logDialog.Close();
                _logDialog = null;
            }
        }

        /// <summary>
        /// Invokes while the user click the (新建) button.
        /// 点击新建按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_NewGame_Click(object sender, RoutedEventArgs e)
        {
            New();
        }

        /// <summary>
        /// Invokes while the user click the (开始) button.
        /// 点击开始按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            _timerInterval = 150;
            _timer.Interval = TimeSpan.FromMilliseconds(_timerInterval);
            if (_match != null && _timer.IsEnabled == false)
            {
                _timer.Start();
            }
            else
            {
                Play();
            }
        }

        /// <summary>
        /// Invokes while the user click the (显示网格) checkbox.
        /// 点击显示网格按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chk_ShowCourt_Checked(object sender, RoutedEventArgs e)
        {
            //this._pitch.Visibility = (this.chk_ShowCourt.IsChecked.Value) ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Invokes while the user click the (新建单步) button.
        /// 点击新建单步按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewStep_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }

        /// <summary>
        /// Invokes while the user click the (开始单步) button.
        /// 点击开始单步按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartStep_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            PlayGame(this, new EventArgs());
        }

        /// <summary>
        /// Invokes while the (进度条) tick event triggered.     
        /// 进度条变化中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadingTimer_Tick(object sender, EventArgs e)
        {
            //var value = this.loadingBar.Value;

            //if (value == 100)
            //{
            //    value = 0;
            //}
            //else
            //{
            //    value += 10;
            //}

            //if (value > 100)
            //{
            //    value = 100;
            //}

            //this.loadingBar.Value = value;
        }

        /// <summary>
        /// Invokes while the user click the (保存) button.
        /// 点击保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this._diagSaveFile.ShowDialog();
            //try
            //{
            //    if (_match == null)
            //    {
            //        MessageBox.Show("请先生成比赛");
            //        return;
            //    }

            //    const string path = @"d:\test\data.bin";
            //    if (File.Exists(path))
            //    {
            //        File.Delete(path);
            //    }
            //    IOUtil.BinWrite(path, _match, 0);
            //    MessageBox.Show("OK!");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("保存失败:" + ex.Message);
            //}
        }

        /// <summary>
        /// Invokes while the user click the (读取) button.
        /// 点击读取按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            //_dialog.ShowDialog();
            _diagOpenFile.ShowDialog();
        }

        /// <summary>
        /// Invokes while the 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFileDialog_Ok(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(_dialog.FileName))
            {
                MessageBox.Show("请先选择文件");
            }

            try
            {
                _match = IOUtil.BinRead<MatchReport>(_dialog.FileName, 0);
                DataMgr.FillFormData(_match);
            }
            catch
            {
                MessageBox.Show("打开文件失败！");
                return;
            }

            if (_match != null)
            {
                _timer.Stop();
                _round = 0;
                _maxRound = GetMaxRound();
                ResetPlayer();
                PlayGame(this, new EventArgs());
            }
        }

        /// <summary>
        /// 跳转回合回合变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJump_Click(object sender, RoutedEventArgs e)
        {
            _timer.Stop();

            if (_match == null)
            {
                MessageBox.Show("请先创建或打开比赛");
                return;
            }

            if (int.TryParse(txtRound.Text, out _round) == false)
            {
                MessageBox.Show("请输入数字");
                return;
            }

            if (_round > _maxRound)
            {
                MessageBox.Show("超出了最大的回合数");
                _round = 0;
                return;
            }

            this.PlayGame(this, new EventArgs());
        }

        private void chk_ShowState_Checked(object sender, RoutedEventArgs e)
        {
            ShowPlayerTip();
        }

        private void ShowPlayerTip()
        {
            bool showState = this.chk_ShowState.IsChecked.Value;
            bool showPosition = this.chk_ShowPosition.IsChecked.Value;
            bool showName = this.chk_ShowName.IsChecked.Value;
            foreach (var player in _playerCache.Values)
            {
                player.SetTipStatus(showState, showPosition, false, showName);
            }
        }

        private void btnLowSpeed_Click(object sender, RoutedEventArgs e)
        {
            var a = _timerInterval + 50;
            if (a > 1000)
                return;
            _timerInterval = a;
            _timer.Stop();
            _timer.Interval = TimeSpan.FromMilliseconds(_timerInterval);
            _timer.Start();
        }

        private void btnAddSpeed_Click(object sender, RoutedEventArgs e)
        {
            var a = _timerInterval - 50;
            if (a < 50)
                return;
            _timerInterval = a;
            _timer.Stop();
            _timer.Interval = TimeSpan.FromMilliseconds(_timerInterval);
            _timer.Start();
        }
        #endregion

        #region 初始化

        /// <summary>
        /// Initialization.
        /// </summary>
        private void Initialize()
        {
            //InitializePitch();
            InitializePlayers();
            InitializeFootball();


            this.btnNewGame.IsEnabled = true;
            this.btnNewStep.IsEnabled = true;

            this.txtTime.Text = "120";
            this.txtTime.IsEnabled = true;

            cmbMatchType.Items.Clear();
            cmbMatchType.Items.Add(new KeyValueComboBoxItem((int)EnumMatchType.Tour, "巡回赛"));
            cmbMatchType.Items.Add(new KeyValueComboBoxItem((int)EnumMatchType.Ladder, "天梯赛"));
            cmbMatchType.Items.Add(new KeyValueComboBoxItem((int)EnumMatchType.TourElite, "精英巡回赛"));
            cmbMatchType.Items.Add(new KeyValueComboBoxItem((int)EnumMatchType.Dailycup, "杯赛"));
            cmbMatchType.Items.Add(new KeyValueComboBoxItem((int)EnumMatchType.League, "联赛"));
            cmbMatchType.Items.Add(new KeyValueComboBoxItem((int)EnumMatchType.WorldChallenge, "世界挑战赛"));
            cmbMatchType.Items.Add(new KeyValueComboBoxItem((int)EnumMatchType.Friend, "友谊赛"));
            cmbMatchType.Items.Add(new KeyValueComboBoxItem((int)EnumMatchType.PlayerKill, "PK赛"));
            cmbMatchType.Items.Add(new KeyValueComboBoxItem((int)EnumMatchType.Arena, "竞技场"));
            cmbMatchType.Items.Add(new KeyValueComboBoxItem((int)EnumMatchType.GuildWar, "公会战"));
            cmbMatchType.Items.Add(new KeyValueComboBoxItem((int)EnumMatchType.Revelation, "球星启示录"));
            cmbMatchType.Items.Add(new KeyValueComboBoxItem((int)EnumMatchType.Crowd, "群雄逐鹿"));
            cmbMatchType.Items.Add(new KeyValueComboBoxItem((int)EnumMatchType.Giants, "豪门试炼"));
            cmbMatchType.Items.Add(new KeyValueComboBoxItem((int)EnumMatchType.CrossCrowd, "跨服群雄逐鹿"));
            cmbMatchType.Items.Add(new KeyValueComboBoxItem((int)EnumMatchType.CrossLadder, "跨服天梯"));
            cmbMatchType.SelectedIndex = 0;
        }

        /// <summary>
        /// Initialize the pitch.
        /// </summary>
        private void InitializePitch()
        {
            _pitch = new Canvas();
            _pitch.SetValue(Panel.BackgroundProperty, new SolidColorBrush(new Color() { A = 5, R = 255, G = 255, B = 255 }));
            pitchCanvas.Children.Add(_pitch);

            for (var x = 0; x < Defines.Pitch.MAX_WIDTH; x++)
            {
                for (var y = 0; y < Defines.Pitch.MAX_HEIGHT; y++)
                {
                    var rectangle = new Rectangle { Width = RECTANGLE_SIZE, Height = RECTANGLE_SIZE };
                    _pitch.Children.Add(rectangle);

                    rectangle.SetValue(Shape.StrokeProperty, Brushes.Black);
                    rectangle.SetValue(Shape.StrokeThicknessProperty, 1.00);
                    rectangle.SetValue(Canvas.LeftProperty, Convert.ToDouble(x * RECTANGLE_SIZE));
                    rectangle.SetValue(Canvas.TopProperty, Convert.ToDouble(y * RECTANGLE_SIZE));
                    rectangle.ToolTip = String.Format("{0},{1}", x, y);
                }
            }

            _pitch.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Initialize the players.
        /// </summary>
        private void InitializePlayers()
        {
            for (var m = 0; m < MANAGER_COUNT; m++)
            {
                for (var p = 0; p < Defines.Match.MAX_PLAYER_COUNT; p++)
                {
                    var player = new Player();
                    this.pitchCanvas.Children.Add(player);

                    player.Name = String.Format("player_{0}_{1}", m, p);
                    player.PlayerColor =  (m == 0) ? Brushes.Red : Brushes.Blue;
                    player.Width = Defines.Player.PLAYER_WIDTH * RECTANGLE_SIZE * 2;
                    player.Height = Defines.Player.PLAYER_WIDTH * RECTANGLE_SIZE * 2;
                    player.SetValue(Canvas.LeftProperty, 10d);
                    player.SetValue(Canvas.TopProperty, 10d);
                    player.SetValue(Panel.ZIndexProperty, 50);
                    player.ToolTip = String.Format("{0},{1}", player.GetValue(Canvas.LeftProperty), player.GetValue(Canvas.TopProperty));

                    _playerCache.Add(player.Name, player);
                }
            }
            _isInitPlayer = true;
        }

        /// <summary>
        /// Initialize the football.
        /// </summary>
        private void InitializeFootball()
        {
            _ball.Name = "Football";
            _ball.SetValue(Shape.FillProperty, Brushes.Black);
            _ball.Width = RECTANGLE_SIZE;
            _ball.Height = RECTANGLE_SIZE;
            _ball.SetValue(Canvas.LeftProperty, 10d);
            _ball.SetValue(Canvas.TopProperty, 10d);
            _ball.SetValue(Panel.ZIndexProperty, 50);
            _ball.ToolTip = String.Format("{0},{1}", _ball.GetValue(Canvas.LeftProperty), _ball.GetValue(Canvas.TopProperty));

            this.pitchCanvas.Children.Add(_ball);
        }

        #endregion

        #region 正常播放

        /// <summary>
        /// New a full time match.
        /// 新建一个比赛
        /// </summary>
        private void New()
        {
            if (!string.IsNullOrEmpty(this.miControl1.txtHomeId.Text.Trim())
                && !string.IsNullOrEmpty(this.miControl2.txtHomeId.Text.Trim()))
            {
                if (!CheckInput())
                    return;
            }
            //this.btnNewGame.IsEnabled = false;
            //this.btnNewStep.IsEnabled = false;
            //this.txtTime.IsEnabled = false;
            this._timer.Stop();
            this._loadingTimer.Stop();
            this._round = 0;
            _openStatistics = this.chk_Statistics.IsChecked.HasValue && this.chk_Statistics.IsChecked.Value;
            var t = new Thread(StartNewMatch) { IsBackground = true }; // create the match use the background thread
            t.Start(this.txtTime.Text);
          
        }

        void _waitTimer_Tick(object sender, EventArgs e)
        {
            if (this._isEndStart == 1)
            {
                ResetPlayer();
                ResetBall();
                BuildSkillInfo();
                this._loadingTimer.Stop();
                //this.loadingBar.Value = 100;
                this.btnNewGame.IsEnabled = true;
                this.btnNewStep.IsEnabled = true;
                this.btnStart.IsEnabled = true;
                this.txtTime.IsEnabled = true;
                if (_statisticsWindow != null && _statisticsMatch!=null)
                {
                    _statisticsWindow.Init(_statisticsMatch);
                }
                var timer = sender as DispatcherTimer;
                if (null != timer)
                    timer.Stop();
                if (timer != _waitTimer)
                    timer.Stop();
            }
        }

        private Guid hmid = Guid.Empty;
        private Guid amid = Guid.Empty;
        private bool homeIsNpc;
        private bool awayIsNpc;
        private int _time = 0;
        private int _matchType = 0;
        bool CheckInput()
        {
            int time = ConvertHelper.ConvertToInt(txtTime.Text);
            if (time < 60 || time > 720 || time % 60 != 0)
            {
                MessageBox.Show("比赛时长取值范围 60-720，必须为60的倍数.");
                return false;
            }

            _time = time;
            if (!miControl1.Check())
            {
                return false;
            }
            if (!miControl2.Check())
            {
                return false;
            }
            hmid = miControl1.ManagerId;
            amid = miControl2.ManagerId;
            homeIsNpc = miControl1.IsNpc;
            awayIsNpc = miControl2.IsNpc;
            return true;
        }

        public void StatisticsAddProcess()
        {
            if (_statisticsMatch != null)
            {
                _statisticsMatch.AddProcess();
            }
        }

        /// <summary>
        /// Start a new match.
        /// (This method is working on another thread.)        
        /// </summary>
        private void StartNewMatch(object o)
        {
            Interlocked.Exchange(ref this._isEndStart, 0);
            MatchInput matchIn=null;
            if (hmid == Guid.Empty && amid == Guid.Empty)
            {
                double time;
                if (!double.TryParse(o.ToString(), out time))
                    time = 120;
                matchIn=DataMgr.CreateDebugMatch((int)time);
               
            }
            else
            {
                matchIn = LocalHelper.GetLocalMatchInput(hmid, homeIsNpc, amid, awayIsNpc, _time);
            }
            if (matchIn == null)
            {
                MessageBox.Show("创建比赛失败,创建MatchInput失败，请检查输入账号.");
                return;
            }
            IMatch match = new MatchEntity(matchIn, StatisticsAddProcess);
            if (_openStatistics)
                _statisticsMatch = new StatisticsMatchEntity(match);
            using (var watch = new Games.NB.Match.Log.LogWatch())
            {
                EmulatorHelper.CreateMatch(match, _statisticsMatch);
                watch.LogCostTime(string.Format("Guid:{0}[{1}] vs Guid:{2}[{3}]. Result {4}:{5})",
                    match.Input.HomeManager.Mid, match.Input.HomeManager.Name,
                    match.Input.AwayManager.Mid, match.Input.AwayManager.Name,
                    match.HomeScore, match.AwayScore));
                var bytes=IOUtil.BinWrite(match.Report, ReportAsset.RPTVerNo);
                watch.LogCostTime("BinWrite");
                _match = IOUtil.BinRead<MatchReport>(bytes, 0);
                watch.LogCostTime("BinRead");
            }
            if (_match == null)
            {
                MessageBox.Show("创建比赛失败");
                return;
            }
            else
            {
                MessageBox.Show("创建比赛成功");
            }
            DataMgr.FillFormData(_match);
            DataMgr.FillRoundData(_match);
            try
            {
                //_match = MatchFacade.CreateMatch(DataMgr.CreateDebugMatch((int)time * 60, 335, 300)).Report;
                this._maxRound = GetMaxRound();
                Interlocked.Exchange(ref this._isEndStart, 1);
                _loadingTimer.Start();
                _waitTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+":\n"+ex.StackTrace,"系统错误");
            }
        }

        /// <summary>
        /// Play the match.
        /// 点击开始比赛
        /// </summary>
        private void Play()
        {
            _timer.Stop();
            _round = 0;
            this.lblSoccer.Content = "0:0";
            _timer.Start();
        }

        /// <summary>
        /// Reset the players.
        /// 重置球员的位置
        /// </summary>
        private void ResetPlayer()
        {
            if (_match == null)
            {
                MessageBox.Show("请先创建比赛");
                return;
            }

            int i = 0;
            foreach (var p in _playerCache.Values)
            {
                if (i++ < Defines.Match.MAX_PLAYER_COUNT)
                {
                    p.Reset(Colors.Red);
                }
                else
                {
                    p.Reset(Colors.Blue);
                }
            }

            bool visiblePosition = _match.HomeManager.Players[_match.HomeManager.Players.Count - 1].Position != (int)Base.Enum.Position.Goalkeeper;
            ManagerReport[] managers = new[] { _match.HomeManager, _match.AwayManager };

            for (var m = 0; m < managers.Length; m++)
            {
                for (var p = 0; p < managers[m].Players.Count; p++)
                {
                    if (managers[m].Players[p].CntMoveResults > 0)
                    {
                        var coordinate = managers[m].Players[p].MoveResults[0].StateData.Current;
                        double x = GetPoint(coordinate.X);
                        double y = GetPoint(coordinate.Y);

                        var player = _playerCache[String.Format("player_{0}_{1}", m, p)];
                        player.SetValue(Canvas.LeftProperty, x);
                        player.SetValue(Canvas.TopProperty, y);
                        player.ToolTip = String.Format("{0},{1}", x, y);
                        var pEntity = managers[m].Players[p];
                        player.DisplayName((Base.Enum.Position)pEntity.Position, (byte)pEntity.ClientId, pEntity.Name, visiblePosition);
                    }
                }
            }
        }

        /// <summary>
        /// Reset the football.
        /// 重置足球的位置
        /// </summary>
        private void ResetBall()
        {
            if (_match == null)
            {
                MessageBox.Show("请先创建比赛");
                return;
            }

            if (_match.CntBallResults > 0)
            {
                var coordinate = _match.BallResults[0].StateData.Current;
                double x = GetPoint(coordinate.X);
                double y = GetPoint(coordinate.Y);

                _ball.SetValue(Canvas.LeftProperty, x);
                _ball.SetValue(Canvas.TopProperty, y);
                _ball.ToolTip = String.Format("{0},{1}", x, y);
            }
        }
        private bool _isInitPlayer = false;
        /// <summary>
        /// Player the game.
        /// 点击播放按钮
        /// </summary>        
        private void PlayGame(object sender, EventArgs e)
        {
            if (_match == null)
            {
                this._timer.Stop();
                MessageBox.Show("请先创建比赛或者打开比赛文件");
                return;
            }

            if (_match.HomeManager.Players[0].CntMoveResults== 0)
            {
                this._timer.Stop();
                MessageBox.Show("没有回合数据");
                return;
            }
            if (_isInitPlayer == false)
            {
                InitializePlayers();
            }
            ManagerReport[] managers = new[] { _match.HomeManager, _match.AwayManager };
            for (var m = 0; m < managers.Length; m++)
            {
                for (var p = 0; p < managers[m].Players.Count; p++)
                {
                    PlayPlayerProcess(m, p, GetPlayerMove(managers[m].Players[p].MoveResults, _round));
                }
            }

            if (_match.CntBallResults > _round)
            {
                PlayFootballProcess(_match.BallResults[_round]);
            }
            if (_statisticsWindow != null)
            {
                _statisticsWindow.SetEmulatorRound(_round);
            }
            if (_round == _maxRound - 1)
            {
                _round = 0;
                _timer.Stop();
                return;
            }
            ShowPlayerTip();
            _round++;
        }

        PlayerMoveReport GetPlayerMove(List<PlayerMoveReport> list, int round)
        {
            var obj = list.Find(i => i.AsRound == round);
            if (null != obj)
                return obj;
            int cnt=list.Count;
            for (int i = round; i >= 0; i--)
            {
                if (i >= cnt)
                    continue;
                if (list[i].AsRound <= round)
                {
                    obj = list[i];
                    break;
                }
            }
            return obj;
        }
        #endregion

        #region 播放回合

        /// <summary>
        /// Play a <see cref="PlayerProcess"/>.
        /// 播放一个球员的回合
        /// </summary>
        /// <param name="m">Manager loop index.</param>
        /// <param name="p">Player loop index.</param>
        /// <param name="process"><see cref="PlayerProcess"/>.</param>
        private void PlayPlayerProcess(int m, int p, PlayerMoveReport process)
        {
            if (null == process)
                return;
            ShowProcess(process.AsRound, Convert.ToInt32(2 * 60 / Defines.Match.ROUND_TIME));
            Player player = _playerCache[String.Format("player_{0}_{1}", m, p)];

            player.Skill = string.Empty;

            #region 显示球员状态
            player.DisplayState(process.StateData.State);
            #endregion

            #region 模型效果
            string skillStr = process.StateData.Disable ? "下场|" : string.Empty;
            if (process.StateData.ModelId!= 0)
            {
                skillStr = string.Concat(skillStr, "模型[", process.StateData.ModelId, "]");
            }
            player.Skill = skillStr;
            #endregion

            #region 球员名字显示效果
            if (process.StateData.NameVisible == 1)
            {
                player.MarkName((m == 0), false);
            }
            else
            {
                player.MarkNormal();
            }
            #endregion

            #region 球员技能
            ManagerReport[] managers = new[] { _match.HomeManager, _match.AwayManager };
            for (int i = 0; i < managers[m].Players[p].SkillResults.Count; i++)
            {
                if (managers[m].Players[p].SkillResults[i].Round == _round)
                {
                    player.Skill += "技能"+managers[m].Players[p].SkillResults[i].SkillId;
                }
            }
            #endregion

            #region 红黄牌效果
            // red card or injured
            if (process.StateData.FoulLevel == Defines.FoulLevel.RED_CARD || process.StateData.FoulLevel == Defines.FoulLevel.INJURED)
            {
                player.Dead();
            }
            else
            {
                bool yellowFlag = process.StateData.FoulLevel == Defines.FoulLevel.YELLOW_CARD;
                bool homeFlag = m == 0;
                player.Active(homeFlag, yellowFlag);
            }
            #endregion

            #region 动画效果
            var coordinate = process.StateData.Current;
            double x = GetPoint(coordinate.X);
            double y = GetPoint(coordinate.Y);

            player.ToolTip = String.Format("{0},{1}", coordinate.X, coordinate.Y);
            player.Angle = GetRotateAngle(process.StateData.Angle);

            // play the moving movie.
            var doubleAnimationX = new DoubleAnimation();
            doubleAnimationX.From = Convert.ToDouble(player.GetValue(Canvas.LeftProperty));
            doubleAnimationX.To = x;
            doubleAnimationX.Duration = new Duration(TimeSpan.FromMilliseconds(Defines.Match.ROUND_TIME * 1000));

            Storyboard.SetTarget(doubleAnimationX, player);
            Storyboard.SetTargetProperty(doubleAnimationX, new PropertyPath("(Canvas.Left)"));

            var doubleAnimationY = new DoubleAnimation();
            doubleAnimationY.From = Convert.ToDouble(player.GetValue(Canvas.TopProperty));
            doubleAnimationY.To = y;
            doubleAnimationY.Duration = new Duration(TimeSpan.FromMilliseconds(Defines.Match.ROUND_TIME * 1000));

            Storyboard.SetTarget(doubleAnimationY, player);
            Storyboard.SetTargetProperty(doubleAnimationY, new PropertyPath("(Canvas.Top)"));

            var storyboard = new Storyboard();
            storyboard.Children.Add(doubleAnimationX);
            storyboard.Children.Add(doubleAnimationY);


            storyboard.Begin();
            #endregion
        }

        /// <summary>
        /// Display the loading process.
        /// </summary>
        /// <param name="round">Current round.</param>
        /// <param name="total">Total round.</param>
        private void ShowProcess(int round, int total)
        {
            //this.loadingBar.Value = ((double)round / total) * 100;
        }

        /// <summary>
        /// Play a <see cref="FootballProcess"/>.
        /// 播放一个足球回合
        /// </summary>
        /// <param name="process"><see cref="IProcess"/></param>
        private void PlayFootballProcess(BallMoveReport process)
        {
            if (null == process)
                return;
            this.lblInterruption.Content = string.Empty;
            this.lblRound.Content = _round.ToString() + '/' + _match.BallResults[_match.CntBallResults - 1].AsRound;

            #region 比分效果
            if (process.ClassId == ReportAsset.MatchStateAsset.CLASSIdGoal)
            {
                var goalState = process.StateData as GoalStateReport;
                if (null != goalState)
                {
                    this.lblSoccer.Content = goalState.HomeScore + ":" + goalState.AwayScore;
                }
            }
            #endregion

            #region 经理技能
            lblManagerName.Content = String.Empty;

            if (process.StateData.State == (int)Base.Enum.EnumMatchBreakState.SectionOpen)
            {
                lblInterruption.Content = "过场动画";
            }

            if (process.StateData.FoulState > 0)
            {
                lblInterruption.Content += "犯规";
            }
            #endregion

            #region 播放足球移动
            var coordinate = process.StateData.Current;
            double x = coordinate.X * RECTANGLE_SIZE - (double)RECTANGLE_SIZE / 2;
            double y = coordinate.Y * RECTANGLE_SIZE - (double)RECTANGLE_SIZE / 2;

            _ball.ToolTip = string.Format("{0},{1}", coordinate.X, coordinate.Y);

            var doubleAnimationX = new DoubleAnimation();
            doubleAnimationX.From = Convert.ToDouble(_ball.GetValue(Canvas.LeftProperty));
            doubleAnimationX.To = x;
            doubleAnimationX.Duration = new Duration(TimeSpan.FromMilliseconds(Defines.Match.ROUND_TIME * 1000));

            Storyboard.SetTarget(doubleAnimationX, _ball);
            Storyboard.SetTargetProperty(doubleAnimationX, new PropertyPath("(Canvas.Left)"));

            var doubleAnimationY = new DoubleAnimation();
            doubleAnimationY.From = Convert.ToDouble(_ball.GetValue(Canvas.TopProperty));
            doubleAnimationY.To = y;
            doubleAnimationY.Duration = new Duration(TimeSpan.FromMilliseconds(Defines.Match.ROUND_TIME * 1000));

            Storyboard.SetTarget(doubleAnimationY, _ball);
            Storyboard.SetTargetProperty(doubleAnimationY, new PropertyPath("(Canvas.Top)"));

            var storyboard = new Storyboard();
            storyboard.Children.Add(doubleAnimationX);
            storyboard.Children.Add(doubleAnimationY);

            storyboard.Begin();
            #endregion
        }

        #endregion

        #region 工具

        /// <summary>
        /// Transform the internal point to pitch point.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private static double GetPoint(double point)
        {
            return point * RECTANGLE_SIZE - Defines.Player.PLAYER_WIDTH * RECTANGLE_SIZE;
        }

        private static int GetRotateAngle(int index)
        {
            switch (index)
            {
                case 0:
                    return 0;
                case 1:
                    return 45;
                case 2:
                    return 90;
                case 3:
                    return 135;
                case 4:
                    return 180;
                case 5:
                    return 225;
                case 6:
                    return 270;
                case 7:
                    return 315;
                default:
                    throw new ApplicationException("index not exsisted. index:" + index);
            }
        }

        private int GetMaxRound()
        {
            int round = 0;

            foreach (var m in new[] { _match.HomeManager, _match.AwayManager })
            {
                foreach (var p in m.Players)
                {
                    if (p.CntMoveResults > round)
                    {
                        round = p.CntMoveResults;
                    }
                }
            }

            return round;
        }

        #endregion

        private void btnLoadFromDB_Click(object sender, RoutedEventArgs e)
        {
            string txt = txtMatchId.Text;
            Guid matchId = Guid.Empty;
            if (!Guid.TryParse(txt, out matchId))
            {
                MessageBox.Show("比赛Id格式不正确！");
                return;
            }
            int matchType = ComboBoxHelper.GetSelectValueInt(cmbMatchType);
            byte[] process = EmulatorHelper.GetProcess(matchId, matchType);
            if (process != null)
            {
                try
                {
                    _match = IOUtil.BinRead<MatchReport>(process,0);
                    DataMgr.FillFormData(_match);
                    DataMgr.FillRoundData(_match);
                    this._maxRound = GetMaxRound();
                    Interlocked.Exchange(ref this._isEndStart, 1);
                    _waitTimer.Start();
                    //const string xmlpath = @"d:\test\datainfofromdb.xml";
                    //IOUtil.XmlWrite(xmlpath,_match);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("获取比赛失败！" + ex.Message);
                    return;
                }
            }
            else
            {
                MessageBox.Show("获取比赛失败！");
                return;
            }
            if (_match != null)
            {
                PlayGame(this, new EventArgs());
            }
        }

        delegate void ShowGridDelegate(List<StatisticsSkillEntity> list);

        void ShowGridMethod(List<StatisticsSkillEntity> list)
        {
            DataGridSkill.ItemsSource = list;
            //lblManagerName.Content = _match.HomeManager.ManagerName + " VS " + _match.AwayManager.ManagerName;
        }

        private void BuildSkillInfo()
        {
            List<StatisticsSkillEntity> list = new List<StatisticsSkillEntity>();
            BuildSkillInfo(_match.HomeManager, list);
            BuildSkillInfo(_match.AwayManager, list);
            Dispatcher.Invoke((Action)delegate { ShowGridMethod(list); });
        }

        private void BuildSkillInfo(ManagerReport manager, List<StatisticsSkillEntity> list)
        {
            foreach (var managerSkill in manager.SkillResults)
            {
                list.Add(BuildSkillEntity(managerSkill, "经理技能", manager.ManagerName));
            }

            foreach (var player in manager.Players)
            {
                foreach (var pskill in player.SkillResults)
                {
                    list.Add(BuildSkillEntity(pskill, "球员技能", player.Name));
                }
                foreach (var process in player.MoveResults)
                {
                    if (process.StateData.ModelId > 0)
                    {
                        list.Add(BuildSkillEntity(process.StateData.ModelId, process.AsRound, player.Name));
                    }
                }
            }
        }

        private StatisticsSkillEntity BuildSkillEntity(SkillReport outputskill, string type, string source)
        {
            var entity = new StatisticsSkillEntity();
            entity.Id = outputskill.SkillId;
            entity.Round = outputskill.Round;
            entity.Name = ModelMgr.GetSkillStr(outputskill.SkillId.ToString());
            if (outputskill.SkillTargets != null && outputskill.SkillTargets.Length > 0)
            {
                foreach (var t in outputskill.SkillTargets)
                {
                    entity.Target += t.ToString() + ',';
                }
                entity.Target = entity.Target.TrimEnd(',');
            }
            entity.Type = type;
            entity.Source = source;
            return entity;
        }

        private StatisticsSkillEntity BuildSkillEntity(int modelId, int round, string source)
        {
            var entity = new StatisticsSkillEntity();
            entity.Id = modelId;
            entity.Round = round;
            entity.Name = ModelMgr.GetModelStr(modelId.ToString());
            entity.Target = "";
            entity.Type = "球员model";
            entity.Source = source;
            return entity;
        }

        #region Statistics
        private StatisticsWindow _statisticsWindow;
        delegate void WillHide();
        private WillHide willHide;
        private bool hasShow=false;
        private void btnViewStatistics_Click(object sender, RoutedEventArgs e)
        {
            if (_statisticsMatch == null)
            {
                MessageBox.Show("没有统计数据,请先创建比赛！");
                return;
            }

            if (_statisticsWindow == null)
            {
                //App.Current.MainWindow = this;
                App.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                _statisticsWindow = new StatisticsWindow(_statisticsMatch);
                this.willHide = new WillHide(this.HideWin2);
                this._statisticsWindow.Closing += new CancelEventHandler(win2_Closing);
            }
            if (!hasShow)
            {
                _statisticsWindow.Init(_statisticsMatch);
            }
            hasShow = true;
            _statisticsWindow.Show();
        }

        void win2_Closing(object sender, CancelEventArgs e)
        {
            hasShow = false;
            e.Cancel = true;
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, this.willHide);
        }

        private void HideWin2()
        {
            this._statisticsWindow.Hide();
        }
        #endregion

    }
}
