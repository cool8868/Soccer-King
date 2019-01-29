/********************************************************************************
 * 文件名：IPlayer
 * 创建人：
 * 创建时间：2009-11-30 22:42:49
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the contracts of the Player class.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Interface.Player;
using Games.NB.Match.Base.Interface.Player.Status;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.Base.Model.TranOut;
using SkillEngine.SkillBase.Xtern;

namespace Games.NB.Match.Base.Interface
{

    /// <summary>
    /// Represents the contracts of a football player.
    /// 表示了一个球员的接口
    /// </summary>    
    public interface IPlayer :
        IMoveable, IDecide, INotifyDecide,
        IShortPass, ILongPass, IStopball, IRotate, IDribble,
        IShoot, IFoul, IAddBuff,
        IModel, IDefence, IDiveBall,ISkillState,IReport,ISkillPlayer
    {

        #region Data
        Side Side { get; }
        Guid Tid { get; }
        byte ClientId { get; }
        IManager Manager { get; }
        IMatch Match { get; }
        PropertyCore PropCore { get; }
        IPlayerStatus Status { get; }
        PlayerInput Input { get; }
        PlayerReport Report { get; }
        List<IPlayer> PassTargets { get; }
        #endregion

        #region RoundInit
        void Init();
        void SectionInit(int sectionNo);
        void MinuteInit();
        void RoundInit();
        #endregion

        #region SetTarget
        Zone GetTargetZone(Coordinate target);
        void SetTarget(Coordinate target);
        void SetTarget(double x, double y);
        void SetTargetBall(bool isCurrent);
        void SetHomeSideCoordinate(Coordinate target);
        bool IsCoordinateInPlayer(Coordinate coordinate);
        #endregion


    }
    
}
