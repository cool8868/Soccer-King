using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Games.NB.Match.Base.Model.TranIn
{
    [DataContract]
    [Serializable]
    public class ManagerInput
    {
        #region Data
        /// <summary>
        /// 经理Id
        /// </summary>
        [DataMember] 
        public Guid Mid
        {
            get;
            set;
        }
        /// <summary>
        /// 经理类型,0-玩家;1-Npc
        /// </summary>
        [DataMember] 
        public int Kind
        {
            get;
            set;
        }
        /// <summary>
        /// 经理名
        /// </summary>
        [DataMember] 
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Logo
        /// </summary>
        [DataMember] 
        public string Logo
        {
            get;
            set;
        }
        /// <summary>
        /// 球衣Id
        /// </summary>
        [DataMember] 
        public int ClothId
        {
            get;
            set;
        }
        /// <summary>
        /// 阵型Id
        /// </summary>
        [DataMember] 
        public int FormId
        {
            get;
            set;
        }
        /// <summary>
        /// 阵型等级
        /// </summary>
        [DataMember] 
        public int FormLv
        {
            get;
            set;
        }
        /// <summary>
        /// 教练Id
        /// </summary>
        [DataMember] 
        public int CoachId
        {
            get;
            set;
        }
        /// <summary>
        /// 教练等级
        /// </summary>
        [DataMember] 
        public int CoachLv
        {
            get;
            set;
        }
        /// <summary>
        /// Buff系数
        /// </summary>
        [DataMember] 
        public int BuffFact
        {
            get;
            set;
        }
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
        /// 分节技能，当前为上下场天赋
        /// </summary>
        [DataMember]
        public string[] SubSkills
        {
            get;
            set;
        }
        /// <summary>
        /// 分节战术
        /// </summary>
        [DataMember]
        public string[] SubTactics
        {
            get;
            set;
        }
        #endregion

        #region 球员
        /// <summary>
        /// 球员列表
        /// </summary>
        [DataMember]
        public List<PlayerInput> Players
        {
            get;
            set;
        }
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
        public void BalanceProp()
        {
            BalanceProp(this.BuffFact);
        }
        public void BalanceProp(int buffFact)
        {
            if (buffFact <= 0 || buffFact == 100 || null == this.Players)
                return;
            foreach (var p in Players)
            {
                p.BalanceProp(buffFact);
            }
        }
        #endregion
    }
}
