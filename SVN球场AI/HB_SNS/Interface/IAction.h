﻿/********************************************************************************
 * 文件名：IAction.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __IACTION_H__
#define __IACTION_H__

class IPlayer;

class IAction 
{
public:

    virtual void Action(IPlayer* player) = 0;
};

#endif //__IACTION_H__
