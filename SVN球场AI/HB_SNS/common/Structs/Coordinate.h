/********************************************************************************
 * 文件名：Coordinate.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __COORDINATE_H__
#define __COORDINATE_H__

#include "../common.h"
#include "../Defines/Defines.h"
#include "../utils.h"

struct Coordinate
{
    Coordinate()
        : X(0.0f)
        , Y(0.0f)
    {

    }

    Coordinate(double x, double y)
        : X(x)
        , Y(y)
    {

    }

    Coordinate(const Coordinate& coord)
        : X(coord.X)
        , Y(coord.Y)
    {

    }

    double X;

    double Y;

    string ToString()
    {
        format fmt("{%f},{%f}");
        fmt % X % Y;

        return fmt.str();
    }

    string Output()
    {
        format fmt("{%d},{%d}");
        fmt % X % Y;

        return fmt.str();
    }

    Coordinate XMirror()
    {
        int middle = Defines_Pitch.MAX_WIDTH / 2;

        if ((int)X == middle) return Coordinate(X, Y);

        return Coordinate(2 * middle - X, Y);
    }

    Coordinate YMirror()
    {
        int middle = Defines_Pitch.MAX_HEIGHT / 2;

        if ((int)Y == middle) return Coordinate(X, Y);

        return Coordinate(X, 2 * middle - Y);
    }

    Coordinate Mirror()
    {
        return Coordinate(this->XMirror().X, this->YMirror().Y);
    }

    /// 计算两点之间的距离
    double Distance(Coordinate target)
    {
        return sqrt(pow(this->X - target.X, 2) + pow(this->Y - target.Y, 2));
    }

    /// 使用简单的方式获取之间两点之间的距离
    double SimpleDistance(Coordinate target)
    {
        return pow(this->X - target.X, 2) + pow(this->Y - target.Y, 2);
    }

    bool operator ==(Coordinate coordinate)
    {
        return MATH::FloatEqual(X, coordinate.X) && MATH::FloatEqual(Y, coordinate.Y);
    }

    bool operator !=(Coordinate coordinate)
    {
        return !(MATH::FloatEqual(X, coordinate.X) && MATH::FloatEqual(Y, coordinate.Y));
    }

    Coordinate& operator = (string c)
    {
        const int NumCount = 2;
        int points[NumCount] = {0};

        char_separator<char> separator(",");
        tokenizer<char_separator<char> > tokens(c, separator);

        int i = 0;
        foreach (string iter, tokens) 
        {
            if (i < NumCount) 
            {
                points[i++] = lexical_cast<int>(iter);
            }
        }

        X = points[0];
        Y = points[1];

        return *this;
    }

    Coordinate& operator = (const Coordinate& coordinate)
    {
        X = coordinate.X;
        Y = coordinate.Y;

        return *this;
    }

    static Coordinate Parse(string c)
    {
        const int NumCount = 2;
        int points[NumCount] = {0};

        char_separator<char> separator(", ");
        tokenizer<char_separator<char> > tokens(c, separator);

        int i = 0;
        foreach (string iter, tokens) 
        {
            if (i < NumCount) 
            {
                points[i++] = lexical_cast<int>(iter);
            }
        }

        return Coordinate(points[0], points[1]);
    }

    bool Equals(Coordinate obj)
    {
        return MATH::FloatEqual(obj.X, X) && MATH::FloatEqual(obj.Y, Y);
    }
};

#endif //__COORDINATE_H__
