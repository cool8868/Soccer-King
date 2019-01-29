/********************************************************************************
 * 文件名：PlayerAddDebuff
 * 创建人：
 * 创建时间：2010-2-22 13:59:36
 * 版本：1.0
 * 本文件版本号：1.0
 * 最后更新：
 * 功能说明：
 * 历史修改记录：
 * <author>  <time>           <version >   <desc>
 *********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Games.NB.Match.Base;
using Games.NB.Match.Base.Interface;
using Games.NB.Match.Base.Enum;
using Games.NB.Match.Common.Random;
using SkillEngine.SkillBase;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;
using SkillEngine.SkillBase.Xtern;
using SkillEngine.SkillCore;
using SkillEngine.SkillImpl.Football;

namespace Games.NB.Match.BLL.Model.Creatures
{
    public partial class Player
    {
        #region Facade
        public void AddFinishingBuff(int last)
        {
            int percent = 5000;
            this.AddBuff(_manager.CreatePropBuff(last, 0, percent, (int)EnumBuffCode.Shooting, (int)EnumBuffCode.FreeKick));
        }
        public void AddDisturbBuff(int last, int percent)
        {
            if (percent < 0)
                this.AddBuff(_manager.CreatePropBuff(last, 0, percent, (int)EnumBuffCode.Shooting, (int)EnumBuffCode.Passing, (int)EnumBuffCode.Mentality));
        }
        public void AddFallDownBuff(int last)
        {
            IBoostBuff boost = null;
            int maxRate = SkillDefines.MAXStorePercent;
            if (this.TryGetAntiRate(ref boost, (int)EnumBlurBuffCode.Falldown))
            {
                double rate = boost.Percent;
                if (rate >= maxRate || rate > 0 && _match.RandomPercent(maxRate) <= rate)
                {
                    //this.AddInertiaBuff(1);
                    return;
                }
            }
            this.AddBuff(_manager.CreateBlurBuff(EnumBlurType.LockMotion, EnumBlurBuffCode.Falldown, last));
            this.SkillCore.AddShowModel(null, (short)EnumSkillModel.Falldown, (short)last);
            this.RaiseBlurEvent(new BlurEventArgs(_manager.RootSkill, this, this, EnumBlurType.LockMotion, EnumBlurBuffCode.Falldown));
        }
        public void AddInertiaBuff(int last)
        {
            this.AddBuff(_manager.CreateBlurBuff(EnumBlurType.SpecMotion, EnumBlurBuffCode.Inertia, last));
        }
        public void AddSilenceBuff(int last)
        {
            this.AddBuff(_manager.CreateBlurBuff(EnumBlurType.LockSkill, EnumBlurBuffCode.Silence, last));
        }
        public void AddForceStateBuff(int forceState, int last,int rate=100)
        {
            if (forceState <= 0)
                return;
            if (last > 0)
                last += _match.Status.Round;
            var buff = new ForceStateBuff(_manager.RootSkill, (EnumForceState)forceState)
            {
                Rate = rate * 100,
                TimeEnd = (short)last,
            };
            this.AddBuff(buff);
        }
        #endregion

    }


   
}
