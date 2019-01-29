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
    public partial class BallStateFilterCtrl : SkillEngine.Editor.Football.UI.ControlBase.FilterCtrl
    {
        public BallStateFilterCtrl()
        {
            InitializeComponent();
        }

        public override void InitData()
        {
            this._dicAControls.Add("p.BallSide", this.combBallSide);
            this._dicAControls.Add("p.BallState", this.combBallState);
            base.InitData();
            this.BindControl(this.combBallSide, SharedData.Instance.BindBallSide());
            this.BindControl(this.combBallState, SharedData.Instance.BindBallState());
        }
    }
}
