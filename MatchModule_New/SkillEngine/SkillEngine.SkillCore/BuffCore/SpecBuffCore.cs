using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;

namespace SkillEngine.SkillCore
{
    public class SpecBuffCore : ISpecBuffCore
    {
        #region Cache
        readonly Dictionary<int, Queue<ISpecEffect>> dicSpec = new Dictionary<int, Queue<ISpecEffect>>(16);
        #endregion

        #region .ctor
        public SpecBuffCore()
        { }
        #endregion

        #region Facade
        public bool AddSpecBuff(ISpecEffect inSpec)
        {
            if (null == inSpec)
                return false;
            Queue<ISpecEffect> que;
            if (!dicSpec.TryGetValue(inSpec.SpecTiming, out que))
            {
                que = new Queue<ISpecEffect>();
                dicSpec[inSpec.SpecTiming] = que;
            }
            if (!que.Contains(inSpec))
                que.Enqueue(inSpec);
            return true;
        }
        public bool TryPickSpecBuff(int inTiming, out ISpecEffect outSpec)
        {
            outSpec = null;
            Queue<ISpecEffect> que;
            if (!dicSpec.TryGetValue(inTiming, out que)
                || null == que || que.Count == 0)
                return false;
            do
            {
                if (que.Count > 0)
                    outSpec = que.Dequeue();
                else
                    outSpec = null;
            }
            while (null != outSpec && outSpec.InvalidFlag);
            return null != outSpec;
        }
        #endregion
    }
}
