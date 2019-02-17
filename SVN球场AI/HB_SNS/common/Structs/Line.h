/********************************************************************************
 * 文件名：Line.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __LINE_H__
#define __LINE_H__

#include "../common.h"
#include "Coordinate.h"

struct Line
{
    Line(Coordinate start = Coordinate(), Coordinate end = Coordinate())
    {
        this->Start = start;
        this->End = end;
    }

    Line(int x1, int y1, int x2, int y2)
    {
        this->Start = Coordinate(x1, y1);
        this->End = Coordinate(x2, y2);
    }

    Coordinate Start;

    Coordinate End;

    Coordinate GetRandomPoint()
    {
        if (this->Start.X == this->End.X)
        {
            return Coordinate(this->Start.X, RandomHelper::GetInt32((int)this->Start.Y, (int)this->End.Y));
        }

        if (this->Start.Y == this->End.Y)
        {
            return GetCoordinateByX(RandomHelper::GetInt32((int)this->Start.X, (int)this->End.X));
        }

        if (RandomHelper::GetBoolean())
        {
            return GetCoordinateByX(RandomHelper::GetInt32((int)this->Start.X, (int)this->End.X));
        }
        else
        {
            return GetCoordinateByY(RandomHelper::GetInt32((int)this->Start.Y, (int)this->End.Y));
        }
    }

    static Line ParseByStr(string str)
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

        return Line(points[0], points[1], points[2], points[3]);
    }

private:

    Coordinate GetCoordinateByX(int x)
    {
        double y = (End.Y - Start.Y) / (End.X - Start.X) * x;
        return Coordinate(x, y);
    }

    Coordinate GetCoordinateByY(int y)
    {
        double x = y / ((End.Y - Start.Y) / (End.X - Start.X));
        return Coordinate(x, y);
    }
};

#endif //__LINE_H__
