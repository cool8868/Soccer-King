/********************************************************************************
 * 文件名：RandomHelper.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __RANDOMHELPER_H__
#define __RANDOMHELPER_H__

#include <ctime>
#include <boost/random.hpp>
#include <boost/pool/detail/singleton.hpp>
#include <boost/random/mersenne_twister.hpp>
#include <boost/lexical_cast.hpp>

using boost::details::pool::singleton_default;

template<typename T> class rng_wrapper//模板类型是随机数发生器
{
private:
	T rng;//随机数发生器成员变量
public:
	rng_wrapper():rng((typename T::result_type)time(0))
	{
	}

	typename T::result_type operator()(const double &min,const double &max)//重载调用操作符
	{
		boost::uniform_real<> ui(min,max);
		return ui(rng);
	}

	typename T::result_type operator()(const int &min,const int &max)//重载调用操作符
	{
		boost::uniform_int<> ui(min,max);
		return ui(rng);
	}

	T GetEngine()
	{
		return rng;
	}
};

typedef singleton_default< rng_wrapper<boost::mt19937> > rng_t;

class RandomHelper
{
public:
	RandomHelper(){}
	static int GetInt32(int min, int max)
	{
        if (min == max) return min;
        if (min > max)
        {
            int tmp = max;
            max = min;
            min = tmp;
        }

		rng_t::object_type &rng = rng_t::instance();		
		return rng(min,max);
	}

    static bool GetBoolean()
    {
        rng_t::object_type &rng = rng_t::instance();
        return lexical_cast<bool>(rng(0, 1));
    }

    static int GetPercentage()
    {
        return GetInt32(0, 100);
    }

    template <class T>
    static vector<T> GetRandomSortList(vector<T> list)
    {
        vector<int> indexList(list.size());
        indexList.clear();

        for (int i = 0; i < list.size(); i++)
        {
            indexList.push_back(i);
        }

        vector<int> randomList(list.size());
        randomList.clear();

        while (indexList.size() > 0)
        {
            int index = indexList[GetInt32(0, indexList.size() - 1)];
            randomList.push_back(list[index]);

            vector<int>::iterator it = find(indexList.begin(), indexList.end(), index);
            if (it != indexList.end())
            {
                indexList.erase(it);
            }
        }

        return randomList;
    }
};

#endif  //__RANDOMHELPER_H__

