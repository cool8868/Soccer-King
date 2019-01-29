/********************************************************************************
 * 文件名：FormationEntity
 * 创建人：
 * 创建时间：2009-12-24 10:01:27
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
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Structs;

namespace Games.NB.Match.Frame.Model {

    [Serializable]
    public sealed class FormationEntity {
        public Position Position { get; set; }
        public Coordinate Default { get; set; }
        public Coordinate HalfDefault { get; set; }
    }
}
