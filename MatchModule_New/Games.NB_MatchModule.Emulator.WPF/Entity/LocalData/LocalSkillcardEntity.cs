using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;

namespace Games.NB.Match.Emulator.WPF.Entity.LocalData
{
    public class LocalSkillcardEntity
    {
        public LocalSkillcardEntity()
        {
            
        }

        public LocalSkillcardEntity(DicSkillcardEntity dicSkillcard)
        {
            this.SkillCode = dicSkillcard.SkillCode;
            this.ItemName = dicSkillcard.ItemName;
            this.SkillClass = dicSkillcard.SkillClass;
            this.SkillLevel = dicSkillcard.SkillLevel;
            this.ActType = dicSkillcard.ActType;
        }

        #region Public Properties

        ///<summary>
        ///SkillCode
        ///</summary>
        public System.String SkillCode { get; set; }

        ///<summary>
        ///名称
        ///</summary>
        public System.String ItemName { get; set; }


        ///<summary>
        ///品质
        ///</summary>
        public System.Int32 SkillClass { get; set; }


        ///<summary>
        ///等级
        ///</summary>
        public System.Int32 SkillLevel { get; set; }

        ///<summary>
        ///动作
        ///</summary>
        public System.Int32 ActType { get; set; }

        #endregion
    }
}
