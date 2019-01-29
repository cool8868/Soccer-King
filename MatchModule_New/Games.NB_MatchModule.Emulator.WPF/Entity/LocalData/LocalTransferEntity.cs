using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;


namespace Games.NB.Match.Emulator.WPF.Entity.LocalData
{
    [XmlRoot(ElementName = "localtransferentity")]
    [Serializable]
    [KnownType(typeof(LocalTransferManagerEntity))]
    [KnownType(typeof(LocalTransferPlayerEntity))]

    [XmlInclude(typeof(LocalTransferManagerEntity))]
    [XmlInclude(typeof(LocalTransferPlayerEntity))]
    public class LocalTransferEntity
    {
        /// <summary>
        /// Represents the home side <see cref="NBA.MatchModule.Base.Model.TransferManagerEntity"/>.
        /// 表示了主队的经理对象
        /// </summary>
        [XmlElement(ElementName = "HomeManager")]
        public LocalTransferManagerEntity HomeManager { get; set; }

        /// <summary>
        /// Represents the away side <see cref="NBA.MatchModule.Base.Model.TransferManagerEntity"/>.
        /// 表示了客队的经理对象
        /// </summary>
        [XmlElement(ElementName = "AwayManager")]
        public LocalTransferManagerEntity AwayManager { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "localmanager")]
    [KnownType(typeof(LocalTransferPlayerEntity))]

    [XmlInclude(typeof(LocalTransferPlayerEntity))]
    public class LocalTransferManagerListEntity
    {
        [XmlArray, XmlArrayItem(ElementName = "Managers")]
        public List<LocalTransferManagerEntity> Managers { get; set; }
    }

    /// <summary>
    /// Represents a manager entity that uses for transfer.
    /// 表示了一个用来传递的经理对象
    /// </summary>
    /// 
    [Serializable]
    [XmlRoot(ElementName = "localmanager")]
    [KnownType(typeof(LocalTransferPlayerEntity))]

