using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using SkillEngine.Editor.Football.Data;
using SkillEngine.Editor.Football.Rules;

namespace SkillEngine.Editor.Football.UI
{
    public enum OperationState
    {
        Renew,
        CopyNew,
        New,
        Edit,
    }
    public partial class SkillListForm : Form
    {
        public SkillListForm()
        {
            InitializeComponent();
            this.FormClosing += SkillListForm_FormClosing;
            this._diagOpenFile.Multiselect = false;
            this._diagOpenFile.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "SKillConfig";
            this._diagOpenFile.Filter = "技能配置文件(*.xml)|*.xml";
            this._diagOpenFile.FileOk += _diagOpenFile_FileOk;
            this._diagSaveFile.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + "SkillConfig";
            this._diagSaveFile.Filter = "技能配置文件(*.xml)|*.xml";
            this._diagSaveFile.FileOk += _diagSaveFile_FileOk;
            this.combSkillType.SelectedIndexChanged += FilterHandler;
            this.txtSearch.TextChanged += FilterHandler;
            this.Load += SkillListForm_Load;
            this.menuOpen.Click += menuOpen_Click;
            this.menuCheck.Click += menuCheck_Click;
            this.menuSave.Click += SaveHandler;
            this.menuSaveAs.Click += menuSaveAs_Click;
            this.menuExit.Click += menuExit_Click;
            this.menuRenewItem.Click += RenewHandler;
            this.menuCopyNewItem.Click += CopyHandler;
            this.menuAddItem.Click += AddHandler;
            this.menuEditItem.Click += EditHandler;
            this.menuDeleteItem.Click += DeleteHandler;
            this.toolRenew.Click += RenewHandler;
            this.toolCopyNew.Click += CopyHandler;
            this.toolAdd.Click += AddHandler;
            this.toolEdit.Click += EditHandler;
            this.toolDelete.Click += DeleteHandler;
            this.btnSave.Click += SaveHandler;
            this.gridList.CellMouseDown += gridList_CellMouseDown;
            this.gridList.CellDoubleClick += EditHandler;
            this.gridList.RowStateChanged += gridList_RowStateChanged;
            this.gridList.RowsRemoved += gridList_RowsRemoved;
        }

        
        #region EventHandler
        void SkillListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定要关闭吗？", "系统信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                e.Cancel = true;
        }
        void _diagOpenFile_FileOk(object sender, CancelEventArgs e)
        {
            this.SelecedFile = this._diagOpenFile.FileName;
            var xd = XDocument.Load(this._openFilePath);
            this._rootXml = SharedLogic.WrapLoadXml(xd.Root);
            SharedLogic.FillBindSkillTable(this._bindData, this._rootXml);
        }
        void _diagSaveFile_FileOk(object sender, CancelEventArgs e)
        {
            this.SelecedFile = this._diagSaveFile.FileName;
        }
        void SkillListForm_Load(object sender, EventArgs e)
        {
            this.Init();
        }
        void menuOpen_Click(object sender, EventArgs e)
        {
            this._diagOpenFile.ShowDialog();
        }
        void menuCheck_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(this._openFilePath))
            {
                SharedLogic.ShowMessage("请先打开需要验证的文件");
                return;
            }
            if (CheckRules.CheckFile(this._openFilePath))
                SharedLogic.ShowMessage("文件格式正确有效");
        }
        void menuSaveAs_Click(object sender, EventArgs e)
        {
            this.Save(true);
        }
        void SaveHandler(object sender, EventArgs e)
        {
            this.Save(false);
        }
        void menuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void FilterHandler(object sender, EventArgs e)
        {
            string join = " and ";
            string filter = string.Empty;
            var item = this.combSkillType.SelectedItem as BindItemData;
            if (null != item)
                filter = item.Code;
            if (!string.IsNullOrEmpty(filter))
                filter = string.Format("{0}='{1}'", SkillItemData.COLSkillSrcType, filter);
            else
                join = string.Empty;
            var filterIn = this.txtSearch.Text.Trim();
            if (!string.IsNullOrEmpty(filterIn))
                filterIn = string.Format("({0} like '{2}%' or {1} like '{2}%')", SkillItemData.COLSkillCode, SkillItemData.COLSkillName, filterIn);
            else
                join = string.Empty;
            this._bindData.DefaultView.RowFilter = string.Concat(filter, join, filterIn);
        }
        void RenewHandler(object sender, EventArgs e)
        {
            this.Edit(OperationState.Renew);
        }
        void AddHandler(object sender, EventArgs e)
        {
            this.Edit(OperationState.New);
        }
        void EditHandler(object sender, EventArgs e)
        {
            this.Edit(OperationState.Edit);
        }
        void CopyHandler(object sender, EventArgs e)
        {
            this.Edit(OperationState.CopyNew);
        }
        void DeleteHandler(object sender, EventArgs e)
        {
            this.Delete();
        }
        void gridList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                gridList.ClearSelection();
                gridList.Rows[e.RowIndex].Selected = true;
                gridList.CurrentCell = gridList.Rows[e.RowIndex].Cells[0];
            }
        }
        void gridList_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();
        }
        void gridList_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            for (int i = e.RowIndex + e.RowCount - 1; i < gridList.Rows.GetLastRow(DataGridViewElementStates.Displayed); i++)
            {
                gridList.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        #endregion

        #region Cache
        readonly OpenFileDialog _diagOpenFile = new OpenFileDialog();
        readonly SaveFileDialog _diagSaveFile = new SaveFileDialog();
        string _openFilePath = string.Empty;
        XElement _rootXml = null;
        DataTable _bindData = null;
        #endregion

        #region Load
        void Init()
        {
            if (null == _bindData)
                _bindData = SharedLogic.GetBindSkillSchema();
            if (null == _rootXml)
                _rootXml = new XElement(SharedLogic.KEYRoot);
            SharedLogic.BindControl(this.combSkillType, SharedData.Instance.BindSkillSrcType(true));
            this.gridList.AutoGenerateColumns = false;
            this.gridList.Columns.Clear();
            var txtCol = new DataGridViewTextBoxColumn();
            txtCol.DataPropertyName =SkillItemData.COLSkillId;
            txtCol.HeaderText = "技能Id";
            txtCol.Width = 100;
            this.gridList.Columns.Add(txtCol);
            txtCol = new DataGridViewTextBoxColumn();
            txtCol.DataPropertyName = SkillItemData.COLSkillCode;
            txtCol.HeaderText = "技能Code";
            txtCol.Width = 100;
            this.gridList.Columns.Add(txtCol);
            txtCol = new DataGridViewTextBoxColumn();
            txtCol.DataPropertyName = SkillItemData.COLSkillName;
            txtCol.HeaderText = "技能名称";
            txtCol.Width = 120;
            this.gridList.Columns.Add(txtCol);
            var combCol = new DataGridViewComboBoxColumn();
            combCol.DataSource = SharedData.Instance.BindSkillSrcType();
            combCol.DisplayMember = BindItemData.COLText;
            combCol.ValueMember = BindItemData.COLCode;
            combCol.DataPropertyName = SkillItemData.COLSkillSrcType;
            combCol.HeaderText = "技能类型";
            combCol.Width = 100;
            combCol.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
            this.gridList.Columns.Add(combCol);
            txtCol = new DataGridViewTextBoxColumn();
            txtCol.DataPropertyName = SkillItemData.COLModelId;
            txtCol.HeaderText = "模型Id";
            txtCol.Width = 80;
            this.gridList.Columns.Add(txtCol);
            txtCol = new DataGridViewTextBoxColumn();
            txtCol.DataPropertyName = SkillItemData.COLOpenClipId;
            txtCol.HeaderText = "开场动画Id";
            txtCol.Width = 80;
            this.gridList.Columns.Add(txtCol);
            txtCol = new DataGridViewTextBoxColumn();
            txtCol.DataPropertyName = SkillItemData.COLClipId;
            txtCol.HeaderText = "释放动画Id";
            txtCol.Width = 80;
            this.gridList.Columns.Add(txtCol);
            txtCol = new DataGridViewTextBoxColumn();
            txtCol.DataPropertyName = SkillItemData.COLMemo;
            txtCol.HeaderText = "效果描述";
            txtCol.Width = 360;
            this.gridList.Columns.Add(txtCol);
            this._bindData.DefaultView.Sort = SkillItemData.COLSkillCode;
            this.gridList.DataSource = _bindData.DefaultView;
        }
        void Save(bool newFlag)
        {
            if (string.IsNullOrEmpty(this._openFilePath) || newFlag)
            {
                if (this._diagSaveFile.ShowDialog() != DialogResult.OK)
                    return;
            }
            try
            {
                this._bindData.DefaultView.RowFilter = "";
                var nXml = SharedLogic.WrapSaveXml(this._rootXml);
                nXml.Save(this._openFilePath);
                var clientXml = SharedLogic.GenClientXml(nXml);
                string fileName = this._openFilePath.Substring(this._openFilePath.LastIndexOf('\\') + 1);
                clientXml.Save(this._openFilePath.Replace(fileName, "SkillClient.xml"));
                SharedLogic.ShowMessage("保存成功");
            }
            catch (Exception ex)
            {
                SharedLogic.ShowMessage("保存文件失败:" + ex.Message + "\n" + ex.StackTrace);
            }
        }
        #endregion

        #region Selected
        string SelecedFile
        {
            get
            {
                return this._openFilePath;
            }
            set
            {
                this._openFilePath = value;
                this.menuFilePath.Text = this._openFilePath;
                this._bindData.DefaultView.RowFilter = "";
            }
        }
        DataRow SelectdRow()
        {
            var gridRow = this.gridList.CurrentRow;
            if (null == gridRow)
                return null;
            var bindRow = gridRow.DataBoundItem as DataRowView;
            if (null == bindRow)
                return null;
            return bindRow.Row;
        }
        string SelectedCode()
        {
            var dataRow = SelectdRow();
            if (null == dataRow)
                return string.Empty;
            return dataRow[SkillItemData.COLSkillCode].ToString();
        }
        #endregion

        #region Edit
        void Edit(OperationState opState)
        {
            string skillCode = string.Empty;
            if (opState == OperationState.Edit || opState == OperationState.CopyNew)
            {
                var selRow = SelectdRow();
                if (null == selRow)
                {
                    SharedLogic.ShowMessage("请选择要编辑的行");
                    return;
                }
                skillCode = selRow[SkillItemData.COLSkillCode].ToString();
            }
            this._bindData.TableName = string.Empty;
            var editForm = new SkillEditForm();
            editForm.Init(opState, skillCode, this._rootXml, this._bindData);
            editForm.ShowDialog();
            string newCode = this._bindData.TableName;
            if (string.IsNullOrEmpty(newCode))
                return;
            foreach (DataGridViewRow gridRow in this.gridList.Rows)
            {
                var bindRow = ((DataRowView)gridRow.DataBoundItem).Row;
                if (string.Compare(bindRow[SkillItemData.COLSkillCode].ToString(), newCode, true) == 0)
                {
                    gridRow.Selected = true;
                    gridList.CurrentCell = gridRow.Cells[0];
                    return;
                }
            }
            this._bindData.DefaultView.RowFilter = "";
        }
        void Delete()
        {
            var selRow = SelectdRow();
            if (null == selRow)
            {
                SharedLogic.ShowMessage("请选择要删除的行");
                return;
            }
            var selXml = SharedLogic.GetSkillXml(this._rootXml, selRow[SkillItemData.COLSkillCode].ToString());
            if (null == selXml)
            {
                SharedLogic.ShowMessage("未找到要删除的节点");
                return;
            }
            string skillCode = selRow[SkillItemData.COLSkillCode].ToString();
            if (MessageBox.Show("确定要删除技能[" + skillCode + "]吗？", "系统信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                return;
            this._bindData.Rows.Remove(selRow);
            this._bindData.AcceptChanges();
            selXml.Remove();
        }
        #endregion

    }
}
