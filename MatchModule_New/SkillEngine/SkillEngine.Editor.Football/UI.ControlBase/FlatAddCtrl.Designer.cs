namespace SkillEngine.Editor.Football.UI.ControlBase
{
    partial class FlatAddCtrl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlatAddCtrl));
            this.pnlTool = new System.Windows.Forms.Panel();
            this.lblTip = new System.Windows.Forms.Label();
            this.btnFold = new System.Windows.Forms.Button();
            this.combClassType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.pnlItem = new System.Windows.Forms.Panel();
            this.pnlTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTool
            // 
            this.pnlTool.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnlTool.Controls.Add(this.lblTip);
            this.pnlTool.Controls.Add(this.btnFold);
            this.pnlTool.Controls.Add(this.combClassType);
            this.pnlTool.Controls.Add(this.btnRemove);
            this.pnlTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTool.Location = new System.Drawing.Point(0, 0);
            this.pnlTool.Name = "pnlTool";
            this.pnlTool.Size = new System.Drawing.Size(678, 29);
            this.pnlTool.TabIndex = 0;
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTip.ForeColor = System.Drawing.Color.Blue;
            this.lblTip.Location = new System.Drawing.Point(83, 8);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(53, 12);
            this.lblTip.TabIndex = 5;
            this.lblTip.Text = "指示文字";
            // 
            // btnFold
            // 
            this.btnFold.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFold.FlatAppearance.BorderSize = 0;
            this.btnFold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFold.Image = ((System.Drawing.Image)(resources.GetObject("btnFold.Image")));
            this.btnFold.Location = new System.Drawing.Point(6, 2);
            this.btnFold.Name = "btnFold";
            this.btnFold.Size = new System.Drawing.Size(25, 25);
            this.btnFold.TabIndex = 4;
            this.btnFold.UseVisualStyleBackColor = true;
            // 
            // combClassType
            // 
            this.combClassType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combClassType.DropDownWidth = 157;
            this.combClassType.FormattingEnabled = true;
            this.combClassType.Location = new System.Drawing.Point(174, 5);
            this.combClassType.Name = "combClassType";
            this.combClassType.Size = new System.Drawing.Size(157, 20);
            this.combClassType.TabIndex = 1;
            // 
            // btnRemove
            // 
            this.btnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Image = global::SkillEngine.Editor.Football.Properties.Resources.miniminus;
            this.btnRemove.Location = new System.Drawing.Point(42, 2);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(25, 25);
            this.btnRemove.TabIndex = 0;
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // pnlItem
            // 
            this.pnlItem.AutoSize = true;
            this.pnlItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlItem.Location = new System.Drawing.Point(0, 29);
            this.pnlItem.Name = "pnlItem";
            this.pnlItem.Size = new System.Drawing.Size(678, 4);
            this.pnlItem.TabIndex = 2;
            // 
            // FlatAddCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.pnlItem);
            this.Controls.Add(this.pnlTool);
            this.Name = "FlatAddCtrl";
            this.Size = new System.Drawing.Size(678, 33);
            this.pnlTool.ResumeLayout(false);
            this.pnlTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public SkillEngine.Editor.Football.UI.ControlLib.VComboBox combClassType;
        public System.Windows.Forms.Button btnRemove;
        protected System.Windows.Forms.Panel pnlTool;
        public System.Windows.Forms.Button btnFold;
        protected System.Windows.Forms.Panel pnlItem;
        private System.Windows.Forms.Label lblTip;
    }
}
