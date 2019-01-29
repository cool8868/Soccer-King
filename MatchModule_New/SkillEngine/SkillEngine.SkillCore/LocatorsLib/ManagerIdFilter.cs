using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern.Enum;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public class ManagerIdFilter : IPlayerFilter, ITrigger
    {
        public ManagerIdFilter(string values)
        {
            if (string.IsNullOrEmpty(values))
                return;
            var splits = values.Split(',');
            Guid mid;
            Values = new Dictionary<Guid, byte>(splits.Length);
            foreach (var str in splits)
            {
                if (Guid.TryParse(str, out mid))
                    Values[mid] = 0;
            }
        }

        #region Data
        public Dictionary<Guid,byte> Values
        {
            get;
            protected set;
        }
        #endregion

        #region IPlayerFilter
        public bool Check(ISkillManager srcManager, ISkillPlayer srcPlayer, ISkillPlayer dstPlayer)
        {
            return CheckCore(srcManager);
        }
        #endregion

        #region ITrigger
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
        public bool Trigger(ISkill srcSkill, ISkillPlayer caster)
        {
            if (null == caster)
                return CheckCore(srcSkill.Owner as ISkillManager);
            return CheckCore(caster.SkillManager);
        }
        #endregion



        protected bool CheckCore(ISkillManager manager)
        {
            if (null == Values || Values.Count == 0 || null == manager)
                return false;
            return Values.ContainsKey(manager.SkillMid);
        }
    }
}
