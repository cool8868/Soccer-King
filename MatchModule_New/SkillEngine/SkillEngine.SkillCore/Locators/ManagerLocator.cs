using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern.Enum;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public class ManagerLocator : ILocator<ISkillManager>
    {
        #region .ctor
        public ManagerLocator()
        { }
        public ManagerLocator(EnumOwnSide side)
        {
            this.Side = side;
        }
        #endregion

        #region Data
        public EnumOwnSide Side
        {
            get;
            set;
        }
        public PlayerLocator Locator
        {
            get;
            set;
        }
        public List<IManagerFilter> Filters
        {
            get;
            set;
        }
        #endregion
      
        #region Facade
        public List<ISkillManager> Locate(ISkill srcSkill, ISkillPlayer caster)
        {
            var targets = new List<ISkillManager>();
            if (!this.Locate(targets, srcSkill, caster))
                return null;
            return targets;
        }
        public bool Locate(List<ISkillManager> targets, ISkill srcSkill, ISkillPlayer caster)
        {
            if (null != this.Locator && this.Locator.Seekers.Count > 0)
            {
                var players = this.Locator.Locate(srcSkill, caster);
                if (null == players || players.Count == 0)
                    return false;
            }
            var srcManager = null != caster ? caster.SkillManager : srcSkill.Owner as ISkillManager;
            if (null == srcManager)
                return false;
            var dstManager = Side == EnumOwnSide.Own ? srcManager : srcManager.OppSkillManager;
            if (null != this.Filters && this.Filters.Count > 0)
            {
                foreach (var filter in Filters)
                {
                    if (!filter.Check(srcManager, dstManager))
                    {
                        return false;
                    }
                }
            }
            targets.Clear();
            targets.Add(dstManager);
            return true;
        }
        #endregion

 
    }
}
