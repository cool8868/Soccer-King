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
    public partial class ManagerStatFilterCtrl : SkillEngine.Editor.Football.UI.ControlBase.FilterCtrl
    {
        public ManagerStatFilterCtrl()
        {
            InitializeComponent();
        }

        public override void InitData()
        {
            this._dicAControls.Add("c.statType", this.combStatType);
            this._dicAControls.Add("c.minValue", this.txtMinValue);
            this._dicAControls.Add("c.maxValue", this.txtMaxValue);
            this._dicAControls.Add("p.Side", this.combSide);
            base.InitData();
            this.BindControl(this.combStatType, SharedData.Instance.BindManagerStatType());
            this.BindControl(this.combSide, SharedData.Instance.BindOwnManagerSide());
        }
    }
}
