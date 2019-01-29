using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public abstract class PlayerSeekerBase : IPlayerSeeker
    {
        #region Data
        public EnumSeekJoinType JoinType
        {
            get;
            set;
        }
        public List<IPlayerFilter> Filters
        {
            get;
            set;
        }
        #endregion

        #region IPlayerSeeker
        public List<ISkillPlayer> Seek(ISkill srcSkill, ISkillPlayer caster)
        {
            var srcManager = null != caster ? caster.SkillManager : srcSkill.Owner as ISkillManager;
            var players = InnerSeek(srcSkill, srcManager, caster);
            if (null == players || players.Count == 0)
                return null;
            if (null == Filters || Filters.Count == 0)
            {
                for (int i = players.Count - 1; i >= 0; --i)
                {
                    if (players[i].Disable)
                        players.RemoveAt(i);
                }
                return players;
            }
            var idxes = srcManager.SkillMatch.ArrayBuffer(100);
            for (int i = 0; i < players.Count; ++i)
            {
                idxes[i] = 0;
                if (players[i].Disable)
                {
                    idxes[i] = 1;
                    continue;
                }
                foreach (var filter in Filters)
                {
                    if (!filter.Check(srcManager, caster, players[i]))
                    {
                        idxes[i] = 1;
                        break;
                    }
                }
            }
            for (int i = players.Count - 1; i >= 0; --i)
            {
                if (idxes[i] > 0)
                    players.RemoveAt(i);
            }
            return players;
        }
        protected abstract List<ISkillPlayer> InnerSeek(ISkill srcSkill, ISkillManager srcManager, ISkillPlayer srcPlayer);
        #endregion
    }
}
