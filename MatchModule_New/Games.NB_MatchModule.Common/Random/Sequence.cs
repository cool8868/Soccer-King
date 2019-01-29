/*****************************************************************************
 * 文件名：Sequence
 * 
 * 创建人：
 * 
 * 创建时间：2009-11-12 14:29:45
 * 
 * 功能说明：
 *  
 * 历史修改记录：
 * 
 * ***************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Games.NB.Match.Common.Random {
    public static class Sequence {
        private static int _sequence = -1;
        private static object _syncRoot = new object();

        public static int GetSequence() {
            return Interlocked.Increment(ref _sequence);
        }
    }
}
