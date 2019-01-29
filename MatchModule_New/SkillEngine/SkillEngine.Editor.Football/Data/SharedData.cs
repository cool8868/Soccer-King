using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SkillEngine.Editor.Football.Util;
using SkillEngine.Extern.Enum;
using SkillEngine.Extern.Enum.Football;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;

namespace SkillEngine.Editor.Football.Data
{
    public class SharedData
    {
        #region Cache
        public const string KEYAdd = "add";
        public const string KEYMemo = "__memo";
        public const string KEYOpenClipId = "__openClipId";
        public const string KEYModelId = "__modelId";
        public const string KEYClipId = "__clipId";
       
        static readonly BindItemData s_NAItem = new BindItemData(BindItemData.NACODE, BindItemData.NATEXT, string.Empty, string.Empty);
        #endregion

        #region Singleton
        static readonly object s_lockObj = new object();
        static SharedData s_instnce = null;
        public readonly bool InitFlag = false;
        public static SharedData Instance
        {
            get
            {
                if (null == s_instnce || !s_instnce.InitFlag)
                {
                    lock (s_lockObj)
                    {
                        if (null == s_instnce || !s_instnce.InitFlag)
                        {
                            s_instnce = new SharedData();
                        }
                    }
                }
                return s_instnce;
            }
        }
        #endregion

        #region .ctor
        private SharedData()
        {
            try
            {
                this.InitFlag = false;
                this.InitClassType();
                this.InitSkillSrcType();
                this.InitSkillSrcTypeInt();
                this.InitSkillActType();
                this.InitSkillTimeType();
                this.InitSkillFlag();
                this.InitSkillCastFlag();
                this.InitSkillParalFlag();
                this.InitSeekerJoinType();
                this.InitEffectLast();
                this.InitRedoLast();
                this.InitEffectRepeat();
                this.InitEffectRecycle();
                this.InitMotion();
                this.InitColour();
                this.InitPosition();
                this.InitTalentPosition();
                this.InitPropBuffId();
                this.InitBlurBuffId();
                this.InitFoulBuffId();
                this.InitExecBuffId();
                this.InitBoostType();
                this.InitBoostBuffId();
                this.InitExecType();
                this.InitEventType();
                this.InitEventTargetType();
                this.InitFactType();
                this.InitStaminaType();
                this.InitMangerSide();
                this.InitOwnManagerSide();
                this.InitOwnPlayerSide();
                this.InitBallSide();
                this.InitBallState();
                this.InitMotionSeekType();
                this.InitMotionFilterType();
                this.InitMotionDoneType();
                this.InitGraphSeekType();
                this.InitManagerStatType();
                this.InitPlayerStatType();
                this.InitForceState();
                this.InitGroundType();
                this.InitFlag = true;
            }
            catch (Exception ex)
            {
                LogUtil.Error("SharedData;Init", ex);
                this.InitFlag = false;
            }
        }
        #endregion

        #region ClassType
        static readonly List<BindItemData> s_lstClassType = new List<BindItemData>();
        void InitClassType()
        {
            s_lstClassType.Clear();
            s_lstClassType.Add(new BindItemData("Effector.Player", "作用于球员", "Effector", "|Player|PlugEffector|"));
            s_lstClassType.Add(new BindItemData("Effector.Manager", "作用于经理", "Effector", "|Manager|PlugEffector|"));
            s_lstClassType.Add(new BindItemData("Effector.Skill", "作用于技能", "Effector", "|Skill|"));
            s_lstClassType.Add(new BindItemData("Effector.PlayerEvent", "作用于球员的特定事件", "Effector", "|PlayerEvent|"));

            s_lstClassType.Add(new BindItemData("Seeker.Motion", "按球员关系查找", "Seeker", "|Player|"));
            s_lstClassType.Add(new BindItemData("Seeker.Id", "按球员Id查找", "Seeker", "|Player|"));
            s_lstClassType.Add(new BindItemData("Seeker.Graph", "按场上距离查找", "Seeker", "|Player|"));
            s_lstClassType.Add(new BindItemData("Seeker.ManagerSkill", "查找经理技能", "Seeker", "|Skill|"));
            s_lstClassType.Add(new BindItemData("Seeker.PlayerSkill", "查找球员技能", "Seeker", "|Skill|"));

            s_lstClassType.Add(new BindItemData("Filter.Motion", "球员动作", "Filter", "|Player|"));
            s_lstClassType.Add(new BindItemData("Filter.IdMotion", "球员Id和动作", "Filter", "|Player|"));
            s_lstClassType.Add(new BindItemData("Filter.Position", "球员位置", "Filter", "|Player|"));
            s_lstClassType.Add(new BindItemData("Filter.Colour", "球员颜色", "Filter", "|Player|"));
            s_lstClassType.Add(new BindItemData("Filter.BallState", "持球状态", "Filter", "|Player|"));
            s_lstClassType.Add(new BindItemData("Filter.ManagerStat", "经理统计", "Filter", "|Manager|Player|"));
            s_lstClassType.Add(new BindItemData("Filter.PlayerStat", "球员统计", "Filter", "|Player|"));
            s_lstClassType.Add(new BindItemData("Filter.Graph", "场上距离", "Filter", "|Player|"));

            s_lstClassType.Add(new BindItemData("Filter.Motion", "球员动作", "Trigger", "|Player|"));
            s_lstClassType.Add(new BindItemData("Filter.IdMotion", "球员Id和动作", "Trigger", "|Player|"));
            s_lstClassType.Add(new BindItemData("Filter.Position", "球员位置", "Trigger", "|Player|"));
            s_lstClassType.Add(new BindItemData("Filter.Colour", "球员颜色", "Trigger", "|Player|"));
            s_lstClassType.Add(new BindItemData("Filter.BallState", "持球状态", "Trigger", "|Player|"));
            s_lstClassType.Add(new BindItemData("Filter.ManagerStat", "经理统计", "Trigger", "|Player|"));
            s_lstClassType.Add(new BindItemData("Filter.PlayerStat", "球员统计", "Trigger", "|Player|"));
            s_lstClassType.Add(new BindItemData("Filter.Graph", "场上距离", "Trigger", "|Player|"));
            s_lstClassType.Add(new BindItemData("Filter.GroundMotion", "位置动作", "Trigger", "|Player|"));

            s_lstClassType.Add(new BindItemData("Effect.PropPlus", "属性加成", "Effect", "|Manager|Player|Skill|"));
            s_lstClassType.Add(new BindItemData("Effect.Blur", "异常状态", "Effect", "|Player|"));
            s_lstClassType.Add(new BindItemData("Effect.Foul", "犯规", "Effect", "|Player|"));
            s_lstClassType.Add(new BindItemData("Effect.FoulPro", "犯规并一定概率得牌", "Effect", "|Player|"));
            s_lstClassType.Add(new BindItemData("Effect.Boost", "技能加强免疫", "Effect", "|Manager|Player|Skill|"));
            s_lstClassType.Add(new BindItemData("Effect.FactPropPlus", "数量相关的属性加成", "Effect", "|Manager|Player|"));
            s_lstClassType.Add(new BindItemData("Effect.EventPropPlus", "特定事件的属性加成", "Effect", "|PlayerEvent|"));
            s_lstClassType.Add(new BindItemData("Effect.ForceState", "强制状态", "Effect", "|Player|"));
            s_lstClassType.Add(new BindItemData("Effect.ClearCD", "清除CD", "Effect", "|Player|Skill|"));
            s_lstClassType.Add(new BindItemData("Effect.Exec", "特殊效果", "Effect", "|Player|"));
        }
        public List<BindItemData> BindClassType(bool incNA, EnumClassRank classRank, EnumClassFlag classFlag)
        {
            string flag = classFlag == EnumClassFlag.None ? string.Empty : classFlag.ToString();
            if (classFlag == EnumClassFlag.PlayerEvent)
                return GetBindList(incNA, s_lstClassType, classRank.ToString(), EnumClassFlag.Player.ToString(), flag);
            return GetBindList(incNA, s_lstClassType, classRank.ToString(), flag);
        }
        #endregion

