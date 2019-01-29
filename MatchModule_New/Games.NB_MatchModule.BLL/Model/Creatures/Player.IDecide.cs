/********************************************************************************
 * 文件名：PlayerDecide
 * 创建人：
 * 创建时间：2009-12-18 22:30:08
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：This partial class implemented the interface of the <see cref="IDecide"/>.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Drawing;
using Games.NB.Match.AI;
using Games.NB.Match.AI.States;
using Games.NB.Match.AI.States.Dribble;
using Games.NB.Match.AI.States.Defence;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Common.Random;
using Games.NB.Match.Log;
using Games.NB.Match.Base;
using SkillEngine.SkillBase.Enum.Football;

namespace Games.NB.Match.BLL.Model.Creatures
{
    /// <summary>
    /// Represents the player entity.
    /// This partial class implemented the interface of the <see cref="IDecide"/>.
    /// </summary>
    public sealed partial class Player
    {
        #region IDecide

        /// <summary>
        /// Player to action.
        /// including the pass action, shoot action, defence action and so on.
        /// </summary>
        public void Action()
        {
            if (this.Disable)
                return;
            if (this.SkillAction())
                return;
            this._status.State.Action(this);
        }

        /// <summary>
        /// Quick Decide.
        /// </summary>
        public void QuickDecide()
        {
            if (!this.SkillEnable)
                return;
            _status.PreState = _status.State;
            if (_status.ActionLast >= _status.State.TimeLast)
            {
                var preview = this._status.State;

                var loop = 0;
                if (!this.SkillDecide() || !this._status.State.Stopable)
                {
                    do
                    {
                        var state = this._status.State;
                        state.Enter(this);
                        this._status.State = state.QuickDecide(this, preview);

                        preview = state;

                        if (++loop > 100)
                        {
                            throw new ApplicationException(String.Format("Player decide infinite. Player:{0}, State:{1}", this, _status.State));
                        }
                    } while (!this._status.State.Stopable);
                }

                DecideEnd();
                _status.ActionLast = 0;
            }
            else
            {
                _status.ActionLast++;
            }
           
            // Skill Trigger
            if (_status.State is DefenceState)
            {
                var target = _status.DefenceStatus.DefenceTarget;
                if (null != target)
                {
                    if(_status.State is HeadingDuelState)
                    {
                        target.Status.ForceState(HeadingDuelState.Instance);
                    }
                    else if (target.Status.Holdball || target.Status.State == DefaultDribbleState.Instance)
                    {
                        target.Status.ForceState(BreakThroughState.Instance);
                        SkillEngine.SkillImpl.SkillFacade.TriggerPlayerSkills(target, 0, true);
                    }
                }
            }
            SkillEngine.SkillImpl.SkillFacade.TriggerPlayerSkills(this, 0);
        }
        #endregion
    }
}
