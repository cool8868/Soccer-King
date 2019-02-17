/********************************************************************************
 * 文件名：Utility.h
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#ifndef __COMMON_UTILITY_H__
#define __COMMON_UTILITY_H__

#include <vector>
#include <string>
#include <fstream>
#include "Package/utlbuffer.h"
#include "String/String.h"

using namespace std;

namespace Common
{
    /// 将时间转化为回合
    static int ConvertTimeToRound(int time, int totalRound)
    {
        return (totalRound * time) / 90;
    }


    /// 字符串转化为int的数组
    static vector<int> ConvertStringToByteArray(string value)
    {
        vector<int> vectorInt;
        char_separator<char> separator(",");
        tokenizer<char_separator<char> > tokens(value, separator);

        foreach (string iter, tokens) 
        {
            vectorInt.push_back(lexical_cast<int>(iter));
        }

        return vectorInt;
    }

    /// 快速从一个数组中获取最大的数字所在的索引位数
    static int GetInt32MaxIndexQuick(int* array, int length)
    {
        int max = array[0];
        int index = 0;

        for (int i = 0; i < length; i++)
        {
            if (array[i] > max)
            {
                max = array[i];
                index = i;
            }
        }

        return index;

    }

    /// 快速从一个数组中获取最大的数字所在的索引位数
    static int GetDoubleMaxIndexQuick(double* array, int length)
    {
        double max = array[0];
        int index = 0;

        for (int i = 0; i < length; i++)
        {
            if (array[i] > max)
            {
                max = array[i];
                index = i;
            }
        }

        return index;
    }

    /// 快速从一个数组中获取最小的数字所在的索引位数
    static int GetDoubleMinIndexQuick(double* array, int length)
    {
        double min = array[0];
        int index = 0;

        for (int i = 0; i < length; i++)
        {
            if (array[i] < min)
            {
                min = array[i];
                index = i;
            }
        }

        return index;
    }

    /// 快速从一个数组中获取最大的数字
    static double GetDoubleMaxQuick(vector<double>& array, int length)
    {
        double max = array[0];

        for (int i = 0; i < length; i++)
        {
            if (array[i] > max)
            {
                max = array[i];                    
            }
        }

        return max;
    }

    /// 快速从一个数组中获取最小的数字
    static double GetDoubleMinQuick(double* array, int length)
    {
        double min = array[0];            

        for (int i = 0; i < length; i++)
        {
            if (array[i] < min)
            {
                min = array[i];                    
            }
        }

        return min;
    }

    /// 快速将一个数组从大到小进行排序
    static void SortMaxDoubleArrayQuick(double* array, int length)
    {
        for (int i = 0; i < length; i++)
        {
            if (i == 0) continue;

            for (int m = i; m > 0; m--)
            {
                if (array[m] > array[m - 1])
                {
                    double tmp = array[m];
                    array[m] = array[m - 1];
                    array[m - 1] = tmp;
                }
            }
        }
    }

    /// 快速将一个数组从小到大进行排序
    static void SortMinDoubleArrayQuick(double* array, int length)
    {
        for (int i = 0; i < length; i++)
        {
            if (i == 0) continue;

            for (int m = i; m > 0; m--)
            {
                if (array[m] < array[m - 1])
                {
                    double tmp = array[m];
                    array[m] = array[m - 1];
                    array[m - 1] = tmp;
                }
            }
        }
    }

    /// 快速将一个数组从小到大进行排序，但返回的是排序后的原数据的索引号(indexes)
    /// 必须保证array和indexes数组的长度一致
    static void SortMinDoubleArrayIndexQuick(double* array, int length, int* indexes)
    {
        for (int i = 0; i < length; i++)
        {
            indexes[i] = i;
        }

        for (int i = 0; i < length; i++)
        {
            if (i == 0) continue;

            for (int m = i; m > 0; m--)
            {
                if (array[m] < array[m - 1])
                {
                    double dTmp = array[m];
                    array[m] = array[m - 1];
                    array[m - 1] = dTmp;

                    int intTmp = indexes[m];
                    indexes[m] = indexes[m - 1];
                    indexes[m - 1] = intTmp;
                }
            }
        }
    }

    /// Represents the System's utilities.
    template <class T>
    class Utility
    {
    public:

        /// 从列表中获取最大的数
        static int GetMaxIndex(T* array, int length)
        {
            T max = array[0];
            int index = 0;

            for (int i = 0; i < length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                    index = i;
                }
            }

            return index;
        }

        /// 从列表中获取最大的数
        static T GetMax(vector<T>& array)
        {
            T max = array[0];

            for (size_t i = 0; i < array.size(); i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }
            }

            return max;
        }
    };

    template<class T>
    static void AddRange(vector<T*>& vec1, vector<T*>& vec2)
    {
        foreach (T* p, vec2)
        {
            if (find(vec1.begin(), vec1.end(), p) == vec1.end())
            {
                vec1.push_back(p);
            }
        }
    }

    template<class T>
    static void Remove(vector<T*>& vec, T* p)
    {
        for (vec::iterator& it = vec.begin(); it != vec.end();)
        {
            if (*it == p)
            {
                it = vec.erase(it);
            }
            else
            {
                ++it;
            }
        }
    }

    /// 显示球员的当前状态
    static string DisplayState(int state)
    {
        if (state == 0)
        {
            return "跑动";
        }

        if (state == 1)
        {
            return "扑救";
        }

        if (state == 2)
        {
            return "站立";
        }

        if (state == 3)
        {
            return "停球";
        }

        if (state == 4)
        {
            return "抢断";
        }

        if (state == 5)
        {
            return "铲球";
        }

        if (state == 6)
        {
            return "带球";
        }

        if (state == 7)
        {
            return "长传";
        }

        if (state == 8)
        {
            return "短传";
        }

        if (state == 9)
        {
            return "射门";
        }

        if (state == 10)
        {
            return "大力抽射";
        }

        if (state == 11)
        {
            return "过人";
        }

        return "None";    
    }

    static void ReadIdAndRound(ifstream& reader, int& id, int& round)
    {
        int height8 = reader.get() & 0xff;
        int lower8  = reader.get() & 0xff;

        //高6~8位为id
        id          = height8 >> 5 & 0x7;
        round       = (height8 & 0x1f) << 8 | lower8;
    }

    static void ReadIdAndRound(CUtlBuffer& reader, int& id, int& round)
    {
        int height8 = reader.GetChar() & 0xff;
        int lower8  = reader.GetChar() & 0xff;

        //高6~8位为id
        id          = height8 >> 5 & 0x7;
        round       = (height8 & 0x1f) << 8 | lower8;
    }

    static string GetUTF(ifstream& reader)
    {
        char strName[128] = {};

        int length = 0;
        reader.read((char*)&length, sizeof(short));
        reader.read(strName, length);

        return strName;
    }

    static string GetUTF(CUtlBuffer& buffer)
    {
        char strName[128] = {};

        buffer.GetUTF(strName, sizeof(strName));

        return strName;
    }

    static void PutUTF(ofstream& writer, const string content)
    {
        short i = content.length();

        writer.write((char*)&i, sizeof(short));
        writer.write(content.c_str(), i);
    }

    static string TransDoubleToString(double value)
    {
        format fmt("%.1f");
        fmt % value;

        return fmt.str();
    }

    //将字符串切割成vector
    static vector<string> TransToStringVector(string c, string strSeparator)
    {
        vector<string> vectorString;
        char_separator<char> separator(strSeparator.c_str());
        tokenizer<char_separator<char> > tokens(c, separator);

        foreach (string iter, tokens) 
        {
            vectorString.push_back(iter);
        }

        return vectorString;
    }

    template<class T>
    static vector<T> TransToValueVector(string c, string strSeparator)
    {
        vector<T> vectorValue;
        char_separator<char> separator(strSeparator.c_str());
        tokenizer<char_separator<char> > tokens(c, separator);

        foreach (string iter, tokens) 
        {
            vectorValue.push_back(lexical_cast<T>(trim_copy(iter)));
        }

        return vectorValue;
    }

    // 找到属性srcAttrName值为srcAttrValue的节点，取出dstAttrName属性的值
    static string GetAttribute(xml_node& node, string srcAttrName, string srcAttrValue, string dstAttrName)
    {
        if (node)
        {
            for (xml_node_iterator& it = node.begin(); it != node.end(); ++it)
            {
                if (it->attribute(srcAttrName.c_str()).value() == srcAttrValue)
                {
                    return it->attribute(dstAttrName.c_str()).value();
                }
            }
        }

        return String::Empty();
    }

    // 找到属性srcAttrName值为srcAttrValue的节点
    static xml_node GetAttributeNode(xml_node& node, string srcAttrName, string srcAttrValue)
    {
        if (node)
        {
            for (xml_node_iterator& it = node.begin(); it != node.end(); ++it)
            {
                if (it->attribute(srcAttrName.c_str()).value() == srcAttrValue)
                {
                    return *it;
                }
            }
        }

        return xml_node();
    }

    //找到节点属性attrName的值attrValue
    static bool GetAttribute(xml_node& node, string attrName, string& attrValue)
    {
        if (node)
        {
            xml_attribute& attribute = node.attribute(attrName.c_str());

            if (attribute)
            {
                attrValue = attribute.value();

                return true;
            }
        }

        return false;
    }
}

#endif //__COMMON_UTILITY_H__