        #region SkillSrcType[]
        static readonly List<BindItemData> s_lstSkillSrcType = new List<BindItemData>();
        void InitSkillSrcType()
        {
            s_lstSkillSrcType.Clear();
            s_lstSkillSrcType.Add(new BindItemData(EnumSkillSrcType.PlayerAction.ToString(), "通用技能"));
            s_lstSkillSrcType.Add(new BindItemData(EnumSkillSrcType.PlayerStar.ToString(), "球星技能"));
            s_lstSkillSrcType.Add(new BindItemData(EnumSkillSrcType.EquipWash.ToString(), "球星封印"));
            s_lstSkillSrcType.Add(new BindItemData(EnumSkillSrcType.Coach.ToString(), "教练"));
            //s_lstSkillSrcType.Add(new BindItemData(EnumSkillSrcType.Equip.ToString(), "装备"));
            s_lstSkillSrcType.Add(new BindItemData(EnumSkillSrcType.EquipSoul.ToString(), "球魂"));
            s_lstSkillSrcType.Add(new BindItemData(EnumSkillSrcType.EquipSuit.ToString(), "套装"));
            s_lstSkillSrcType.Add(new BindItemData(EnumSkillSrcType.ManagerTalent.ToString(), "经理天赋"));
            s_lstSkillSrcType.Add(new BindItemData(EnumSkillSrcType.Will.ToString(), "意志"));
            s_lstSkillSrcType.Add(new BindItemData(EnumSkillSrcType.PlayerSuit.ToString(), "组合"));
            s_lstSkillSrcType.Add(new BindItemData(EnumSkillSrcType.StarWakeup.ToString(), "球员觉醒"));
            s_lstSkillSrcType.Add(new BindItemData(EnumSkillSrcType.StarSign.ToString(), "星座技能"));
            s_lstSkillSrcType.Add(new BindItemData(EnumSkillSrcType.GuildSkill.ToString(), "公会技能"));

        }
        public List<BindItemData> BindSkillSrcType()
        {
            return GetBindList(false, s_lstSkillSrcType, string.Empty);
        }
        public List<BindItemData> BindSkillSrcType(bool incNa)
        {
            return GetBindList(incNa, s_lstSkillSrcType, string.Empty);
        }
        #endregion

        #region SkillSrcTypeInt[]
        static readonly List<BindItemData> s_lstSkillSrcTypeInt = new List<BindItemData>();
        void InitSkillSrcTypeInt()
        {
            s_lstSkillSrcTypeInt.Clear();
            s_lstSkillSrcTypeInt.Add(new BindItemData(((int)EnumSkillSrcType.PlayerAction).ToString(), "通用技能"));
            s_lstSkillSrcTypeInt.Add(new BindItemData(((int)EnumSkillSrcType.PlayerStar).ToString(), "球星技能"));
            s_lstSkillSrcTypeInt.Add(new BindItemData(((int)EnumSkillSrcType.EquipWash).ToString(), "球星封印"));
            s_lstSkillSrcTypeInt.Add(new BindItemData(((int)EnumSkillSrcType.Coach).ToString(), "教练"));
            //s_lstSkillSrcTypeInt.Add(new BindItemData(EnumSkillSrcType.Equip.ToString(), "装备"));
            s_lstSkillSrcTypeInt.Add(new BindItemData(((int)EnumSkillSrcType.EquipSoul).ToString(), "球魂"));
            s_lstSkillSrcTypeInt.Add(new BindItemData(((int)EnumSkillSrcType.EquipSuit).ToString(), "套装"));
            s_lstSkillSrcTypeInt.Add(new BindItemData(((int)EnumSkillSrcType.ManagerTalent).ToString(), "经理天赋"));
            s_lstSkillSrcTypeInt.Add(new BindItemData(((int)EnumSkillSrcType.Will).ToString(), "意志"));
            s_lstSkillSrcTypeInt.Add(new BindItemData(((int)EnumSkillSrcType.PlayerSuit).ToString(), "组合"));
            s_lstSkillSrcTypeInt.Add(new BindItemData(((int)EnumSkillSrcType.StarWakeup).ToString(), "球员觉醒"));
            s_lstSkillSrcTypeInt.Add(new BindItemData(((int)EnumSkillSrcType.StarSign).ToString(), "星座技能"));
        }
        public List<BindItemData> BindSkillSrcTypeInt()
        {
            return GetBindList(false, s_lstSkillSrcTypeInt, string.Empty);
        }
        public List<BindItemData> BindSkillSrcTypeInt(bool incNa)
        {
            return GetBindList(incNa, s_lstSkillSrcTypeInt, string.Empty);
        }
        #endregion

        #region SkillActType[]
        static readonly List<BindItemData> s_lstSkillActType = new List<BindItemData>();
        void InitSkillActType()
        {
            s_lstSkillActType.Clear();
            s_lstSkillActType.Add(new BindItemData(((int)EnumSkillActType.None).ToString(), "任意"));
            s_lstSkillActType.Add(new BindItemData(EnumSkillActType.Shoot.ToString(), "射门技能"));
            s_lstSkillActType.Add(new BindItemData(EnumSkillActType.Defence.ToString(), "防守技能"));
            s_lstSkillActType.Add(new BindItemData(EnumSkillActType.Organize.ToString(), "组织技能"));
            s_lstSkillActType.Add(new BindItemData(EnumSkillActType.BreakThrough.ToString(), "过人技能"));
            s_lstSkillActType.Add(new BindItemData(EnumSkillActType.GoalKeeping.ToString(), "守门技能"));
        }
        public List<BindItemData> BindSkillActType()
        {
            return GetBindList(false, s_lstSkillActType, string.Empty);
        }
        #endregion

        #region SkillFlag
        static readonly List<BindItemData> s_lstSkillFlag = new List<BindItemData>();
        void InitSkillFlag()
        {
            s_lstSkillFlag.Clear();
            s_lstSkillFlag.Add(new BindItemData(EnumSkillFlag.None.ToString(), "<任意>"));
            s_lstSkillFlag.Add(new BindItemData(EnumSkillFlag.Buff.ToString(), "增益技能"));
            s_lstSkillFlag.Add(new BindItemData(EnumSkillFlag.Debuff.ToString(), "减益技能"));
        }
        public List<BindItemData> BindSkillFlag()
        {
            return GetBindList(false, s_lstSkillFlag, string.Empty);
        }
        #endregion

