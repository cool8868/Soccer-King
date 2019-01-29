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

namespace SkillEngine.Editor.Football.UI.ControlBase
{
    public partial class EffectorCtrl : SkillEngine.Editor.Football.UI.ControlBase.FlatCtrl
    {
        public EffectorCtrl()
        {
            InitializeComponent();
            this.tabEffector.SelectedIndexChanged += tabEffector_SelectedIndexChanged;
            this.cbClip.CheckedChanged += cbClip_CheckedChanged;
            this.tabEffector.SelectedIndex = 1;
            tabEffector_SelectedIndexChanged(this, null);
        }

        #region EventHandler
        void cbClip_CheckedChanged(object sender, EventArgs e)
        {
            bool flag = this.cbClip.Checked;
            this.txtClipId.Enabled = flag;
            this.cbClipType.Enabled = flag;
            this.txtClipLast.Enabled = flag;
        }
        void tabEffector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabEffector.SelectedIndex == 0)
            {
                this.pnlEffects.Visible = false;
                this.pnlLocator.Visible = true;
            }
            else
            {
                this.pnlLocator.Visible = false;
                this.pnlEffects.Visible = true;
            }
        }
        #endregion

        #region Cache
        readonly Dictionary<string, Control> _dicClip = new Dictionary<string, Control>();
        #endregion

        #region FlatCtrl
        public override void InitData()
        {
            this.ucEffects.ClassRank = EnumClassRank.Effect;
            this.ucEffects.ClassFlag = this.ClassFlag;
            this.ucEffects.InitData();
            this._dicNControls.Add("c.effects", this.ucEffects);
            this._dicNControls.Add("p.ClipSetting", this.cbClip);
            this._dicClip.Add("p.ClipId", this.txtClipId);
            this._dicClip.Add("p.ClipType", this.cbClipType);
            this._dicClip.Add("p.ClipLast", this.txtClipLast);
        }
        protected override bool PreProcessControlValue(DataAccessMode daMode, Control control, ref XElement xe)
        {
            if (control == this.cbClip)
            {
                ProcessClipValue(daMode, ref xe);
                return true;
            }
            return false;
        }
        protected override bool PreProcessControlValue(DataAccessMode daMode, Control control, ref string value)
        {
            if (control == this.cbClipType)
            {
                ControlDataUtil.ProcessCheckBoxValue(daMode, this.cbClipType, ref value, null, ControlDataUtil.DicBoolInt);
                return true;
            }
            return base.PreProcessControlValue(daMode, control, ref value);
        }
        #endregion

        #region Tools
        bool ProcessClipValue(DataAccessMode daMode, ref XElement xe)
        {
            string rootKey = "p.ClipSetting";
            var dicValues = new Dictionary<string, string>();
            if (daMode == DataAccessMode.GetData)
            {
                if (!this.cbClip.Checked)
                {
                    xe = null;
                    return true;
                }
                ProcessControlsValue(daMode, _dicClip, dicValues);
                xe = new XElement(rootKey);
                foreach (var kvp in dicValues)
                {
                    xe.Add(new XAttribute(kvp.Key, kvp.Value));
                }
            }
            else if (daMode == DataAccessMode.SetData)
            {
                this.cbClip.Checked = null != xe;
                if (null == xe)
                    return true;
                string key = string.Empty;
                foreach (var xa in xe.Attributes())
                {
                    key = xa.Name.LocalName;
                    if (_dicClip.ContainsKey(key))
                        dicValues[key] = xa.Value;
                }
                ProcessControlsValue(daMode, _dicClip, dicValues);
            }
            return true;
        }
        #endregion
    }
}
