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
    public partial class SilverSplit : System.Windows.Forms.Label
    {
        public SilverSplit()
        {
            InitializeComponent();
            this.AutoSize = false;
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BackColor = System.Drawing.Color.Silver;
            this.Height = 2;
            this.Enabled = false;
        }
    }
}
