/********************************************************************************
 * 文件名：PlayerFoul
 * 创建人：
 * 创建时间：2010-2-21 11:30:54
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using Games.NB.Match.Base;
using Games.NB.Match.Common.Random;

namespace Games.NB.Match.BLL.Model.Creatures
{
    /// <summary>
    /// 球员对象的犯规相关逻辑
    /// </summary>
    public sealed partial class Player
    {
        /// <summary>
        /// Foul
        /// 犯规
        /// </summary>
        /// <param name="level">
        /// foul's level.
        /// 0 - Normal
        /// 1 - Yellow Card
        /// 2 - Red Card
        /// </param>
        public void Foul(byte level)
        {
            if (level < Defines.FoulLevel.NORMAL || level > Defines.FoulLevel.RED_CARD)
                return;
            if (this.Disable)
                return;
            if (level == Defines.FoulLevel.YELLOW_CARD && this._status.FoulStatus.FoulLevel == Defines.FoulLevel.YELLOW_CARD)
                level = Defines.FoulLevel.RED_CARD;
            if (level == Defines.FoulLevel.NORMAL)
            {
                this.Match.Foul(this, false);
                return;
            }
            _status.FoulStatus.FoulLevel = level;
            this.Match.Foul(this, false);
        }

    }
}
