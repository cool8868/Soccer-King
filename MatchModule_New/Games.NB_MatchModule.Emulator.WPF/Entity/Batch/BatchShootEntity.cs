using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Model.TranOut;

namespace Games.NB.Match.Emulator.WPF.Entity.Batch
{
    public class BatchShootEntity
    {
        public void AddData(BatchShootEntity entity)
        {
            ShootTimes += entity.ShootTimes;
            GoalTimes += entity.GoalTimes;
            DoorFrame += entity.DoorFrame;
            ShootRate += entity.ShootRate;
        }

        public void CalAvg(int totalCount)
        {
            ShootTimes = ShootTimes / totalCount;
            DoorFrame = DoorFrame / totalCount;
            GoalTimes = GoalTimes / totalCount;
            ShootRate = ShootRate / totalCount;
        }

        /// <summary>
        /// 射门次数
        /// </summary>
        public int ShootTimes { get; set; }

        /// <summary>
        /// 进球次数
        /// </summary>
        public int GoalTimes { get; set; }

        /// <summary>
        /// 射中门框次数
        /// </summary>
        public int DoorFrame { get; set; }

        /// <summary>
        /// 射门概率
        /// </summary>
        public double ShootRate { get; set; }

        /// <summary>
        /// 计算平均射中概率
        /// </summary>
        public double AvgShootRate
        {
            get
            {
                if (ShootTimes == 0) return 0;
                return ShootRate / ShootTimes;
            }
        }
    }
}
