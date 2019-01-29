/********************************************************************************
 * 文件名：IServiceFacade
 * 创建人：
 * 创建时间：2009-12-1 11:10:45
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the interface of the match service.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.ServiceModel;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.BLL.Model;
using Games.NB.Match.Base.Model;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.BLL.Model.Creatures;

namespace Games.NB.Match.BLF
{

    /// <summary>
    /// Represents the interface of the match service.
    /// 表示了比赛服务的接口
    /// </summary>
    [ServiceContract(Namespace = "http://nb_match.com/")]
    public interface IMatchService
    {

        [OperationContract]
        byte[] CreateMatchBin(byte[] rawInput);

        [OperationContract]
        byte[] CreateMatchToBin(MatchInput input);

    }
}
