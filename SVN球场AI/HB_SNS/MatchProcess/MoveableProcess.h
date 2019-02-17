/********************************************************************************
 * 文件名：MoveableProcess.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MOVEABLEPROCESS_H__
#define __MOVEABLEPROCESS_H__

#include "Process.h"
#include "../common/String/String.h"

/// 表示了一个可以移动物体的结果数据
class MoveableProcess : public Process, public IMoveableProcess
{
public:

    string ToString();

public:

    string      GetCoordinate() { return m_Coordinate; }
    void        SetCoordinate(string coord) { m_Coordinate = coord; }

    Coordinate  GetCoordinate2() { return m_Coordinate2; }
    void        SetCoordinate2(Coordinate coord) { m_Coordinate2 = coord; }

    int         Acceleration() { return m_Acceleration; }
    void        SetAcceleration(int acc) { m_Acceleration = acc; }

protected:

    void        Output(CUtlBuffer& writer, int id);
    void        Output(ofstream& writer, int id);

    void        ReadInvoke(ifstream& reader);
    void        ReadInvoke(CUtlBuffer& reader);

protected:

    /// 表示了当前的坐标
    string m_Coordinate;

    //当前的坐标
    Coordinate m_Coordinate2;

    /// 表示了当前的加速度
    int m_Acceleration;
};

#endif //__MOVEABLEPROCESS_H__
