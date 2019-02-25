using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;



namespace SoccerKing.Common
{
	public class LogHelper
	{
		private static LogHelper _logHelper = null;
		private static readonly object lockObj = new object();
		static Logger logger = LogManager.GetCurrentClassLogger();

		private LogHelper()
		{			
		}
		public static LogHelper Instance
		{
			get
			{
				if (_logHelper == null)
				{
					lock (lockObj)
					{
						if (_logHelper == null)
							_logHelper = new LogHelper();
					}
				}
				return _logHelper;
			}
		}

		public void Info(string msg)
		{
			logger.Info(msg);
		}

		public void Error(string msg)
		{
			logger.Error(msg);
		}

	}
}
