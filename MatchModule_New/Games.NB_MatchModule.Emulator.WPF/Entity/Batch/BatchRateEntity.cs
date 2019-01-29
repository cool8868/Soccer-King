using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NB.Match.Emulator.WPF.Entity.Batch
{
    public class BatchRateEntity
    {
        public void AddData(BatchRateEntity entity)
        {
            Times += entity.Times;
            SuccTimes += entity.SuccTimes;
            Rate2 += entity.Rate2;
        }

        public void CalAvg(int totalCount)
        {
            Times = Times / totalCount;
            SuccTimes = SuccTimes / totalCount;
            Rate2 = Rate2 / totalCount;
        }

        public int Times { get; set; }

        public int SuccTimes { get; set; }

        public int FailTimes { get { return Times - SuccTimes; } }

        /// <summary>
        /// 服务运算的概率
        /// </summary>
        public double Rate2 { get; set; }

        public double AvgRate2
        {
            get
            {
                if (Times == 0) return 0;
                return Rate2 / Times;
            }
        }

        public string RateStr { get { return Rate.ToString("f1"); } }

        public double Rate
        {
            get
            {
                if (Times == 0) return 0;
                return SuccTimes * 100 / Times;
            }
        }
    }
}
