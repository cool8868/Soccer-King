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
    public partial class ManagerSkillSeekerCtrl : PlayerSkillSeekerCtrl
    {
        public ManagerSkillSeekerCtrl()
        {
            InitializeComponent();
            this.txtType.Text = "Seeker.ManagerSkill";
        }
       
    }
}
