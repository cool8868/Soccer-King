using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern.Enum;
using SkillEngine.Extern.Enum.Football;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillCore;

namespace SkillEngine.SkillImpl.Football
{
    public class FootballFactPropPlusEffect : FactPropPlusEffect
    {
        #region .ctor
        public FootballFactPropPlusEffect(int[] buffId,
            bool mainFlag, bool pureFlag, bool debuffFlag)
            : base(buffId, mainFlag, pureFlag, debuffFlag)
        { }
        #endregion

        public override EnumBuffExec ExecType
        {
            get { return EnumBuffExec.None; }
        }

        protected override IBuff InnerCreatePropBuff(ISkill srcSkill, ISkillPlayer caster, double multiFact, double addFact)
        {
            short last = BuffUtil.GetBuffLast(srcSkill, caster, this.Last);
            if (last > 0)
                last += srcSkill.Context.MatchRound;
            //int arg;
            //if (!int.TryParse(srcSkill.Args, out arg))
            //    arg = 1;
            return new FootballPropBuff(srcSkill, this.BuffId)
            {
                Rate = SkillDefines.MAXStorePercent,
                //Point = Convert.ToInt32(this.Point * (0.95 + arg * 0.05) * multiFact + addFact),
                //Percent = Convert.ToInt32(this.Percent * (0.95 + arg * 0.05) * multiFact + addFact),
                Point = Convert.ToInt32(this.Point * multiFact + addFact),
                Percent = Convert.ToInt32(this.Percent * multiFact + addFact),
                TimeEnd = last,
                Times = this.Repeat,
            };
        }
        protected override void GetBuffFact(ISkill srcSkill, ISkillPlayer caster, out double multiFact, out double addFact)
        {
            multiFact = 1;
            addFact = 0;
            var srcManager = null != caster ? caster.SkillManager : srcSkill.Owner as ISkillManager;
            if (null == srcManager)
                return;
            switch (this.FactType)
            {
                case EnumBuffFact.ColourQty:
                    if (null == this.Values || Values.Length == 0)
                        return;
                    var players = Side == EnumOwnSide.Own ? srcManager.SkillPlayerList : srcManager.OppSkillManager.SkillPlayerList;
                    int qty = 0;
                    foreach (var player in players)
                    {
                        foreach (int val in Values)
                        {
                            if (player.SkillColour == val)
                                qty++;
                        }
                    }
                    multiFact = qty;
                    return;
                case EnumBuffFact.IncDribbleMinutes:
                    multiFact = srcManager.GetStatInt((int)EnumManagerStat.IncDribbleMinutes);
                    return;
                case EnumBuffFact.CombDribbleMinutes:
                    multiFact = srcManager.GetStatInt((int)EnumManagerStat.CombDribbleMinutes);
                    return;
            }
        }

        public override INormalEffect Clone()
        {
            var effect = new FootballFactPropPlusEffect(this.BuffId, this.MainFlag, this.PureFlag, this.DebuffFlag);
            effect.CopyValue(this);
            effect.Side = this.Side;
            effect.FactType = this.FactType;
            effect.Values = this.Values;
            return effect;
        }

    }
}
