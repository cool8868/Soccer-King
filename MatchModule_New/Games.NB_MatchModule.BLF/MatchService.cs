/********************************************************************************
 * 文件名：ServiceFacade
 * 创建人：
 * 创建时间：2009-12-2 16:39:50
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the match serice's facade.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.Text;
using Games.NB.Match.AI;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.Base.Model.TranOut;
using Games.NB.Match.Common.Collections;
using Games.NB.Match.Log;
using Games.NB.Match.Common.Random;
using Games.NB.Match.BLL.Facade;

namespace Games.NB.Match.BLF
{

    /// <summary>
    /// Represents the match serice's facade.
    /// This class implemented the singlteton pattern, please use the Instance property to use.
    /// </summary>
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, AddressFilterMode = AddressFilterMode.Any, UseSynchronizationContext = false)]
    public sealed class MatchService : IMatchService
    {
        public byte[] CreateMatchBin(byte[] rawInput)
        {
            return MatchFacade.CreateMatchBin(rawInput);
        }
        public byte[] CreateMatchToBin(MatchInput input)
        {
            return MatchFacade.CreateMatchToBin(input);
        }

       
    }
}
