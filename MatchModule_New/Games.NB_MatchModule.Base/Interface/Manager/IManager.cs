/********************************************************************************
 * 文件名：IManager
 * 创建人：
 * 创建时间：2009-11-30 17:22:27
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *  2009-12-17 10:20:02 1.1 Reflect the abstract class to interface.
 *********************************************************************************/
using System;
using System.Collections.Generic;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Interface.Manager;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.Base.Model.TranOut;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.SkillBase.Xtern;


namespace Games.NB.Match.Base.Interface
{

    /// <summary>
    /// Represents the interface of the manager.(In the Crazy Soccer II, a manager means a real player)
    /// 表示了一个经理(经理表示了玩家)
    /// </summary>
    public interface IManager : IReport,ISkillManager
    {
        #region Data
        /// <summary>
        /// Represents the the manager's side in the match,
        /// this value can only be the home and away.
        /// 表示了经理是主队或客队
        /// </summary>
        Side Side { get; }
        /// <summary>
        /// Represents the reference of the <see cref="IMatch"/>.
        /// 表示了对正常比赛的引用
        /// </summary>
        IMatch Match { get; }
        /// <summary>
        /// Represents the reference of the opponent.
        /// 表示了对手经理
        /// </summary>
        IManager Opponent { get; }
        /// <summary>
        /// Represents the manager's status.
        /// 表示了经理的当前状态
        /// </summary>
        IManagerStatus Status { get; }

        ManagerInput Input { get; }

        ManagerReport Report { get; }

        /// <summary>
        /// Represents the list of the players.
        /// 表示了球员的集合
        /// </summary>
        List<IPlayer> Players { get; }
        /// <summary>
        /// Get the player by index.
        /// 通过index获取球员
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IPlayer this[int index] { get; }
        bool IsAttackSide { get; }

        int GoalScore { get; set; }
        #endregion

        #region RoundInit
        void ChangeSide();
        void SectionInit(int sectionNo);
        void MinuteInit();
        void RoundInit();
        #endregion

        #region Func
        /// <summary>
        /// Clears the disable debuff.
        /// 清除所有球员的所有异常状态
        /// </summary>
        void ClearDisable();
        /// <summary>
        /// 设置所有球员的完成状态
        /// </summary>
        void SetDoneState();
        /// <summary>
        /// 设置球员状态
        /// </summary>
        void SetState(IState state);
        /// <summary>
        /// 球员复位
        /// </summary>
        void Reset();
        /// <summary>
        /// Get a list of players by <see cref="Position"/>.
        /// 获取球员通过场上位置
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        List<IPlayer> GetPlayersByPosition(Position position);
        #endregion

        #region Buff
        IBuff CreatePropBuff(int last, int point, int percent, params int[] buffIds);
        IBuff CreateBlurBuff(EnumBlurType blurType, EnumBlurBuffCode blurCode, int last);
        IBoostEffect CreateBoostBuff(int boostType, int last, int point, int percent, params int[] buffIds);
        #endregion

    }
}
