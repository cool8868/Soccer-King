using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.Extern.Enum;

namespace SkillEngine.SkillBase.Xtern
{
    public interface ISkillManager : ISkillOwner, ISkillStat
    {
        #region Refer
        ISkillMatch SkillMatch
        {
            get;
        }
        /// <summary>
        /// 对方经理
        /// </summary>
        ISkillManager OppSkillManager
        {
            get;
        }
        /// <summary>
        /// 球员列表
        /// </summary>
        List<ISkillPlayer> SkillPlayerList
        {
            get;
        }
        ISkill RootSkill
        {
            get;
        }
        EnumMatchSide SkillSide
        {
            get;
        }
        #endregion

        #region Owned
        /// <summary>
        /// 阵型Id
        /// </summary>
        //int FormationId
        //{
        //    get;
        //}
        /// <summary>
        /// 战术Id
        /// </summary>
        string SkillTaticId
        {
            get;
        }
        Guid SkillMid
        {
            get;
        }
        #endregion

    }
}
