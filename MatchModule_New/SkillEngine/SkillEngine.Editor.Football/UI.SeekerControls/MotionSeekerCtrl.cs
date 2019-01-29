using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SkillEngine.Editor.Football.Data;

namespace SkillEngine.Editor.Football.UI.SeekerControls
{
    public partial class MotionSeekerCtrl : SkillEngine.Editor.Football.UI.ControlBase.SeekerCtrl
    {
        public MotionSeekerCtrl()
        {
            InitializeComponent();
        }


        public override void InitData()
        {
            base.InitData();
            this._dicAControls.Add("p.SeekType", this.combSeekType);
            this.BindControl(this.combSeekType, SharedData.Instance.BindMotionSeekType());
        }
    }
}
