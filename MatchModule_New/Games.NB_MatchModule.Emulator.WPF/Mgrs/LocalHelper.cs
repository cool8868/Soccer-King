using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Serialization;
using Games.NB.Match.Base.Model.TranIn;
using Games.NB.Match.Emulator.WPF.Entity;
using Games.NB.Match.Emulator.WPF.Entity.LocalData;
using Games.NBall.Bll;
using Games.NBall.Bll.Match;
using Games.NBall.Bll.Share;
using Games.NBall.Common;
using Games.NBall.Entity;
using Games.NBall.Entity.Enums;
using Games.NBall.Entity.Match;
using Newtonsoft.Json;

namespace Games.NB.Match.Emulator.WPF.Mgrs
{
    public class LocalHelper
    {
        private static readonly string basePath = AppDomain.CurrentDomain.BaseDirectory + "\\Config\\";
        public static readonly string talentxmlpath = "LocalTalent.xml";
        public static readonly string managerxmlpath = "LocalManager.xml";
        public static readonly string willxmlpath = "LocalWill.xml";
        public static readonly string suitxmlpath = "LocalSuit.xml";
        public static readonly string playercachepath = "LocalCache.txt";
        private static readonly DateTime _createTime = Convert.ToDateTime("2013-1-1 00:00:00." + ConfigurationManager.AppSettings["StaticDataVersion"]);

        public static string EmulatorHomeId = ConfigurationManager.AppSettings["EmulatorHomeId"];
        public static string EmulatorAwayId = ConfigurationManager.AppSettings["EmulatorAwayId"];

        #region .ctor
        public static LocalTransferManagerListEntity ManagerList { get; set; }
        public static LocalTransferTalentListEntity TalentList { get; set; }
        public static LocalTransferWillListEntity WillList { get; set; }
        public static LocalTransferSuitListEntity SuitList { get; set; }
        public static Dictionary<Guid,LocalTransferManagerEntity> LocalManagerDic { get; set; } 

        private static bool _isInit = false;
        static LocalHelper()
        {
            try
            {
                LoadLocalTransferManager();
                LoadLocalTransferTalent();
                LoadLocalTransferWill();
                LoadLocalTransferSuit();
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                throw ex;
            }
            
        }
        #endregion

        #region NewId
        public static int ManagerNewId
        {
            get
            {
                if (ManagerList.Managers.Count > 0)
                {
                    return ManagerList.Managers.Max(d => d.Id) + 1;
                }
                else
                {
                    return 1;
                }
            }
        }

        public static int TalentNewId
        {
            get
            {
                if (TalentList.Talents.Count > 0)
                {
                    return TalentList.Talents.Max(d => d.Id) + 1;
                }
                else
                {
                    return 1;
                }
            }
        }

        public static int WillNewId
        {
            get
            {
                if (WillList.Wills.Count > 0)
                {
                    return WillList.Wills.Max(d => d.Id) + 1;
                }
                else
                {
                    return 1;
                }
            }
        }

        public static int SuitNewId
        {
            get
            {
                if (SuitList.Suits.Count > 0)
                {
                    return SuitList.Suits.Max(d => d.Id) + 1;
                }
                else
                {
                    return 1;
                }
            }
        }
        #endregion

        #region SaveAndLoad
        public static void SaveLocalTransferManager()
        {
            SaveLocalTransfer<LocalTransferManagerListEntity>(managerxmlpath, ManagerList);
            BuildLocalManagerIdList();
        }

        public static void LoadLocalTransferManager()
        {
            ManagerList = LoadLocalTransfer<LocalTransferManagerListEntity>(managerxmlpath);
            if (ManagerList.Managers == null)
                ManagerList.Managers = new List<LocalTransferManagerEntity>();
            BuildLocalManagerIdList();
        }

        public static void BuildLocalManagerIdList()
        {
            LocalManagerDic = new Dictionary<Guid, LocalTransferManagerEntity>();
            foreach (var manager in ManagerList.Managers)
            {
                LocalManagerDic.Add(BuildLocalManagerId(manager.Id),manager);
            }
        }

        public static void SaveLocalTransferTalent()
        {
            SaveLocalTransfer<LocalTransferTalentListEntity>(talentxmlpath, TalentList);
        }

