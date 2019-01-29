namespace SkillEngine.Editor.Football.UI.FilterControls
{
    partial class StaminaCompareFilterCtrl
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
            this.combRateFlag = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblMaxValue = new System.Windows.Forms.Label();
            this.lblMinValue = new System.Windows.Forms.Label();
            this.lblRateFlag = new System.Windows.Forms.Label();
            this.cbxRelateFlag = new System.Windows.Forms.CheckBox();
            this.pnlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBox
            // 
            this.pnlBox.Controls.Add(this.cbxRelateFlag);
            this.pnlBox.Controls.Add(this.txtMaxValue);
            this.pnlBox.Controls.Add(this.txtMinValue);
            this.pnlBox.Controls.Add(this.combRateFlag);
            this.pnlBox.Controls.Add(this.lblMaxValue);
            this.pnlBox.Controls.Add(this.lblMinValue);
            this.pnlBox.Controls.Add(this.lblRateFlag);
            // 
            // txtMaxValue
            // 
            this.txtMaxValue.Location = new System.Drawing.Point(566, 10);
            this.txtMaxValue.Mask = "0000";
            this.txtMaxValue.Name = "txtMaxValue";
            this.txtMaxValue.PromptChar = '0';
            this.txtMaxValue.Size = new System.Drawing.Size(40, 21);
            this.txtMaxValue.TabIndex = 14;
            this.txtMaxValue.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            // 
            // txtMinValue
            // 
            this.txtMinValue.Location = new System.Drawing.Point(443, 10);
            this.txtMinValue.Mask = "0000";
            this.txtMinValue.Name = "txtMinValue";
            this.txtMinValue.PromptChar = '0';
            this.txtMinValue.Size = new System.Drawing.Size(40, 21);
            this.txtMinValue.TabIndex = 15;
            this.txtMinValue.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            // 
            // combRateFlag
            // 
            this.combRateFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combRateFlag.FormattingEnabled = true;
            this.combRateFlag.Location = new System.Drawing.Point(93, 10);
            this.combRateFlag.Name = "combRateFlag";
            this.combRateFlag.Size = new System.Drawing.Size(100, 20);
            this.combRateFlag.TabIndex = 13;
            // 
            // lblMaxValue
            // 
            this.lblMaxValue.AutoSize = true;
            this.lblMaxValue.Location = new System.Drawing.Point(510, 13);
            this.lblMaxValue.Name = "lblMaxValue";
            this.lblMaxValue.Size = new System.Drawing.Size(53, 12);
            this.lblMaxValue.TabIndex = 8;
            this.lblMaxValue.Text = "最大值：";
            // 
            // lblMinValue
            // 
            this.lblMinValue.AutoSize = true;
            this.lblMinValue.Location = new System.Drawing.Point(386, 13);
            this.lblMinValue.Name = "lblMinValue";
            this.lblMinValue.Size = new System.Drawing.Size(53, 12);
            this.lblMinValue.TabIndex = 9;
            this.lblMinValue.Text = "最小值：";
            // 
            // lblRateFlag
            // 
            this.lblRateFlag.AutoSize = true;
            this.lblRateFlag.Location = new System.Drawing.Point(21, 15);
            this.lblRateFlag.Name = "lblRateFlag";
            this.lblRateFlag.Size = new System.Drawing.Size(65, 12);
            this.lblRateFlag.TabIndex = 11;
            this.lblRateFlag.Text = "体力类型：";
            // 
            // cbxRelateFlag
            // 
            this.cbxRelateFlag.AutoSize = true;
            this.cbxRelateFlag.Location = new System.Drawing.Point(231, 12);
            this.cbxRelateFlag.Name = "cbxRelateFlag";
            this.cbxRelateFlag.Size = new System.Drawing.Size(120, 16);
            this.cbxRelateFlag.TabIndex = 16;
            this.cbxRelateFlag.Text = "减去对方球员体力";
            this.cbxRelateFlag.UseVisualStyleBackColor = true;
            // 
            // StaminaCompareFilterCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "StaminaCompareFilterCtrl";
            this.pnlBox.ResumeLayout(false);
            this.pnlBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMaxValue;
        private System.Windows.Forms.Label lblMinValue;
        private System.Windows.Forms.Label lblRateFlag;
        public ControlLib.VComboBox combRateFlag;
        public System.Windows.Forms.CheckBox cbxRelateFlag;
        public ControlLib.VMaskedTextBox txtMaxValue;
        public ControlLib.VMaskedTextBox txtMinValue;
    }
}
