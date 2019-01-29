using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using SkillEngine.Editor.Football.Data;
using SkillEngine.Editor.Football.Util;
using SkillEngine.Editor.Football.UI.ControlBase;

namespace SkillEngine.Editor.Football.UI.Controls
{
    public partial class SkillEffectorCtrl : SkillEngine.Editor.Football.UI.ControlBase.EffectorCtrl
    {
        public SkillEffectorCtrl()
        {
            InitializeComponent();
            this.cbEffectors.CheckedChanged += cbEffectors_CheckedChanged;
        }

        void cbEffectors_CheckedChanged(object sender, EventArgs e)
        {
            this.ucEffectors.Enabled = this.cbEffectors.Checked;
        }


        public override void InitData()
        {
            this.ucLocator.ClassFlag = this.ClassFlag;
            this.ucLocator.InitData();
            this.ucEffectors.ClassRank = EnumClassRank.Effector;
            this.ucEffectors.ClassFlag = EnumClassFlag.PlugEffector;
            this.ucEffectors.InitData();
            this._dicNControls.Add("c.locator", this.ucLocator);
            base.InitData();
            this._dicNControls.Add("p.PlugEffectors", this.ucEffectors);
        }

        protected override bool PreProcessControlValue(DataAccessMode daMode, Control control, ref XElement xe)
        {
            if (control == this.ucEffectors)
            {
                ProcessFlatValue(daMode, ref xe);
                return true;
            }
            return base.PreProcessControlValue(daMode, control, ref xe);
        }

        bool ProcessFlatValue(DataAccessMode daMode, ref XElement xe)
        {
            if (daMode == DataAccessMode.GetData)
            {
                if (!cbEffectors.Checked)
                {
                    xe = null;
                    return true;
                }
                ProcessFlatControlValue(daMode, this.ucEffectors, ref xe);
            }
            else if (daMode == DataAccessMode.SetData)
            {
                cbEffectors.Checked = null != xe;
                if (null == xe)
                    return true;
                ProcessFlatControlValue(daMode, this.ucEffectors, ref xe);
            }
            return true;
        }
    }
}
