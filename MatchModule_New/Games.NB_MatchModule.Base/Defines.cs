/********************************************************************************
 * 文件名：Defines
 * 创建人：
 * 创建时间：2009-11-4 17:07:27
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：Represents the const values in this system.
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Games.NB.Match.Base
{
    /// <summary>
    /// Represents the const values in this system.
    /// 表示了整个系统的各个常量
    /// </summary>
    public struct Defines
    {

        /// <summary>
        /// Represents the system's configs.
        /// 表示了系统的配置设置
        /// </summary>
        public struct System
        {
            
        }

        /// <summary>
        /// Represents the const value in the court.
        /// Unit: 0.5m
        /// 表示了球场的配置
        /// </summary>
        public struct Pitch
        {
            /// <summary>
            /// Represents the pitch's width.
            /// 表示了球场的宽
            /// </summary>
            public const int MAX_WIDTH = 210;

            /// <summary>
            /// Represents the pitch's height.
            /// 表示了球场的高
            /// </summary>
            public const int MAX_HEIGHT = 136;

            /// <summary>
            /// Represents the pitch's width.
            /// 表示了球场一半的宽
            /// </summary>
            public const int MID_WIDTH = 105;

            /// <summary>
            /// Represents the pitch's height.
            /// 表示了球场一半的高
            /// </summary>
            public const int MID_HEIGHT = 68;

            /// <summary>
            /// Represents the home side's penalty area.
            /// 表示了主队的禁区
            /// </summary>
            public const string HOME_PENALTY_AREA = "0,28,33,108";

            /// <summary>
            /// Represents the away side's penalty area.
            /// 表示了客队的禁区
            /// </summary>
            public const string AWAY_PENALTY_AREA = "177,28,210,108";

            /// <summary>
            /// Represents the home side's goal.
            /// 表示了主队的球门
            /// </summary>
            public const string HOME_GOAL = "0,68";

            /// <summary>
            /// Represents the away side's goal.
            /// 表示了客队的球门
            /// </summary>
            public const string AWAY_GOAL = "210,68";

            /// <summary>
            /// Represents the ball's acceleration.
            /// 表示了球的加速度
            /// </summary>
            public const int BALL_ACCELERATION = -15;

            /// <summary>
            /// 传球保护距离
            /// </summary>
            public const int PASS_PROTECTED_LINE = 20;

            /// <summary>
            /// 主队中路目标区域
            /// </summary>
            public const string HOME_DESTINATION_CENTER = "10,63,10,73";

            /// <summary>
            /// 主队左路目标区域
            /// </summary>
            public const string HOME_DESTINATION_LEFT = "10,116,10,126";

            /// <summary>
            /// 主队右路目标区域
            /// </summary>
            public const string HOME_DESTINATION_RIGHT = "10,10,10,20";

            /// <summary>
            /// 客队中路目标区域
            /// </summary>
            public const string AWAY_DESTINATION_CENTER = "200,63,200,73";

            /// <summary>
            /// 客队左路目标区域
            /// </summary>
            public const string AWAY_DESTINATION_LEFT = "200,10,200,20";

            /// <summary>
            /// 客队右路目标区域
            /// </summary>
            public const string AWAY_DESTINATION_RIGHT = "200,116,200,126";

            /// <summary>
            /// 主队强制射门区域
            /// </summary>
            public const string HOME_FORCE_SHOOT_REGION = "0,28,33,108";

            /// <summary>
            /// 主队强制传球区域
            /// </summary>
            public const string HOME_FORCE_PASS_REGION = "0,0,33,136";
            /// <summary>
            /// 主队边路传中区域
            /// </summary>
            public const string HOME_WING_CROSS_REGION = "0,28,35,108";
        }

        /// <summary>
        /// Represents the config of the match.
        /// </summary>
        public struct Match
        {

            /// <summary>
            /// Represents each round's time.
            /// </summary>
            public const double ROUND_TIME = 0.25;

            /// <summary>
            /// Represents the max player count in a team.
            /// </summary>
            public const byte MAX_PLAYER_COUNT = 11;

            /// <summary>
            /// Represents the stamina value while the first half start.
            /// 表示了上半场开始时，所有的属性比值
            /// </summary>
            public const double STAMINA_FIRST_HALF_START = 100;

            /// <summary>
            /// Represents the stamina value while the second half start.
            /// 表示了下半场开始时，所有的属性比值
            /// </summary>
            public const double STAMINA_SECOND_HALF_START = 95;
        }

        /// <summary>
        /// Represents the config of the player.
        /// </summary>
        public struct Player
        {
            /// <summary>
            /// Represents the player's width.
            /// </summary>
            public const int PLAYER_WIDTH = 2;

            /// <summary>
            /// Represents the player's defence range.
            /// 防守半径
            /// </summary>
            public const int DEFENCE_RANGE = 10;//15;
            /// <summary>
            /// 防守半径平方
            /// </summary>
            public const int DEFENCE_RANGEPow = 100;
            /// <summary>
            /// 单刀防守半径平方
            /// </summary>
            public const int DEFENCE_RANGESoloPow = 160;
            /// <summary>
            /// 突破防守半径平方
            /// </summary>
            public const int DEFENCE_RANGERushPow = 225;
            /// <summary>
            /// 干扰半径
            /// </summary>
            public const int DISTURB_RANGE = 12;
            /// <summary>
            /// 抢断半径
            /// </summary>
            public const int STEAL_RANGE = 10; //15;
            /// <summary>
            /// 协防准入范围
            /// </summary>
            public const int HELPDefenceAllowRangePow = 1000;
            /// <summary>
            /// Represents the player's sight range.
            /// </summary>
            public const int SIGHT_RANGE = 40;

            /// <summary>
            /// Represents a line, full back will defence under the line.
            /// </summary>
            public const int DEFENCE_LINE = 40;

            /// <summary>
            /// Represents the player's short decide's range.
            /// 表示了球员一次思考时能移动的距离边界。
            /// </summary>
            public const byte SHORT_MOVE_RANGE = 20;

            /// <summary>
            /// Represents the range that ahead of the pass receiver.
            /// 表示了球员传球给目标的提前量。
            /// </summary>
            public const byte PASS_AHEAD_RANGE = 20;

            /// <summary>
            /// Represents a range that ball handler can pass back.
            /// 表示了可以回传的距离
            /// </summary>
            public const byte PASS_BACK_RANGE = 10;

            /// <summary>
            /// Represents in the puzzle debuff, player running range.
            /// 表示了困惑状态下球员乱跑的半径。
            /// </summary>
            public const byte PUZZLE_RANGE = 10;

            /// <summary>
            /// Represents the player's acceleration config.
            /// </summary>
            //public static readonly double[] PLAYER_ACCELERATION = new[] { 2.5, 2, 1.5, 1, 0.5 };

            public static readonly double[] PLAYER_ACCELERATION = new[] { 2, 1.5, 1.25, 1, 0.5 };
            /// <summary>
            /// 传球目标和它相应的防守人直接的极限距离
            /// 只有目标和防守人的距离大于该值时，才会选择传给该球员
            /// </summary>
            public const int PASS_LIMIT_DISTANCE = 10;

            /// <summary>
            /// 传球的最远距离
            /// </summary>
            public const int PASS_MAX_RANGE = 120;//Raw:70;
            public const int PASS_MAX_RANGEPow = 14400;
            /// <summary>
            /// 短传的最远距离
            /// </summary>
            public const int SHORT_PASS_MAX_RANGE = 50;
            public const int SHORT_PASS_MAX_RANGEPow = 2500;
            /// <summary>
            /// 长传的概率
            /// </summary>
            public const int LONG_PASS_PERCENTAGE = 50;//Raw:30
          
            /// <summary>
            /// 传空挡概率
            /// </summary>
            public const int PASS_GAP_PERCENTAGE = 50;
            /// <summary>
            /// 脱手角球概率
            /// </summary>
            public const int OUTHAND_CORNER_PERCENTAGE = 30;
            /// <summary>
            /// 守门员在门线上的移动半径
            /// </summary>
            public const int GK_MOVE_RANGE = 5;

        

            /// <summary>
            /// 后卫的相关配置
            /// </summary>
            public struct Fullback
            {

                /// <summary>
                /// 选择传球的概率（和带球相对）
                /// </summary>
                public const byte PASS_PROBABILITY = 70;
            }

            /// <summary>
            /// 中场的相关配置
            /// </summary>
            public struct Midfielder
            {

                /// <summary>
                /// 选择传球的概率（和带球相对）
                /// </summary>
                public const byte PASS_PROBABILITY = 70;
            }

            /// <summary>
            /// 前锋的相关配置
            /// </summary>
            public struct Forward
            {

                /// <summary>
                /// 选择传球的概率（和带球相对）
                /// </summary>
                public const byte PASS_PROBABILITY = 90;
            }

            /// <summary>
            /// 球员移动的配置
            /// </summary>
            public struct PositionalConfig
            {
                /// <summary>
                /// 进攻的最大值
                /// </summary>
                public const double ATTACK_MAX = 200;

                /// <summary>
                /// 进攻的奖励值
                /// </summary>
                public const double ATTACK_AWARD = 250;

                /// <summary>
                /// 进攻奖励
                /// </summary>
                public const double ForwardAward = 200;

                /// <summary>
                /// 后退惩罚
                /// </summary>
                public const double BackPenelty = 150;
            }
        }

        /// <summary>
        /// Represents the hash keys.
        /// </summary>
        public struct Keys
        {

            /// <summary>
            /// Represents the match object's key.
            /// </summary>
            public const string MATCH = "HB_Match";

            /// <summary>
            /// Represents the match time's key.
            /// </summary>
            public const string MATCH_TIME = "HB_MATCH_TIME";

            /// <summary>
            /// Represents the home manager's id.
            /// </summary>
            public const string HOME_MANAGER_ID = "HB_HomeManagerId";

            /// <summary>
            /// Represents the away manager's id.
            /// </summary>
            public const string AWAY_MANAGER_ID = "HB_AwayManagerId";

            /// <summary>
            /// Represents the match's type.
            /// </summary>
            public const string MATCH_TYPE = "HB_MatchType";

            /// <summary>
            /// Represents the match's round number.
            /// </summary>
            public const string ROUND_NUMBER = "HB_RoundNumber";
        }

        /// <summary>
        /// Represents the default position.
        /// </summary>
        public struct Position
        {
            /// <summary>
            /// 门将默认位置
            /// </summary>
            public const string GK = "0,62";

            /// <summary>
            /// 后卫默认位置
            /// </summary>
            public const string LB = "37,16";

            /// <summary>
            /// 中场默认位置
            /// </summary>
            public const string LM = "62,16";

            /// <summary>
            /// 前锋默认位置
            /// </summary>
            public const string LF = "98,16";

            /// <summary>
            /// 开球位置1
            /// </summary>
            public const string OPENBALL_POSITION_1 = "105,66";

            /// <summary>
            /// 开球位置2
            /// </summary>
            public const string OPENBALL_POSITION_2 = "105,72";

            /// <summary>
            /// 球默认位置
            /// </summary>
            public const string BALL_DEFAULT = "105,68";
        }

        /// <summary>
        /// Represents the shoot's config. 
        /// </summary>
        public struct Shoot
        {
            /// <summary>
            /// 射中门框的概率
            /// </summary>
            public const int ShootToFramePercentage = 5;
        }

        /// <summary>
        /// Represents the foul level's config
        /// 犯规等级的配置
        /// </summary>
        public struct FoulLevel
        {
            /// <summary>
            /// None foul
            /// 没有犯规
            /// </summary>
            public const int NONE = -255;

            /// <summary>
            /// Normal foul
            /// 普通犯规
            /// </summary>
            public const byte NORMAL = 0x00;

            /// <summary>
            /// Yellow card.
            /// 黄牌
            /// </summary>
            public const byte YELLOW_CARD = 0x01;

            /// <summary>
            /// Red card.
            /// 红牌
            /// </summary>
            public const byte RED_CARD = 0x02;

            /// <summary>
            /// Injured.
            /// 受伤
            /// </summary>
            public const byte INJURED = 0x03;
        }
    }
}
