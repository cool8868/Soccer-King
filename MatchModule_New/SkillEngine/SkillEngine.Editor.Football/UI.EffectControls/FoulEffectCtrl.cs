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
    public partial class FoulEffectCtrl : SkillEngine.Editor.Football.UI.ControlBase.EffectCtrl
    {
        public FoulEffectCtrl()
        {
            InitializeComponent();
            this.txtPoint.Enabled = this.txtPercent.Enabled = false;
            this.combLast.Enabled = false;
            this.txtRate.Text = "0100";
        }
     
        public override void InitData()
        {
            this._dicAControls.Add("c.foulCode", this.combBuffId);
            this._dicAControls.Add("c.mainFlag", this.cbxMainFlag);
            this._dicAControls.Add("c.pureFlag", this.cbxPureFlag);
            base.InitData();
            this.BindControl(this.combBuffId, SharedData.Instance.BindFoulBuffId());
        
        }
    }
}
