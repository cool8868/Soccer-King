/********************************************************************************
 * 文件名：IModelStatus.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IMODELSTATUS_H__
#define __IMODELSTATUS_H__

/// 表示了球员的模型状态
class IModelStatus 
{
public:

    virtual ~IModelStatus() {}

public:

    /// 表示了模型id
    virtual int         Mid() = 0;
    virtual void        SetMid(int vl) = 0;

    /// 表示了模型的剩余时间(Round)
    virtual int         RemainTime() = 0;
    virtual void        SetRemainTime(int vl) = 0;
    virtual void        DecRemainTime() = 0;

    /// 表示了该模型是否是持球时就保持
    virtual bool        IsHoldBall() = 0;
    virtual void        SetIsHoldBall(bool vl) = 0;
};

#endif //__IMODELSTATUS_H__

