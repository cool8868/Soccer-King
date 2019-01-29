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
    public partial class IdSeekerCtrl : SkillEngine.Editor.Football.UI.ControlBase.SeekerCtrl
    {
        public IdSeekerCtrl()
        {
            InitializeComponent();
        }

        public override void InitData()
        {
            base.InitData();
            this._dicAControls.Add("p.Side", this.combSide);
            this._dicAControls.Add("p.Postions", this.combPositions);
            this._dicAControls.Add("p.Colours", this.combColours);
            this._dicAControls.Add("p.Ids", this.txtIds);
            this.BindControl(this.combSide, SharedData.Instance.BindMangerSide());
            this.BindControl(this.combPositions, SharedData.Instance.BindPosition());
            this.BindControl(this.combColours, SharedData.Instance.BindColour());
        }
      
    }
}
