/********************************************************************************
 * �ļ�����Macros.h
 * �����ˣ��α�
 * ����ʱ�䣺2011-6-10
 * �汾��1.0
 * ���ļ��汾�ţ�1.0
 * �����£�
 * ����˵����
 * ��ʷ�޸ļ�¼��
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __MACROS_H__
#define __MACROS_H__

#include <map>
#include <string>

using namespace std;

#define PointerCastSafe(type, ptr) ((type*) (void*) (ptr))

#define foreach BOOST_FOREACH

#define DeletePtrSafe(ptr) if (ptr) delete ptr; ptr = NULL

#define classId "class "
#define Is(a, b) a->IsKindOf(classId#b) ? true : false

#define TypeId(a) string(#a)

#define Debug_Mode

//////////////////////////////////////////////////////////////////////////
typedef map<int,double> MapInt_Double;

class IRawSkill;
typedef map<string, IRawSkill*>                 MapString_IRawSkill;

class ISkillPartBuilder;
typedef map<string, ISkillPartBuilder*>         MapString_ISkillPartBuilder;

class IState;
class IActionSkill;
typedef map<IState*, vector<IActionSkill*> >    MapIState_VectorIActionSkill;

class ISkillResult;
typedef map<int, ISkillResult*>                 MapInt_ISkillResult;



#endif //__MACROS_H__
