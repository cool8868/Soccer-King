/********************************************************************************
 * 文件名：IMatch
 * 创建人：
 * 创建时间：
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the contracts of a football match.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.Base.Model.TranOut;
using SkillEngine.SkillBase.Xtern;

namespace Games.NB.Match.Base.Interface
{
    /// <summary>
    /// Represents the contracts of a football match.
    /// 表示了比赛的接口
    /// </summary>
    public interface IMatch : IReport, ISkillMatch
    {
        #region Data
        /// <summary>
        /// Represents a <see cref="IPitch"/>.
        /// 表示了球场
        /// </summary>
        IPitch Pitch { get; }

        /// <summary>
        /// Represents a <see cref="IFootball"/>.
        /// 表示了一个足球
        /// </summary>        
        IFootball Football { get; }

        /// <summary>
        /// Represents the managers in this match.
        /// 表示了两队的经理
        /// </summary>
        IManager[] Managers { get; }

        /// <summary>
        /// Represents the Home side <see cref="IManager"/>.
        /// 表示了主队经理
        /// </summary>
        IManager HomeManager { get; }

        /// <summary>
        /// Represents the away side <see cref="IManager"/>.
        /// 表示了客队经理
        /// </summary>
        IManager AwayManager { get; }
        /// <summary>
        /// Represents the <see cref="MatchStatus"/>.
        /// 表示了比赛的当前状态
        /// </summary>
        MatchStatus Status { get; }
        /// <summary>
        /// 比赛输入
        /// </summary>
        MatchInput Input { get; }
        /// <summary>
        /// 比赛战报
        /// </summary>
        MatchReport Report { get; }
        /// <summary>
        /// Represents the home side score.
        /// 表示了主队比分
        /// </summary>
        int HomeScore { get; }
        /// <summary>
        /// Represents the away side score.
        /// 表示了客队比分
        /// </summary>
        int AwayScore { get; }
        #endregion

        #region Func
        /// <summary>
        /// 开场初始化
        /// </summary>
        void SessionInit();
        /// <summary>
        /// Initialize while in a new round.
        /// 回合初始化
        /// </summary>
        void RoundInit();
        /// <summary>
        /// 开球
        /// </summary>
        /// <param name="manager">开球的经理</param>
        void Openball(IManager manager);
        /// <summary>
        /// 守门员开球
        /// </summary>
        /// <param name="manager">开球的经理</param>
        void GKOpenball(IManager manager);
        /// <summary>
        /// 进球
        /// </summary>
        /// <param name="manager">进球的经理</param>
        void Goal(IManager manager);
        /// <summary>
        /// 射偏或者射飞
        /// </summary>
        /// <param name="manager">射门失败的经理</param>
        /// <param name="openFlag">重新开球标记</param>
        void MissGoal(IManager manager, bool openFlag);

        /// <summary>
        /// The incoming <see cref="IManager"/> foul.
        /// 传入的经理犯规
        /// </summary>
        void Foul(IPlayer player, bool openFlag);

        /// <summary>
        /// Represents the football out border.
        /// 出界
        /// </summary>
        /// <param name="manager">
        /// Represents the manager who's player is making the ball out border.
        /// 表示了让球出界的经理
        /// </param>
        void OutBorder(IManager manager);
        /// <summary>
        /// 球员破坏出界
        /// </summary>
        /// <param name="player"></param>
        bool PassOutside(IPlayer player);
        #endregion

    }
   
}
