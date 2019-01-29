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
    public partial class TriggerCtrl : SkillEngine.Editor.Football.UI.ControlBase.FlatAddCtrl
    {
        public TriggerCtrl()
        {
            InitializeComponent();
        }

        public override void InitData()
        {
            this.ClassRank = EnumClassRank.Trigger;
            this.ClassFlag = EnumClassFlag.Player;
            base.InitData();
            this._dicAControls.Add("p.Repeat", this.cbxRepeat);
            this._dicAControls.Add("p.Recycle", this.cbxRecycle);
            this._dicAControls.Add("p.Delay", this.cbxDelay);
        }
    }
}
