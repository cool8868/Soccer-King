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
    public partial class TalentPositonFilterCtrl : SkillEngine.Editor.Football.UI.ControlBase.FilterCtrl
    {
        public TalentPositonFilterCtrl()
        {
            InitializeComponent();
        }

        public override void InitData()
        {
            this._dicAControls.Add("p.Values", this.combValues);
            base.InitData();
            this.BindControl(this.combValues, SharedData.Instance.BindTalentPosition(), false);
        }
    }
}
