using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern.Enum;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public class PlayerIdSeeker : PlayerSeekerBase
    {
        #region .ctor
        public PlayerIdSeeker()
        { }
        public PlayerIdSeeker(byte side, int[] ids, int[] positions,int[] colours)
        {
            this.Side = side;
            this.Ids = ids;
            this.Postions = positions;
            this.Colours = colours;
        }
        #endregion

        #region Data
        public byte Side
        {
            get;
            set;
        }
        public int[] Ids
        {
            get;
            set;
        }
        public int[] Postions
        {
            get;
            set;
        }
        public int[] Colours
        {
            get;
            set;
        }
        #endregion

        protected override List<ISkillPlayer> InnerSeek(ISkill srcSkill, ISkillManager srcManager, ISkillPlayer srcPlayer)
        {
            var rst = new List<ISkillPlayer>();
            var orig = Side > 0 ? srcManager.OppSkillManager.SkillPlayerList : srcManager.SkillPlayerList;
            bool hitFlag = true;
            foreach (var item in orig)
            {
                //if (item.Disabled)
                //    continue;
                hitFlag = true;
                if (null != Ids && Ids.Length > 0)
                {
                    for (int i = 0; i < Ids.Length; i++)
                    {
                        if (Ids[i] == item.SkillPlayerId)
                            break;
                        if (i == Ids.Length - 1)
                            hitFlag = false;
                    }
                    if (!hitFlag)
                        continue;
                }
                if (null != Postions && Postions.Length > 0)
                {
                    for (int i = 0; i < Postions.Length; i++)
                    {
                        if (Postions[i] == item.SkillPosition)
                            break;
                        if (i == Postions.Length - 1)
                            hitFlag = false;
                    }
                    if (!hitFlag)
                        continue;
                }
                if (null != Colours && Colours.Length > 0)
                {
                    for (int i = 0; i < Colours.Length; i++)
                    {
                        if (Colours[i] == item.SkillColour)
                            break;
                        if (i == Colours.Length - 1)
                            hitFlag = false;
                    }
                }
                if (hitFlag)
                    rst.Add(item);
            }
            return rst;
        }
    }
}
