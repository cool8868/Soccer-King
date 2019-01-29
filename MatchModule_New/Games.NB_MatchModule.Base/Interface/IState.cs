/********************************************************************************
 * 文件名：IState
 * 创建人：
 * 创建时间：2009-11-18 10:19:53
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the interface of the state.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using Games.NB.Match.Base.BaseClass;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Model.TranOut;

namespace Games.NB.Match.Base.Interface
{

    /// <summary>
    /// Represents the interface of the state.
    /// 表示了状态的接口
    /// </summary>
    public interface IState : IAction
    {

        /// <summary>
        /// Represents the state's id.
        /// 内部编号
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Represents a state's client id.
        /// 客户端编号
        /// </summary>
        byte ClientId { get; set; }

        /// <summary>
        /// Represents whether the current state is a stopable state.
        /// 表示了该状态是否可以成为最终状态
        /// </summary>
        bool Stopable { get; }

        /// <summary>
        /// Represents the action lasting time.
        /// 表示了该动作的持续时间
        /// </summary>
        int TimeLast { get; }

        /// <summary>
        /// Initialize current <see cref="IState"/>.
        /// 初始化状态
        /// </summary>
        void Initialize();

        /// <summary>
        /// Decide a next state.
        /// 思考下一个状态
        /// </summary>
        /// <param name="player">Represents the <see cref="IPlayer"/>.</param>
        /// <param name="preview">Represents the preview <see cref="IState"/>.</param>
        /// <returns></returns>
        IState Decide(IPlayer player, IState preview);

        /// <summary>
        /// Decides the nex <see cref="IState"/>
        /// 高速思考下一个状态
        /// </summary>
        /// <param name="player">Represents the current <see cref="IPlayer"/>.</param>
        /// <param name="preview">Represents the preview <see cref="IState"/></param>
        /// <returns></returns>
        IState QuickDecide(IPlayer player, IState preview);

        /// <summary>
        /// Invoked while enter current state.
        /// 进入一个状态
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        void Enter(IPlayer player);

        /// <summary>
        /// Invoked while exit current state.
        /// 退出一个状态
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/></param>
        void Exit(IPlayer player);

        /// <summary>
        /// 保存当前状态
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        PlayerMoveReport SaveRpt(IPlayer player);
     
    }
}