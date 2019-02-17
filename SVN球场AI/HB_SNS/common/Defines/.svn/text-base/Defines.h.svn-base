/********************************************************************************
 * 文件名：Defines.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __DEFINES_H__
#define __DEFINES_H__

#include "../common.h"

using boost::details::pool::singleton_default;

//////////////////////////////////////////////////////////////////////////
#define MyDefine_Player_Count               11

// 可传球员的数组的最大数
#define MyDefine_Player_PassTargets_Count   11

#define MyDefine_Player_Defenders_Count     11

#define MyDefine_MAX_PLAYER_COUNT           11

//////////////////////////////////////////////////////////////////////////
//宏替代
#define Macro_PlayerCount                   11
#define Macro_PlayerPassTargets             (MyDefine_Player_Count - 1)
#define Macro_CarePlayers                   (2 * MyDefine_Player_Count - 2)
#define Macro_InSightPlayers                MyDefine_Player_Count
#define Macro_PlayerProcess                 12500
#define Macro_FootballProcess               12500
#define Macro_MatchEntityProcess            12500

#define Macro_NonDefenders                  9
#define Macro_NonDefenceTargetPlayer        8

#define Macro_SumPropertyNum                484
#define Macro_BallHanderRefreshCounter      20
 
//整型的总数
#define Macro_FormationMax                  8
//////////////////////////////////////////////////////////////////////////

/// 表示了整个系统的各个常量
namespace Defines
{
    /// 表示了系统的配置设置
    struct System
    {
        System() : OUTPUT_VERSION(2) {}

        /// 表示了输出模型的版本号
        const int OUTPUT_VERSION;
    };

    /// 表示了球场的配置
    struct Pitch
    {
        Pitch()
            : MAX_WIDTH(210)
            , MAX_HEIGHT(136)
            , HOME_PENALTY_AREA("0,45,13,91")
            , AWAY_PENALTY_AREA("197,45,210,91")
            , HOME_GOAL("0,68")
            , AWAY_GOAL("210,68")
            , BALL_ACCELERATION(-15)
            , PASS_PROTECTED_LINE(20)
            , HOME_DESTINATION_CENTER("10,63,10,73")
            , HOME_DESTINATION_LEFT("10,116,10,126")
            , HOME_DESTINATION_RIGHT("10,10,10,20")
            , AWAY_DESTINATION_CENTER("200,63,200,73")
            , AWAY_DESTINATION_LEFT("200,10,200,20")
            , AWAY_DESTINATION_RIGHT("200,116,200,126")
            , HOME_FORCE_SHOOT_REGION("0,45,13,91")
            , HOME_FORCE_PASS_REGION("0,0,13,136")
        {
        }

        /// 表示了球场的宽
        const int MAX_WIDTH;

        /// 表示了球场的高
        const int MAX_HEIGHT;

        /// 表示了主队的禁区
        const string HOME_PENALTY_AREA;

        /// 表示了客队的禁区
        const string AWAY_PENALTY_AREA;

        /// 表示了主队的球门
        const string HOME_GOAL;

        /// 表示了客队的球门
        const string AWAY_GOAL;

        /// 表示了球的加速度
        const int BALL_ACCELERATION;

        /// 传球保护距离
        const int PASS_PROTECTED_LINE;

        /// 主队中路目标区域
        const string HOME_DESTINATION_CENTER;

        /// 主队左路目标区域
        const string HOME_DESTINATION_LEFT;

        /// 主队右路目标区域
        const string HOME_DESTINATION_RIGHT;

        /// 客队中路目标区域
        const string AWAY_DESTINATION_CENTER;

        /// 客队左路目标区域
        const string AWAY_DESTINATION_LEFT;

        /// 客队右路目标区域
        const string AWAY_DESTINATION_RIGHT;

        /// 主队强制射门区域
        const string HOME_FORCE_SHOOT_REGION;

        /// 主队强制传球区域
        const string HOME_FORCE_PASS_REGION;
    };

    /// Represents the config of the match.
    struct Match
    {
        Match()
            : ROUND_TIME(0.25)
            , MAX_PLAYER_COUNT(Macro_PlayerCount)
            , STAMINA_FIRST_HALF_START(100)
            , STAMINA_SECOND_HALF_START(95)
        {

        }

        /// Represents each round's time.
        const double ROUND_TIME;

        /// Represents the max player count in a team.
        const int MAX_PLAYER_COUNT;

        /// 表示了上半场开始时，所有的属性比值
        const double STAMINA_FIRST_HALF_START;

        /// 表示了下半场开始时，所有的属性比值
        const double STAMINA_SECOND_HALF_START;
    };

    /// Represents the config of the player.
    struct Player
    {
        Player()
            : PLAYER_WIDTH(2)
            , DEFENCE_RANGE(7)
            , INTERRUPTION_RANGE(7)
            , SIGHT_RANGE(20)
            , DEFENCE_LINE(40)
            , SHORT_MOVE_RANGE(10)
            , PASS_AHEAD_RANGE(10)
            , PASS_BACK_RANGE(10)
            , PUZZLE_RANGE(10)
            , PASS_LIMIT_DISTANCE(10)
            , PASS_MAX_RANGE(70)
            , GK_MOVE_RANGE(5)
            , LONG_PASS_PERCENTAGE(30)
        {
            PLAYER_ACCELERATION[0] = 2.5;
            PLAYER_ACCELERATION[1] = 2;
            PLAYER_ACCELERATION[2] = 1.5;
            PLAYER_ACCELERATION[3] = 1;
            PLAYER_ACCELERATION[4] = 0.5;
        }

        /// Represents the player's width.
        const int PLAYER_WIDTH;

        /// 干扰半径
        const int DEFENCE_RANGE;

        /// 抢断半径
        const int INTERRUPTION_RANGE;

        /// Represents the player's sight range.
        const int SIGHT_RANGE;

        /// Represents a line, full back will defence under the line.
        const int DEFENCE_LINE;

        /// 表示了球员一次思考时能移动的距离边界。
        const int SHORT_MOVE_RANGE;

        /// 表示了球员传球给目标的提前量。
        const int PASS_AHEAD_RANGE;

        /// 表示了可以回传的距离
        const int PASS_BACK_RANGE;

        /// 表示了困惑状态下球员乱跑的半径。
        const int PUZZLE_RANGE;

        /// Represents the player's acceleration config.
        double PLAYER_ACCELERATION[5];

        /// 传球目标和它相应的防守人直接的极限距离
        /// 只有目标和防守人的距离大于该值时，才会选择传给该球员
        const int PASS_LIMIT_DISTANCE;

        /// <summary>
        /// 传球的最远距离
        const int PASS_MAX_RANGE;

        /// 守门员在门线上的移动半径
        const int GK_MOVE_RANGE;

        /// 长传的概率
        const int LONG_PASS_PERCENTAGE;

        /// 后卫的相关配置
        struct Fullback
        {
            Fullback() : PASS_PROBABILITY(70) {}

            /// 选择传球的概率（和带球相对）
            const int PASS_PROBABILITY;
        };

        /// 中场的相关配置
        struct MidFielder
        {
            MidFielder(): PASS_PROBABILITY(70) {}

            /// 选择传球的概率（和带球相对）
            const int PASS_PROBABILITY;
        };

        /// 前锋的相关配置
        struct Forward
        {
            Forward() : PASS_PROBABILITY(90) {}

            /// 选择传球的概率（和带球相对）
            const int PASS_PROBABILITY;
        };

        /// 球员移动的配置
        struct PositionalConfig
        {                      
            PositionalConfig()
                : ATTACK_MAX(200)
                , ATTACK_AWARD(250)
                , ForwardAward(200)
                , BackPenelty(150)
            {
            
            }

            /// 进攻的最大值
            const double ATTACK_MAX;

            /// 进攻的奖励值
            const double ATTACK_AWARD;                                                               

            /// 进攻奖励
            const double ForwardAward;

            /// 后退惩罚
            const double BackPenelty;
        };
    };

    /// Represents the hash keys.
    struct Keys
    {
        Keys()
            : MATCH("HB_Match")
            , MATCH_TIME("HB_MATCH_TIME")
            , HOME_MANAGER_ID("HB_HomeManagerId")
            , AWAY_MANAGER_ID("HB_AwayManagerId")
            , MATCH_TYPE("HB_MatchType")
            , ROUND_NUMBER("HB_RoundNumber")
        {

        }

        /// Represents the match object's key.
        const string MATCH;

        /// Represents the match time's key.
        const string MATCH_TIME;

        /// Represents the home manager's id.
        const string HOME_MANAGER_ID;

        /// Represents the away manager's id.
        const string AWAY_MANAGER_ID;

        /// Represents the match's type.
        const string MATCH_TYPE;

        /// Represents the match's round number.
        const string ROUND_NUMBER;
    };

    /// Represents the default position.
    struct Position
    {
        Position()
            : GK("0,62")
            , LB("37,16")
            , LM("62,16")
            , LF("98,16")
            , OPENBALL_POSITION_1("105,66")
            , OPENBALL_POSITION_2("105,72")
            , BALL_DEFAULT("105,68")
        {

        }

        /// 门将默认位置
        const string GK;

        /// 后卫默认位置
        const string LB;

        /// 中场默认位置
        const string LM;

        /// 前锋默认位置
        const string LF;

        /// 开球位置1
        const string OPENBALL_POSITION_1;

        /// 开球位置2
        const string OPENBALL_POSITION_2;

        /// 球默认位置
        const string BALL_DEFAULT;
    };

    /// Represents the shoot's config. 
    struct Shoot
    {
        Shoot(): ShootToFramePercentage(5) {}

        /// 射中门框的概率
        const int ShootToFramePercentage;
    };

    /// 犯规等级的配置
    struct FoulLevel
    {
        FoulLevel()
            : NONE(-255)
            , NORMAL(0x00)
            , YELLOW_CARD(0x01)
            , RED_CARD(0x02)
            , INJURED(0x03)
        {

        }

        /// 没有犯规
        const int NONE;

        /// 普通犯规
        const int NORMAL;

        /// 黄牌
        const int YELLOW_CARD;

        /// 红牌
        const int RED_CARD;

        /// 受伤
        const int INJURED;
    };
}


#define Defines_System                      singleton_default<Defines::System>::instance()

#define Defines_Pitch                       singleton_default<Defines::Pitch>::instance()

#define Defines_Match                       singleton_default<Defines::Match>::instance()

#define Defines_Player                      singleton_default<Defines::Player>::instance()
#define Defines_Player_Forward              singleton_default<Defines::Player::Forward>::instance()
#define Defines_Player_Midfielder           singleton_default<Defines::Player::MidFielder>::instance()
#define Defines_Player_Fullback             singleton_default<Defines::Player::Fullback>::instance()
#define Defines_Player_PositionalConfig     singleton_default<Defines::Player::PositionalConfig >::instance()

#define Defines_Keys                        singleton_default<Defines::Keys>::instance()

#define Defines_Position                    singleton_default<Defines::Position>::instance()

#define Defines_Shoot                       singleton_default<Defines::Shoot>::instance()

#define Defines_FoulLevel                   singleton_default<Defines::FoulLevel>::instance()


#endif  //__DEFINES_H__