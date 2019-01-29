using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.Extern.Enum;

namespace SkillEngine.SkillBase.Xtern
{
    public interface ISkillPlayer : ISkillOwner, ISkillStat, ISkillFoul
    {
        #region Refer
        EnumMatchSide SkillSide
        {
            get;
        }
        ISkillManager SkillManager
        {
            get;
        }
        /// <summary>
        /// 传球目标
        /// </summary>
        ISkillPlayer SkillPairingPlayer
        {
            get;
        }
        /// <summary>
        /// 传球来源
        /// </summary>
        ISkillPlayer SkillPairedPlayer
        {
            get;
        }
        /// <summary>
        /// 助攻球员
        /// </summary>
        ISkillPlayer SkillAssistPlayer
        {
            get;
        }
        /// <summary>
        /// 对位球员
        /// </summary>
        ISkillPlayer OppSkillPlayer
        {
            get;
        }
        #endregion

        #region MotionState
        /// <summary>
        /// 当前动作状态
        /// </summary>
        int DoingState
        {
            get;
        }
        /// <summary>
        /// 最近动作状态
        /// </summary>
        int DoneState
        {
            get;
        }
        /// <summary>
        /// 最近动作成功标记
        /// </summary>
        EnumDoneStateFlag DoneStateFlag
        {
            get;
        }
        #endregion

        #region Owned
        /// <summary>
        /// 真实球员Id
        /// </summary>
        int SkillPlayerId
        {
            get;
        }
        /// <summary>
        /// 输出球员Id
        /// </summary>
        byte SkillClientId
        {
            get;
        }
        /// <summary>
        /// 球员位置
        /// </summary>
        int SkillPosition
        {
            get;
        }
        /// <summary>
        /// 球员颜色等级
        /// </summary>
        int SkillColour
        {
            get;
        }
        /// <summary>
        /// 持球标记
        /// </summary>
        bool SkillHoldBall
        {
            get;
        }
        /// <summary>
        /// 下场标记
        /// </summary>
        bool Disable
        {
            get;
        }
        /// <summary>
        /// 下场状态
        /// </summary>
        int DisableState
        {
            get;
            set;
        }
        /// <summary>
        /// 正常标记
        /// </summary>
        bool SkillEnable
        {
            get;
        }
        bool SkillLock
        {
            get;
        }
        /// <summary>
        /// 锁定动作状态
        /// </summary>
        int SkillLockState
        {
            get;
        }
        /// <summary>
        /// 限定动作状态
        /// </summary>
        int SkillLimitState
        {
            get;
        }
        /// <summary>
        /// 强制动作状态
        /// </summary>
        bool ForceState(int forceState);
        #endregion

        #region Event
        event EventHandler<FoulEventArgs> FoulEvent;
        event EventHandler<BlurEventArgs> BlurEvent;
        ISkill FoulSrcSkill
        {
            get;
            set;
        }
        ISkill BlurSrcSkill
        {
            get;
            set;
        }
        void RaiseFoulEvent(FoulEventArgs args);
        void RaiseBlurEvent(BlurEventArgs args);
        #endregion

    }
}