        public static void LoadLocalTransferTalent()
        {
            TalentList = LoadLocalTransfer<LocalTransferTalentListEntity>(talentxmlpath);
            if (TalentList.Talents == null)
                TalentList.Talents = new List<LocalTransferTalentEntity>();

        }

        public static void SaveLocalTransferWill()
        {
            SaveLocalTransfer<LocalTransferWillListEntity>(willxmlpath, WillList);
        }

        public static void LoadLocalTransferWill()
        {
            WillList = LoadLocalTransfer<LocalTransferWillListEntity>(willxmlpath);
            if (WillList.Wills == null)
                WillList.Wills = new List<LocalTransferWillEntity>();

        }

        public static void SaveLocalTransferSuit()
        {
            SaveLocalTransfer<LocalTransferSuitListEntity>(suitxmlpath, SuitList);
        }

        public static void LoadLocalTransferSuit()
        {
            SuitList = LoadLocalTransfer<LocalTransferSuitListEntity>(suitxmlpath);
            if (SuitList.Suits == null)
                SuitList.Suits = new List<LocalTransferSuitEntity>();

        }
        #endregion

        #region SaveLocalTransfer
        public static void SaveLocalTransfer<T>(string path, T t)
        {
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            var xmlpath = basePath + path;
            if (File.Exists(xmlpath))
            {
                File.Delete(xmlpath);
            }
            using (FileStream stream = new FileStream(xmlpath, FileMode.Create))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, t);
            }
        }
        #endregion

        #region LoadLocalTransfer
        public static T LoadLocalTransfer<T>(string path)
        {
            try
            {
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                var xmlpath = basePath + path;
                if (File.Exists(xmlpath))
                {
                    using (FileStream stream = new FileStream(xmlpath, FileMode.Open))
                    {
                        using (XmlReader reader = XmlReader.Create(stream))
                        {
                            var serializer = new XmlSerializer(typeof(T));
                            return (T)serializer.Deserialize(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
            }

            return (T)Activator.CreateInstance(typeof(T));
        }

        #endregion

        #region SaveConfig
        public static string SaveConfig<T>(T t, string configFileName)
        {
            try
            {
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                var xmlpath = basePath + configFileName;
                if (File.Exists(xmlpath))
                {
                    File.Delete(xmlpath);
                }
                using (StreamWriter stream = new StreamWriter(xmlpath, false))
                {
                    stream.Write(JsonConvert.SerializeObject(t));
                }
                File.SetCreationTime(xmlpath, _createTime);
                return xmlpath;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return "";
            }
        }

        public static T LoadConfig<T>(string configFileName)
        {
            try
            {
                if (!Directory.Exists(basePath))
                {
                    Directory.CreateDirectory(basePath);
                }
                var xmlpath = basePath + configFileName;
                if (File.Exists(xmlpath))
                {
                    FileInfo fileInfo = new FileInfo(xmlpath);
                    if (fileInfo.CreationTime != _createTime)
                        return default(T);
                    using (StreamReader reader = new StreamReader(xmlpath))
                    {
                        string s = reader.ReadToEnd();
                        return JsonConvert.DeserializeObject<T>(s);
                    }

                }
                return default(T);
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                throw ex;
            }
        }
        #endregion

        #region Bind
        public static void BindManagerTree(ComboBox cmb)
        {
            if (TalentList == null)
            {
                return;
            }
            var list = new List<KeyValueComboBoxItem>();
            list.Add(new KeyValueComboBoxItem("", "无"));
            foreach (var entity in TalentList.Talents)
            {
                list.Add(new KeyValueComboBoxItem(entity.Id, entity.Name));
            }
            cmb.ItemsSource = list;
            cmb.SelectedIndex = 0;
        }

        public static void BindWill(ComboBox cmb)
        {
            if (WillList == null)
            {
                return;
            }
            var list = new List<KeyValueComboBoxItem>();
            list.Add(new KeyValueComboBoxItem("", "无"));
            foreach (var entity in WillList.Wills)
            {
                list.Add(new KeyValueComboBoxItem(entity.Id, entity.Name));
            }
            cmb.ItemsSource = list;
            cmb.SelectedIndex = 0;
        }

        public static void BindSuit(ComboBox cmb)
        {
            if (SuitList == null)
            {
                return;
            }
            var list = new List<KeyValueComboBoxItem>();
            list.Add(new KeyValueComboBoxItem("", "无"));
            foreach (var entity in SuitList.Suits)
            {
                list.Add(new KeyValueComboBoxItem(entity.Id, entity.Name));
            }
            cmb.ItemsSource = list;
            cmb.SelectedIndex = 0;
        }

        public static void BindFormation(ComboBox cmb)
        {
            if (LocalCache == null)
            {
                return;
            }
            var list = new List<KeyValueComboBoxItem>();
            foreach (var entity in LocalCache.Formations)
            {
                list.Add(new KeyValueComboBoxItem(entity.Key, entity.Value));
            }
            cmb.ItemsSource = list;
            cmb.SelectedIndex = 0;
        }
        #endregion

        #region GetLocalMatchEntity
        public static MatchInput GetLocalMatchInput(Guid homeId, bool ishomeNpc, Guid awayId, bool isawayNpc, int time = 120)
        {
            try
            {
                MatchInput match = new MatchInput();
                match.MatchType = 1;
                match.TranTime = 120;
                match.HomeManager = BuildTransferManager(homeId,ishomeNpc);
                match.AwayManager = BuildTransferManager(awayId,isawayNpc);
                return match;
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return null;
            }

        }

        static ManagerInput BuildTransferManager(Guid managerId,bool isNpc)
        {
            LocalTransferManagerEntity localTransferManager = null;
            LocalManagerDic.TryGetValue(managerId, out localTransferManager);
            if (localTransferManager!=null)
            {
                return LocalManagerHelper.BuildTransfer(localTransferManager);
            }
            else
            {
                return MatchTransferUtil.BuildTransferManager(new MatchManagerInfo(managerId, isNpc));
            }
        }




        public static Guid GetManagerId(string account)
        {
            try
            {
                var list = NbManagerMgr.GetByAccount(account);
                if (list != null)
                    return list[0].Idx;
                else
                {
                    return Guid.Empty;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Insert(ex);
                return Guid.Empty;
            }
        }

        public static Guid BuildLocalManagerId(int id)
        {
            return BuildLocalManagerId(id.ToString());
        }

        public static Guid BuildLocalManagerId(string id)
        {
            var account = id.PadLeft(9, '0');
            return new Guid("00000000-0000-0000-0000-000" + account);
        }
        #endregion

        #region BuildPosition
        public static void BuildPlayerPosition(LocalTransferManagerEntity manager)
        {
            var formationDetails = CacheFactory.FormationCache.GetFormationDetail(manager.FormationId);
            for (int i = 0; i < manager.Players.Count; i++)
            {
                int p = GetPosition(i, formationDetails);
                manager.Players[i].Position = p;
                manager.Players[i].PositionStr = EmulatorHelper.GetPositionStr(p);
            }
        }

        static int GetPosition(int index, Dictionary<int, DicFormationdetailEntity> formationdetails)
        {
            if (formationdetails.ContainsKey(index))
                return formationdetails[index].Position;
            else
            {
                return -1;
            }
        }
        #endregion

        #region NpcTypes

        private static List<KeyValueComboBoxItem> _npcTypes;
        public static List<KeyValueComboBoxItem> NpcTypes
        {
            get
            {
                if (_npcTypes == null)
                {
                    var list = new List<KeyValueComboBoxItem>();
                    list.Add(new KeyValueComboBoxItem(99,"本地经理"));
                    list.Add(new KeyValueComboBoxItem(1,"巡回赛"));
                    list.Add(new KeyValueComboBoxItem(2, "精英巡回赛"));
                    list.Add(new KeyValueComboBoxItem(3, "世界挑战赛"));
                    _npcTypes = list;
                }
                return _npcTypes;
            }
        } 
        #endregion

        #region LocalCache
        private static LocalCacheEntity _localCache = null;
        public static LocalCacheEntity LocalCache
        {
            get
            {
                if (_localCache == null)
                {
                    var entity = LoadConfig<LocalCacheEntity>(playercachepath);
                    if (entity == null)
                    {
                        var list = DicPlayerMgr.GetAllForCache();
                        List<LocalDicPlayer> players = new List<LocalDicPlayer>(list.Count);
                        list.ForEach((d) => players.Add(new LocalDicPlayer(d)));
                        entity = new LocalCacheEntity();
                        entity.Players = players;

                        var list2 = DicFormationMgr.GetAll();
                        List<LocalKeyValueEntity> formations = new List<LocalKeyValueEntity>(list2.Count);
                        list2.ForEach((d) => formations.Add(new LocalKeyValueEntity() { Key = d.Idx.ToString(), Value = d.Name }));
                        entity.Formations = formations;

                        var list3 = DicManagertalentMgr.GetAll();
                        entity.Talents=new List<LocalTalentEntity>(list3.Count);
                        list3.ForEach((d) => entity.Talents.Add(new LocalTalentEntity() { Id = d.SkillCode,Name = d.SkillName,DriveType = d.DriveFlag}));

                        var list4 = DicManagerwillMgr.GetAll();
                        entity.Wills = new List<LocalWillEntity>(list4.Count);
                        list4.ForEach((d) => entity.Wills.Add(new LocalWillEntity() { Id = d.SkillCode, Name = d.SkillName,WillRank = d.WillRank}));

                        var coaches = ConfigCoachskillMgr.GetAll();
                        coaches.ForEach((d) =>  entity.Wills.Add(new LocalWillEntity() { Id = string.Concat("H", d.CoachIdx.ToString("000"), "_9"), Name = d.SkillName, WillRank = 2 }));

                        var list5 = DicNpcMgr.GetAll();
                        var tourList = DicTourstageMgr.GetAll();
                        var tourEliteList = DicToureliteMgr.GetAll();
                        var worldChallengeList = DicWorldchallengeMgr.GetAll();
                        var worldChallengeLink = ConfigWorldchallengenpclinkMgr.GetAll();
                        List<LocalNpcEntity> npcs = new List<LocalNpcEntity>(list5.Count);
                        foreach (var dicNpcEntity in list5)
                        {
                            var local = new LocalNpcEntity(){Name = dicNpcEntity.Name,NpcId = dicNpcEntity.Idx,Type = dicNpcEntity.Type};
                            var view = NpcDataHelper.GetMemberView(dicNpcEntity);
                            local.Kpi = view.Kpi;
                            switch (dicNpcEntity.Type)
                            {
                                case 1:
                                    var t = tourList.Find(d => d.NpcId == dicNpcEntity.Idx);
                                    if (t != null)
                                    {
                                        local.Stage = t.Name;
                                    }
                                    break;
                                case 2:
                                    var e = tourEliteList.Find(d => d.NpcId == dicNpcEntity.Idx);
                                    if (e!= null)
                                    {
                                        local.Stage = e.Name;
                                    }
                                    break;
                                case 3:

                                    var w = worldChallengeLink.Find(d => d.NpcId == dicNpcEntity.Idx);
                                    if (w != null)
                                    {
                                        var ww = worldChallengeList.Find(d => d.Idx == w.StageId);
                                        local.Name = ww.Name;
                                        local.Stage = ww.Idx.ToString();
                                    }
                                    break;
                            }
                            npcs.Add(local);
                        }
                        entity.Npcs = npcs;

                        var list6 = DicEquipmentsuitMgr.GetAll();
                        entity.Suits = new List<LocalSuitEntity>(list6.Count);
                        list6.ForEach((d) => entity.Suits.Add(new LocalSuitEntity() { Id = d.Idx, Name = d.Name ,SuitType = d.SuitType}));

                        var list7 = DicSkillcardMgr.GetAll();
                        entity.Skillcards = new List<LocalSkillcardEntity>(list7.Count);
                        list7.ForEach((d)=>entity.Skillcards.Add(new LocalSkillcardEntity(d)));

                        var list8 = DicStarskillsMgr.GetAll();
                        entity.StarSkills = new List<LocalStarSkillEntity>(list8.Count);
                        list8.ForEach((d) => entity.StarSkills.Add(new LocalStarSkillEntity(d)));

                        SaveConfig(entity, playercachepath);
                    }
                    _localCache = entity;
                }
                
                return _localCache;
            }
        }
        #endregion

        public static LocalNpcEntity CurrentNpc { get; set; }
    }
}
