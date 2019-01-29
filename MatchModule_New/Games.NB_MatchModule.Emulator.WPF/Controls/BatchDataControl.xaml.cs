using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Games.NB.Match.Emulator.WPF.Entity;

namespace Games.NB.Match.Emulator.WPF.Controls
{
    /// <summary>
    /// MatchAnalysisDataControl.xaml 的交互逻辑
    /// </summary>
    public partial class BatchDataControl : UserControl
    {
        public BatchDataControl()
        {
            InitializeComponent();
        }

        public void SetData(BatchManagerEntity manager)
        {
            lblScore.Content = "得分：" + manager.Score;
            lblControlTime.Content = string.Format("控球时间占比：{0:f1}" , manager.ControlRound*100.00/manager.TotalRound);
            lblGoal.Content = "进球数：" + manager.TotalShoot.GoalTimes;
            lblShootCount.Content = "射门次数：" + manager.TotalShoot.ShootTimes;
            lblDoorframeCount.Content = "射中门框次数：" + manager.TotalShoot.DoorFrame;

            lblGoalKeepCount.Content = "扑救次数：" + manager.GoalKeep.Times;
            lblGoalKeepSuccessCount.Content = "扑救成功次数：" + manager.GoalKeep.SuccTimes;
            lblGoalKeepFailCount.Content = "脱手次数：" + manager.GoalKeep.FailTimes;

            lblPassCount.Content = "传球次数：" + manager.TotalPass.Times;
            lblPassRate.Content = "传球成功率：" + manager.TotalPass.RateStr;

            lblShortPassCount.Content = "短传次数：" + manager.ShortPass.Times;
            lblShortPassRate.Content = "短传成功率：" + manager.ShortPass.RateStr;

            lblLongPassCount.Content = "长传次数：" + manager.LongPass.Times;
            lblLongPassRate.Content = "长传成功率：" + manager.LongPass.RateStr;

            lblHeadPassCount.Content = "头球次数：" + manager.HeadPass.Times;
            lblHeadPassRate.Content = "头球成功率：" + manager.HeadPass.RateStr;

            lblStealsCount.Content = "抢断次数：" + manager.Steal.Times;
            lblStealsRate.Content = "抢断成功率：" + manager.Steal.RateStr;

            lblBreakThroughCount.Content = "过人次数：" + manager.Breakthrough.Times;
            lblBreakThroughRate.Content = "过人成功率：" + manager.Breakthrough.RateStr;

            lblFoulCount.Content = "犯规次数：" + manager.Foul.FoulTimes;
            lblFoulRedCount.Content = "红牌数：" + manager.Foul.RedTimes;
            lblFoulYellowCount.Content = "黄牌数：" + manager.Foul.YellowTimes;

            lblInjuredCount.Content = "受伤次数：" + manager.InjuredTimes;

            lblRebelShootCount.Content = "乌龙次数：" + manager.RebelShoot.ShootTimes;
            lblRebelShootGoal.Content = "乌龙进球数：" + manager.RebelShoot.GoalTimes;
        }

        
    }
}
