using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillCore
{
    public class PlayerLocator : ILocator<ISkillPlayer>
    {

        #region Data
        public List<IPlayerSeeker> Seekers
        {
            get;
            set;
        }
        #endregion

        #region Locate
        public List<ISkillPlayer> Locate(ISkill srcSkill, ISkillPlayer caster)
        {
            var targets = new List<ISkillPlayer>();
            if (!this.Locate(targets, srcSkill, caster))
                return null;
            return targets;
        }
        public bool Locate(List<ISkillPlayer> targets, ISkill srcSkill, ISkillPlayer caster)
        {
            if (null == Seekers || Seekers.Count == 0)
                return false;
            if (Seekers.Count == 1)
            {
                var players = Seekers[0].Seek(srcSkill, caster);
                if (null == players || players.Count == 0)
                    return false;
                targets.Clear();
                targets.AddRange(players);
                return true;
            }
            targets.Clear();
            List<ISkillPlayer> src = null;
            var dic = srcSkill.Context.DicBuffer(100);
            foreach (var seeker in Seekers)
            {
                switch (seeker.JoinType)
                {
                    case EnumSeekJoinType.Nest:
                        src = new List<ISkillPlayer>();
                        NestPlayers(targets, seeker, srcSkill, src, dic);
                        break;
                    case EnumSeekJoinType.And:
                        src = seeker.Seek(srcSkill, caster);
                        IntersectPlayers(targets, src, dic);
                        break;
                    case EnumSeekJoinType.Or:
                        src = seeker.Seek(srcSkill, caster);
                        UnionPlayers(targets, src, dic);
                        break;
                }
            }
            return true;
        }
        void NestPlayers(List<ISkillPlayer> dstPlayers, IPlayerSeeker seeker, ISkill srcSkill, List<ISkillPlayer> srcPlayers, Dictionary<int, int> dic)
        {
            if (null == dstPlayers || dstPlayers.Count == 0)
                return;
            if (null == srcPlayers)
                srcPlayers = new List<ISkillPlayer>();
            else
                srcPlayers.Clear();
            srcPlayers.AddRange(dstPlayers);
            dstPlayers.Clear();
            dic.Clear();
            List<ISkillPlayer> tmp = null;
            foreach (var player in srcPlayers)
            {
                tmp = seeker.Seek(srcSkill, player);
                if (null == tmp || tmp.Count == 0)
                    continue;
                foreach (var item in tmp)
                {
                    if (null == item)
                        continue;
                    if (!dic.ContainsKey(item.InnerId))
                    {
                        dic[item.InnerId] = 0;
                        dstPlayers.Add(item);
                    }
                }
            }
        }
        void UnionPlayers(List<ISkillPlayer> dstPlayers, List<ISkillPlayer> srcPlayers, Dictionary<int, int> dic)
        {
            if (null == srcPlayers || srcPlayers.Count == 0)
                return;
            dic.Clear();
            foreach (var item in dstPlayers)
            {
                dic[item.InnerId] = 0;
            }
            foreach (var item in srcPlayers)
            {
                if (!dic.ContainsKey(item.InnerId))
                    dstPlayers.Add(item);
            }
        }
        void IntersectPlayers(List<ISkillPlayer> dstPlayers, List<ISkillPlayer> srcPlayers, Dictionary<int, int> dic)
        {
            if (null == srcPlayers || srcPlayers.Count == 0)
            {
                dstPlayers.Clear();
                return;
            }
            dic.Clear();
            for (int i = dstPlayers.Count - 1; i >= 0; i--)
            {
                dic[dstPlayers[i].InnerId] = i;
            }
            foreach (var item in srcPlayers)
            {
                if (dic.ContainsKey(item.InnerId))
                    dic[item.InnerId] = -1;
            }
            foreach (var kvp in dic)
            {
                if (kvp.Value >= 0)
                    dstPlayers.RemoveAt(kvp.Value);
            }
        }
        #endregion

    }
}
