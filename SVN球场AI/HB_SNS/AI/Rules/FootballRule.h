/********************************************************************************
 * 文件名：FootballRule.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __FOOTBALLRULE_H__
#define __FOOTBALLRULE_H__

class FootballRule
{
public:

    /// Get the pass action's original ball speed.
    /// </summary>
    /// <param name="start">Represents the start coordinate.</param>
    /// <param name="target">Represents the target coordinate.</param>
    /// <returns>Represents the ball speed.</returns>
    static double GetPassSpeed(Coordinate start, Coordinate target)
    {
        double vb = 2.0f; // RandomHelper.GetInt32(5, 15); // 终点速度
        double d = start.Distance(target); // 距离
        double t = -(pow(pow(vb, 2) - 2 * Defines_Pitch.BALL_ACCELERATION * d, 0.5) + vb) / Defines_Pitch.BALL_ACCELERATION;

        return InternalGetPassSpeed(vb, t);
    }

    static int GetLongPassAngle()
    {
        return RandomHelper::GetInt32(20, 30);
    }

private:

    static double InternalGetPassSpeed(double vb, double t)
    {
        return vb - Defines_Pitch.BALL_ACCELERATION * t;
    }
};

#endif //__FOOTBALLRULE_H__
