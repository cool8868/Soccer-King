using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SkillEngine.Editor.Football
{
    public interface IFlatControl
    {
        XElement GetValue();
        bool SetValue(XElement xe);
    }
}
