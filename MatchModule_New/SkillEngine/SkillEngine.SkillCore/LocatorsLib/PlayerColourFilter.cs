﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public class PlayerColourFilter : ValueSetFilterBase<int>, IPlayerFilter, ITrigger
    {
        public PlayerColourFilter(int[] values)
            : base(values)
        {
        }

        #region IPlayerFilter
        public bool Check(ISkillManager srcManager, ISkillPlayer srcPlayer, ISkillPlayer dstPlayer)
        {
            return CheckValue(dstPlayer.SkillColour);
        }
        protected override bool InnerEquals(int x, int y)
        {
            return x == y;
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
                return false;
            return CheckValue(caster.SkillColour);
        }
        #endregion
    }
}
