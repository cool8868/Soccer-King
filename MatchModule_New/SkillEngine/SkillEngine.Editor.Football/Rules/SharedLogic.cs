using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Data;
using SkillEngine.Editor.Football.UI.ControlBase;
using SkillEngine.Editor.Football.UI.Controls;
using SkillEngine.Editor.Football.UI.SeekerControls;
using SkillEngine.Editor.Football.UI.FilterControls;
using SkillEngine.Editor.Football.UI.EffectControls;
using SkillEngine.Editor.Football.Data;
using SkillEngine.SkillBase.Enum.Football;

namespace SkillEngine.Editor.Football.Rules
{
    public class SharedLogic
    {
        #region Cache
        public const string KEYRoot = "SkillConfig";
        public const string KEYSkillSet = "Skills";
        public const string KEYSkill = "Skill";
        public const string KEYSkillSrcType = "p.SrcType";
        public const string KEYSkillCode = "c.skillCode";
        public const string KEYSkillId = "c.skillId";
        public const string KEYSkillName = "p.SkillName";
        public const string KEYSkillEffects = "c.effects";
        public const string KEYOpenClipId = "p.OpenClipId";
        public const string KEYClipSetting = "p.ClipSetting";
        public const string KEYModelSeting = "p.SrcModelSetting";
        public const string KEYClipId = "p.ClipId";
        public const string KEYModelId = "p.ModelId";
        public const string KEYAdd = "add";
        public const string KEYTypeNode = "Type";
        public const string KEYName = "name";
        public const string KEYTypeName = "typeName";
        public const string KEYFileName = "fileName";
        static readonly object s_lockRoot = new object();
        static XElement s_innerTypesConfig = null;
        static readonly Dictionary<string, Func<FlatCtrl>> s_dicControlType = new Dictionary<string, Func<FlatCtrl>>();
        #endregion

        #region Init
        static SharedLogic()
        {
            InitControlType();
        }
        static void InitControlType()
        {
            s_dicControlType.Clear();
            s_dicControlType.Add("Effector.Manager", () => new ManagerEffectorCtrl() { ClassFlag = EnumClassFlag.Manager });
            s_dicControlType.Add("Effector.Player", () => new PlayerEffectorCtrl() { ClassFlag = EnumClassFlag.Player });
            s_dicControlType.Add("Effector.PlayerEvent", () => new PlayerEffectorCtrl() { ClassFlag = EnumClassFlag.PlayerEvent });
            s_dicControlType.Add("Effector.Skill", () => new SkillEffectorCtrl() { ClassFlag = EnumClassFlag.Skill });
            s_dicControlType.Add("Seeker.Id", () => new IdSeekerCtrl());
            s_dicControlType.Add("Seeker.Motion", () => new MotionSeekerCtrl());
            s_dicControlType.Add("Seeker.Graph", () => new GraphSeekerCtrl());
            s_dicControlType.Add("Seeker.TalentPositon", () => new TalentPositonSeekerCtrl());
            s_dicControlType.Add("Seeker.ManagerSkill", () => new ManagerSkillSeekerCtrl());
            s_dicControlType.Add("Seeker.PlayerSkill", () => new PlayerSkillSeekerCtrl());
            s_dicControlType.Add("Filter.Motion", () => new MotionFilterCtrl());
            s_dicControlType.Add("Filter.IdMotion", () => new IdMotionFilterCtrl());
            s_dicControlType.Add("Filter.Position", () => new PositionFilterCtrl());
            s_dicControlType.Add("Filter.Colour", () => new ColourFilterCtrl());
            s_dicControlType.Add("Filter.BallState", () => new BallStateFilterCtrl());
            s_dicControlType.Add("Filter.Graph", () => new GraphFilterCtrl());
            s_dicControlType.Add("Filter.TalentPositon", () => new TalentPositonFilterCtrl());
            s_dicControlType.Add("Filter.StaminaCompare", () => new StaminaCompareFilterCtrl());
            s_dicControlType.Add("Filter.SkillState", () => new SkillStateFilterCtrl());
            s_dicControlType.Add("Filter.ManagerStat", () => new ManagerStatFilterCtrl());
            s_dicControlType.Add("Filter.PlayerStat", () => new PlayerStatFilterCtrl());
            s_dicControlType.Add("Filter.GroundMotion", () => new GroundMotionFilterCtrl());
            s_dicControlType.Add("Effect.Boost", () => new BoostEffectCtrl());
            s_dicControlType.Add("Effect.Blur", () => new BlurEffectCtrl());
            s_dicControlType.Add("Effect.Foul", () => new FoulEffectCtrl());
            s_dicControlType.Add("Effect.FoulPro", () => new FoulProEffectCtrl());
            s_dicControlType.Add("Effect.PropPlus", () => new PropPlusEffectCtrl());
            s_dicControlType.Add("Effect.RatePropPlus", () => new RatePropPlusEffectCtrl());
            s_dicControlType.Add("Effect.VaryPropPlus", () => new VaryPropPlusEffectCtrl());
            s_dicControlType.Add("Effect.FactPropPlus", () => new FactPropPlusEffectCtrl());
            s_dicControlType.Add("Effect.EventPropPlus", () => new EventPropPlusEffectCtrl());
            s_dicControlType.Add("Effect.Exec", () => new ExecEffectCtrl());
            s_dicControlType.Add("Effect.ClearCD", () => new ClearCDEffectCtrl());
            s_dicControlType.Add("Effect.ForceState", () => new ForceStateEffectCtrl());
        }
        #endregion

