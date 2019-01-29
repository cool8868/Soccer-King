using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillCore;

namespace SkillEngine.SkillCore
{
    public class PlayerNumTrigger : ITrigger
    {
        #region .ctor
        public PlayerNumTrigger()
        { }
        public PlayerNumTrigger(int minNum, int maxNum)
        {
            this.MinNum = minNum;
            this.MaxNum = maxNum;
        }
        #endregion

        #region Data
        public int MinNum
        {
            get;
            set;
        }
        public int MaxNum
        {
            get;
            set;
        }
        public bool Repeat
        {
            get;
            set;
        }
        public bool Recycle
        {
            get;
            set;
        }
        public bool Delay
        {
            get;
            set;
        }
        public PlayerLocator Locator
        {
            get;
            set;
        }
        #endregion

        public bool Trigger(ISkill srcSkill, ISkillPlayer caster)
        {
            if (null == this.Locator)
                return false;
            var players = this.Locator.Locate(srcSkill, caster);
            int cnt = 0;
            if (null != players)
                cnt = players.Count;
            return MinNum <= cnt && cnt <= MaxNum;
        }
    }
}
