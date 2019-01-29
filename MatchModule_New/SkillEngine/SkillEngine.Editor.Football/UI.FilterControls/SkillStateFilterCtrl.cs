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
    public partial class SkillStateFilterCtrl : SkillEngine.Editor.Football.UI.ControlBase.FilterCtrl
    {
        public SkillStateFilterCtrl()
        {
            InitializeComponent();
        }

        public override void InitData()
        {
            this._dicAControls.Add("p.SrcTypes", this.combSrcType);
            this._dicAControls.Add("p.ActTypes", this.combActType);
            this._dicAControls.Add("p.Ids", this.txtIds);
            base.InitData();
            this.BindControl(this.combSrcType, SharedData.Instance.BindSkillSrcType());
            this.BindControl(this.combActType, SharedData.Instance.BindSkillActType());
        }
    }
}
