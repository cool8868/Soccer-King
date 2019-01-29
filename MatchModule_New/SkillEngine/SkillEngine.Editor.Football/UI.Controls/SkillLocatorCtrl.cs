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
using SkillEngine.Editor.Football.UI.ControlBase;

namespace SkillEngine.Editor.Football.UI.Controls
{
    public partial class SkillLocatorCtrl : SkillEngine.Editor.Football.UI.ControlBase.LocatorCtrl
    {
        public SkillLocatorCtrl()
        {
            InitializeComponent();
            this.cbPLocator.CheckedChanged += cbPLocator_CheckedChanged;
            this.cbMSkillSeeker.CheckedChanged += cbMSkillSeeker_CheckedChanged;
            this.cbPSkillSeeker.CheckedChanged += cbPSkillSeeker_CheckedChanged;
        }

        #region EventHandler
        void cbPLocator_CheckedChanged(object sender, EventArgs e)
        {
            this.ucPLocator.Enabled = this.cbPLocator.Checked;
        }
        void cbMSkillSeeker_CheckedChanged(object sender, EventArgs e)
        {
            this.ucMSkillSeeker.Enabled = this.cbMSkillSeeker.Checked;
        }
        void cbPSkillSeeker_CheckedChanged(object sender, EventArgs e)
        {
            this.ucPSkillSeeker.Enabled = this.cbPSkillSeeker.Checked;
        }
        #endregion


        public override void InitData()
        {
            this.ucPLocator.ClassFlag = EnumClassFlag.Player;
            this.ucPLocator.InitData();
            this.ucMSkillSeeker.InitData();
            this.ucPSkillSeeker.InitData();
            this._dicNControls.Add("p.PlayerLocator", this.ucPLocator);
            this._dicNControls.Add("p.ManagerSeeker", this.ucMSkillSeeker);
            this._dicNControls.Add("p.PlayerSeeker", this.ucPSkillSeeker);
            base.InitData();
        }

        protected override bool PreProcessControlValue(DataAccessMode daMode, Control control, ref XElement xe)
        {
            if (control == this.ucPLocator)
            {
                ProcessFlatValue(this.cbPLocator, this.ucPLocator, daMode, ref xe);
                return true;
            }
            if (control == this.ucMSkillSeeker)
            {
                ProcessFlatValue(this.cbMSkillSeeker, this.ucMSkillSeeker, daMode, ref xe);
                return true;
            }
            if (control == this.ucPSkillSeeker)
            {
                ProcessFlatValue(this.cbPSkillSeeker, this.ucPSkillSeeker, daMode, ref xe);
                return true;
            }
            return base.PreProcessControlValue(daMode, control, ref xe);
        }

        bool ProcessFlatValue(CheckBox cbx, FlatCtrl flatCtrl, DataAccessMode daMode, ref XElement xe)
        {
            if (daMode == DataAccessMode.GetData)
            {
                if (!cbx.Checked)
                {
                    xe = null;
                    return true;
                }
                ProcessFlatControlValue(daMode, flatCtrl, ref xe);
            }
            else if (daMode == DataAccessMode.SetData)
            {
                cbx.Checked = null != xe;
                if (null == xe)
                    return true;
                ProcessFlatControlValue(daMode, flatCtrl, ref xe);
            }
            return true;
        }
    
    }
}
