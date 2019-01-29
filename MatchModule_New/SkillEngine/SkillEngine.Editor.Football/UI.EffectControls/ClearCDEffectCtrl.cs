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
    public partial class ClearCDEffectCtrl : SkillEngine.Editor.Football.UI.ControlBase.EffectCtrl
    {
        public ClearCDEffectCtrl()
        {
            InitializeComponent();
            this.combLast.Enabled = false;
            this.combRepeat.Enabled = false;
            this.combRecylce.Enabled = false;
            this.txtPoint.Enabled = false;
            this.txtPercent.Enabled = false;
        }
       
        public override void InitData()
        {
            this._dicAControls.Add("p.SrcTypes", this.combSrcType);
            this._dicAControls.Add("p.ActTypes", this.combActType);
            this._dicAControls.Add("p.Ids", this.txtIds);
            this._dicAControls.Add("c.mainFlag", this.cbxMainFlag);
            this._dicAControls.Add("c.pureFlag", this.cbxPureFlag);
            this._dicAControls.Add("c.debuffFlag", this.cbxDebuffFlag);
            base.InitData();
            this.BindControl(this.combSrcType, SharedData.Instance.BindSkillSrcType());
            this.BindControl(this.combActType, SharedData.Instance.BindSkillActType());

        }
    }
}