    [XmlInclude(typeof(LocalTransferPlayerEntity))]
    public class LocalTransferManagerEntity
    {
        /// <summary>
        /// Represents the manager's id.
        /// 表示了经理的Id
        /// </summary>
        [XmlAttribute(AttributeName = "Id")]
        public int Id { get; set; }
        [XmlAttribute(AttributeName = "Kpi")]
        public int Kpi { get; set; }
        /// <summary>
        /// Represents the manager's chinese name.
        /// 表示了经理的中文名
        /// </summary>
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "WillId")]
        public int WillId { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "Players")]
        public List<LocalTransferPlayerEntity> Players { get; set; }

        [XmlAttribute(AttributeName = "FormationId")]
        public int FormationId { get; set; }
        [XmlAttribute(AttributeName = "FormationStr")]
        public string FormationStr { get; set; }

        [XmlAttribute(AttributeName = "FormationLevel")]
        public int FormationLevel { get; set; }

        [XmlAttribute(AttributeName = "TalentId")]
        public int TalentId { get; set; }
        [XmlAttribute(AttributeName = "SuitId")]
        public int SuitId { get; set; }
    }

    /// <summary>
    /// Represents a player entity that uses for transfer.
    /// 表示了一个用来传递的球员对象
    /// </summary>
    [Serializable]
    public class LocalTransferPlayerEntity
    {
        [XmlAttribute(AttributeName = "Index")]
        public int Index { get; set; }
        /// <summary>
        /// Represents the player's first name.
        /// 表示了球员的名字
        /// </summary>
        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "PlayerId")]
        public int PlayerId { get; set; }
        [XmlAttribute(AttributeName = "Kpi")]
        public int Kpi { get; set; }

        [XmlAttribute(AttributeName = "Position")]
        public int Position { get; set; }
        [XmlAttribute(AttributeName = "PositionStr")]
        public string PositionStr { get; set; }
        [XmlAttribute(AttributeName = "Speed")]
        public double Speed { get; set; }
        [XmlAttribute(AttributeName = "Shooting")]
        public double Shooting { get; set; }
        [XmlAttribute(AttributeName = "FreeKick")]
        public double FreeKick { get; set; }
        [XmlAttribute(AttributeName = "Balance")]
        public double Balance { get; set; }
        [XmlAttribute(AttributeName = "Stamina")]
        public double Stamina { get; set; }
        [XmlAttribute(AttributeName = "Strength")]
        public double Strength { get; set; }
        [XmlAttribute(AttributeName = "Aggression")]
        public double Aggression { get; set; }
        [XmlAttribute(AttributeName = "Disturb")]
        public double Disturb { get; set; }
        [XmlAttribute(AttributeName = "Interception")]
        public double Interception { get; set; }
        [XmlAttribute(AttributeName = "Dribble")]
        public double Dribble { get; set; }
        [XmlAttribute(AttributeName = "Passing")]
        public double Passing { get; set; }
        [XmlAttribute(AttributeName = "Mentality")]
        public double Mentality { get; set; }
        [XmlAttribute(AttributeName = "Reflexes")]
        public double Reflexes { get; set; }
        [XmlAttribute(AttributeName = "Positioning")]
        public double Positioning { get; set; }
        [XmlAttribute(AttributeName = "Handling")]
        public double Handling { get; set; }
        [XmlAttribute(AttributeName = "Acceleration")]
        public double Acceleration { get; set; }

        /// <summary>
        /// Represents the player's action skills.
        /// 表示了球员装备的技能
        /// </summary>
        [XmlAttribute(AttributeName = "Skill")]
        public string Skill { get; set; }
        [XmlAttribute(AttributeName = "Skill2")]
        public string Skill2 { get; set; }
        [XmlAttribute(AttributeName = "StarSkill")]
        public string StarSkill { get; set; }

    }

    [Serializable]
    [XmlRoot(ElementName = "localtalent")]
    [KnownType(typeof(LocalTransferTalentEntity))]
    [XmlInclude(typeof(LocalTransferTalentEntity))]
    public class LocalTransferTalentListEntity
    {
        [XmlArray, XmlArrayItem(ElementName = "Talents")]
        public List<LocalTransferTalentEntity> Talents { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "localtalent")]
    [KnownType(typeof(LocalTransferTalentDataEntity))]

    [XmlInclude(typeof(LocalTransferTalentDataEntity))]
    public class LocalTransferTalentEntity
    {
        [XmlAttribute(AttributeName = "Id")]
        public int Id { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }
        
        [XmlArray, XmlArrayItem(ElementName = "Talentdatas")]
        public List<LocalTransferTalentDataEntity> Talentdatas { get; set; }
    }

    [Serializable]
    public class LocalTransferTalentDataEntity
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "localwill")]
    [KnownType(typeof(LocalTransferWillEntity))]
    [XmlInclude(typeof(LocalTransferWillEntity))]
    public class LocalTransferWillListEntity
    {
        [XmlArray, XmlArrayItem(ElementName = "Wills")]
        public List<LocalTransferWillEntity> Wills { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "localwill")]
    [KnownType(typeof(LocalTransferWillDataEntity))]

    [XmlInclude(typeof(LocalTransferWillDataEntity))]
    public class LocalTransferWillEntity
    {
        [XmlAttribute(AttributeName = "Id")]
        public int Id { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "Willdatas")]
        public List<LocalTransferWillDataEntity> Willdatas { get; set; }
    }

    [Serializable]
    public class LocalTransferWillDataEntity
    {
        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "localSuit")]
    [KnownType(typeof(LocalTransferSuitEntity))]
    [XmlInclude(typeof(LocalTransferSuitEntity))]
    public class LocalTransferSuitListEntity
    {
        [XmlArray, XmlArrayItem(ElementName = "Suits")]
        public List<LocalTransferSuitEntity> Suits { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "localSuit")]
    [KnownType(typeof(LocalTransferSuitDataEntity))]

    [XmlInclude(typeof(LocalTransferSuitDataEntity))]
    public class LocalTransferSuitEntity
    {
        [XmlAttribute(AttributeName = "Id")]
        public int Id { get; set; }

        [XmlAttribute(AttributeName = "Name")]
        public string Name { get; set; }

        [XmlArray, XmlArrayItem(ElementName = "Suitdatas")]
        public List<LocalTransferSuitDataEntity> Suitdatas { get; set; }
    }

    [Serializable]
    public class LocalTransferSuitDataEntity
    {
        [XmlAttribute(AttributeName = "Id")]
        public int Id { get; set; }
    }
}

