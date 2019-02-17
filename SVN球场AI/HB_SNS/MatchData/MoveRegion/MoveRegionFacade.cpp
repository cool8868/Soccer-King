/********************************************************************************
 * 文件名：MoveRegionFacade.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "MoveRegionFacade.h"

xmlMoveRegion::xmlMoveRegion()
{
    if (!m_doc.load_file("MatchData/MoveRegion/RegionConfig.xml"))
    {
        //cout << "Load file failed! RegionConfig.xml\n";
    }
}

Region MoveRegionFacade::GetMoveRegion(Coordinate position)
{
    string str_xRange = "";
    string str_yRange = "";

    xml_node tools = singleton_xmlMoveRegion::instance().GetDoc().child("config");

    for (xml_node_iterator it = tools.begin(); it != tools.end(); ++it)
    {
        std::string type_str = it->attribute("type").value();

        if (type_str == "x")
        {
            for (xml_node_iterator it2 = it->begin(); it2 != it->end(); ++it2)
            {
                std::string key     = it2->attribute("key").value();
                std::string value   = it2->attribute("value").value();

                if (position.X == lexical_cast<float>(key))
                {
                    str_xRange = value;
                }
            }
        }

        if (type_str == "y")
        {
            xml_node child = tools.child("type");
            for (xml_node_iterator it2 = it->begin(); it2 != it->end(); ++it2)
            {
                std::string key     = it2->attribute("key").value();
                std::string value   = it2->attribute("value").value();

                if (position.Y == lexical_cast<float>(key))
                {
                    str_yRange = value;
                }
            }
        }
    }

    Coordinate coord1 = Coordinate::Parse(str_xRange);
    Coordinate coord2 = Coordinate::Parse(str_yRange);

    return Region(coord1.X, coord2.X, coord1.Y, coord2.Y);
}

