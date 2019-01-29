using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.NB.Match.BLL.Model
{
    public partial class MatchEntity
    {
        public void SaveRpt()
        {
            int round = this.Status.Round;
            if (round > this.Status.TotalRound)
                return;
            HomeManager.SaveRpt();
            AwayManager.SaveRpt();
            //统计数据
            if (_statisticsCallback != null)
            {
                _statisticsCallback();
            }
           
            var list = this._report.BallResults;
            int dif = list.Count == 0 ? 2 : round - list[list.Count - 1].AsRound;
            if (dif <= 0)
                return;
            var obj = _status.MatchState.SaveRpt(this);
            if (null != obj)
            {
                obj.Round = (round == 1 || dif > 1) ? round : 0;
                list.Add(obj);
            }
        }
        public void SaveRptEnd()
        {
            HomeManager.SaveRptEnd();
            AwayManager.SaveRptEnd();
            _report.HomeScore = HomeScore;
            _report.AwayScore = AwayScore;
            _report.HomeManager = HomeManager.Report;
            _report.AwayManager = AwayManager.Report;
        }
    }
}