        #region SkillTimeType
        static readonly List<BindItemData> s_lstSkillTimeType = new List<BindItemData>();
        void InitSkillTimeType()
        {
            s_lstSkillTimeType.Clear();
            s_lstSkillTimeType.Add(new BindItemData(EnumSkillTimeType.Round.ToString(), "回合"));
            s_lstSkillTimeType.Add(new BindItemData(EnumSkillTimeType.Minute.ToString(), "分钟"));
            s_lstSkillTimeType.Add(new BindItemData(EnumSkillTimeType.Section.ToString(), "半场"));
            s_lstSkillTimeType.Add(new BindItemData(EnumSkillTimeType.Session.ToString(), "场前"));
            s_lstSkillTimeType.Add(new BindItemData(EnumSkillTimeType.PreDecide.ToString(), "回合思考前"));
        }
        public List<BindItemData> BindSkillTimeType()
        {
            return GetBindList(false, s_lstSkillTimeType, string.Empty);
        }
        #endregion

        #region SkillCastFlag
        static readonly List<BindItemData> s_lstSkillCastFlag = new List<BindItemData>();
        void InitSkillCastFlag()
        {
            s_lstSkillCastFlag.Clear();
            s_lstSkillCastFlag.Add(new BindItemData(true.ToString().ToLower(), "球员视角"));
            s_lstSkillCastFlag.Add(new BindItemData(false.ToString().ToLower(), "经理视角"));
        }
        public List<BindItemData> BindSkillCastFlag()
        {
            return GetBindList(false, s_lstSkillCastFlag, string.Empty);
        }
        #endregion

        #region SkillParalFlag
        static readonly List<BindItemData> s_lstSkillParalFlag = new List<BindItemData>();
        void InitSkillParalFlag()
        {
            s_lstSkillParalFlag.Clear();
            s_lstSkillParalFlag.Add(new BindItemData("0", "互斥发动"));
            s_lstSkillParalFlag.Add(new BindItemData("1", "限量同时"));
            s_lstSkillParalFlag.Add(new BindItemData("2", "完全同时"));
        }
        public List<BindItemData> BindSkillParalFlag()
        {
            return GetBindList(false, s_lstSkillParalFlag, string.Empty);
        }
        #endregion

        #region SeekerJoinType
        static readonly List<BindItemData> s_lstSeekerJoinType = new List<BindItemData>();
        void InitSeekerJoinType()
        {
            s_lstSeekerJoinType.Clear();
            s_lstSeekerJoinType.Add(new BindItemData(EnumSeekJoinType.Or.ToString(), "或者"));
            s_lstSeekerJoinType.Add(new BindItemData(EnumSeekJoinType.And.ToString(), "并且"));
            s_lstSeekerJoinType.Add(new BindItemData(EnumSeekJoinType.Nest.ToString(), "嵌套"));
        }
        public List<BindItemData> BindSeekerJoinType()
        {
            return GetBindList(false, s_lstSeekerJoinType, string.Empty);
        }
        #endregion

        #region EffectLast
        static readonly List<BindItemData> s_lstEffectLast = new List<BindItemData>();
        void InitEffectLast()
        {
            s_lstEffectLast.Clear();
            s_lstEffectLast.Add(new BindItemData(((int)EnumBuffLast.Action).ToString(), "动作持续时间", string.Empty, "|Main|Undo|Blur|"));
            s_lstEffectLast.Add(new BindItemData(((int)EnumBuffLast.Round).ToString(), "回合", string.Empty, "|Main|Undo|Blur|"));
            s_lstEffectLast.Add(new BindItemData(((int)EnumBuffLast.Minutes).ToString(), "分钟", string.Empty, "|Main|Undo|Blur|"));
            s_lstEffectLast.Add(new BindItemData(((int)EnumBuffLast.TillSectionEnd).ToString(), "直至节末", string.Empty, "|Main|Undo|Blur|"));
            s_lstEffectLast.Add(new BindItemData(((int)EnumFootBallBuffLast.TillUndo).ToString(), "直至回收", string.Empty, "|Undo|"));
            s_lstEffectLast.Add(new BindItemData(((int)EnumFootBallBuffLast.StunLevel1).ToString(), "晕眩等级1", string.Empty, "|Blur|"));
            s_lstEffectLast.Add(new BindItemData(((int)EnumFootBallBuffLast.StunLevel2).ToString(), "晕眩等级2", string.Empty, "|Blur|"));
        }
        public List<BindItemData> BindEffectLast(bool incNA, string flag)
        {
            return GetBindList(incNA, s_lstEffectLast, string.Empty, flag);
        }
        #endregion

        #region RedoLast
        static readonly List<BindItemData> s_lstRedoLast = new List<BindItemData>();
        void InitRedoLast()
        {
            s_lstRedoLast.Clear();
            s_lstRedoLast.Add(new BindItemData(((int)EnumBuffLast.Action).ToString(), "未设定", string.Empty, string.Empty));
            s_lstRedoLast.Add(new BindItemData(((int)EnumBuffLast.Round).ToString(), "回合", string.Empty, string.Empty));
            s_lstRedoLast.Add(new BindItemData(((int)EnumBuffLast.Minutes).ToString(), "分钟", string.Empty, string.Empty));
        }
        public List<BindItemData> BindRedoLast(bool incNA, string flag)
        {
            return GetBindList(incNA, s_lstRedoLast, string.Empty, flag);
        }
        #endregion

        #region EffectRepeat
        static readonly List<BindItemData> s_lstEffectRepeat = new List<BindItemData>();
        void InitEffectRepeat()
        {
            s_lstEffectRepeat.Clear();
            s_lstEffectRepeat.Add(new BindItemData(((int)EnumBuffRepeat.Once).ToString(), "非重复可叠加"));
            s_lstEffectRepeat.Add(new BindItemData(((int)EnumBuffRepeat.OverTime).ToString(), "重复可叠加"));
            s_lstEffectRepeat.Add(new BindItemData(((int)EnumBuffRepeat.PickOnce).ToString(), "非叠加"));
        }
        public List<BindItemData> BindEffectRepeat()
        {
            return GetBindList(false, s_lstEffectRepeat, string.Empty);
        }
        #endregion

        #region EffectRecycle
        static readonly List<BindItemData> s_lstEffectRecycle = new List<BindItemData>();
        void InitEffectRecycle()
        {
            s_lstEffectRecycle.Clear();
            s_lstEffectRecycle.Add(new BindItemData(false.ToString().ToLower(), "非回收"));
            s_lstEffectRecycle.Add(new BindItemData(true.ToString().ToLower().ToLower(), "回收"));
        }
        public List<BindItemData> BindEffectRecycle()
        {
            return GetBindList(false, s_lstEffectRecycle, string.Empty);
        }
        #endregion

