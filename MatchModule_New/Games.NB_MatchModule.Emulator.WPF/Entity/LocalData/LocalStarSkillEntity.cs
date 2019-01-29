using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;

namespace Games.NB.Match.Emulator.WPF.Entity.LocalData
{
    public class LocalStarSkillEntity
    {
        public LocalStarSkillEntity()
        { 
        }

        public LocalStarSkillEntity(DicStarskillsEntity dic)
        {
            this.SkillCode = dic.SkillCode;
            this.SkillName = dic.Name;
            this.ActType = dic.ActType;
        }

        public string SkillCode
        {
            get;
            set;
        }
        public string SkillName
        {
            get;
            set;
        }
        public int ActType 
        { 
            get; 
            set; 
        }
    }
}
