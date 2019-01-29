using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Games.NBall.Entity;

namespace Games.NB.Match.Emulator.WPF.Entity.LocalData
{
    public class LocalCacheEntity
    {
        public List<LocalDicPlayer> Players { get; set; }

        public List<LocalKeyValueEntity> Formations { get; set; }

        public List<LocalSuitEntity> Suits { get; set; }

        public List<LocalNpcEntity> Npcs { get; set; }

        public List<LocalSkillcardEntity> Skillcards { get; set; }

        public List<LocalStarSkillEntity> StarSkills { get; set; }

        public List<LocalTalentEntity> Talents { get; set; }

        public List<LocalWillEntity> Wills { get; set; } 
    }

    public class LocalKeyValueEntity
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }

    
}
