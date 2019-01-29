using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SkillEngine.Editor.Football.Data;

namespace SkillEngine.Editor.Football.UI.ControlBase
{
    public partial class SeekerCtrl : SkillEngine.Editor.Football.UI.ControlBase.FlatCtrl
    {
        public SeekerCtrl()
        {
            InitializeComponent();
        }

        public override void InitData()
        {
            this.ucFilters.ClassRank = EnumClassRank.Filter;
            this.ucFilters.ClassFlag = this.ClassFlag;
            this.ucFilters.InitData();
            this._dicAControls.Add("p.JoinType", this.combJoinType);
            this._dicNControls.Add("p.Filters", this.ucFilters);
            this.BindControl(this.combJoinType, SharedData.Instance.BindSeekerJoinType());
        }
    }
}
