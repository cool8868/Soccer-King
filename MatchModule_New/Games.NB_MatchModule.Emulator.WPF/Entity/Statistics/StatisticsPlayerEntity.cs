using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Emulator.WPF.Mgrs;

namespace Games.NB.Match.Emulator.WPF.Entity.Statistics
{
    public class StatisticsPlayerEntity
    {
        public StatisticsPlayerEntity(IPlayer player)
        {
            this.Pid = player.Input.Pid;
            this.Name = player.Input.FamilyName;
            this.Position = player.Input.Position;
            this.State = EmulatorHelper.GetStateContent(player.Status.ClientState);

            player.PropCore.TraceCore.TraceFull();
            this.Speed = player.PropCore.TraceCore[PlayerProperty.Speed];
            this.Shooting = player.PropCore.TraceCore[PlayerProperty.Shooting];
            this.FreeKick = player.PropCore.TraceCore[PlayerProperty.FreeKick];
            this.Balance = player.PropCore.TraceCore[PlayerProperty.Balance];
            this.Stamina = player.PropCore.TraceCore[PlayerProperty.Stamina];
            this.Strength = player.PropCore.TraceCore[PlayerProperty.Strength];
            this.Aggression = player.PropCore.TraceCore[PlayerProperty.Aggression];
            this.Disturb = player.PropCore.TraceCore[PlayerProperty.Disturb];
            this.Interception = player.PropCore.TraceCore[PlayerProperty.Interception];
            this.Dribble = player.PropCore.TraceCore[PlayerProperty.Dribble];
            this.Passing = player.PropCore.TraceCore[PlayerProperty.Passing];
            this.Mentality = player.PropCore.TraceCore[PlayerProperty.Mentality];
            this.Reflexes = player.PropCore.TraceCore[PlayerProperty.Reflexes];
            this.Positioning = player.PropCore.TraceCore[PlayerProperty.Positioning];
            this.Handling = player.PropCore.TraceCore[PlayerProperty.Handling];
            this.Acceleration = player.PropCore.TraceCore[PlayerProperty.Acceleration];
            this.PassChooseRate = player.PropCore.TraceCore[PlayerProperty.PassChooseRate];
            this.DribbleChooseRate = player.PropCore.TraceCore[PlayerProperty.DribbleChooseRate];
            this.ShootChooseRate = player.PropCore.TraceCore[PlayerProperty.ShootChooseRate];
            this.StealChooseRate = player.PropCore.TraceCore[PlayerProperty.StealChooseRate];
            this.PassSuccRate = player.PropCore.TraceCore[PlayerProperty.PassSuccRate];
            this.DribbleSuccRate = player.PropCore.TraceCore[PlayerProperty.DribbleSuccRate];
            this.ShootSuccRate = player.PropCore.TraceCore[PlayerProperty.ShootSuccRate];
            this.StealSuccRate = player.PropCore.TraceCore[PlayerProperty.StealSuccRate];
            this.TurnStealRate = player.PropCore.TraceCore[PlayerProperty.TurnStealRate];
            this.ShootRange = player.PropCore.TraceCore[PlayerProperty.ShootRange];
            this.DisturbRange = player.PropCore.TraceCore[PlayerProperty.DisturbRange];
            this.StealRange = player.PropCore.TraceCore[PlayerProperty.StealRange];

            this.ShootingDist = player.PropCore.TraceCore[PlayerProperty.ShootingDist];
        }

        public int Pid { get; set; }

        public int Position { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public double Speed { get; set; }
        public double Shooting { get; set; }
        public double FreeKick { get; set; }
        public double Balance { get; set; }
        public double Stamina { get; set; }
        public double Strength { get; set; }
        public double Aggression { get; set; }
        public double Disturb { get; set; }
        public double Interception { get; set; }
        public double Dribble { get; set; }
        public double Passing { get; set; }
        public double Mentality { get; set; }
        public double Reflexes { get; set; }
        public double Positioning { get; set; }
        public double Handling { get; set; }
        public double Acceleration { get; set; }


        public double PassChooseRate { get; set; }
        public double DribbleChooseRate { get; set; }
        public double ShootChooseRate { get; set; }
        public double StealChooseRate { get; set; }
        public double PassSuccRate { get; set; }
        public double DribbleSuccRate { get; set; }
        public double ShootSuccRate { get; set; }
        public double StealSuccRate { get; set; }
        public double DiveSuccRate { get; set; }
        public double TurnStealRate { get; set; }

        public double ShootRange { get; set; }
        public double DisturbRange { get; set; }
        public double StealRange { get; set; }

        public double ShootingDist { get; set; }


        public string SpeedStr { get { return Speed.ToString("f2"); } }
        public string ShootingStr { get { return Shooting.ToString("f2"); } }
        public string FreeKickStr { get { return FreeKick.ToString("f2"); } }
        public string BalanceStr { get { return Balance.ToString("f2"); } }
        public string StaminaStr { get { return Stamina.ToString("f2"); } }
        public string StrengthStr { get { return Strength.ToString("f2"); } }
        public string AggressionStr { get { return Aggression.ToString("f2"); } }
        public string DisturbStr { get { return Disturb.ToString("f2"); } }
        public string InterceptionStr { get { return Interception.ToString("f2"); } }
        public string DribbleStr { get { return Dribble.ToString("f2"); } }
        public string PassingStr { get { return Passing.ToString("f2"); } }
        public string MentalityStr { get { return Mentality.ToString("f2"); } }
        public string ReflexesStr { get { return Reflexes.ToString("f2"); } }
        public string PositioningStr { get { return Positioning.ToString("f2"); } }
        public string HandlingStr { get { return Handling.ToString("f2"); } }
        public string AccelerationStr { get { return Acceleration.ToString("f2"); } }

        public string PassChooseRateStr { get { return PassChooseRate.ToString("f2"); } }
        public string DribbleChooseRateStr { get { return DribbleChooseRate.ToString("f2"); } }
        public string ShootChooseRateStr { get { return ShootChooseRate.ToString("f2"); } }
        public string StealChooseRateStr { get { return StealChooseRate.ToString("f2"); } }
        public string PassSuccRateStr { get { return PassSuccRate.ToString("f2"); } }
        public string DribbleSuccRateStr { get { return DribbleSuccRate.ToString("f2"); } }
        public string ShootSuccRateStr { get { return ShootSuccRate.ToString("f2"); } }
        public string StealSuccRateStr { get { return StealSuccRate.ToString("f2"); } }
        public string DiveSuccRateStr { get { return DiveSuccRate.ToString("f2"); } }
        public string TurnStealRateStr { get { return TurnStealRate.ToString("f2"); } }

        public string ShootRangeStr { get { return ShootRange.ToString("f2"); } }
        public string DisturbRangeStr { get { return DisturbRange.ToString("f2"); } }
        public string StealRangeStr { get { return StealRange.ToString("f2"); } }
        public string ShootingDistStr { get { return ShootingDist.ToString("f0"); } }


    }
}