        #region Motion[]
        static readonly List<BindItemData> s_lstMotion = new List<BindItemData>();
        void InitMotion()
        {
            s_lstMotion.Clear();
            s_lstMotion.Add(new BindItemData(((int)EnumMotionState.Idle).ToString(), "站立"));
            s_lstMotion.Add(new BindItemData(((int)EnumMotionState.Walk).ToString(), "走动"));
            s_lstMotion.Add(new BindItemData(((int)EnumMotionState.Chace).ToString(), "跑动"));
            s_lstMotion.Add(new BindItemData(((int)EnumMotionState.DefaultShoot).ToString(), "普通射门"));
            s_lstMotion.Add(new BindItemData(((int)EnumMotionState.VolleyShoot).ToString(), "大力射门"));
            s_lstMotion.Add(new BindItemData(((int)EnumMotionState.FreekickShoot).ToString(), "任意球射门"));
            s_lstMotion.Add(new BindItemData(((int)EnumMotionState.RebelShoot).ToString(), "乌龙射门"));
            s_lstMotion.Add(new BindItemData(((int)EnumMotionState.ShortPass).ToString(), "短传"));
            s_lstMotion.Add(new BindItemData(((int)EnumMotionState.LongPass).ToString(), "长传"));
            s_lstMotion.Add(new BindItemData(((int)EnumMotionState.DefaultDribble).ToString(), "带球"));
            s_lstMotion.Add(new BindItemData(((int)EnumMotionState.BreakThrough).ToString(), "过人"));
            s_lstMotion.Add(new BindItemData(((int)EnumMotionState.Interruption).ToString(), "抢断"));
            s_lstMotion.Add(new BindItemData(((int)EnumMotionState.SlideTackle).ToString(), "铲球"));
            s_lstMotion.Add(new BindItemData(((int)EnumMotionState.DiveBall).ToString(), "扑救"));
            s_lstMotion.Add(new BindItemData(((int)EnumMotionState.GKHoldBall).ToString(), "守门员持球站立"));
        }
        public List<BindItemData> BindMotion()
        {
            return GetBindList(false, s_lstMotion, string.Empty);
        }
        #endregion

        #region Colour[]
        static readonly List<BindItemData> s_lstColour = new List<BindItemData>();
        void InitColour()
        {
            s_lstColour.Clear();
            s_lstColour.Add(new BindItemData(((int)EnumPlayerColour.Gold).ToString(), "金卡"));
            s_lstColour.Add(new BindItemData(((int)EnumPlayerColour.Orange).ToString(), "橙卡"));
            s_lstColour.Add(new BindItemData(((int)EnumPlayerColour.Purple).ToString(), "紫卡"));
            s_lstColour.Add(new BindItemData(((int)EnumPlayerColour.Blue).ToString(), "蓝卡"));
            s_lstColour.Add(new BindItemData(((int)EnumPlayerColour.Green).ToString(), "绿卡"));
        }
        public List<BindItemData> BindColour()
        {
            return GetBindList(false, s_lstColour, string.Empty);
        }
        #endregion

        #region Position[]
        static readonly List<BindItemData> s_lstPosition = new List<BindItemData>();
        void InitPosition()
        {
            s_lstPosition.Clear();
            s_lstPosition.Add(new BindItemData(((int)EnumPlayerPosition.Goalkeeper).ToString(), "守门员"));
            s_lstPosition.Add(new BindItemData(((int)EnumPlayerPosition.Fullback).ToString(), "后卫"));
            s_lstPosition.Add(new BindItemData(((int)EnumPlayerPosition.Midfielder).ToString(), "中锋"));
            s_lstPosition.Add(new BindItemData(((int)EnumPlayerPosition.Forward).ToString(), "前锋"));
        }
        public List<BindItemData> BindPosition()
        {
            return GetBindList(false, s_lstPosition, string.Empty);
        }
        #endregion

        #region TalentPosition[]
        static readonly List<BindItemData> s_lstTalentPosition = new List<BindItemData>();
        void InitTalentPosition()
        {
            s_lstTalentPosition.Clear();
            s_lstTalentPosition.Add(new BindItemData("0", "初级进攻强化位"));
            s_lstTalentPosition.Add(new BindItemData("1", "中级进攻强化位"));
            s_lstTalentPosition.Add(new BindItemData("2", "高级进攻强化位"));
            s_lstTalentPosition.Add(new BindItemData("3", "初级防守强化位"));
            s_lstTalentPosition.Add(new BindItemData("4", "中级防守强化位"));
            s_lstTalentPosition.Add(new BindItemData("5", "高级防守强化位"));
            s_lstTalentPosition.Add(new BindItemData("6", "初级通用强化位"));
            s_lstTalentPosition.Add(new BindItemData("7", "中级通用强化位"));
            s_lstTalentPosition.Add(new BindItemData("8", "高级通用强化位"));
        }
        public List<BindItemData> BindTalentPosition()
        {
            return GetBindList(false, s_lstTalentPosition, string.Empty);
        }
        #endregion

