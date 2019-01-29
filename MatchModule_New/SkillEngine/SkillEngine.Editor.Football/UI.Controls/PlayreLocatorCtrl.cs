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
    public partial class PlayreLocatorCtrl : SkillEngine.Editor.Football.UI.ControlBase.LocatorCtrl
    {
        public PlayreLocatorCtrl()
        {
            InitializeComponent();
        }

       
        public override void InitData()
        {
            this.ucSeekers.ClassRank = EnumClassRank.Seeker;
            this.ucSeekers.ClassFlag = this.ClassFlag;
            this.ucSeekers.InitData();
            this._dicNControls.Add("p.Seekers", this.ucSeekers);
            base.InitData();
        }
     
    }
}
