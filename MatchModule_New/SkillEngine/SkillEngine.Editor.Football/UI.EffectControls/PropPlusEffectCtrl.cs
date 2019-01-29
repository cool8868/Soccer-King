using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SkillEngine.Editor.Football.Data;

namespace SkillEngine.Editor.Football.UI.EffectControls
{
    public partial class PropPlusEffectCtrl : SkillEngine.Editor.Football.UI.ControlBase.EffectCtrl
    {
        public PropPlusEffectCtrl()
        {
            InitializeComponent();
            this.txtRate.Enabled = false;
        }

        public override void InitData()
        {
            this._dicAControls.Add("c.buffId", this.combBuffId);
            this._dicAControls.Add("c.mainFlag", this.cbxMainFlag);
            this._dicAControls.Add("c.pureFlag", this.cbxPureFlag);
            this._dicAControls.Add("c.debuffFlag", this.cbxDebuffFlag);
            base.InitData();
            this.BindControl(this.combBuffId, SharedData.Instance.BindPropBuffId(), false);
        }

        public override bool SetValue(System.Xml.Linq.XElement xe)
        {
            return base.SetValue(xe);
        }

    }
}
