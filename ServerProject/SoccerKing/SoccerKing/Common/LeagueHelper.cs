using SoccerKing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerKing.Common
{
	public class LeagueHelper
	{
		private static LeagueHelper _leaguehelper = null;
		private static readonly object lockHelper = new object();
		private static soccerkingContext _context;
		private LeagueHelper()
		{
			_context = new soccerkingContext();			
		}
		private static LeagueHelper Instance()
		{
			if (_leaguehelper == null)
			{
				lock (lockHelper)
				{
					if(_leaguehelper == null)
						_leaguehelper = new LeagueHelper();
				}
			}
			return _leaguehelper;
		}
	}
}
