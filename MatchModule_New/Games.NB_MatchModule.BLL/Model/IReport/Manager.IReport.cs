using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NB.Match.BLL.Model.Creatures
{
    public partial class Manager
    {
        public void SaveRpt()
        {
            foreach (var p in this.Players)
            {
                p.SaveRpt();
            }
        }
        public void SaveRptEnd()
        {
            _report.Players.Clear();
            foreach (var p in this.Players)
            {
                p.SaveRptEnd();
                _report.Players.Add(p.Report);
            }
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
