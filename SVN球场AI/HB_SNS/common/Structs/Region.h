/********************************************************************************
 * 文件名：Region.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __REGION_H__
#define __REGION_H__

#include "../common.h"
#include "../RandomHelper/RandomHelper.h"
#include "Coordinate.h"
#include "../utils.h"

struct Region
{
    Region(int x1, int y1, int x2, int y2)
    {
        this->Start = Coordinate(x1, y1);
        this->End = Coordinate(x2, y2);
    }

    Region(double x1 = 0.0f, double y1 = 0.0f, double x2 = 0.0f, double y2 = 0.0f)
    {
        this->Start = Coordinate(x1, y1);
        this->End = Coordinate(x2, y2);
    }

    Region(Coordinate start, Coordinate end)
    {
        this->Start = start;
        this->End = end;
    }

    Region(const Region& region)
    {
        this->Start = region.Start;
        this->End   = region.End;
    }

    Coordinate Start;

    Coordinate End;

    string ToString()
    {
        format fmt("Region[Start:{%.1f, %.1f}, End:{%.1f, %.1f}]");
        fmt % Start.X % Start.Y % End.X % End.Y;

        return fmt.str();
    }

    static Region ParseByStr(string str)
    {
        const int NumCount = 4;
        int points[NumCount] = {0};

        char_separator<char> separator(",");
        tokenizer<char_separator<char> > tokens(str, separator);

        int i = 0;
        foreach (string iter, tokens) 
        {
            if (i < NumCount) 
            {
                points[i++] = lexical_cast<int>(iter);
            }
        }

        return Region(points[0], points[1], points[2], points[3]);
    }

    Region& operator =(const Region& region)
    {
        this->Start = region.Start;
        this->End   = region.End;

        return *this;
    }

    bool operator ==(Region region)
    {
        return (Start == region.Start) && (Start == region.End);
    }

    bool operator !=(Region region)
    {
        return (Start != region.Start) || (End != region.End);
    }

    bool Equals(Region obj)
    {
        return obj.Start.Equals(Start) && obj.End.Equals(End);
    }

    Region Mirror()
    {
        return Region(End.Mirror(), Start.Mirror());
    }

    Coordinate GetRandomPoint()
    {
        return Coordinate(RandomHelper::GetInt32((int)Start.X, (int)End.X), RandomHelper::GetInt32((int)Start.Y, (int)End.Y));
    }

    Coordinate Center()
    {
        return Coordinate((Start.X + End.X)/2, (Start.Y + End.Y) / 2);
    }

    double Top()
    {
        return MATH_MIN(Start.Y, End.Y);
    }

    double Bottom()
    {
        return MATH_MAX(Start.Y, End.Y);
    }

    double Left()
    {
        return MATH_MIN(Start.X, End.X);
    }

    double Right()
    {
        return MATH_MAX(Start.X, End.X);
    }

    bool IsCoordinateInRegion(Coordinate coordinate)
    {
        if (coordinate.X < Start.X)
        {
            return false;
        }

        if (coordinate.X > End.X)
        {
            return false;
        }

        if (coordinate.Y < Start.Y)
        {
            return false;
        }

        if (coordinate.Y > End.Y)
        {
            return false;
        }

        return true;
    }
};

#endif //__REGION_H__
