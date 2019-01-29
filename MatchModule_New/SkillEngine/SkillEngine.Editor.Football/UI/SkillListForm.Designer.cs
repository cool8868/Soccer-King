namespace SkillEngine.Editor.Football.UI
{
    partial class SkillListForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkillListForm));
            this.menuGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolRenew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuForm = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuRenewItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAddItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuEditItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDeleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFilePath = new System.Windows.Forms.ToolStripTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.combSkillType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gridList = new System.Windows.Forms.DataGridView();
            this.menuCopyNewItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolCopyNew = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGrid.SuspendLayout();
            this.menuForm.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).BeginInit();
            this.SuspendLayout();
            // 
            // menuGrid
            // 
            this.menuGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolRenew,
            this.toolCopyNew,
            this.toolStripSeparator3,
            this.toolAdd,
            this.toolEdit,
            this.toolDelete});
            this.menuGrid.Name = "contextMenuStrip1";
            this.menuGrid.ShowImageMargin = false;
            this.menuGrid.Size = new System.Drawing.Size(100, 120);
            // 
            // toolRenew
            // 
            this.toolRenew.Name = "toolRenew";
            this.toolRenew.Size = new System.Drawing.Size(99, 22);
            this.toolRenew.Text = "连续添加";
            // 
            // toolAdd
            // 
            this.toolAdd.Name = "toolAdd";
            this.toolAdd.Size = new System.Drawing.Size(99, 22);
            this.toolAdd.Text = "添加";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(96, 6);
            // 
            // toolEdit
            // 
            this.toolEdit.Name = "toolEdit";
            this.toolEdit.Size = new System.Drawing.Size(99, 22);
            this.toolEdit.Text = "编辑";
            // 
            // toolDelete
            // 
            this.toolDelete.Name = "toolDelete";
            this.toolDelete.Size = new System.Drawing.Size(99, 22);
            this.toolDelete.Text = "删除";
            // 
            // menuForm
            // 
            this.menuForm.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuFilePath});
            this.menuForm.Location = new System.Drawing.Point(0, 0);
            this.menuForm.Name = "menuForm";
            this.menuForm.Size = new System.Drawing.Size(721, 27);
            this.menuForm.TabIndex = 6;
            this.menuForm.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOpen,
            this.menuCheck,
            this.toolStripSeparator1,
            this.menuSave,
            this.menuSaveAs,
            this.toolStripSeparator2,
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(44, 23);
            this.menuFile.Text = "文件";
            // 
            // menuOpen
            // 
            this.menuOpen.Name = "menuOpen";
            this.menuOpen.Size = new System.Drawing.Size(112, 22);
            this.menuOpen.Text = "打开";
            this.menuOpen.ToolTipText = "打开配置文件";
            // 
            // menuCheck
            // 
            this.menuCheck.Name = "menuCheck";
            this.menuCheck.Size = new System.Drawing.Size(112, 22);
            this.menuCheck.Text = "验证";
            this.menuCheck.ToolTipText = "验证文件格式";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(109, 6);
            // 
            // menuSave
            // 
            this.menuSave.Name = "menuSave";
            this.menuSave.Size = new System.Drawing.Size(112, 22);
            this.menuSave.Text = "保存";
            // 
            // menuSaveAs
            // 
            this.menuSaveAs.Name = "menuSaveAs";
            this.menuSaveAs.Size = new System.Drawing.Size(112, 22);
            this.menuSaveAs.Text = "另存为";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(109, 6);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(112, 22);
            this.menuExit.Text = "退出";
            // 
            // menuEdit
            // 
            this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuRenewItem,
            this.menuCopyNewItem,
            this.toolStripSeparator4,
            this.menuAddItem,
            this.menuEditItem,
            this.menuDeleteItem});
            this.menuEdit.Name = "menuEdit";
            this.menuEdit.Size = new System.Drawing.Size(44, 23);
            this.menuEdit.Text = "编辑";
            // 
            // menuRenewItem
            // 
            this.menuRenewItem.Name = "menuRenewItem";
            this.menuRenewItem.Size = new System.Drawing.Size(152, 22);
            this.menuRenewItem.Text = "连续添加";
            // 
            // menuAddItem
            // 
            this.menuAddItem.Name = "menuAddItem";
            this.menuAddItem.Size = new System.Drawing.Size(152, 22);
            this.menuAddItem.Text = "添加";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // menuEditItem
            // 
            this.menuEditItem.Name = "menuEditItem";
            this.menuEditItem.Size = new System.Drawing.Size(152, 22);
            this.menuEditItem.Text = "编辑";
            // 
            // menuDeleteItem
            // 
            this.menuDeleteItem.Name = "menuDeleteItem";
            this.menuDeleteItem.Size = new System.Drawing.Size(152, 22);
            this.menuDeleteItem.Text = "删除";
            // 
            // menuFilePath
            // 
            this.menuFilePath.AutoSize = false;
            this.menuFilePath.ForeColor = System.Drawing.Color.Maroon;
            this.menuFilePath.Name = "menuFilePath";
            this.menuFilePath.ReadOnly = true;
            this.menuFilePath.Size = new System.Drawing.Size(620, 23);
            this.menuFilePath.Text = "<未打开文件>";
            this.menuFilePath.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.menuFilePath.ToolTipText = "文件路径";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.combSkillType);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(721, 46);
            this.panel1.TabIndex = 7;
            // 
            // combSkillType
            // 
            this.combSkillType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSkillType.FormattingEnabled = true;
            this.combSkillType.Location = new System.Drawing.Point(13, 14);
            this.combSkillType.Name = "combSkillType";
            this.combSkillType.Size = new System.Drawing.Size(121, 20);
            this.combSkillType.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("华文彩云", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(610, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 32);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(267, 14);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(317, 21);
            this.txtSearch.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(142, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "技能编号\\名称查找：";
            // 
            // gridList
            // 
            this.gridList.AllowUserToAddRows = false;
            this.gridList.AllowUserToDeleteRows = false;
            this.gridList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridList.ContextMenuStrip = this.menuGrid;
            this.gridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridList.Location = new System.Drawing.Point(0, 73);
            this.gridList.Name = "gridList";
            this.gridList.ReadOnly = true;
            this.gridList.RowHeadersWidth = 60;
            this.gridList.RowTemplate.Height = 23;
            this.gridList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridList.Size = new System.Drawing.Size(721, 402);
            this.gridList.TabIndex = 8;
            // 
            // menuCopyNewItem
            // 
            this.menuCopyNewItem.Name = "menuCopyNewItem";
            this.menuCopyNewItem.Size = new System.Drawing.Size(152, 22);
            this.menuCopyNewItem.Text = "复制添加";
            // 
            // toolCopyNew
            // 
            this.toolCopyNew.Name = "toolCopyNew";
            this.toolCopyNew.Size = new System.Drawing.Size(99, 22);
            this.toolCopyNew.Text = "复制添加";
            // 
            // SkillListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 475);
            this.Controls.Add(this.gridList);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuForm);
            this.Name = "SkillListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "技能列表";
            this.menuGrid.ResumeLayout(false);
            this.menuForm.ResumeLayout(false);
            this.menuForm.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip menuGrid;
        private System.Windows.Forms.ToolStripMenuItem toolAdd;
        private System.Windows.Forms.ToolStripMenuItem toolEdit;
        private System.Windows.Forms.ToolStripMenuItem toolDelete;
        private System.Windows.Forms.MenuStrip menuForm;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuEdit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolStripMenuItem menuOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuSave;
        private System.Windows.Forms.ToolStripMenuItem menuSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuAddItem;
        private System.Windows.Forms.ToolStripMenuItem menuEditItem;
        private System.Windows.Forms.ToolStripMenuItem menuDeleteItem;
        private System.Windows.Forms.DataGridView gridList;
        private System.Windows.Forms.ToolStripTextBox menuFilePath;
        private System.Windows.Forms.ToolStripMenuItem toolRenew;
        private System.Windows.Forms.ToolStripMenuItem menuRenewItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuCheck;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private ControlLib.VComboBox combSkillType;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem toolCopyNew;
        private System.Windows.Forms.ToolStripMenuItem menuCopyNewItem;
    }
}