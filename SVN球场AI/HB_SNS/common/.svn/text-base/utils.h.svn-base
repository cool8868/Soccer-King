/********************************************************************************
 * 文件名：utils.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __UTILS_H__
#define __UTILS_H__

namespace MATH
{
    const double PI         = 3.14159f;
    const double EPSINON    = 0.00001;

    static bool FloatEqual(double a, double b)
    {
        if (fabs(a - b) > MATH::EPSINON)
        {
            return false;
        }

        return true;
    }
}


#define MATH_MIN(a, b) (a) > (b) ? (b) : (a)
#define MATH_MAX(a, b) (a) > (b) ? (a) : (b)

#endif //__UTILS_H__
