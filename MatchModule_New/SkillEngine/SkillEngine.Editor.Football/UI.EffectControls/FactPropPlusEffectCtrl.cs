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
    public partial class FactPropPlusEffectCtrl : SkillEngine.Editor.Football.UI.ControlBase.EffectCtrl
    {
        public FactPropPlusEffectCtrl()
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
            this._dicAControls.Add("p.Side", this.combSide);
            this._dicAControls.Add("p.FactType", this.combFactType);
            this._dicAControls.Add("p.Values", this.combValues);
            base.InitData();
            this.BindControl(this.combBuffId, SharedData.Instance.BindPropBuffId(), false);
            this.BindControl(this.combSide, SharedData.Instance.BindOwnPlayerSide());
            this.BindControl(this.combFactType, SharedData.Instance.BindFactType());
            this.BindControl(this.combValues, SharedData.Instance.BindColour());
        }
      
    }
}
