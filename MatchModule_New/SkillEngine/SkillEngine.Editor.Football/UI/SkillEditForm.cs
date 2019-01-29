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
    public partial class SkillEditForm : Form
    {
        public SkillEditForm()
        {
            InitializeComponent();
            this.btnSave.Click += btnSave_Click;
            this.FormClosing += SkillEditForm_FormClosing;
        }

        #region EventHandler
        void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }
        void SkillEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确定要关闭吗？", "系统信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
                e.Cancel = true;
        }
        #endregion

        #region Cache
        OperationState _opState = OperationState.New;
        string _editCode = string.Empty;
        XElement _editXml = null;
        XElement _rootXml = null;
        DataTable _bindData = null;
        #endregion

        #region Data
        OperationState OpState
        {
            get { return this._opState; }
            set
            {
                this._opState=value;
                string title=string.Empty;
                switch (this._opState)
                {
                    case OperationState.Renew:
                        title = "--连续添加";
                        break;
                    case OperationState.New:
                        title = "--添加";
                        break;
                    case OperationState.Edit:
                        title = "--编辑";
                        break;
                    case OperationState.CopyNew:
                        title = "--复制添加";
                        break;
                }
                this.Text = "技能信息" + title;
                this.ucSkill.txtSkillCode.Enabled = this._opState != OperationState.Edit;
            }
        }
        #endregion

        #region Init
        public void Init(OperationState opState,string editCode, XElement rootXml, DataTable bindData)
        {
            this.OpState = opState;
            this._editCode = editCode;
            this._rootXml = rootXml;
            this._bindData = bindData;
            this.ucSkill.InitData();
            if (string.IsNullOrEmpty(this._editCode))
            {
                this._editXml = new XElement(SharedLogic.KEYSkill);
                return;
            }
            this._editXml = SharedLogic.GetSkillXml(this._rootXml, this._editCode);
            this.ucSkill.SetValue(this._editXml);
            if (opState == OperationState.CopyNew)
            {
                this._editXml = new XElement(SharedLogic.KEYSkill);
                this.ucSkill.txtSkillCode.Text = "";
                this.ucSkill.txtSkillCode.Focus();
            }
        }
        #endregion

        #region Save
        void Save()
        {
            string skillCode = this.ucSkill.txtSkillCode.Text.Trim();
            string nSkillId = this.ucSkill.txtSKillId.Text.Trim();
            if (string.IsNullOrEmpty(skillCode))
            {
                SharedLogic.ShowMessage("技能Code不可为空");
                return;
            }
            var rows = _bindData.Select(SkillItemData.COLSkillCode + "='" + skillCode + "'");
            if (skillCode != this._editCode && null != rows && rows.Length > 0)
            {
                SharedLogic.ShowMessage("该技能Code已存在");
                this.ucSkill.txtSkillCode.Focus();
                return;
            }
            rows = _bindData.Select(SkillItemData.COLSkillId + "='" + nSkillId.TrimStart('0') + "'");
            if (null != rows)
            {
                foreach (var dr in rows)
                {
                    if (dr[SkillItemData.COLSkillCode].ToString() != _editCode)
                    {
                        SharedLogic.ShowMessage("该技能Id已存在");
                        this.ucSkill.txtSKillId.Focus();
                        return;
                    }
                }
            }
            var nXml = this.ucSkill.GetValue();
            if (null == nXml)
            {
                SharedLogic.ShowMessage("构造XML失败");
                return;
            }
            this._editXml.RemoveAll();
            this._editXml.Add(nXml.Attributes());
            this._editXml.Add(nXml.Nodes());
            var obj = new SkillItemData();
            SharedLogic.TryGetBindSkillItem(nXml, obj);
            if (this.OpState != OperationState.Edit)
            {
                this._rootXml.Add(this._editXml);
                this._bindData.Rows.Add(obj.Values);
                if (this.OpState == OperationState.New)
                {
                    this.OpState = OperationState.Edit;
                    this._editCode = skillCode;
                }
                else if (this.OpState == OperationState.Renew || this.OpState == OperationState.CopyNew)
                {
                    this._editXml = new XElement(SharedLogic.KEYSkill);
                    this.ucSkill.txtSkillCode.Text = "";
                    this.ucSkill.txtSkillCode.Focus();
                }
                this._bindData.TableName = skillCode;
            }
            else
            {
                var editRows = _bindData.Select(SkillItemData.COLSkillCode + "='" + this._editCode + "'");
                if (null == editRows || editRows.Length == 0)
                {
                    SharedLogic.ShowMessage("未找到编辑的行");
                    return;
                }
                editRows[0][SkillItemData.COLSkillId] = obj.SkillId;
                editRows[0][SkillItemData.COLSkillName] = obj.SkillName;
                editRows[0][SkillItemData.COLSkillSrcType] = obj.SkillType;
                editRows[0][SkillItemData.COLModelId] = obj.ModelId;
                editRows[0][SkillItemData.COLOpenClipId] = obj.OpenClipId;
                editRows[0][SkillItemData.COLClipId] = obj.ClipId;
                editRows[0][SkillItemData.COLMemo] = obj.Memo;
            }
            this._bindData.AcceptChanges();
            SharedLogic.ShowMessage("保存成功");
        }
        #endregion


    }
}
