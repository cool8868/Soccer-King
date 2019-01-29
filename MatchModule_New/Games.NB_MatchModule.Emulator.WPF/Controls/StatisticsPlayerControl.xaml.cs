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
    /// StatisticsPlayerControl.xaml 的交互逻辑
    /// </summary>
    public partial class StatisticsPlayerControl : UserControl
    {
        public StatisticsPlayerControl()
        {
            InitializeComponent();
        }

        public void SetRoundData(StatisticsPlayerEntity player)
        {
            this.lblName.Content = player.Name;
            this.lblState.Content = player.State;
            this.lblPosition.Content = player.Position;

            this.lblSpeedStr.Content = player.SpeedStr;
            this.lblShootingStr.Content = player.ShootingStr;
            this.lblShootingDistStr.Content = player.ShootingDistStr;
            this.lblFreeKickStr.Content = player.FreeKickStr;
            this.lblBalanceStr.Content = player.BalanceStr;
            this.lblStaminaStr.Content = player.StaminaStr;
            this.lblStrengthStr.Content = player.StrengthStr;
            this.lblAggressionStr.Content = player.AggressionStr;
            this.lblDisturbStr.Content = player.DisturbStr;
            this.lblInterceptionStr.Content = player.InterceptionStr;
            this.lblDribbleStr.Content = player.DribbleStr;
            this.lblPassingStr.Content = player.PassingStr;
            this.lblMentalityStr.Content = player.MentalityStr;
            this.lblReflexesStr.Content = player.ReflexesStr;
            this.lblPositioningStr.Content = player.PositioningStr;
            this.lblHandlingStr.Content = player.HandlingStr;
            this.lblAccelerationStr.Content = player.AccelerationStr;

            this.lblShootChooseRateStr.Content = player.ShootChooseRateStr;
            this.lblPassChooseRateStr.Content = player.PassChooseRateStr;
            this.lblDribbleChooseRateStr.Content = player.DribbleChooseRateStr;
            this.lblStealChooseRateStr.Content = player.StealChooseRateStr;
            this.lblShootSuccRateStr.Content = player.ShootSuccRateStr;
            this.lblPassSuccRateStr.Content = player.PassSuccRateStr;
            this.lblDribbleSuccRateStr.Content = player.DribbleSuccRateStr;
            this.lblStealSuccRateStr.Content = player.StealSuccRateStr;
            this.lblDiveSuccRateStr.Content = player.DiveSuccRateStr;
            this.lblTurnStealRateStr.Content = player.TurnStealRateStr;

            this.lblShootRangeStr.Content = player.ShootRangeStr;
            this.lblDisturbRangeStr.Content = player.DisturbRangeStr;
            this.lblStealRangeStr.Content = player.StealRangeStr;
        }
    }
}
