using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Xtern;

namespace SkillEngine.SkillBase
{
    public interface ISkillCore
    {
        #region Data
        /// <summary>
        /// 技能列表
        /// </summary>
        List<ISkill> SkillList
        {
            get;
        }
        /// <summary>
        /// 模型Id
        /// </summary>
        short ModelId
        {
            get;
        }
        /// <summary>
        /// 技能存照列表
        /// </summary>
        ICollection<SkillClipResult> ClipList
        {
            get;
        }
        #endregion

        void LoadSkill(ISkillOwner owner, List<string> skills);
        void InvokeSkill(byte timeFlag);
        bool GetInvokeFlag(byte paralFlag);
        void SetInvokeFlag(byte paralFlag);
        void AddShowModel(ISkill srcSkill, short modelId, int last);
        void RemoveShowModel(ISkill srcSkill, short modelId);
        void AddOpenClip();
        void AddOpenClip(ISkill srcSkill);
        void AddShowClip(ISkill srcSkill, SkillClipSetting clip, byte[] targets);
    }
}
