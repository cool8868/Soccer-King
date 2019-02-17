/********************************************************************************
 * 文件名：FormationFacade.cpp
 * 创建人：宋斌
 * 创建时间：2011-6-10
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
#include "FormationFacade.h"

xmlFormation::xmlFormation()
{
    if (!m_doc.load_file("MatchData/Formation/Formation.xml"))
    {
        //cout << "Load file failed! Formation.xml\n";
    }
}

vector<FormationEntity> FormationFacade::GetFormation(int formationId)
{
    vector<FormationEntity> formationEntity;

    xml_node tools = singleton_Formation::instance().GetDoc().child("Formations");

    for (xml_node_iterator it = tools.begin(); it != tools.end(); ++it)
    {
        std::string formationid_str = it->attribute("id").value();

        if (formationid_str == boost::lexical_cast<std::string>(formationId))
        {
            for (xml_node_iterator it2 = it->begin(); it2 != it->end(); ++it2)
            {
                std::string type    = it2->attribute("type").value();
                std::string coord   = it2->attribute("coordinate").value();

                formationEntity.push_back(FormationEntity(type, coord));
            }
        }
    }

    return formationEntity;
}


