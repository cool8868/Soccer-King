/********************************************************************************
 * 文件名：Utility.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __COMMON_DISPLAYUTILITY_H__
#define __COMMON_DISPLAYUTILITY_H__

#include "utils.h"

struct DisPlayCollection
{
    enum PlayMode
    {
        PLAY_MIN    = 0,
        PLAY_SLOW   = 0,        //慢速
        PLAY_NORMAL,            //普通速度
        PLAY_FAST,              //快速

        PLAY_MAX
    };

    struct PlayCollection
    {
        PlayCollection()
            : enable(false)
            , fps(0)
        {
        }

        bool    enable;
        int     fps;
    };

    DisPlayCollection()
    {
        PlaySpeed[PLAY_SLOW].fps    = 1;
        PlaySpeed[PLAY_NORMAL].fps  = 5;
        PlaySpeed[PLAY_FAST].fps    = 10;

        PlaySpeed[PLAY_NORMAL].enable = true;

        ScaleSize   = 4;
    }

    //////////////////////////////////////////////////////////////////////////
    //播放速度
    PlayCollection  PlaySpeed[PLAY_MAX];

    //游戏控制
    bool    Pause;                          //暂停

    //球员信息
    bool    PlayerId;                       //球员ID
    bool    PlayerState;                    //球员状态
    bool    PlayerDirection;                //球员朝向
    bool    PlayerDefenceRange;             //干扰半径
    bool    PlayerInterRuptionRange;        //抢断半径
    bool    PlayerSightRange;               //视野半径

    //足球信息
    bool    FootBallPoint;          //足球坐标点

    //////////////////////////////////////////////////////////////////////////
    //坐标缩放的倍数
    double  ScaleSize;

    //球场的边界
    int     PitchBounds()
    {
        return (int)ScaleSize * 10;
    }
};

#define DisPlayerController     singleton_default<DisPlayCollection>::instance()

#define TransToWindow(x)        DisPlayerController.PitchBounds() + x * DisPlayerController.ScaleSize

#define PITCH_BOUNDS            DisPlayerController.PitchBounds()

#define TransCoordinate(coord)  Coordinate(TransToWindow(coord.X), TransToWindow(coord.Y))

#define SCALE_SIZE              DisPlayerController.ScaleSize

//比赛的数据格式(数据的分析还是实时输出)
//#define REALTIME_MODE

//解析比赛生成的数据模式
//#define PARSE_PROCESS_MODE

//纯解析数据模式
//#define PARSE_RESULT_FILE_MODE

//生成并解析数据模式
//#define OUTPUT_AND_PARSE_RESULT_FILE_MODE

// 解析到用户文件模式
//#define TRANSLATE_OUTPUT_FILE_MODE

//解析内存模式
//#define PARSE_BUFFER_MODE

//从文件读取输入模式
#define PARSE_INPUT_FILE_MODE
//////////////////////////////////////////////////////////////////////////
static inline double TransIndexToAngle(int eightDirection)
{
    double angle;

    switch (eightDirection)
    {
    case 0:
        angle = 0;
        break;

    case 1:
        angle = MATH::PI / 4;
        break;

    case 2:
        angle = MATH::PI / 2;
        break;

    case 3:
        angle = MATH::PI * 3 / 4;
        break;

    case 4:
        angle = MATH::PI;
        break;

    case 5:
        angle = MATH::PI * 5 / 4;
        break;

    case 6:
        angle = MATH::PI * 3 / 2;
        break;

        //剩下的都返回dir_8
    default:
        angle = MATH::PI * 7 / 4;
    }

    return angle;
}

static string StateToString(int state)
{
    string stateName = "";

    switch (state)
    {
    case 0:
        stateName = "ActionState";
        break;

    case 1:
        stateName = "ChaceState";
        break;

    case 2:
        stateName = "DefenceState";
        break;

    case 3:
        stateName = "DiveBallState";
        break;

    case 4:
        stateName = "DribbleState";
        break;

    case 5:
        stateName = "HoldBallState";
        break;

    case 6:
        stateName = "IdleState";
        break;

    case 7:
        stateName = "OffBallState";
        break;

    case 8:
        stateName = "PassState";
        break;

    case 9:
        stateName = "PositionalState";
        break;

    case 10:
        stateName = "ShootState";
        break;

    case 11:
        stateName = "StopballState";

    case 12:
        stateName = "InterruptionState";
        break;

    case 13:
        stateName = "SlideTackleState";
        break;

    case 14:
        stateName = "DefaultDribbleState";
        break;

    case 15:
        stateName = "LongPassState";
        break;

    case 16:
        stateName = "ShortPassState";
        break;

    case 17:
        stateName = "DefaultShootState";
        break;

    case 18:
        stateName = "VolleyShootState";
        break;
    }

    return stateName;
}

#endif //__COMMON_UTILITY_H__
