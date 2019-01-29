using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Games.NB.Match.Log;

namespace Games.NB.Match.Base.Structs
{
    public struct ReportAsset
    {
        static ReportAsset()
        {
            try
            {
                int val = 0;
                string str = ConfigurationManager.AppSettings["RPTVerNo"] ?? string.Empty;
                if (int.TryParse(str, out val))
                    RPTVerNo = val;
                str = ConfigurationManager.AppSettings["RPTZipNo"] ?? string.Empty;
                if (int.TryParse(str, out val))
                    RPTZipNo = val;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }
        }

        /// <summary>
        /// 输出压缩号
        /// </summary>
        public static readonly int RPTZipNo = 1;
        /// <summary>
        /// 输出版本号
        /// </summary>
        public static readonly int RPTVerNo = 102;
        /// <summary>
        /// Deflat压缩
        /// </summary>
        public const int RPTZipNo4Deflate = 1;
        /// <summary>
        /// 短技能模型版本号
        /// </summary>
        public const int RPTVerNo4ShortModel = 1;

        public static bool IsShortModel(int verNo)
        {
            return verNo == ReportAsset.RPTVerNo4ShortModel || verNo > 100; 
        }

        public struct PlayerStateAsset
        {
            public const int CLASSIdDefault = 1;
            public const int CLASSIdDive = 2;
            public const int CLASSIdShoot = 3;

            public const int CLASSIdGkDefaultV2 = 2;
            public const int CLASSIdDiveV2 = 3;
            public const int CLASSIdShootV2 = 4;
            public const int CLASSIdStealV2 = 5;
        }
        public struct MatchStateAsset
        {
            public const int CLASSIdDefault = 1;
            public const int CLASSIDAir = 2;
            public const int CLASSIdGoal = 3;
        }

      
    }
}
