/********************************************************************************
 * 文件名：Goal
 * 创建人：
 * 创建时间：2009-11-17 9:32:15
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Base.Attributes;
using SkillEngine.Extern;

namespace Games.NB.Match.BLL.Model.Pitchs {

    /// <summary>
    /// Represents the goal.
    /// </summary>
    [Serializable, Singleton]
    public class Goal : IGoal {

        /// <summary>
        /// Represents the door's frames.
        /// </summary>
        public List<ShootTarget> DoorFrame {
            get { return _shootTargets[0]; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Goal"/> class.
        /// </summary>
        public Goal() {            
            this._shootTargets.Add(0, new List<ShootTarget>(4));
            this._shootTargets.Add(1, new List<ShootTarget>());
            this._shootTargets.Add(2, new List<ShootTarget>());
            this._shootTargets.Add(3, new List<ShootTarget>());
            this._shootTargets.Add(4, new List<ShootTarget>());

            for (var i = 0; i < 4; i++) {
                this._shootTargets[0].Add(new ShootTarget(i + 1, 0, true));
            }

            for (var x = 0; x <= 20; x++) {
                for (var y = 0; y <= 7; y++) {
                    if ((y > 1) && ((x >= 2 && x <= 3) || (x >= 17 && x <= 18))) {
                        this._shootTargets[1].Add(new ShootTarget(x, y));
                        continue;
                    }

                    if ((y > 1) && ((x >= 4 && x <= 6) || (x >= 14 && x <= 16))) {
                        this._shootTargets[2].Add(new ShootTarget(x, y));
                        continue;
                    }

                    if ((y > 1) && ((x >= 7 && x <= 13))) {
                        this._shootTargets[3].Add(new ShootTarget(x, y));
                        continue;
                    }

                    this._shootTargets[4].Add(new ShootTarget(x, y));                    
                }
            }
        }

        /// <summary>
        /// Gets a shoot target by index.
        /// </summary>
        /// <param name="index">Represents the index of the Goal.</param>
        /// <returns><see cref="ShootTarget"/></returns>
        public ShootTarget GetShootTargetByIndex(IRandom random,int index) {
            return this._shootTargets[index][random.RandomInt32(0, this._shootTargets[index].Count - 1)];
        }

        /// <summary>
        /// Gets a random door frame.
        /// </summary>
        /// <returns><see cref="ShootTarget"/></returns>
        public ShootTarget GetRandomDoorFrame(IRandom random) {
            return this._shootTargets[0][random.RandomInt32(0, this._shootTargets[0].Count - 1)];
        }

        #region encapusulation

        private readonly Dictionary<int, List<ShootTarget>> _shootTargets = new Dictionary<int, List<ShootTarget>>(7 * 20);

        #endregion
    }
}
