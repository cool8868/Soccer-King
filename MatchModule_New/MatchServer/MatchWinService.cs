using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using Games.NB.Match.BLF;
using log4net;

namespace Games.NB_Match.MatchServer
{
    partial class MatchWinService : ServiceBase
    {
        List<ServiceHost> hostList = new List<ServiceHost>();
        log4net.ILog logger = LogManager.GetLogger(typeof(MatchWinService));
        public MatchWinService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Start the match service.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            StartService(args);
        }

        /// <summary>
        /// Stop the match service.
        /// </summary>
        protected override void OnStop()
        {
            StopService();
        }

        #region EventHandler
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                logger.ErrorFormat("terminate:{0}; error:{1}", e.IsTerminating, ex.ToString());
            }
            else
            {
                logger.ErrorFormat("terminate:{0}; ", e.IsTerminating);
            }
        }
        private void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            StopService();
        }
        private void ServiceHost_Closed(object sender, EventArgs e)
        {
            ServiceHost host = sender as ServiceHost;
            Debug.Assert(host != null, "host is null");
            logger.InfoFormat("host {0} closed", host.Description);
        }
        private void ServiceHost_Faulted(object sender, EventArgs e)
        {
            ServiceHost host = sender as ServiceHost;
            Debug.Assert(host != null, "host is null");
            logger.InfoFormat("host {0} Faulted", host.Description);
        }
        #endregion

        /// <summary>
        /// 开始服务
        /// </summary>
        /// <param name="args"></param>
        public void StartService(string[] args)
        {
            try
            {
                logger.Info("MatchService...Starting");
                System.Threading.ThreadPool.SetMinThreads(Environment.ProcessorCount * 2, Environment.ProcessorCount * 32);
                AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                AppDomain.CurrentDomain.ProcessExit -= new EventHandler(CurrentDomain_ProcessExit);
                AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
                hostList.Clear();
                hostList.Add(new ServiceHost(typeof(Games.NB.Match.BLF.MatchService)));
                foreach (var item in hostList)
                {
                    item.Faulted -= ServiceHost_Faulted;
                    item.Faulted += ServiceHost_Faulted;
                    item.Closed -= ServiceHost_Closed;
                    item.Closed += ServiceHost_Closed;
                    item.Open();
                    logger.Info(item.Status());
                }
                Games.NB.Match.BLL.Facade.MatchFacade.Boot();
                logger.Info("MatchService...Started");
            }
            catch (Exception ex)
            {
                logger.Info("MatchService...Start Failed", ex);
            }
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        public void StopService()
        {
            try
            {
                logger.Info("MatchService...Stoping");
                foreach (var item in hostList)
                {
                    if (null != item)
                        item.Close();
                }
                logger.Info("MatchService...Stoped");
            }
            catch (Exception ex)
            {
                logger.Info("MatchService...Stop Failed", ex);
            }
        }

    }
}
