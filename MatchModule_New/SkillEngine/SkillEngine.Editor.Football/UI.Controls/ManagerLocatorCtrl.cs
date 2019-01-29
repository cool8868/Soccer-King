using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SkillEngine.Editor.Football.Data;

namespace SkillEngine.Editor.Football.UI.Controls
{
    public partial class ManagerLocatorCtrl : SkillEngine.Editor.Football.UI.ControlBase.LocatorCtrl
    {
        public ManagerLocatorCtrl()
        {
            InitializeComponent();
        }

        public override void InitData()
        {
            this.ucPLocator.ClassFlag = EnumClassFlag.Player;
            this.ucPLocator.InitData();
            this.ucMFilters.ClassRank = EnumClassRank.Filter;
            this.ucMFilters.ClassFlag = EnumClassFlag.Manager;
            this.ucMFilters.InitData();
            this._dicAControls.Add("p.Side", this.combManagerSide);
            this._dicNControls.Add("p.Locator", this.ucPLocator);
            this._dicNControls.Add("p.Filters", this.ucMFilters);
            this.BindControl(this.combManagerSide, SharedData.Instance.BindOwnManagerSide());
            base.InitData();
        }

       
    }
}
