using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;

namespace Games.NB.Match.Emulator.WPF.Entity.LocalData
{
    public class LocalDicPlayer
    {
        public LocalDicPlayer()
        {
            
        }
        public LocalDicPlayer(DicPlayerEntity dicPlayer)
        {
            this.Idx = dicPlayer.Idx;
            this.Name = dicPlayer.Name;
            this.CardLevel = dicPlayer.CardLevel;
            this.LeagueLevel = dicPlayer.LeagueLevel;
            this.Speed = dicPlayer.Speed.ToString("f0");
            this.Shoot = dicPlayer.Shoot.ToString("f0");
            this.FreeKick = dicPlayer.FreeKick.ToString("f0");
            this.Balance = dicPlayer.Balance.ToString("f0");
            this.Physique = dicPlayer.Physique.ToString("f0");
            this.Bounce = dicPlayer.Bounce.ToString("f0");
            this.Aggression = dicPlayer.Aggression.ToString("f0");
            this.Disturb = dicPlayer.Disturb.ToString("f0");
            this.Interception = dicPlayer.Interception.ToString("f0");
            this.Dribble = dicPlayer.Dribble.ToString("f0");
            this.Pass = dicPlayer.Pass.ToString("f0");
            this.Mentality = dicPlayer.Mentality.ToString("f0");
            this.Response = dicPlayer.Response.ToString("f0");
            this.Positioning = dicPlayer.Positioning.ToString("f0");
            this.HandControl = dicPlayer.HandControl.ToString("f0");
            this.Acceleration = dicPlayer.Acceleration.ToString("f0");
        }

        #region Public Properties

        ///<summary>
        ///Idx
        ///</summary>
        public System.Int32 Idx { get; set; }

        ///<summary>
        ///名字
        ///</summary>
        public System.String Name { get; set; }

        
        ///<summary>
        ///卡牌颜色
        ///</summary>
        public System.Int32 CardLevel { get; set; }

        ///<summary>
        ///联赛级别
        ///</summary>
        public System.Int32 LeagueLevel { get; set; }
        ///<summary>
        ///速度
        ///</summary>
        public string Speed { get; set; }

        ///<summary>
        ///射门
        ///</summary>
        public string Shoot { get; set; }

        ///<summary>
        ///任意球
        ///</summary>
        public string FreeKick { get; set; }

        ///<summary>
        ///控制
        ///</summary>
        public string Balance { get; set; }

        ///<summary>
        ///体质
        ///</summary>
        public string Physique { get; set; }

        ///<summary>
        ///弹跳
        ///</summary>
        public string Bounce { get; set; }

        ///<summary>
        ///侵略性
        ///</summary>
        public string Aggression { get; set; }

        ///<summary>
        ///干扰
        ///</summary>
        public string Disturb { get; set; }

        ///<summary>
        ///抢断
        ///</summary>
        public string Interception { get; set; }

        ///<summary>
        ///控球
        ///</summary>
        public string Dribble { get; set; }

        ///<summary>
        ///传球
        ///</summary>
        public string Pass { get; set; }

        ///<summary>
        ///意识
        ///</summary>
        public string Mentality { get; set; }

        ///<summary>
        ///反应
        ///</summary>
        public string Response { get; set; }

        ///<summary>
        ///位置感
        ///</summary>
        public string Positioning { get; set; }

        ///<summary>
        ///手控球
        ///</summary>
        public string HandControl { get; set; }

        ///<summary>
        ///加速度
        ///</summary>
        public string Acceleration { get; set; }

        #endregion
    }
}
