/********************************************************************************
 * 文件名：IPlayerStatus
 * 创建人：
 * 创建时间：4/24/2010 5:26:24 PM
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base.Interface.Player.Status;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Enum;
using SkillEngine.Extern.Enum;

namespace Games.NB.Match.Base.Interface
{
    /// <summary>
    /// Represents the contracts of the player's status.
    /// 表示了球员的当前状态
    /// </summary>    
    public interface IPlayerStatus : INotifyDecide
    {
        /// <summary>
        /// Represents whether the player is enable.
        /// 表示了球员是否在场上
        /// </summary>
        bool Enable { get; }

        /// <summary>
        /// Represents the player has hold ball?
        /// (Only the player is has ball and ball distance less than 2)
        /// 表示了球员是否拥有球，只有球员持球并离球小于2时为真
        /// </summary>
        bool Holdball { get; }

        /// <summary>
        /// Represents the play has ball?
        /// 表示了球员是否被标记为有球
        /// </summary>
        bool Hasball { get; set; }
        /// <summary>
        /// Represents the player's current coordinate.
        /// 表示了球员的当前坐标
        /// </summary>
        Coordinate Current { get; set; }

        /// <summary>
        /// Represents the player's default coordiante.
        /// 表示了球员的默认坐标
        /// </summary>
        Coordinate Default { get; set; }

        /// <summary>
        /// 己方半场的默认坐标
        /// </summary>
        Coordinate HalfDefault { get; set; }

        /// <summary>
        /// Represents the player's destination coordinate.
        /// 表示了球员的目标坐标
        /// </summary>
        Coordinate Destination { get; set; }
        /// <summary>
        /// Represents the player's current angle.
        /// 表示了球员的朝向角度
        /// <remarks>
        /// In this system, towards right means 0° and towards left means 180°        
        /// 朝向正右方为0°，朝向正左方为180°
        /// </remarks>
        /// </summary>
        int Angle { get; set; }

        /// <summary>
        /// Represents the player's current speed.
        /// 表示了球员的当前速度
        /// </summary>
        double Speed { get; set; }

        /// <summary>
        /// Represents the player's current max speed.
        /// 表示了球员的最高速度
        /// </summary>
        double MaxSpeed { get; set; }
        /// <summary>
        /// Represents the player's accleration.
        /// 表示了球员的加速度
        /// </summary>
        double Acceleration { get; set; }
        byte ClientState { get; }
        /// <summary>
        /// Represents current <see cref="IState"/>.
        /// 表示了球员的前一状态
        /// </summary>
        IState PreState { get; set; }
        /// <summary>
        /// Represents current <see cref="IState"/>.
        /// 表示了球员的当前状态
        /// </summary>
        IState State { get; set; }
        /// <summary>
        /// 球员的完成状态
        /// </summary>
        IState DoneState { get; }
        /// <summary>
        /// 完成标记
        /// </summary>
        EnumDoneStateFlag DoneStateFlag { get; }
        /// <summary>
        /// 球员的细分动作
        /// </summary>
        ISubState SubState { get; set; }
        /// <summary>
        /// 强制状态
        /// </summary>
        /// <param name="state"></param>
        void ForceState(IState state);
        /// <summary>
        /// 设置完成状态
        /// </summary>
        /// <param name="doneState"></param>
        /// <param name="doneStateFlag"></param>
        void SetDoneState(IState doneState, EnumDoneStateFlag doneStateFlag);
        /// <summary>
        /// Represents current round's last time.
        /// 表示了行动的持续时间
        /// </summary>
        int ActionLast { get; set; }
        /// <summary>
        /// Represents the player's current <see cref="Zone"/> in the pitch.
        /// 表示了球员当前的区域
        /// </summary>
        Zone Zone { get; }
        /// <summary>
        /// Represents the player is in the attacking side.
        /// 表示了球员是否是进攻方
        /// </summary>
        bool IsAttackSide { get; }
        /// <summary>
        /// 是否边路球员
        /// </summary>
        bool IsWinger { get; }
        /// <summary>
        /// Represents whether the player is backward moving.
        /// 表示了球员是否向后移动
        /// </summary>
        bool IsBackward { get; set; }
        /// <summary>
        /// Represents whether the goal kepper is stand up.
        /// 表示了守门员是否是站立状态
        /// </summary>
        bool IsStantUp { get; set; }
        /// <summary>
        /// 是否强制传中
        /// </summary>
        bool IsForceCross { get; }
        /// <summary>
        /// Represents whether the player need to decide.        
        /// 表示了球员是否需要重新思考
        /// </summary>
        bool NeedRedecide { get; }
      
      
        #region Range
        /// <summary>
        /// Represents the player's width.
        /// 表示了球员的宽度
        /// </summary>
        byte Width { get; }
        /// <summary>
        /// Represents a distance between player's edge and the ball.
        /// 表示了球员离球的距离
        /// </summary>
        int BallDistance { get; }
        double BallDistancePow { get; }
        bool BallDistanceZero { get; }
        /// <summary>
        /// Represents a distance between current coordinate and the destination coordinate.
        /// 表示了球员离目标点的距离
        /// </summary>
        double DestinationDistance { get; }
        double DestinationDistancePow { get; }
        bool DestinationDistanceZero { get; }
        /// <summary>
        /// Represents the player's move region.
        /// 表示了球员的可移动区域
        /// </summary>
        Region MoveRegion { get; set; }
        /// <summary>
        /// 活跃区域
        /// </summary>
        Region ActiveRegion { get; }
        #endregion

        #region Func
        /// <summary>
        /// 换边
        /// </summary>
        void ChangeSide();
        /// <summary>
        /// Reset player to default position.
        /// 重置物体的当前状态
        /// </summary>
        void Reset();
        /// <summary>
        /// 回合初始化
        /// </summary>
        void RoundInit();
        #endregion

        #region Sub Status

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //
        // The Status in each State
        //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        IPlayerStatStatus StatStatus { get; }
        /// <summary>
        /// Represents the shoot state's status.
        /// 表示了球员射门时的状态
        /// </summary>
        IShootStatus ShootStatus { get; }

        /// <summary>
        /// Represents the pass state's status.
        /// 表示了球员的传球状态
        /// </summary>
        IPassStatus PassStatus { get; }

        /// <summary>
        /// Represents the defence state's status.
        /// 表示了球员的防守状态
        /// </summary>
        IDefenceStatus DefenceStatus { get; }

        /// <summary>
        /// Represents the player's foul informations.
        /// 表示了球员的各种犯规状态
        /// </summary>
        IFoulStatus FoulStatus { get; }

        /// <summary>
        /// Represents the goal keeper's dive action informations
        /// 表示了守门员扑救时的各种状态
        /// </summary>
        IDiveStatus DiveStatus { get; }

        /// <summary>
        /// Represents the player's model status.
        /// 表示了球员的模型状态
        /// </summary>
        IModelStatus ModelStatus { get; }

        #endregion
       
    }
}
