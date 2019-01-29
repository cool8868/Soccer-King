namespace SkillEngine.Editor.Football.UI.FilterControls
{
    partial class PlayerStatFilterCtrl
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
            this.txtMaxValue = new SkillEngine.Editor.Football.UI.ControlLib.VMaskedTextBox();
            this.txtMinValue = new SkillEngine.Editor.Football.UI.ControlLib.VMaskedTextBox();
            this.combStatType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblMaxValue = new System.Windows.Forms.Label();
            this.lblMinValue = new System.Windows.Forms.Label();
            this.lblStatType = new System.Windows.Forms.Label();
            this.pnlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBox
            // 
            this.pnlBox.Controls.Add(this.txtMaxValue);
            this.pnlBox.Controls.Add(this.txtMinValue);
            this.pnlBox.Controls.Add(this.combStatType);
            this.pnlBox.Controls.Add(this.lblMaxValue);
            this.pnlBox.Controls.Add(this.lblMinValue);
            this.pnlBox.Controls.Add(this.lblStatType);
            // 
            // txtMaxValue
            // 
            this.txtMaxValue.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtMaxValue.Location = new System.Drawing.Point(571, 10);
            this.txtMaxValue.Mask = "#000";
            this.txtMaxValue.Name = "txtMaxValue";
            this.txtMaxValue.PromptChar = '0';
            this.txtMaxValue.Size = new System.Drawing.Size(40, 21);
            this.txtMaxValue.TabIndex = 26;
            this.txtMaxValue.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            // 
            // txtMinValue
            // 
            this.txtMinValue.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtMinValue.Location = new System.Drawing.Point(448, 10);
            this.txtMinValue.Mask = "#000";
            this.txtMinValue.Name = "txtMinValue";
            this.txtMinValue.PromptChar = '0';
            this.txtMinValue.Size = new System.Drawing.Size(40, 21);
            this.txtMinValue.TabIndex = 27;
            this.txtMinValue.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            // 
            // combStatType
            // 
            this.combStatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combStatType.FormattingEnabled = true;
            this.combStatType.Location = new System.Drawing.Point(93, 10);
            this.combStatType.Name = "combStatType";
            this.combStatType.Size = new System.Drawing.Size(255, 20);
            this.combStatType.TabIndex = 25;
            // 
            // lblMaxValue
            // 
            this.lblMaxValue.AutoSize = true;
            this.lblMaxValue.Location = new System.Drawing.Point(515, 13);
            this.lblMaxValue.Name = "lblMaxValue";
            this.lblMaxValue.Size = new System.Drawing.Size(53, 12);
            this.lblMaxValue.TabIndex = 22;
            this.lblMaxValue.Text = "最大值：";
            // 
            // lblMinValue
            // 
            this.lblMinValue.AutoSize = true;
            this.lblMinValue.Location = new System.Drawing.Point(391, 13);
            this.lblMinValue.Name = "lblMinValue";
            this.lblMinValue.Size = new System.Drawing.Size(53, 12);
            this.lblMinValue.TabIndex = 23;
            this.lblMinValue.Text = "最小值：";
            // 
            // lblStatType
            // 
            this.lblStatType.AutoSize = true;
            this.lblStatType.Location = new System.Drawing.Point(21, 15);
            this.lblStatType.Name = "lblStatType";
            this.lblStatType.Size = new System.Drawing.Size(65, 12);
            this.lblStatType.TabIndex = 24;
            this.lblStatType.Text = "统计类型：";
            // 
            // PlayerStatFilterCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "PlayerStatFilterCtrl";
            this.pnlBox.ResumeLayout(false);
            this.pnlBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMaxValue;
        private System.Windows.Forms.Label lblMinValue;
        private System.Windows.Forms.Label lblStatType;
        public ControlLib.VComboBox combStatType;
        public ControlLib.VMaskedTextBox txtMinValue;
        public ControlLib.VMaskedTextBox txtMaxValue;
    }
}