        #region Facade
        public static FlatCtrl GetFlatControl(string classKey)
        {
            Func<FlatCtrl> ctor = null;
            if (!s_dicControlType.TryGetValue(classKey, out ctor) || null == ctor)
                return null;
            var ctrl = ctor();
            ctrl.InitData();
            return ctrl;
        }
        public static void BindControl(ComboBox comb, List<BindItemData> bindData)
        {
            comb.DisplayMember = BindItemData.COLText;
            comb.ValueMember = BindItemData.COLCode;
            comb.DataSource = bindData;
        }
        public static DialogResult ShowMessage(string msg)
        {
            return MessageBox.Show(msg, "系统信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region BindSkill
        public static DataTable GetBindSkillTable(XElement root)
        {
            DataTable dt = GetBindSkillSchema();
            FillBindSkillTable(dt, root);
            return dt;
        }
        public static void FillBindSkillTable(DataTable dt, XElement root)
        {
            dt.Rows.Clear();
            SkillItemData obj = null;
            foreach (var item in root.Descendants(KEYSkill))
            {
                obj = new SkillItemData();
                if (TryGetBindSkillItem(item, obj))
                    dt.Rows.Add(obj.Values);
            }
            dt.AcceptChanges();
        }
        public static bool TryGetBindSkillItem(XElement skillXe, SkillItemData skillItem)
        {
            var xa = skillXe.Attribute(KEYSkillCode);
            if (null == xa)
                return false;
            skillItem.SkillCode = xa.Value;
            skillItem.SkillId = skillXe.Attribute(KEYSkillId).Value;
            skillItem.SkillName = skillXe.Attribute(KEYSkillName).Value;
            skillItem.SkillType = skillXe.Attribute(KEYSkillSrcType).Value;
            skillItem.Memo = GetAttributeValue(skillXe, SharedData.KEYMemo);
            skillItem.ModelId = GetAttributeValue(skillXe, SharedData.KEYModelId);
            skillItem.OpenClipId = GetAttributeValue(skillXe, KEYOpenClipId);
            skillItem.ClipId = GetAttributeValue(skillXe, SharedData.KEYClipId);
            return true;
        }
        public static string GetSkillMemo(XElement skillXe)
        {
            if (null == skillXe)
                return string.Empty;
            string memo = string.Empty;
            XAttribute xa = null;
            foreach (var xe in skillXe.Descendants(KEYSkillEffects))
            {
                foreach (var xe2 in xe.Elements(KEYAdd))
                {
                    xa = xe2.Attribute(SharedData.KEYMemo);
                    if (null == xa)
                        continue;
                    memo += (string.IsNullOrEmpty(memo) ? "" : "；") + xa.Value;
                }
            }
            return memo;
        }
        public static string GetModelId(XElement skillXe)
        {
            if (null == skillXe)
                return string.Empty;
            string modelId = string.Empty;
            XAttribute xa = null;
            foreach (var xe in skillXe.Descendants(KEYModelSeting))
            {
                xa = xe.Attribute(KEYModelId);
                if (null == xa)
                    continue;
                modelId += (string.IsNullOrEmpty(modelId) ? "" : "；") + xa.Value;
            }
            return modelId;
        }
        public static string GetClipId(XElement skillXe)
        {
            if (null == skillXe)
                return string.Empty;
            string clipId = string.Empty;
            XAttribute xa = null;
            foreach (var xe in skillXe.Descendants(KEYClipSetting))
            {
                xa = xe.Attribute(KEYClipId);
                if (null == xa)
                    continue;
                clipId += (string.IsNullOrEmpty(clipId) ? "" : "；") + xa.Value;
            }
            return clipId;
        }
        public static DataTable GetBindSkillSchema()
        {
            var dt = new DataTable();
            dt.Columns.Add(SkillItemData.COLSkillId);
            dt.Columns.Add(SkillItemData.COLSkillCode);
            dt.Columns.Add(SkillItemData.COLSkillName);
            dt.Columns.Add(SkillItemData.COLSkillSrcType);
            dt.Columns.Add(SkillItemData.COLModelId);
            dt.Columns.Add(SkillItemData.COLOpenClipId);
            dt.Columns.Add(SkillItemData.COLClipId);
            dt.Columns.Add(SkillItemData.COLMemo);
            return dt;
        }
        #endregion

        #region XML
        public static XElement GetSkillXml(XElement root, string skillCode)
        {
            XAttribute xa = null;
            foreach (var item in root.Descendants(SharedLogic.KEYSkill))
            {
                xa = item.Attribute(SharedLogic.KEYSkillCode);
                if (null == xa)
                    continue;
                if (string.Compare(xa.Value, skillCode, true) == 0)
                    return item;
            }
            return null;
        }
        public static XElement GenClientXml(XElement root)
        {
            var lstModels = new List<XElement>();
            var lstClips = new List<XElement>();
            int modelId = 0;
            int clipId = 0;
            EnumSkillSrcType asSkillType = EnumSkillSrcType.PlayerStar;
            string skillType = string.Empty;
            string skillCode = string.Empty;
            string skillName = string.Empty;
            int last = 0;
            foreach (var skill in root.Descendants(KEYSkill))
            {
                skillType = GetAttributeValue(skill, KEYSkillSrcType);
                if (Enum.TryParse(skillType, out asSkillType))
                    skillType = ((int)asSkillType).ToString();
                skillCode = GetAttributeValue(skill, KEYSkillCode);
                skillName = GetAttributeValue(skill, KEYSkillName);
                int.TryParse(GetAttributeValue(skill, "p.OpenClipId"), out clipId);
                if (clipId > 0)
                    lstClips.Add(GenClipElement(clipId, skillType, skillCode, skillName, clipId, string.Empty));
                foreach (var model in skill.Descendants("p.SrcModelSetting"))
                {
                    if (!int.TryParse(GetAttributeValue(model, "p.ModelId"), out modelId))
                        continue;
                    int.TryParse(GetAttributeValue(model, "p.ModelLast"), out last);
                    lstModels.Add(GenModelElement(modelId, skillCode, skillName, modelId, last));
                }
                foreach (var clip in skill.Descendants("p.ClipSetting"))
                {
                    if (!int.TryParse(GetAttributeValue(clip, "p.ClipId"), out clipId))
                        continue;
                    lstClips.Add(GenClipElement(clipId, skillType, skillCode, skillName, clipId, string.Empty));
                }
            }
            lstModels.Add(GenModelElement(240, "root", "致命陷阱", 240, 0));
            lstModels.Add(GenModelElement(241, "root", "致残", 241, 0));
            lstModels.Add(GenModelElement(242, "root", "致伤", 242, 0));
            lstModels.Add(GenModelElement(243, "root", "静止", 243, 0));
            lstModels.Add(GenModelElement(244, "root", "倒地", 244, 0));
            lstModels.Add(GenModelElement(245, "root", "困惑", 245, 0));
            lstModels.Add(GenModelElement(246, "root", "晕眩", 246, 0));
            lstModels.Add(GenModelElement(247, "root", "脱手", 247, 0));
            lstModels.Add(GenModelElement(248, "root", "迷惑", 248, 0));
            lstModels.Add(GenModelElement(249, "root", "沉默", 249, 0));
            lstModels.Add(GenModelElement(251, "root", "复活", 251, 0));


            var models = new XElement("Models", lstModels.OrderBy(i => Convert.ToInt32(GetAttributeValue(i, "ModelId"))).ToArray());
            var clips = new XElement("Skills", lstClips.OrderBy(i => Convert.ToInt32(GetAttributeValue(i, "SkillId"))).ToArray());
            return new XElement("SkillClient",
                //new XComment("***模型列表***\r\nSkillName:技能名\r\nResId:动作资源Id\r\n"
                //    + "Last:持续回合数，AS端备用，服务端有维护，即过了持续回合模型会消失；此处如有特殊模型的持续回合需要AS端写死，则可在此配置其他属性\r\n"
                //    + "AS端可自行添加其他编程需要的属性"), 
                    models,
                //new XComment("***动画列表***\r\nSkillType:技能来源类型(天赋-7;意志-8;教练-9)\r\nSkillName:技能名\r\n"
                //    + "ResId:动画资源Id\r\nMeta:意志组合的球员列表"), 
                    clips);
        }
        static XElement GenModelElement(int modelId, string skillCode, string skillName, int resId, int modelLast)
        {
            return new XElement("Model",
                        new XAttribute("ModelId", modelId),
                        new XAttribute("SkillCode", skillCode),
                        new XAttribute("SkillName", skillName),
                        new XAttribute("ResId", resId),
                        new XAttribute("ModelLast", modelLast));
        }
        static XElement GenClipElement(int clipId, string skillType, string skillCode, string skillName, int resId, string meta)
        {
            return new XElement("Skill",
                      new XAttribute("SkillId", clipId),
                        new XAttribute("SkillType", skillType),
                      new XAttribute("SkillCode", skillCode),
                      new XAttribute("SkillName", skillName),
                      new XAttribute("ResId", resId),
                      new XAttribute("Meta", meta));
        }
        static string GetAttributeValue(XElement xe, string attrName)
        {
            return null == xe.Attribute(attrName) ? string.Empty : xe.Attribute(attrName).Value;
        }
        public static XElement WrapLoadXml(XElement root)
        {
            string skillMemo = string.Empty;
            string effectMemo = string.Empty;
            string modelId = string.Empty;
            string clipId = string.Empty;
            EnumSkillSrcType srcType = EnumSkillSrcType.PlayerAction;
            var lstRoot = root.Descendants(KEYSkill).OrderBy(i =>
            {
                Enum.TryParse(i.Attribute(KEYSkillSrcType).Value, out srcType);
                return (int)srcType;
            }).ThenBy(i => i.Attribute(KEYSkillCode).Value).ToList();
            foreach (var item in lstRoot)
            {
                skillMemo = string.Empty;
                foreach (var xe in item.Descendants(KEYSkillEffects))
                {
                    effectMemo = string.Empty;
                    foreach (var node in xe.Nodes().ToArray())
                    {
                        if (node.NodeType == System.Xml.XmlNodeType.Comment)
                        {
                            effectMemo = ((XComment)node).Value;
                            skillMemo += (string.IsNullOrEmpty(skillMemo) ? "" : "；") + effectMemo;
                            node.Remove();
                            continue;
                        }
                        if (string.IsNullOrEmpty(effectMemo))
                            continue;
                        if (node.NodeType == System.Xml.XmlNodeType.Element)
                        {
                            ((XElement)node).Add(new XAttribute(SharedData.KEYMemo, effectMemo));
                        }
                    }
                }
                modelId = string.Empty;
                foreach (var xe in item.Descendants(KEYModelSeting))
                {
                    modelId += (string.IsNullOrEmpty(modelId) ? "" : ";") + xe.Attribute(KEYModelId).Value;
                }
                clipId = string.Empty;
                foreach (var xe in item.Descendants(KEYClipSetting))
                {
                    clipId += (string.IsNullOrEmpty(clipId) ? "" : "；") + xe.Attribute(KEYClipId).Value;
                }
                if (!string.IsNullOrEmpty(skillMemo))
                    item.Add(new XAttribute(SharedData.KEYMemo, skillMemo));
                if (!string.IsNullOrEmpty(modelId))
                    item.Add(new XAttribute(SharedData.KEYModelId, modelId));
                if (!string.IsNullOrEmpty(clipId))
                    item.Add(new XAttribute(SharedData.KEYClipId, clipId));
            }
            root.RemoveAll();
            root.Add(lstRoot.ToArray());
            lstRoot.Clear();
            return root;
        }
        public static XElement WrapSaveXml(XElement root)
        {
            var nRoot = new XElement(root);
            XAttribute memoXa = null;
            EnumSkillSrcType srcType = EnumSkillSrcType.PlayerAction;
            var lstRoot = nRoot.Descendants(KEYSkill).OrderBy(i =>
            {
                Enum.TryParse(i.Attribute(KEYSkillSrcType).Value, out srcType);
                return (int)srcType;
            }).ThenBy(i => i.Attribute(KEYSkillCode).Value).ToList();
            var dicRoot = new Dictionary<EnumSkillSrcType, List<XElement>>();
            foreach (var item in lstRoot)
            {
                Enum.TryParse(item.Attribute(KEYSkillSrcType).Value, out srcType);
                if (!dicRoot.ContainsKey(srcType))
                    dicRoot[srcType] = new List<XElement>();
                dicRoot[srcType].Add(item);
                foreach (var xe in item.Descendants(KEYSkillEffects))
                {
                    foreach (var xe2 in xe.Elements(KEYAdd).ToArray())
                    {
                        memoXa = xe2.Attribute(SharedData.KEYMemo);
                        if (null == memoXa)
                            continue;
                        xe2.AddBeforeSelf(new XComment(memoXa.Value));
                        xe2.SetAttributeValue(SharedData.KEYMemo, null);
                    }
                }
                item.SetAttributeValue(SharedData.KEYMemo, null);
                item.SetAttributeValue(SharedData.KEYModelId, null);
                item.SetAttributeValue(SharedData.KEYClipId, null);
            }
            nRoot.RemoveAll();
            foreach (var kvp in dicRoot)
            {
                nRoot.Add(new XElement(KEYSkillSet, kvp.Value.ToArray()));
            }
            nRoot.Add(InnerTypesConfig());
            lstRoot.Clear();
            dicRoot.Clear();
            return nRoot;
        }
        
        static XElement InnerTypesConfig()
        {
            if (null == s_innerTypesConfig)
            {
                lock (s_lockRoot)
                {
                    if (null != s_innerTypesConfig)
                        return s_innerTypesConfig;
                    var xe = new XElement("Types");
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "RawSkill"),
                        new XAttribute(KEYTypeName, "SkillEngine.SkillCore.RawSkill"), new XAttribute(KEYFileName, "SkillEngine.SkillCore")));
                    //Effectors
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Effector.Manager"),
                        new XAttribute(KEYTypeName, "SkillEngine.SkillCore.ManagerEffector"), new XAttribute(KEYFileName, "SkillEngine.SkillCore")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Effector.Player"),
                        new XAttribute(KEYTypeName, "SkillEngine.SkillCore.PlayerEffector"), new XAttribute(KEYFileName, "SkillEngine.SkillCore")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Effector.PlayerEvent"),
                      new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.FootballPlayerEventEffector"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Effector.Skill"),
                      new XAttribute(KEYTypeName, "SkillEngine.SkillCore.SkillEffector"), new XAttribute(KEYFileName, "SkillEngine.SkillCore")));
                    //Seekers
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Seeker.Id"),
                      new XAttribute(KEYTypeName, "SkillEngine.SkillCore.PlayerIdSeeker"), new XAttribute(KEYFileName, "SkillEngine.SkillCore")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Seeker.Motion"),
                        new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.FootballPlayerMotionSeeker"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Seeker.Graph"),
                       new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.FootballPlayerGraphSeeker"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Seeker.ManagerSkill"),
                       new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.ManagerSkillSeeker"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Seeker.PlayerSkill"),
                      new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.PlayerSkillSeeker"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));
                    //Filters
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Filter.Mid"),
                       new XAttribute(KEYTypeName, "SkillEngine.SkillCore.ManagerIdFilter"), new XAttribute(KEYFileName, "SkillEngine.SkillCore")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Filter.Motion"),
                        new XAttribute(KEYTypeName, "SkillEngine.SkillCore.PlayerMotionFilter"), new XAttribute(KEYFileName, "SkillEngine.SkillCore")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Filter.IdMotion"),
                       new XAttribute(KEYTypeName, "SkillEngine.SkillCore.PlayerIdMotionFilter"), new XAttribute(KEYFileName, "SkillEngine.SkillCore")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Filter.Position"),
                        new XAttribute(KEYTypeName, "SkillEngine.SkillCore.PlayerPositionFilter"), new XAttribute(KEYFileName, "SkillEngine.SkillCore")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Filter.Colour"),
                        new XAttribute(KEYTypeName, "SkillEngine.SkillCore.PlayerColourFilter"), new XAttribute(KEYFileName, "SkillEngine.SkillCore")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Filter.BallState"),
                        new XAttribute(KEYTypeName, "SkillEngine.SkillCore.PlayerBallStateFilter"), new XAttribute(KEYFileName, "SkillEngine.SkillCore")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Filter.ManagerStat"),
                      new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.ManagerStatFilter"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Filter.PlayerStat"),
                      new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.PlayerStatFilter"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Filter.Graph"),
                       new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.FootballPlayerGraphFilter"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Filter.GroundMotion"),
                       new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.PlayerGroundMotionFilter"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));
                    //Effects
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Effect.Boost"),
                        new XAttribute(KEYTypeName, "SkillEngine.SkillCore.BoostEffect"), new XAttribute(KEYFileName, "SkillEngine.SkillCore")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Effect.ClearCD"),
                       new XAttribute(KEYTypeName, "SkillEngine.SkillCore.ClearCDEffect"), new XAttribute(KEYFileName, "SkillEngine.SkillCore")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Effect.PropPlus"),
                       new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.FootballPropPlusEffect"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Effect.FactPropPlus"),
                        new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.FootballFactPropPlusEffect"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Effect.EventPropPlus"),
                        new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.FootballEventPropPlusEffect"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Effect.Blur"),
                        new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.FootballBlurEffect"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Effect.Foul"),
                        new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.FootballFoulEffect"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Effect.FoulPro"),
                      new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.FootballProFoulEffect"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Effect.Exec"),
                        new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.FootballExecEffect"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));
                    xe.Add(new XElement(KEYTypeNode, new XAttribute(KEYName, "Effect.ForceState"),
                        new XAttribute(KEYTypeName, "SkillEngine.SkillImpl.Football.FootballForceStateEffect"), new XAttribute(KEYFileName, "SkillEngine.SkillImpl.Football")));

                    s_innerTypesConfig = xe;
                }
            }
            return s_innerTypesConfig;
        }
        #endregion
    }
}
