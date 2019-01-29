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
    public partial class MotionFilterCtrl : SkillEngine.Editor.Football.UI.ControlBase.FilterCtrl
    {
        public MotionFilterCtrl()
        {
            InitializeComponent();
        }

        public override void InitData()
        {
            this._dicAControls.Add("p.SeekType", this.combSeekType);
            this._dicAControls.Add("c.values", this.combValues);
            this._dicAControls.Add("c.stateType", this.combStateType);
            base.InitData();
            this.BindControl(this.combSeekType, SharedData.Instance.BindMotionFilterType());
            this.BindControl(this.combValues, SharedData.Instance.BindMotion());
            this.BindControl(this.combStateType, SharedData.Instance.BindMotionDoneType());
        }
    }
}
