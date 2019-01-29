namespace SkillEngine.Editor.Football.UI.FilterControls
{
    partial class GraphFilterCtrl
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMinSpan = new System.Windows.Forms.Label();
            this.txtMinSpan = new SkillEngine.Editor.Football.UI.ControlLib.VMaskedTextBox();
            this.lblMaxSpan = new System.Windows.Forms.Label();
            this.txtMaxSpan = new SkillEngine.Editor.Football.UI.ControlLib.VMaskedTextBox();
            this.combGraphType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblGroundType = new System.Windows.Forms.Label();
            this.combSeekType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblSeekType = new System.Windows.Forms.Label();
            this.pnlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBox
            // 
            this.pnlBox.Controls.Add(this.combGraphType);
            this.pnlBox.Controls.Add(this.lblGroundType);
            this.pnlBox.Controls.Add(this.combSeekType);
            this.pnlBox.Controls.Add(this.lblSeekType);
            this.pnlBox.Controls.Add(this.txtMaxSpan);
            this.pnlBox.Controls.Add(this.txtMinSpan);
            this.pnlBox.Controls.Add(this.lblMaxSpan);
            this.pnlBox.Controls.Add(this.lblMinSpan);
            this.pnlBox.Size = new System.Drawing.Size(821, 40);
            // 
            // lblMinSpan
            // 
            this.lblMinSpan.AutoSize = true;
            this.lblMinSpan.Location = new System.Drawing.Point(478, 13);
            this.lblMinSpan.Name = "lblMinSpan";
            this.lblMinSpan.Size = new System.Drawing.Size(65, 12);
            this.lblMinSpan.TabIndex = 5;
            this.lblMinSpan.Text = "最小距离：";
            // 
            // txtMinSpan
            // 
            this.txtMinSpan.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtMinSpan.Location = new System.Drawing.Point(550, 10);
            this.txtMinSpan.Mask = "0000";
            this.txtMinSpan.Name = "txtMinSpan";
            this.txtMinSpan.PromptChar = '0';
            this.txtMinSpan.Size = new System.Drawing.Size(40, 21);
            this.txtMinSpan.TabIndex = 7;
            this.txtMinSpan.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            // 
            // lblMaxSpan
            // 
            this.lblMaxSpan.AutoSize = true;
            this.lblMaxSpan.Location = new System.Drawing.Point(601, 13);
            this.lblMaxSpan.Name = "lblMaxSpan";
            this.lblMaxSpan.Size = new System.Drawing.Size(65, 12);
            this.lblMaxSpan.TabIndex = 5;
            this.lblMaxSpan.Text = "最大距离：";
            // 
            // txtMaxSpan
            // 
            this.txtMaxSpan.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtMaxSpan.Location = new System.Drawing.Point(673, 10);
            this.txtMaxSpan.Mask = "0000";
            this.txtMaxSpan.Name = "txtMaxSpan";
            this.txtMaxSpan.PromptChar = '0';
            this.txtMaxSpan.Size = new System.Drawing.Size(40, 21);
            this.txtMaxSpan.TabIndex = 7;
            this.txtMaxSpan.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            // 
            // combGraphType
            // 
            this.combGraphType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combGraphType.FormattingEnabled = true;
            this.combGraphType.Location = new System.Drawing.Point(323, 10);
            this.combGraphType.Name = "combGraphType";
            this.combGraphType.Size = new System.Drawing.Size(120, 20);
            this.combGraphType.TabIndex = 13;
            // 
            // lblGroundType
            // 
            this.lblGroundType.AutoSize = true;
            this.lblGroundType.Location = new System.Drawing.Point(252, 13);
            this.lblGroundType.Name = "lblGroundType";
            this.lblGroundType.Size = new System.Drawing.Size(65, 12);
            this.lblGroundType.TabIndex = 12;
            this.lblGroundType.Text = "球员位置：";
            // 
            // combSeekType
            // 
            this.combSeekType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSeekType.FormattingEnabled = true;
            this.combSeekType.Location = new System.Drawing.Point(102, 10);
            this.combSeekType.Name = "combSeekType";
            this.combSeekType.Size = new System.Drawing.Size(120, 20);
            this.combSeekType.TabIndex = 11;
            // 
            // lblSeekType
            // 
            this.lblSeekType.AutoSize = true;
            this.lblSeekType.Location = new System.Drawing.Point(31, 13);
            this.lblSeekType.Name = "lblSeekType";
            this.lblSeekType.Size = new System.Drawing.Size(65, 12);
            this.lblSeekType.TabIndex = 10;
            this.lblSeekType.Text = "查找类型：";
            // 
            // GraphFilterCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "GraphFilterCtrl";
            this.Size = new System.Drawing.Size(821, 40);
            this.pnlBox.ResumeLayout(false);
            this.pnlBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMinSpan;
        public ControlLib.VMaskedTextBox txtMinSpan;
        private System.Windows.Forms.Label lblMaxSpan;
        public ControlLib.VMaskedTextBox txtMaxSpan;
        public ControlLib.VComboBox combGraphType;
        private System.Windows.Forms.Label lblGroundType;
        public ControlLib.VComboBox combSeekType;
        private System.Windows.Forms.Label lblSeekType;
    }
}
