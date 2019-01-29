using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern;
using SkillEngine.Extern.Enum;
using SkillEngine.SkillBase.Xtern;


namespace SkillEngine.SkillBase
{
    public interface ISkillContext : IRandom
    {
        /// <summary>
        /// 每节回合数
        /// </summary>
        short RoundPerSection
        {
            get;
        }
        /// <summary>
        /// 每分钟回合数
        /// </summary>
        short RoundPerMinute
        {
            get;
        }
        /// <summary>
        /// 有效比赛回合
        /// </summary>
        short MatchRound
        {
            get;
        }
        /// <summary>
        /// 运行时比赛回合
        /// </summary>
        short MatchRunRound
        {
            get;
        }
        bool SkillSkip();
        short GetBuffLast(ISkill srcSkill, ISkillPlayer caster, int last, ISkillPlayer target = null);
        Dictionary<int, int> DicBuffer(int hashNo);
        List<int> ListBuffer(int hashNo);
        int[] ArrayBuffer(int hashNo);
    }
}
