/********************************************************************************
 * 文件名：TriggerFactory.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __TRIGGERFACTORY_H__
#define __TRIGGERFACTORY_H__

#include "../../../Interface/Skill/Resolver/ISkillPartBuilder.h"
#include "../../../Interface/Skill/ITrigger.h"
#include "../../../Exception/MyException.h"

/// Represent a factory to create the trigger.
class TriggerFactory : public ISkillPartBuilder
{
public:

    /// Create the <see cref="ITrigger"/>.
    ISkillPart* Build(xml_node& element);

private:

    ITrigger* CreateInstance(string name);
};

class TriggerFactoryException : public MyException
{
public:

    TriggerFactoryException()
    {

    }

    TriggerFactoryException(const string& message)
        : MyException(message)
    {

    }

public:

    const char* what() const throw()
    {
        string output = "TriggerFactoryException:";
        output += m_err;

        return output.c_str();
    }
};
#endif // __TRIGGERFACTORY_H__

