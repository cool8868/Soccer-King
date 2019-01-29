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
using SkillEngine.Editor.Football.Util;
using SkillEngine.Editor.Football.Data;

namespace SkillEngine.Editor.Football.UI.ControlLib
{
    public partial class ValueSetDropDownUI : UserControl, IDropDownUI
    {
        public ValueSetDropDownUI()
        {
            InitializeComponent();
            this.cbxAll.CheckStateChanged += cbxAll_CheckStateChanged;
            this.clbList.SelectedIndexChanged += clbList_SelectedIndexChanged;
            this.btnOk.Click += btnOk_Click;
            this.btnCancel.Click += btnCancel_Click;
        }


        #region Data
        public bool NoneAsFullFlag
        {
            get;
            set;
        }
        public void SetDataSource(List<BindItemData> bindList)
        {
            this.clbList.DataSource = bindList;
            this.clbList.DisplayMember = BindItemData.COLText;
            this.clbList.ValueMember = BindItemData.COLCode;
        }
        #endregion

        #region EventHandler
        void cbxAll_CheckStateChanged(object sender, EventArgs e)
        {
            if (this.clbList.Focused || this.cbxAll.CheckState == CheckState.Indeterminate)
                return;
            this.SetCheckedListBoxAllChecked(this.clbList, this.cbxAll.Checked);
        }

        void clbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.clbList.Focused)
                return;
            if (this.clbList.CheckedItems.Count == 0)
                this.cbxAll.CheckState = CheckState.Unchecked;
            else if (this.clbList.CheckedItems.Count == this.clbList.Items.Count)
                this.cbxAll.CheckState = CheckState.Checked;
            else
                this.cbxAll.CheckState = CheckState.Indeterminate;
        }
        void btnOk_Click(object sender, EventArgs e)
        {
            this._coreValue = this.CoreValue;
            this.RaiseCloseDropDown();
        }
        void btnCancel_Click(object sender, EventArgs e)
        {
            this.RaiseCloseDropDown();
        }
        #endregion

        #region IDropDownUI
        public event EventHandler CloseDropDownHolder;
        public object GetSelectedValue()
        {
            return this._coreValue;
        }
        public void SetSelectedValue(object value)
        {
            if (null != value)
                this._coreValue = value.ToString();
            this.CoreValue = this._coreValue;
        }
        #endregion

        #region DropDownCore
        void RaiseCloseDropDown()
        {
            if (this.CloseDropDownHolder == null)
                return;
            if (this.InvokeRequired)
            {
                this.Invoke(this.CloseDropDownHolder, this, EventArgs.Empty);
            }
            else
            {
                this.CloseDropDownHolder(this, EventArgs.Empty);
            }
        }
        string _coreValue = string.Empty;
        string CoreValue
        {
            get
            {
                if (this.cbxAll.CheckState==CheckState.Checked && this.NoneAsFullFlag)
                    return string.Empty;
                string val = string.Empty;
                ControlDataUtil.ProcessCheckedListBoxCore(DataAccessMode.GetData, true, this.clbList, ref val);
                return val;
            }
            set
            {
                string val = value;
                if (string.IsNullOrEmpty(val))
                {
                    this.cbxAll.Checked = this.NoneAsFullFlag;
                    return;
                }
                ControlDataUtil.ProcessCheckedListBoxCore(DataAccessMode.SetData, true, this.clbList, ref val);
            }
        }
        #endregion

        #region Tools
        void SetCheckedListBoxAllChecked(CheckedListBox clb, bool checkedFlag)
        {
            if (checkedFlag)
            {
                for (int i = 0; i < clb.Items.Count; i++)
                {
                    clb.SetItemChecked(i, true);
                }
            }
            else
            {
                foreach (int idx in clb.CheckedIndices)
                {
                    clb.SetItemChecked(idx, false);
                }
            }
        }
        #endregion
    }
}
