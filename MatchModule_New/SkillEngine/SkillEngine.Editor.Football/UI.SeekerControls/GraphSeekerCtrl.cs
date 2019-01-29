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
    public partial class GraphSeekerCtrl : SkillEngine.Editor.Football.UI.ControlBase.SeekerCtrl
    {
        public GraphSeekerCtrl()
        {
            InitializeComponent();
        }

        public override void InitData()
        {
            base.InitData();
            this._dicAControls.Add("p.SeekType", this.combSeekType);
            this._dicAControls.Add("p.Side", this.combSide);
            this._dicAControls.Add("p.MinSpan", this.txtMinSpan);
            this._dicAControls.Add("p.MaxSpan", this.txtMaxSpan);
            this.BindControl(this.combSeekType, SharedData.Instance.BindGraphSeekType());
            this.BindControl(this.combSide, SharedData.Instance.BindOwnPlayerSide());
        }

    }
}
