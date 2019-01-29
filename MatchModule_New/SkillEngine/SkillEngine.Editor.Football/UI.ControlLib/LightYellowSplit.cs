using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SkillEngine.Editor.Football.UI.ControlLib
{
    public partial class LightYellowSplit : System.Windows.Forms.Label
    {
        public LightYellowSplit()
        {
            InitializeComponent();
            this.AutoSize = false;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BackColor = System.Drawing.Color.Khaki;
            this.Height = 2;
            this.Enabled = false;
        }
       
    }
}
