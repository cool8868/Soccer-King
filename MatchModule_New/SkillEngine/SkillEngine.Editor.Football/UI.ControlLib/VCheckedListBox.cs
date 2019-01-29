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
    public partial class VCheckedListBox : CheckedListBox
    {
        public VCheckedListBox()
        {
            InitializeComponent();
            this.CheckOnClick = true;
            this.MultiColumn = true;
            this.ColumnWidth = 300;
            this.ScrollAlwaysVisible = true;
        }
        
    }
}
