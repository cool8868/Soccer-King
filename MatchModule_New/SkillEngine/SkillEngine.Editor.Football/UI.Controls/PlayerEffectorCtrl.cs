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
    public partial class PlayerEffectorCtrl : SkillEngine.Editor.Football.UI.ControlBase.EffectorCtrl
    {
        public PlayerEffectorCtrl()
        {
            InitializeComponent();
        }

        public override void InitData()
        {
            this.ucLocator.ClassFlag = this.ClassFlag;
            this.ucLocator.InitData();
            this._dicNControls.Add("c.locator", this.ucLocator);
            base.InitData();
        }
     
    }
}
