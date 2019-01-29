using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Games.NB.Match.Base.Enum;

namespace Games.NB.Match.Base.Model.TranIn
{
    [DataContract]
    [Serializable]
    public class PlayerInput
    {
        #region Data
        /// <summary>
        /// 球员TeammemberId
        /// </summary>
        [DataMember]
        public Guid Tid
        {
            get;
            set;
        }
        /// <summary>
        /// 强化等级
        /// </summary>
        [DataMember]
        public byte Plus
        {
            get;
            set;
        }
        /// <summary>
        /// 球员等级
        /// </summary>
        [DataMember]
        public byte Level
        {
            get;
            set;
        }
        #endregion

        #region Raw
        /// <summary>
        /// 球员字典Id
        /// </summary>
        [DataMember]
        public int Pid
        {
            get;
            set;
        }
        /// <summary>
        /// 位置枚举
        /// </summary>
        public Position AsPosition
        {
            get
            {
                return (Position)Position;
            }
        }
        /// <summary>
        /// 位置
        /// </summary>
        [DataMember]
        public byte Position
        {
            get;
            set;
        }
        /// <summary>
        /// 颜色级别
        /// </summary>
        [DataMember]
        public byte Color
        {
            get;
            set;
        }
        /// <summary>
        /// 头部造型
        /// </summary>
        [DataMember]
        public int HeadStyle
        {
            get;
            set;
        }
        /// <summary>
        /// 身体造型
        /// </summary>
        [DataMember]
        public int BodyStyle
        {
            get;
            set;
        }
        /// <summary>
        /// 球员名字
        /// </summary>
        [DataMember]
        public string FirstName
        {
            get;
            set;
        }
        /// <summary>
        /// 球员的姓
        /// </summary>
        [DataMember]
        public string FamilyName
        {
            get;
            set;
        }
        /// <summary>
        /// 身高
        /// </summary>
        public int Height
        {
            get;
            set;
        }
        #endregion

        #region 技能
        /// <summary>
        /// 全场技能
        /// </summary>
        [DataMember]
        public List<string> Skills
        {
            get;
            set;
        }
        /// <summary>
        /// 分节技能
        /// </summary>
        [DataMember]
        public string[] SubSkills
        {
            get;
            set;
        }
        #endregion

        #region 属性
        /// <summary>
        /// Speed
        /// 速度
        /// </summary>
        [DataMember]
        public double Speed { get; set; }

        /// <summary>
        /// Shooting
        /// 射门
        /// </summary>
        [DataMember]
        public double Shooting { get; set; }

        /// <summary>
        /// Free Kick
        /// 任意球
        /// </summary>
        [DataMember]
        public double FreeKick { get; set; }

        /// <summary>
        /// Balance
        /// 控制
        /// </summary>
        [DataMember]
        public double Balance { get; set; }

        /// <summary>
        /// Starmina
        /// 体能
        /// </summary>
        [DataMember]
        public double Stamina { get; set; }

        /// <summary>
        /// Strength
        /// 力量
        /// </summary>
        [DataMember]
        public double Strength { get; set; }

        /// <summary>
        /// Aggression
        /// 侵略性
        /// </summary>
        [DataMember]
        public double Aggression { get; set; }

        /// <summary>
        /// Disturb
        /// 干扰
        /// </summary>
        [DataMember]
        public double Disturb { get; set; }

        /// <summary>
        /// Interception
        /// 抢断
        /// </summary>
        [DataMember]
        public double Interception { get; set; }

        /// <summary>
        /// Dribble
        /// 控球
        /// </summary>
        [DataMember]
        public double Dribble { get; set; }

        /// <summary>
        /// Passing
        /// 传球
        /// </summary>
        [DataMember]
        public double Passing { get; set; }

        /// <summary>
        /// Mentality
        /// 意识
        /// </summary>
        [DataMember]
        public double Mentality { get; set; }

        /// <summary>
        /// Reflexes
        /// 反应
        /// </summary>
        [DataMember]
        public double Reflexes { get; set; }

        /// <summary>
        /// Positioning
        /// 位置感
        /// </summary>
        [DataMember]
        public double Positioning { get; set; }

        /// <summary>
        /// Handling
        /// 手控球
        /// </summary>
        [DataMember]
        public double Handling { get; set; }

        /// <summary>
        /// Acceleration
        /// 加速度
        /// </summary>
        [DataMember]
        public double Acceleration { get; set; }
        #endregion

        #region Buff
        [DataMember]
        public List<PropInput> PropList
        {
            get;
            set;
        }
        [DataMember]
        public List<BoostInput> BoostList
        {
            get;
            set;
        }
        #endregion

        #region
        public void BalanceProp(int buffFact = 100)
        {
            double fact = Math.Max(0.01, buffFact / 100);
            this.Speed *= fact;
            this.Shooting *= fact;
            this.FreeKick *= fact;
            this.Balance *= fact;
            this.Stamina *= fact;
            this.Strength *= fact;
            this.Aggression *= fact;
            this.Disturb *= fact;
            this.Interception *= fact;
            this.Dribble *= fact;
            this.Passing *= fact;
            this.Mentality *= fact;
            this.Reflexes *= fact;
            this.Positioning *= fact;
            this.Handling *= fact;
            this.Acceleration *= fact;
        }
        #endregion

    }

    [DataContract]
    [Serializable]
    public class PropInput
    {
        public PropInput()
        { }
        public PropInput(double point, double percent, params int[] buffId)
        {
            this.Point = point;
            this.Percent = percent;
            this.BuffId = buffId;
        }
        [DataMember] 
        public int[] BuffId
        {
            get;
            set;
        }
        [DataMember] 
        public double Point
        {
            get;
            set;
        }
        [DataMember] 
        public double Percent
        {
            get;
            set;
        }
    }
    [DataContract]
    [Serializable]
    public class BoostInput
    {
        public BoostInput()
        { }
        public BoostInput(int boostType, double point, double percent, params int[] buffId)
        {
            this.BoostType = boostType;
            this.Point = point;
            this.Percent = percent;
            this.BuffId = buffId;
        }
        [DataMember] 
        public int BoostType
        {
            get;
            set;
        }
        [DataMember] 
        public int[] BuffId
        {
            get;
            set;
        }
        [DataMember]
        public double Point
        {
            get;
            set;
        }
        [DataMember]
        public double Percent
        {
            get;
            set;
        }
    }
}
