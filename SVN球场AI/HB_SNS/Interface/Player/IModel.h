/********************************************************************************
 * 文件名：IPlayer.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IMODEL_H__
#define __IMODEL_H__

/// 表示了模型相关的接口
class IModel 
{
public:

    virtual ~IModel() {}

public:

    /// 为一个球员添加一个模型
    /// <param name="model">Represents the model id.</param>
    /// <param name="last">Represents the lasting time.</param>
    /// <param name="isHoldBall">Represents whether the model is lasting until player lose the ball.</param>
    virtual void AddModel(int model, int last, bool isHoldBall) = 0;

    /// 为一个球员添加一个模型
    /// <param name="model">Represents the model id.</param>
    /// <param name="last">Represents the lasting time.</param>        
    virtual void AddModel(int model, int last) = 0;
};

#endif //__IMODEL_H__
