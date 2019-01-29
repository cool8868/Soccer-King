using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NB.Match.Base.Model.TranOut;

namespace Games.NB.Match.Base.Interface
{
    public interface IMatchState
    {
        /// <summary>
        /// 保存当前状态
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        BallMoveReport SaveRpt(IMatch match);
    }
}
