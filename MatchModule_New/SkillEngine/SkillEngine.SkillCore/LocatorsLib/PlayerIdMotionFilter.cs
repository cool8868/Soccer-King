using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern.Enum;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillBase.Enum.NBA;

namespace SkillEngine.SkillCore
{
    public class PlayerIdMotionFilter : PlayerMotionFilter
    {
        public PlayerIdMotionFilter(int[] values, EnumDoneStateFlag stateType)
            : base(values, stateType)
        { }

        public int[] Ids
        {
            get;
            set;
        }

        protected override bool CheckCore(ISkillPlayer dstPlayer)
        {
            if (null != Ids && Ids.Length > 0)
            {
                for (int i = 0; i < Ids.Length; i++)
                {
                    if (Ids[i] == dstPlayer.SkillPlayerId)
                        break;
                    if (i == Ids.Length - 1)
                        return false;
                }
            }
            return base.CheckCore(dstPlayer);
        }
    }
}
