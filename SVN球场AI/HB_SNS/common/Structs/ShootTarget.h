/********************************************************************************
 * 文件名：ShootTarget.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __SHOOTTARGET_H__
#define __SHOOTTARGET_H__

#include "../common.h"

struct ShootTarget
{
    ShootTarget() : X(0), Y(0) {}

    int X;

    int Y;

    // 是否是门框
    bool IsFrame;

    ShootTarget(int x, int y)
    {
        X = x;
        Y = y;
        IsFrame = false;
    }

    ShootTarget(int x, int y, bool isFrame)
    {
        X = x;
        Y = y;
        IsFrame = isFrame;
    }

    string ToString()
    {
        return "[ShootTarget]" + Output();
    }

    string Output()
    {
        if (IsFrame)
        {
            return "F" + X;
        }

        return X + "," + Y;
    }
};

#endif //__SHOOTTARGET_H__
