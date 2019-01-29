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
    public partial class VComboBox : ComboBox
    {
        public VComboBox()
        {
            InitializeComponent();
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            HandledMouseEventArgs mwe = (HandledMouseEventArgs)e;
            mwe.Handled = true;
        }
       
    }
}
