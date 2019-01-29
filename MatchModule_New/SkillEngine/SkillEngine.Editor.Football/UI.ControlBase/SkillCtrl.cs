using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SkillEngine.Editor.Football.Data;
using SkillEngine.Editor.Football.Rules;
using SkillEngine.Editor.Football.Util;
using SkillEngine.SkillBase.Enum;

namespace SkillEngine.Editor.Football.UI.ControlBase
{
    public partial class SkillCtrl : SkillEngine.Editor.Football.UI.ControlBase.FlatCtrl
    {
        public SkillCtrl()
        {
            InitializeComponent();
            this.txtType.Text = "RawSkill";
            this.combCD.SelectedIndexChanged += combCD_SelectedIndexChanged;
            this.combLast.SelectedIndexChanged += combLast_SelectedIndexChanged;
            this.tabControl1.SelectedTab = this.tabPage2;
        }

        void combLast_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lastType = GetSelectedValue(this.combLast);
            int val = 0;
            int.TryParse(lastType, out val);
            this.txtLast.Text = "";
            this.txtLast.Enabled = val > 0;
            if (val == (int)EnumBuffLast.Round)
                this.lblMinute2.Text = "回合";
            else
                this.lblMinute2.Text = "分钟";
        }

        void combCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lastType = GetSelectedValue(this.combCD);
            int val = 0;
            int.TryParse(lastType, out val);
            this.txtCD.Text = "";
            this.txtCD.Enabled = val > 0;
            if (val == (int)EnumBuffLast.Round)
                this.lblMinute.Text = "回合";
            else
                this.lblMinute.Text = "分钟";
        }

        public override void InitData()
        {
            this.ucTriggers.ClassRank = EnumClassRank.Trigger;
            this.ucTriggers.ClassFlag = EnumClassFlag.Player;
            this.ucTriggers.InitData();
            this.ucEffectors.ClassRank = EnumClassRank.Effector;
            this.ucEffectors.ClassFlag = EnumClassFlag.None;
            this.ucEffectors.InitData();
            this._rootXe.Name = SharedLogic.KEYSkill;
            this._dicAControls.Add("type", this.txtType);
            this._dicAControls.Add("c.skillId", this.txtSKillId);
            this._dicAControls.Add("c.skillCode", this.txtSkillCode);
            this._dicAControls.Add("p.SkillName", this.txtSkillName);
            this._dicAControls.Add("p.SkillLevel", this.txtSkillLevel);
            this._dicAControls.Add("p.OpenClipId", this.txtOpenClipId);
            this._dicAControls.Add("p.SrcType", this.combSrcType);
            this._dicAControls.Add("p.ActType", this.combActType);
            this._dicAControls.Add("p.TimeType", this.combTimeType);
            this._dicAControls.Add("p.CastFlag", this.combCastFlag);
            this._dicAControls.Add("p.ParalFlag", this.combParalFlag);
            this._dicAControls.Add("p.CD", this.combCD);
            this._dicAControls.Add("p.RedoLast", this.combLast);
            this._dicAControls.Add("p.Rate", this.txtRate);
            this._dicNControls.Add("c.triggers", this.ucTriggers);
            this._dicNControls.Add("c.effectors", this.ucEffectors);
            this.BindControl(this.combCD, SharedData.Instance.BindEffectLast(false, "Main"));
            this.BindControl(this.combLast, SharedData.Instance.BindRedoLast(false, string.Empty));
            this.BindControl(this.combSrcType, SharedData.Instance.BindSkillSrcType());
            this.BindControl(this.combActType, SharedData.Instance.BindSkillActType());
            this.BindControl(this.combTimeType, SharedData.Instance.BindSkillTimeType());
            this.BindControl(this.combCastFlag, SharedData.Instance.BindSkillCastFlag());
            this.BindControl(this.combParalFlag, SharedData.Instance.BindSkillParalFlag());
        }

        public override System.Xml.Linq.XElement GetValue()
        {
            var xe = base.GetValue();
            if (null != xe)
            {
                xe.SetAttributeValue(SharedData.KEYMemo, SharedLogic.GetSkillMemo(xe));
                xe.SetAttributeValue(SharedData.KEYClipId, SharedLogic.GetClipId(xe));
                xe.SetAttributeValue(SharedData.KEYModelId, SharedLogic.GetModelId(xe));
            }
            return xe;
        }

        const int SPLITMinutsVal = 1000;
        protected override bool PreProcessControlValue(DataAccessMode daMode, Control control, ref string value)
        {
            if (control == this.combCD)
            {
                ProcessLastValue(daMode, this.combCD, this.txtCD, ref value);
                return true;
            }
            if (control == this.combLast)
            {
                ProcessLastValue(daMode, this.combLast, this.txtLast, ref value);
                return true;
            }
            return base.PreProcessControlValue(daMode, control, ref value);
        }

        bool ProcessLastValue(DataAccessMode daMode, ComboBox comb, MaskedTextBox mask, ref string value)
        {
            if (daMode == DataAccessMode.GetData)
            {
                value = GetSelectedValue(comb);
                int val = 0;
                if (value == ((int)EnumBuffLast.Round).ToString())
                {
                    int.TryParse(mask.Text, out val);
                    value = val.ToString();
                }
                else if (value == ((int)EnumBuffLast.Minutes).ToString())
                {
                    int.TryParse(mask.Text, out val);
                    value = (val * SPLITMinutsVal).ToString();
                }
            }
            else if (daMode == DataAccessMode.SetData)
            {
                int val = 0;
                int.TryParse(value, out val);
                if (val <= 0)
                {
                    comb.SelectedValue = val.ToString();
                    mask.Text = "";
                }
                else if (0 < val && val < SPLITMinutsVal)
                {
                    comb.SelectedValue = ((int)EnumBuffLast.Round).ToString();
                    mask.Text = val.ToString("000");
                }
                else
                {
                    comb.SelectedValue = ((int)EnumBuffLast.Minutes).ToString();
                    mask.Text = (val / 1000).ToString("000");
                }
            }
            return true;

        }
    }
}