        #region PropBuffId[]
        static readonly List<BindItemData> s_lstPropBuffId = new List<BindItemData>();
        void InitPropBuffId()
        {
            s_lstPropBuffId.Clear();
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.Speed).ToString(), "速度"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.Shooting).ToString(), "射门"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.FreeKick).ToString(), "任意球"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.Balance).ToString(), "控制"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.Stamina).ToString(), "体质"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.Bounce).ToString(), "弹跳"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.Aggression).ToString(), "侵略性"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.Disturb).ToString(), "干扰"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.Interception).ToString(), "抢断"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.Dribble).ToString(), "控球"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.Passing).ToString(), "传球"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.Mentality).ToString(), "意识"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.Reflexes).ToString(), "反应"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.Positioning).ToString(), "位置感"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.Handling).ToString(), "手控球"));
            //s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.Acceleration).ToString(), "加速度"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.PassChooseRate).ToString(), "传球选择率"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.DribbleChooseRate).ToString(), "带球选择率"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.ShootChooseRate).ToString(), "射门选择率"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.StealChooseRate).ToString(), "抢断选择率"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.PassSuccRate).ToString(), "传球成功率"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.DribbleSuccRate).ToString(), "带球成功率"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.ShootSuccRate).ToString(), "射门成功率"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.StealSuccRate).ToString(), "抢断成功率"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.DiveSuccRate).ToString(), "扑救成功率"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.OutHandRate).ToString(), "脱手概率"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.TurnStealRate).ToString(), "反抢概率"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.ShootRange).ToString(), "射门范围"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.DisturbRange).ToString(), "干扰半径"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.StealRange).ToString(), "防守半径"));
            s_lstPropBuffId.Add(new BindItemData(((int)EnumBuffCode.StaminaLossSpeed).ToString(), "体能流失速度"));
           
        }
        public List<BindItemData> BindPropBuffId()
        {
            return GetBindList(false, s_lstPropBuffId, string.Empty);
        }
        #endregion

        #region BlurBuffId
        static readonly List<BindItemData> s_lstBlurBuffId = new List<BindItemData>();
        void InitBlurBuffId()
        {
            s_lstBlurBuffId.Clear();
            s_lstBlurBuffId.Add(new BindItemData(EnumBlurBuffCode.Stand.ToString(), "静止"));
            s_lstBlurBuffId.Add(new BindItemData(EnumBlurBuffCode.Falldown.ToString(), "倒地"));
            s_lstBlurBuffId.Add(new BindItemData(EnumBlurBuffCode.Puzzle.ToString(), "困惑"));
            s_lstBlurBuffId.Add(new BindItemData(EnumBlurBuffCode.Stun.ToString(), "晕眩"));
            s_lstBlurBuffId.Add(new BindItemData(EnumBlurBuffCode.OutHand.ToString(), "脱手"));
            s_lstBlurBuffId.Add(new BindItemData(EnumBlurBuffCode.Rebel.ToString(), "迷惑"));
            s_lstBlurBuffId.Add(new BindItemData(EnumBlurBuffCode.Silence.ToString(), "沉默"));
            s_lstBlurBuffId.Add(new BindItemData(EnumBlurBuffCode.Injure.ToString(), "致伤"));
            s_lstBlurBuffId.Add(new BindItemData(EnumBlurBuffCode.Disable.ToString(), "致残"));
        }
        public List<BindItemData> BindBlurBuffId()
        {
            return GetBindList(false, s_lstBlurBuffId, string.Empty);
        }
        #endregion

        #region FoulBuffId
        static readonly List<BindItemData> s_lstFoulBuffId = new List<BindItemData>();
        void InitFoulBuffId()
        {
            s_lstFoulBuffId.Clear();
            s_lstFoulBuffId.Add(new BindItemData(EnumFoulBuffCode.FoulNormal.ToString(), "普通犯规"));
            s_lstFoulBuffId.Add(new BindItemData(EnumFoulBuffCode.FoulYellow.ToString(), "黄牌", "card", string.Empty));
            s_lstFoulBuffId.Add(new BindItemData(EnumFoulBuffCode.FoulRed.ToString(), "红牌","card",string.Empty));
        }
        public List<BindItemData> BindFoulBuffId()
        {
            return GetBindList(false, s_lstFoulBuffId, string.Empty);
        }
        public List<BindItemData> BindFoulProBuffId()
        {
            return GetBindList(false, s_lstFoulBuffId, "card");
        }
        #endregion

        #region ExecBuffId
        static readonly List<BindItemData> s_lstExecBuffId = new List<BindItemData>();
        void InitExecBuffId()
        {
            s_lstExecBuffId.Clear();
            s_lstExecBuffId.Add(new BindItemData(EnumSpecEffect.HighPass.ToString(), "下次传球不被拦截"));
            s_lstExecBuffId.Add(new BindItemData(EnumSpecEffect.PassOutside.ToString(), "破坏出界"));
            s_lstExecBuffId.Add(new BindItemData(EnumSpecEffect.Reborn.ToString(), "伤残立即复活"));
            s_lstExecBuffId.Add(new BindItemData(EnumSpecEffect.FalldownThenInjure.ToString(), "倒地后致伤"));
        }
        public List<BindItemData> BindExecBuffId()
        {
            return GetBindList(false, s_lstExecBuffId, string.Empty);
        }
        #endregion

        #region BoostType
        static readonly List<BindItemData> s_lstBoostType = new List<BindItemData>();
        void InitBoostType()
        {
            s_lstBoostType.Clear();
            s_lstBoostType.Add(new BindItemData(EnumBoostType.AmpLast.ToString(), "加强持续时间"));
            s_lstBoostType.Add(new BindItemData(EnumBoostType.AmpRate.ToString(), "加强效果概率"));
            s_lstBoostType.Add(new BindItemData(EnumBoostType.AmpValue.ToString(), "加强效果值"));
            s_lstBoostType.Add(new BindItemData(EnumBoostType.EaseLast.ToString(), "减弱持续时间"));
            s_lstBoostType.Add(new BindItemData(EnumBoostType.EaseRate.ToString(), "减弱效果概率"));
            s_lstBoostType.Add(new BindItemData(EnumBoostType.EaseValue.ToString(), "减弱效果值"));
            s_lstBoostType.Add(new BindItemData(EnumBoostType.AntiRate.ToString(), "免疫概率"));
            s_lstBoostType.Add(new BindItemData(EnumBoostType.AntiDebuff.ToString(), "免疫所有Debuff概率"));
            s_lstBoostType.Add(new BindItemData(EnumBoostType.PureBuff.ToString(), "无视免疫概率"));
            s_lstBoostType.Add(new BindItemData(EnumBoostType.SkillCD.ToString(), "技能CD"));
            s_lstBoostType.Add(new BindItemData(EnumBoostType.SkillRate.ToString(), "技能触发率"));
        }
        public List<BindItemData> BindBoostType()
        {
            return GetBindList(false, s_lstBoostType, string.Empty);
        }
        #endregion

        #region BoostBuffId[]
        static readonly List<BindItemData> s_lstBoostBuffId = new List<BindItemData>();
        void InitBoostBuffId()
        {
            s_lstBoostBuffId.Clear();
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBoostRootType.PropBuffLast).ToString(), "增益属性持续时间", "", "|AmpLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBoostRootType.PropDebuffLast).ToString(), "减益属性持续时间", "", "|AmpLast|EaseLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBoostRootType.BlurLast).ToString(), "异常状态持续时间", "", "|AmpLast|EaseLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBoostRootType.BlurRate).ToString(), "异常状态概率", "", "|AmpRate|EaseRate|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBoostRootType.FoulRate).ToString(), "犯规概率", "", "|AmpRate|EaseRate|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBoostRootType.PropBuffValue).ToString(), "增益属性效果值", "", "|AmpValue|EaseValue|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBoostRootType.PropDebuffValue).ToString(), "减益属性效果值", "", "|AmpValue|EaseValue|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBoostRootType.AntiPropDebuff).ToString(), "免疫减益属性", "", "|AntiRate|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBoostRootType.AntiBlur).ToString(), "免疫异常状态", "", "|AntiRate|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBoostRootType.AntiFoul).ToString(), "免疫犯规", "", "|AntiRate|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBoostRootType.TurnFoul).ToString(), "反弹犯规", "", "|AntiRate|"));

            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBuffCode.Speed).ToString(), "速度属性", "", "|AmpValue|EaseValue|AmpLast|EaseLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBuffCode.Shooting).ToString(), "射门属性", "", "|AmpValue|EaseValue|AmpLast|EaseLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBuffCode.FreeKick).ToString(), "任意球属性", "", "|AmpValue|EaseValue|AmpLast|EaseLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBuffCode.Balance).ToString(), "控制属性", "", "|AmpValue|EaseValue|AmpLast|EaseLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBuffCode.Stamina).ToString(), "体质属性", "", "|AmpValue|EaseValue|AmpLast|EaseLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBuffCode.Bounce).ToString(), "弹跳属性", "", "|AmpValue|EaseValue|AmpLast|EaseLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBuffCode.Aggression).ToString(), "侵略性属性", "", "|AmpValue|EaseValue|AmpLast|EaseLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBuffCode.Disturb).ToString(), "干扰属性", "", "|AmpValue|EaseValue|AmpLast|EaseLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBuffCode.Interception).ToString(), "抢断属性", "", "|AmpValue|EaseValue|AmpLast|EaseLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBuffCode.Dribble).ToString(), "控球属性", "", "|AmpValue|EaseValue|AmpLast|EaseLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBuffCode.Passing).ToString(), "传球属性", "", "|AmpValue|EaseValue|AmpLast|EaseLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBuffCode.Mentality).ToString(), "意识属性", "", "|AmpValue|EaseValue|AmpLast|EaseLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBuffCode.Reflexes).ToString(), "反应属性", "", "|AmpValue|EaseValue|AmpLast|EaseLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBuffCode.Positioning).ToString(), "位置感属性", "", "|AmpValue|EaseValue|AmpLast|EaseLast|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBuffCode.Handling).ToString(), "手控球属性", "", "|AmpValue|EaseValue|AmpLast|EaseLast|"));

            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBlurBuffCode.Stand).ToString(), "静止", "", "|AmpLast|EaseLast|AmpRate|EaseRate|AntiRate|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBlurBuffCode.Falldown).ToString(), "倒地", "", "|AmpLast|EaseLast|AmpRate|EaseRate|AntiRate|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBlurBuffCode.Puzzle).ToString(), "困惑", "", "|AmpLast|EaseLast|AmpRate|EaseRate|AntiRate|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBlurBuffCode.Stun).ToString(), "晕眩", "", "|AmpLast|EaseLast|AmpRate|EaseRate|AntiRate|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBlurBuffCode.OutHand).ToString(), "脱手", "", "|AmpLast|EaseLast|AmpRate|EaseRate|AntiRate|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBlurBuffCode.Rebel).ToString(), "迷惑", "", "|AmpLast|EaseLast|AmpRate|EaseRate|AntiRate|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBlurBuffCode.Silence).ToString(), "沉默", "", "|AmpLast|EaseLast|AmpRate|EaseRate|AntiRate|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBlurBuffCode.Injure).ToString(), "致伤", "", "|AmpRate|EaseRate|AntiRate|"));
            s_lstBoostBuffId.Add(new BindItemData(((int)EnumBlurBuffCode.Disable).ToString(), "致残", "", "|AmpRate|EaseRate|AntiRate|"));
           
        }
        public List<BindItemData> BindBoostBuffId(string boostType)
        {
            return GetBindList("0", "<无>", s_lstBoostBuffId, string.Empty, boostType);
        }
        #endregion

        #region ExecType
        static readonly List<BindItemData> s_lstExecType = new List<BindItemData>();
        void InitExecType()
        {
            s_lstExecType.Clear();
            s_lstExecType.Add(new BindItemData(EnumBuffExec.IgnoreHit.ToString(), "一次随机无需命中"));
            s_lstExecType.Add(new BindItemData(EnumBuffExec.MustHit.ToString(), "一次随机且命中"));
            s_lstExecType.Add(new BindItemData(EnumBuffExec.None.ToString(), "无需执行"));
        }
        public List<BindItemData> BindExecType()
        {
            return GetBindList(false, s_lstExecType, string.Empty);
        }
        #endregion

        #region EventType
        static readonly List<BindItemData> s_lstEventType = new List<BindItemData>();
        void InitEventType()
        {
            s_lstEventType.Clear();
            s_lstEventType.Add(new BindItemData(EnumBuffEventType.Blur.ToString(), "每次受伤下场时"));
            s_lstEventType.Add(new BindItemData(EnumBuffEventType.Foul.ToString(), "每次犯规时"));
        }
        public List<BindItemData> BindEventType()
        {
            return GetBindList(false, s_lstEventType, string.Empty);
        }
        #endregion

        #region EventTargetSide
        static readonly List<BindItemData> s_lstEventTgtType = new List<BindItemData>();
        void InitEventTargetType()
        {
            s_lstEventTgtType.Clear();
            s_lstEventTgtType.Add(new BindItemData(EnumEventTargetSide.OwnPlayer.ToString(), "自己"));
            s_lstEventTgtType.Add(new BindItemData(EnumEventTargetSide.OppPlayer.ToString(), "对位球员"));
            s_lstEventTgtType.Add(new BindItemData(EnumEventTargetSide.OwnManager.ToString(), "己方"));
            s_lstEventTgtType.Add(new BindItemData(EnumEventTargetSide.OppManager.ToString(), "对方"));
        }
        public List<BindItemData> BindEventTagetType()
        {
            return GetBindList(false, s_lstEventTgtType, string.Empty);
        }
        #endregion

        #region FactType
        static readonly List<BindItemData> s_lstFactType = new List<BindItemData>();
        void InitFactType()
        {
            s_lstFactType.Clear();
            s_lstFactType.Add(new BindItemData(EnumBuffFact.ColourQty.ToString(), "球员颜色的数量"));
            s_lstFactType.Add(new BindItemData(EnumBuffFact.IncDribbleMinutes.ToString(), "累计控球分钟"));
            s_lstFactType.Add(new BindItemData(EnumBuffFact.CombDribbleMinutes.ToString(), "连续控球分钟"));
        }
        public List<BindItemData> BindFactType()
        {
            return GetBindList(false, s_lstFactType, string.Empty);
        }
        #endregion

        #region StaminaType
        static readonly List<BindItemData> s_lstStaminaType = new List<BindItemData>();
        void InitStaminaType()
        {
            s_lstStaminaType.Clear();
            s_lstStaminaType.Add(new BindItemData(false.ToString().ToLower(), "体力绝对值"));
            s_lstStaminaType.Add(new BindItemData(true.ToString().ToLower(), "体力百分比"));
        }
        public List<BindItemData> BindStaminaType()
        {
            return GetBindList(false, s_lstStaminaType, string.Empty);
        }
        #endregion

        #region MangerSide
        static readonly List<BindItemData> s_lstMangerSide = new List<BindItemData>();
        void InitMangerSide()
        {
            s_lstMangerSide.Clear();
            s_lstMangerSide.Add(new BindItemData(((int)EnumOwnSide.Own).ToString(), "己方"));
            s_lstMangerSide.Add(new BindItemData(((int)EnumOwnSide.Opp).ToString(), "对方"));
        }
        public List<BindItemData> BindMangerSide()
        {
            return GetBindList(false, s_lstMangerSide, string.Empty);
        }
        #endregion

        #region OwnSide
        static readonly List<BindItemData> s_lstOwnManagerSide = new List<BindItemData>();
        void InitOwnManagerSide()
        {
            s_lstOwnManagerSide.Clear();
            s_lstOwnManagerSide.Add(new BindItemData(EnumOwnSide.Own.ToString(), "己方经理"));
            s_lstOwnManagerSide.Add(new BindItemData(EnumOwnSide.Opp.ToString(), "对方经理"));
        }
        public List<BindItemData> BindOwnManagerSide()
        {
            return GetBindList(false, s_lstOwnManagerSide, string.Empty);
        }
        #endregion

        #region OwnSide
        static readonly List<BindItemData> s_lstOwnPlayerSide = new List<BindItemData>();
        void InitOwnPlayerSide()
        {
            s_lstOwnPlayerSide.Clear();
            s_lstOwnPlayerSide.Add(new BindItemData(EnumOwnSide.Own.ToString(), "己方"));
            s_lstOwnPlayerSide.Add(new BindItemData(EnumOwnSide.Opp.ToString(), "对方"));
        }
        public List<BindItemData> BindOwnPlayerSide()
        {
            return GetBindList(false, s_lstOwnPlayerSide, string.Empty);
        }
        #endregion

        #region BallSide
        static readonly List<BindItemData> s_lstBallSide = new List<BindItemData>();
        void InitBallSide()
        {
            s_lstBallSide.Clear();
            s_lstBallSide.Add(new BindItemData(EnumBallSide.None.ToString(), "任意"));
            s_lstBallSide.Add(new BindItemData(EnumBallSide.Atk.ToString(), "进攻方"));
            s_lstBallSide.Add(new BindItemData(EnumBallSide.Def.ToString(), "防守方"));
        }
        public List<BindItemData> BindBallSide()
        {
            return GetBindList(false, s_lstBallSide, string.Empty);
        }
        #endregion

        #region BallState
        static readonly List<BindItemData> s_lstBallState = new List<BindItemData>();
        void InitBallState()
        {
            s_lstBallState.Clear();
            s_lstBallState.Add(new BindItemData(EnumBallState.None.ToString(), "任意"));
            s_lstBallState.Add(new BindItemData(EnumBallState.HoldBall.ToString(), "持球"));
            s_lstBallState.Add(new BindItemData(EnumBallState.OffBall.ToString(), "无球"));
        }
        public List<BindItemData> BindBallState()
        {
            return GetBindList(false, s_lstBallState, string.Empty);
        }
        #endregion

        #region MotionSeekType
        static readonly List<BindItemData> s_lstMotionSeekType = new List<BindItemData>();
        void InitMotionSeekType()
        {
            s_lstMotionSeekType.Clear();
            s_lstMotionSeekType.Add(new BindItemData(EnumMotionSeekType.OwnPlayer.ToString(), "自己"));
            s_lstMotionSeekType.Add(new BindItemData(EnumMotionSeekType.OppPlayer.ToString(), "对位球员"));
            s_lstMotionSeekType.Add(new BindItemData(EnumMotionSeekType.PairingPlayer.ToString(), "传球目标"));
            s_lstMotionSeekType.Add(new BindItemData(EnumMotionSeekType.OppPairingPlayer.ToString(), "传球目标对位球员"));
            s_lstMotionSeekType.Add(new BindItemData(EnumMotionSeekType.PairedPlayer.ToString(), "传球来源"));
            //s_lstMotionSeekType.Add(new BindItemData(EnumMotionSeekType.AssistPlayer.ToString(), "助攻球员"));
            s_lstMotionSeekType.Add(new BindItemData(EnumMotionSeekType.OwnBallHandler.ToString(), "己方持球人"));
            s_lstMotionSeekType.Add(new BindItemData(EnumMotionSeekType.OppBallHandler.ToString(), "对方持球人"));
            s_lstMotionSeekType.Add(new BindItemData(EnumMotionSeekType.OwnGoalKeeper.ToString(), "己方门将"));
            s_lstMotionSeekType.Add(new BindItemData(EnumMotionSeekType.OppGoalKeeper.ToString(), "对方门将"));
            s_lstMotionSeekType.Add(new BindItemData(EnumMotionSeekType.OwnMates.ToString(), "队友"));
            s_lstMotionSeekType.Add(new BindItemData(EnumMotionSeekType.OwnTeam.ToString(), "己方所有成员"));
            s_lstMotionSeekType.Add(new BindItemData(EnumMotionSeekType.OppTeam.ToString(), "对方所有成员"));

        }
        public List<BindItemData> BindMotionSeekType()
        {
            return GetBindList(false, s_lstMotionSeekType, string.Empty);
        }
        #endregion

        #region MotionFilterType
        static readonly List<BindItemData> s_lstMotionFilterType = new List<BindItemData>();
        void InitMotionFilterType()
        {
            s_lstMotionFilterType.Clear();
            s_lstMotionFilterType.Add(new BindItemData(EnumMotionSeekType.OwnPlayer.ToString(), "自己"));
            s_lstMotionFilterType.Add(new BindItemData(EnumMotionSeekType.OppPlayer.ToString(), "对位球员"));
            s_lstMotionFilterType.Add(new BindItemData(EnumMotionSeekType.PairingPlayer.ToString(), "传球目标"));
            s_lstMotionFilterType.Add(new BindItemData(EnumMotionSeekType.OppPairingPlayer.ToString(), "传球目标对位球员"));
            s_lstMotionFilterType.Add(new BindItemData(EnumMotionSeekType.PairedPlayer.ToString(), "传球来源"));
            //s_lstMotionFilterType.Add(new BindItemData(EnumMotionSeekType.AssistPlayer.ToString(), "助攻球员"));
            s_lstMotionFilterType.Add(new BindItemData(EnumMotionSeekType.OwnBallHandler.ToString(), "己方持球人"));
            s_lstMotionFilterType.Add(new BindItemData(EnumMotionSeekType.OppBallHandler.ToString(), "对方持球人"));
            s_lstMotionFilterType.Add(new BindItemData(EnumMotionSeekType.OwnTeam.ToString(), "己方成员"));
            s_lstMotionFilterType.Add(new BindItemData(EnumMotionSeekType.OppTeam.ToString(), "对方成员"));
            s_lstMotionFilterType.Add(new BindItemData(EnumMotionSeekType.BothTeam.ToString(), "双方成员"));
        }
        public List<BindItemData> BindMotionFilterType()
        {
            return GetBindList(false, s_lstMotionFilterType, string.Empty);
        }
        #endregion

        #region MotionDoneType
        static readonly List<BindItemData> s_lstMotionDoneType = new List<BindItemData>();
        void InitMotionDoneType()
        {
            s_lstMotionDoneType.Clear();
            s_lstMotionDoneType.Add(new BindItemData(EnumDoneStateFlag.None.ToString(), "动作时"));
            s_lstMotionDoneType.Add(new BindItemData(EnumDoneStateFlag.Over.ToString(), "动作后"));
            s_lstMotionDoneType.Add(new BindItemData(EnumDoneStateFlag.Succ.ToString(), "动作成功"));
            s_lstMotionDoneType.Add(new BindItemData(EnumDoneStateFlag.Fail.ToString(), "动作失败"));
        }
        public List<BindItemData> BindMotionDoneType()
        {
            return GetBindList(false, s_lstMotionDoneType, string.Empty);
        }
        #endregion

        #region GraphSeekType
        static readonly List<BindItemData> s_lstGraphSeekType = new List<BindItemData>();
        void InitGraphSeekType()
        {
            s_lstGraphSeekType.Clear();
            s_lstGraphSeekType.Add(new BindItemData(EnumGraphSeekType.OwnGoal.ToString(), "己方球门"));
            s_lstGraphSeekType.Add(new BindItemData(EnumGraphSeekType.OppGoal.ToString(), "对方球门"));
            s_lstGraphSeekType.Add(new BindItemData(EnumGraphSeekType.OwnBackLine.ToString(), "己方半场底线"));
            s_lstGraphSeekType.Add(new BindItemData(EnumGraphSeekType.OppBackLine.ToString(), "对方半场底线"));
            s_lstGraphSeekType.Add(new BindItemData(EnumGraphSeekType.OwnRegion.ToString(), "是否在己方半场"));
            s_lstGraphSeekType.Add(new BindItemData(EnumGraphSeekType.OppRegion.ToString(), "是否在对方半场"));
        }
        public List<BindItemData> BindGraphSeekType()
        {
            return GetBindList(false, s_lstGraphSeekType, string.Empty);
        }
        #endregion

        #region ManagerStatType
        static readonly List<BindItemData> s_lstManagerStatType = new List<BindItemData>();
        void InitManagerStatType()
        {
            s_lstManagerStatType.Clear();
            s_lstManagerStatType.Add(new BindItemData(EnumManagerStat.GS.ToString(), "球队得分数"));
            s_lstManagerStatType.Add(new BindItemData(EnumManagerStat.GA.ToString(), "球队失分数"));
            s_lstManagerStatType.Add(new BindItemData(EnumManagerStat.GD.ToString(), "球队净得分"));
            s_lstManagerStatType.Add(new BindItemData(EnumManagerStat.IncDribbleRound.ToString(), "累计控球回合"));
            s_lstManagerStatType.Add(new BindItemData(EnumManagerStat.IncDribbleMinutes.ToString(), "累计控球分钟"));
            s_lstManagerStatType.Add(new BindItemData(EnumManagerStat.CombDribbleRound.ToString(), "连续控球回合"));
            s_lstManagerStatType.Add(new BindItemData(EnumManagerStat.CombDribbleMinutes.ToString(), "连续控球分钟"));

            s_lstManagerStatType.Add(new BindItemData(EnumManagerStat.ShootTimes.ToString(), "射门次数"));
            s_lstManagerStatType.Add(new BindItemData(EnumManagerStat.PassTimes.ToString(), "传球次数"));
            s_lstManagerStatType.Add(new BindItemData(EnumManagerStat.SuccPassTimes.ToString(), "传球成功次数"));
            s_lstManagerStatType.Add(new BindItemData(EnumManagerStat.ThroughTimes.ToString(), "过人次数"));
            s_lstManagerStatType.Add(new BindItemData(EnumManagerStat.SuccThroughTimes.ToString(), "过人成功次数"));
            s_lstManagerStatType.Add(new BindItemData(EnumManagerStat.StealTimes.ToString(), "抢断次数"));
            s_lstManagerStatType.Add(new BindItemData(EnumManagerStat.SuccStealTimes.ToString(), "抢断成功次数"));
            s_lstManagerStatType.Add(new BindItemData(EnumManagerStat.DiveTimes.ToString(), "扑救次数"));
            s_lstManagerStatType.Add(new BindItemData(EnumManagerStat.SuccDiveTimes.ToString(), "扑救成功次数"));
        }
        public List<BindItemData> BindManagerStatType()
        {
            return GetBindList(false, s_lstManagerStatType, string.Empty);
        }
        #endregion

        #region PlayerStatType
        static readonly List<BindItemData> s_lstPlayerStatType = new List<BindItemData>();
        void InitPlayerStatType()
        {
            s_lstPlayerStatType.Clear();
            s_lstPlayerStatType.Add(new BindItemData(EnumPlayerStat.Goals.ToString(), "进球数"));
            s_lstPlayerStatType.Add(new BindItemData(EnumPlayerStat.ShootTimes.ToString(), "射门次数"));
            s_lstPlayerStatType.Add(new BindItemData(EnumPlayerStat.PassTimes.ToString(), "传球次数"));
            s_lstPlayerStatType.Add(new BindItemData(EnumPlayerStat.SuccPassTimes.ToString(), "传球成功次数"));
            s_lstPlayerStatType.Add(new BindItemData(EnumPlayerStat.ThroughTimes.ToString(), "过人次数"));
            s_lstPlayerStatType.Add(new BindItemData(EnumPlayerStat.SuccThroughTimes.ToString(), "过人成功次数"));
            s_lstPlayerStatType.Add(new BindItemData(EnumPlayerStat.StealTimes.ToString(), "抢断次数"));
            s_lstPlayerStatType.Add(new BindItemData(EnumPlayerStat.SuccStealTimes.ToString(), "抢断成功次数"));
            s_lstPlayerStatType.Add(new BindItemData(EnumPlayerStat.DiveTimes.ToString(), "扑救次数"));
            s_lstPlayerStatType.Add(new BindItemData(EnumPlayerStat.SuccDiveTimes.ToString(), "扑救成功次数"));
        }
        public List<BindItemData> BindPlayerStatType()
        {
            return GetBindList(false, s_lstPlayerStatType, string.Empty);
        }
        #endregion

        #region ForceState
        static readonly List<BindItemData> s_lstForceState = new List<BindItemData>();
        void InitForceState()
        {
            s_lstForceState.Clear();
            s_lstForceState.Add(new BindItemData(EnumForceState.ShootState.ToString(), "强制射门(射门范围限制)"));
            s_lstForceState.Add(new BindItemData(EnumForceState.UltraShootState.ToString(), "无视地形射门"));
            s_lstForceState.Add(new BindItemData(EnumForceState.PassState.ToString(), "强制传球"));
            s_lstForceState.Add(new BindItemData(EnumForceState.DribbleState.ToString(), "强制带球"));
            s_lstForceState.Add(new BindItemData(EnumForceState.DefenceState.ToString(), "强制防守(防守半径限制)"));
            s_lstForceState.Add(new BindItemData(EnumForceState.UltraDefenceState.ToString(), "无视地形瞬移抢断"));
        }
        public List<BindItemData> BindForceState()
        {
            return GetBindList(false, s_lstForceState, string.Empty);
        }
        #endregion

        #region GroundType
        static readonly List<BindItemData> s_lstGroundType = new List<BindItemData>();
        void InitGroundType()
        {
            s_lstGroundType.Clear();
            s_lstGroundType.Add(new BindItemData(EnumGroundType.All.ToString(), "全场"));
            s_lstGroundType.Add(new BindItemData(EnumGroundType.Own.ToString(), "己方半场"));
            s_lstGroundType.Add(new BindItemData(EnumGroundType.Opp.ToString(), "对方半场"));
        }
        public List<BindItemData> BindGroundType()
        {
            return GetBindList(false, s_lstGroundType, string.Empty);
        }
        #endregion

        #region Tools
        static List<BindItemData> GetBindList(bool incNA, List<BindItemData> srcList, string preCode, params string[] flags)
        {
            var bindList = new List<BindItemData>();
            if (incNA)
                bindList.Add(s_NAItem);
            FillBindList(bindList, srcList, preCode, flags);
            return bindList;
        }
        static List<BindItemData> GetBindList(string naCode, string naText, List<BindItemData> srcList, string preCode, params string[] flags)
        {
            var bindList = new List<BindItemData>();
            bindList.Add(new BindItemData(naCode, naText));
            FillBindList(bindList, srcList, preCode, flags);
            return bindList;
        }
        static void FillBindList(List<BindItemData> bindList, List<BindItemData> srcList, string preCode, params string[] flags)
        {
            if (null == bindList || null == srcList)
                return;
            bool preFilter = !string.IsNullOrEmpty(preCode);
            bool flagFilter = false;
            if (null != flags && flags.Length > 0)
            {
                flagFilter = true;
                for (int i = 0; i < flags.Length; i++)
                {
                    if (!string.IsNullOrEmpty(flags[i]))
                        flags[i] = "|" + flags[i] + "|";
                }
            }
            bool hitFlag = false;
            foreach (var item in srcList)
            {
                if (preFilter && string.Compare(item.PreCode, preCode, true) != 0)
                    continue;
                if (flagFilter)
                {
                    hitFlag = false;
                    foreach (string flag in flags)
                    {
                        if (string.IsNullOrEmpty(flag) || item.Flag.IndexOf(flag) >= 0)
                        {
                            hitFlag = true;
                            break;
                        }
                    }
                    if (!hitFlag)
                        continue;
                }
                bindList.Add(item);
            }
        }
        #endregion
    }
}
