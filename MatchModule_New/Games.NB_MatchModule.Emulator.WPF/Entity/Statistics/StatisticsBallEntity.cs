using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model.TranOut;

namespace Games.NB.Match.Emulator.WPF.Entity.Statistics
{
    public class StatisticsBallEntity
    {
        public StatisticsBallEntity()
        {

        }

        public StatisticsBallEntity(IMatch match, int index, BallMoveReport ballMoveReport)
        {
            Round = ballMoveReport.AsRound;
            var state = ballMoveReport.ClassId;
            if (ballMoveReport.ClassId == 2)
                BallState = "长传";
            else if (ballMoveReport.ClassId == 3)
                BallState = "射进";
            else
            {
                BallState = GetStateStr(ballMoveReport.StateData.State);
            }
            if (ballMoveReport.ClassId > 1)
            {
                for (int i = index; i < match.Report.BallResults.Count; i++)
                {
                    if (match.Report.BallResults[i].ClassId == state)
                        EndRound = match.Report.BallResults[i].AsRound;
                    else
                    {
                        return;
                    }
                }
            }
        }

        public string GetStateStr(byte state)
        {
            string content = "Unk";
            switch (state)
            {
                case 0:
                    content = "正常";
                    break;
                case 1:
                    content = "上下半场开球";
                    break;
                case 2:
                    content = "中场开球";
                    break;
                case 3:
                    content = "开球门球";
                    break;
                case 4:
                    content = "任意球";
                    break;
                case 5:
                    content = "射门";
                    break;
                case 6:
                    content = "犯规";
                    break;
                case 7:
                    content = "出界";
                    break;
                case 11:
                    content = "边线手抛球";
                    break;
                case 12:
                    content = "角球";
                    break;
                case 13:
                    content = "球门球";
                    break;
                case 14:
                    content = "射飞";
                    break;
                case 15:
                    content = "射进";
                    break;
            }
            return content;
        }

        public int Round { get; set; }

        public string BallState { get; set; }

        public int EndRound { get; set; }


    }
}
