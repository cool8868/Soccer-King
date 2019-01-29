using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkillEngine.Extern.Enum.Football;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;

namespace SkillEngine.SkillBase.Xtern
{
    public class FoulEventArgs : EventArgs
    {
        public FoulEventArgs()
        { }
        public FoulEventArgs(ISkill srcSkill, ISkillPlayer ownPlayer, ISkillPlayer oppPlayer, EnumFoulState foulType)
        {
            this.SrcSkill = srcSkill;
            this.OwnPlayer = ownPlayer;
            this.OppPlayer = oppPlayer;
            this.FoulType = foulType;
        }
        public EnumFoulState FoulType;
        public ISkill SrcSkill;
        public ISkillPlayer OwnPlayer;
        public ISkillPlayer OppPlayer;
    }

    public class BlurEventArgs : EventArgs
    {
        public BlurEventArgs()
        { }
        public BlurEventArgs(ISkill srcSkill, ISkillPlayer ownPlayer, ISkillPlayer oppPlayer, EnumBlurType blurType, EnumBlurBuffCode blurCode)
        {
            this.SrcSkill = srcSkill;
            this.OwnPlayer = ownPlayer;
            this.OppPlayer = oppPlayer;
            this.BlurType = blurType;
            this.BlurCode = blurCode;
        }
        public EnumBlurType BlurType;
        public EnumBlurBuffCode BlurCode;
        public ISkill SrcSkill;
        public ISkillPlayer OwnPlayer;
        public ISkillPlayer OppPlayer;
    }
}
