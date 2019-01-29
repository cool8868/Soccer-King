/*****************************************************************************
 * 文件名：Creature
 * 
 * 创建人：Jiawei Chegn
 * 
 * 创建时间：2009-11-5 13:28:02
 * 
 * 功能说明：Represents the baseclass of the manager and player.
 *  
 * 历史修改记录：
 * 
 * ***************************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Base.Interface;

namespace Games.NB.Match.Base.BaseClass
{

    /// <summary>
    /// Represents the baseclass of the manager and player.
    /// </summary>
    [Serializable]
    public abstract class Creature : BusinessBase
    {
       
    }
  
}
