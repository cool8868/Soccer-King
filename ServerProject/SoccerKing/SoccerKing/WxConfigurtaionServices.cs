using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerKing
{
	public class WxConfigurtaionServices
	{
		private readonly IOptions<WxConfig> _wxConfig;
		public WxConfigurtaionServices(IOptions<WxConfig> wxConfig)
		{
			_wxConfig = wxConfig;
		}

		public WxConfig WxConfiguration
		{
			get
			{
				return _wxConfig.Value;
			}
		}
	}
}
