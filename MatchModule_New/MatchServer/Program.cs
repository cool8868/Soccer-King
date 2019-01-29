/********************************************************************************
 * 文件名：Program
 * 创建人：
 * 创建时间：2009-12-2 17:12:17
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the service's entry.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Games.NB_Match.MatchServer {

    /// <summary>
    /// Represents the service's entry.
    /// </summary>
    static class Program {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (args != null && args.Contains("-debug"))
            {
                MatchWinService service = new MatchWinService();
                service.StartService(args);
                Console.WriteLine("press any key to exit");
                Console.ReadLine();
                service.StopService();
                service.Dispose();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                    new MatchWinService() 
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
