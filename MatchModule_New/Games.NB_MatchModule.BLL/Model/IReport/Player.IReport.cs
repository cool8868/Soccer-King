using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NB.Match.BLL.Model.Creatures
{
    public partial class Player
    {
        public void SaveRpt()
        {
            int round = this._match.Status.Round;
            var list = this._report.MoveResults;
            int cnt = list.Count - 1;
            int dif = cnt < 0 ? 2 : round - list[cnt].AsRound;
            if (dif <= 0)
                return;
            if (this.Disable && cnt >= 0 && list[cnt].StateData.Disable)
                return;
            var obj = _status.State.SaveRpt(this);
            if (null != obj)
            {
                obj.Round = (round == 1 || dif > 1) ? round : 0;
                obj.StateData.ModelId = Convert.ToUInt16(this.SkillCore.ModelId);
                list.Add(obj);
            }
        }
        public void SaveRptEnd()
        {
            int cntSkill = 0;
            if (null != this.SkillCore)
                cntSkill = this.SkillCore.ClipList.Count;
            if (cntSkill == 0)
                return;
            _report.SkillResults = new List<Base.Model.TranOut.SkillReport>(cntSkill);
            foreach (var clip in this.SkillCore.ClipList)
            {
                _report.SkillResults.Add(new Base.Model.TranOut.SkillReport()
                {
                    Round = clip.Round,
                    SkillId = clip.ClipId,
                    SkillTargets = clip.Targets.Keys.ToArray(),
                });
            }
        }
    }
}
