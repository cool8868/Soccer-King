using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using SkillEngine.Editor.Football.Util;
using SkillEngine.Editor.Football.Data;
using SkillEngine.SkillBase.Enum;

namespace SkillEngine.Editor.Football.UI.ControlBase
{
    public partial class EffectCtrl : SkillEngine.Editor.Football.UI.ControlBase.FlatCtrl
    {
        public EffectCtrl()
        {
            InitializeComponent();
            this.combLast.SelectedIndexChanged += combLast_SelectedIndexChanged;
            this.cbSrcModel.CheckedChanged += cbSrcModel_CheckedChanged;
            this.cbTgtModel.CheckedChanged += cbTgtModel_CheckedChanged;
            this.combRecylce.SelectedIndexChanged += combRecylce_SelectedIndexChanged;
        }

        #region EventHandler
        void combRecylce_SelectedIndexChanged(object sender, EventArgs e)
        {
            string recylce = GetSelectedValue(this.combRecylce);
            bool val = false;
            bool.TryParse(recylce, out val);
            if (val)
                this.BindControl(this.combLast, SharedData.Instance.BindEffectLast(false, "Undo"));
            else
                this.BindControl(this.combLast, SharedData.Instance.BindEffectLast(false, "Main"));
        }
        void combLast_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lastType = GetSelectedValue(this.combLast);
            int val = 0;
            int.TryParse(lastType, out val);
            this.txtLast.Text = "";
            this.txtLast.Enabled = val > 0;
            if (val == (int)EnumBuffLast.Round)
                this.lblMinute.Text = "回合";
            else
                this.lblMinute.Text = "分钟";
        }
        void cbSrcModel_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = this.cbSrcModel.Checked;
            this.txtSrcModelId.Enabled = flag;
            this.txtSrcModelLast.Enabled = flag;
        }
        void cbTgtModel_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = this.cbTgtModel.Checked;
            this.txtTgtModelId.Enabled = flag;
            this.txtTgtModelLast.Enabled = flag;
        }
        #endregion

        #region Cache
        const int SPLITMinutsVal = 1000;
        readonly Dictionary<string, Control> _dicSrcModel = new Dictionary<string, Control>();
        readonly Dictionary<string, Control> _dicTgtModel = new Dictionary<string, Control>();
        #endregion

      
        #region FlatCtrl
        public override void InitData()
        {
            this._dicAControls.Add("p.Repeat", this.combRepeat);
            this._dicAControls.Add("p.Recycle", this.combRecylce);
            this._dicAControls.Add("p.Last", this.combLast);
            this._dicAControls.Add("p.Rate", this.txtRate);
            this._dicAControls.Add("p.Point", this.txtPoint);
            this._dicAControls.Add("p.Percent", this.txtPercent);
            this._dicAControls.Add(SharedData.KEYMemo, this.txtMemo);
            this._dicNControls.Add("p.SrcModelSetting", this.cbSrcModel);
            this._dicNControls.Add("p.TgtModelSetting", this.cbTgtModel);

            this._dicSrcModel.Add("p.ModelId", this.txtSrcModelId);
            this._dicSrcModel.Add("p.ModelLast", this.txtSrcModelLast);
            this._dicTgtModel.Add("p.ModelId", this.txtTgtModelId);
            this._dicTgtModel.Add("p.ModelLast", this.txtTgtModelLast);
          
            this.BindControl(this.combLast, SharedData.Instance.BindEffectLast(false, "Main"));
            this.BindControl(this.combRepeat, SharedData.Instance.BindEffectRepeat());
            this.BindControl(this.combRecylce, SharedData.Instance.BindEffectRecycle());
        }
        protected override bool PreProcessControlValue(DataAccessMode daMode, Control control, ref string value)
        {
            if (control == this.combLast)
            {
                ProcessLastValue(daMode, ref value);
                return true;
            }
            return base.PreProcessControlValue(daMode, control, ref value);
        }
        protected override bool PreProcessControlValue(DataAccessMode daMode, Control control, ref XElement xe)
        {
            if (control == this.cbSrcModel || control == this.cbTgtModel)
            {
                ProcessModelValue(control == this.cbSrcModel, daMode, ref xe);
                return true;
            }
            return base.PreProcessControlValue(daMode, control, ref xe);
        }
        #endregion

        #region Tools
        bool ProcessLastValue(DataAccessMode daMode, ref string value)
        {
            if (daMode == DataAccessMode.GetData)
            {
                value = GetSelectedValue(this.combLast);
                int val = 0;
                if (value == ((int)EnumBuffLast.Round).ToString())
                {
                    int.TryParse(this.txtLast.Text, out val);
                    value=val.ToString();
                }
                else if (value == ((int)EnumBuffLast.Minutes).ToString())
                {
                    int.TryParse(this.txtLast.Text, out val);
                    value = (val * SPLITMinutsVal).ToString();
                }
            }
            else if (daMode == DataAccessMode.SetData)
            {
                int val = 0;
                int.TryParse(value, out val);
                if (val <= 0)
                {
                    this.combLast.SelectedValue = val.ToString();
                    this.txtLast.Text="";
                }
                else if (0 < val && val < SPLITMinutsVal)
                {
                    this.combLast.SelectedValue = ((int)EnumBuffLast.Round).ToString();
                    this.txtLast.Text = val.ToString("000");
                }
                else
                {
                    this.combLast.SelectedValue = ((int)EnumBuffLast.Minutes).ToString();
                    this.txtLast.Text = (val / 1000).ToString("000");
                }
            }
            return true;
        }
        bool ProcessModelValue(bool srcFlag, DataAccessMode daMode, ref XElement xe)
        {
            var cbModel = srcFlag ? this.cbSrcModel : this.cbTgtModel;
            string rootKey = srcFlag ? "p.SrcModelSetting" : "p.TgtModelSetting";
            var dicControls = srcFlag ? _dicSrcModel : _dicTgtModel;
            var dicValues=new Dictionary<string,string>();
            if (daMode == DataAccessMode.GetData)
            {
                if (!cbModel.Checked)
                {
                    xe = null;
                    return true;
                }
                ProcessControlsValue(daMode, dicControls, dicValues);
                xe = new XElement(rootKey);
                foreach (var kvp in dicValues)
                {
                    xe.Add(new XAttribute(kvp.Key, kvp.Value));
                }
            }
            else if (daMode == DataAccessMode.SetData)
            {
                cbModel.Checked = null != xe;
                if (null == xe)
                    return true;
                string key = string.Empty;
                foreach (var xa in xe.Attributes())
                {
                    key = xa.Name.LocalName;
                    if (dicControls.ContainsKey(key))
                        dicValues[key] = xa.Value;
                }
                ProcessControlsValue(daMode, dicControls, dicValues);
            }
            return true;
        }
        #endregion
    }
}
