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
    public partial class VMaskedTextBox : MaskedTextBox
    {
        public VMaskedTextBox()
        {
            InitializeComponent();
            this.PromptChar = '0';
            this.InsertKeyMode = InsertKeyMode.Overwrite;
            this.TextMaskFormat = MaskFormat.IncludePrompt;
        }
    }
}
