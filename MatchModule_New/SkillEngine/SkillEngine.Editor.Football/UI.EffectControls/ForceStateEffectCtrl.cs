using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SkillEngine.Editor.Football.Data;

namespace SkillEngine.Editor.Football.UI.EffectControls
{
    public partial class ForceStateEffectCtrl : SkillEngine.Editor.Football.UI.ControlBase.EffectCtrl
    {
        public ForceStateEffectCtrl()
        {
            InitializeComponent();
            this.txtPoint.Enabled = this.txtPercent.Enabled = false;
            this.combRepeat.Enabled = this.combRecylce.Enabled = false;
            this.txtRate.Text = "0100";
        }

        public override void InitData()
        {
            this._dicAControls.Add("c.forceState", this.combForceState);
            this._dicAControls.Add("c.mainFlag", this.cbxMainFlag);
            this._dicAControls.Add("c.debuffFlag", this.cbxDebuffFlag);
            base.InitData();
            this.BindControl(this.combForceState, SharedData.Instance.BindForceState());
        }
    }
}
