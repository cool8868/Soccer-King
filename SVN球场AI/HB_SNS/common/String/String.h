/********************************************************************************
 * �ļ�����String.h
 * �����ˣ��α�
 * ����ʱ�䣺2011-6-10
 * �汾��1.0
 * ���ļ��汾�ţ�1.0
 * �����£�
 * ����˵����
 * ��ʷ�޸ļ�¼��
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __STRING_H__
#define __STRING_H__

#include <string>

class String
{
public:
    String();

    static std::string  Format(char* format, ...);

    static std::string  Empty();

    static bool         IsNullOrEmpty(std::string str);
};

#endif //__STRING_H__
