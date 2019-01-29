using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SkillEngine.Editor.Football.Data;
using SkillEngine.Extern.Enum;

namespace SkillEngine.Editor.Football.UI.FilterControls
{
    public partial class GraphFilterCtrl : SkillEngine.Editor.Football.UI.ControlBase.FilterCtrl
    {
        public GraphFilterCtrl()
        {
            InitializeComponent();
        }

        public override void InitData()
        {
            this._dicAControls.Add("p.SeekType", this.combSeekType);
            this._dicAControls.Add("p.GraphType", this.combGraphType);
            this._dicAControls.Add("p.MinSpan", this.txtMinSpan);
            this._dicAControls.Add("p.MaxSpan", this.txtMaxSpan);
            base.InitData();
            this.BindControl(this.combSeekType, SharedData.Instance.BindMotionFilterType());
            this.BindControl(this.combGraphType, SharedData.Instance.BindGraphSeekType());
        }
    }
}
