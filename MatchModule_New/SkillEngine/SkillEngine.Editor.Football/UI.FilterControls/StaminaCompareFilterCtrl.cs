using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SkillEngine.Editor.Football.Data;

namespace SkillEngine.Editor.Football.UI.FilterControls
{
    public partial class StaminaCompareFilterCtrl : SkillEngine.Editor.Football.UI.ControlBase.FilterCtrl
    {
        public StaminaCompareFilterCtrl()
        {
            InitializeComponent();
        }

        public override void InitData()
        {
            this._dicAControls.Add("p.RateFlag", this.combRateFlag);
            this._dicAControls.Add("p.RelateFlag", this.cbxRelateFlag);
            this._dicAControls.Add("p.MinValue", this.txtMinValue);
            this._dicAControls.Add("p.MaxValue", this.txtMaxValue);
            base.InitData();
            this.BindControl(this.combRateFlag, SharedData.Instance.BindStaminaType());
        }
    }
}
