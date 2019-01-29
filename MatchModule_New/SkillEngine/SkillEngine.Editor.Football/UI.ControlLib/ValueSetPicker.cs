using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pickers;
using SkillEngine.Editor.Football.Data;

namespace SkillEngine.Editor.Football.UI.ControlLib
{
    public partial class ValueSetPicker : PickerBase<string, ValueSetDropDownUI>
    {
        public ValueSetPicker()
        {
            InitializeComponent();
        }

        #region Data
        public bool NoneAsFullFlag
        {
            get;
            set;
        }
        List<BindItemData> _dataSource;
        public List<BindItemData> DataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = value;
                this.DisplayAdapter.DisplayControl.Text = this.FormatValue(this.Value);
            }
        }
        #endregion

        #region PickerBase
        protected override string GetDefaultValue()
        {
            return string.Empty;
        }
        protected override string FormatValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return this.NoneAsFullFlag ? "<任意>" : "<无>";
            string[] vals = value.Split(',');
            if (vals.Length == 0 || null == this.DataSource)
                return value;
            var dicBind = this.DataSource.ToDictionary(i => i.Code, i => i.Text);
            string[] txts = new string[vals.Length];
            for (int i = 0; i < vals.Length; ++i)
            {
                if (!dicBind.TryGetValue(vals[i], out txts[i]))
                    txts[i] = vals[i];
            }
            return string.Join(",", txts);

        }
        protected override void InitializeDropDownControl(ValueSetDropDownUI control)
        {
            control.NoneAsFullFlag = this.NoneAsFullFlag;
            control.SetDataSource(this.DataSource);
        }
        #endregion
    }
}
