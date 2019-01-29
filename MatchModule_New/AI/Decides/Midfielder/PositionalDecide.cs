/********************************************************************************
 * 文件名：PositionalDecide
 * 创建人：
 * 创建时间：2009-12-21 15:06:40
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
using Games.NB.Match.Base;
using Games.NB.Match.Base.Attributes;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Structs;
using Games.NB.Match.Common;
using Games.NB.Match.Common.Random;

namespace Games.NB.Match.AI.Decides.Midfielder
{
    /// <summary>
    /// 中场寻位接口
    /// </summary>
    [Singleton]
    public sealed class PositionalDecide : Decides.PositionalDecide
    {
    }
}
