namespace SkillEngine.Editor.Football.UI.EffectControls
{
    partial class BlurEffectCtrl
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
            this.cbxPureFlag = new System.Windows.Forms.CheckBox();
            this.cbxMainFlag = new System.Windows.Forms.CheckBox();
            this.combBuffId = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblBuffId = new System.Windows.Forms.Label();
            this.lblExecType = new System.Windows.Forms.Label();
            this.combExecType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.txtBlurType = new System.Windows.Forms.TextBox();
            this.pnlHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.txtBlurType);
            this.pnlHead.Controls.Add(this.lblExecType);
            this.pnlHead.Controls.Add(this.combExecType);
            this.pnlHead.Controls.Add(this.cbxPureFlag);
            this.pnlHead.Controls.Add(this.cbxMainFlag);
            this.pnlHead.Controls.Add(this.combBuffId);
            this.pnlHead.Controls.Add(this.lblBuffId);
            // 
            // cbxPureFlag
            // 
            this.cbxPureFlag.AutoSize = true;
            this.cbxPureFlag.Location = new System.Drawing.Point(522, 8);
            this.cbxPureFlag.Name = "cbxPureFlag";
            this.cbxPureFlag.Size = new System.Drawing.Size(72, 16);
            this.cbxPureFlag.TabIndex = 24;
            this.cbxPureFlag.Text = "神圣效果";
            this.cbxPureFlag.UseVisualStyleBackColor = true;
            // 
            // cbxMainFlag
            // 
            this.cbxMainFlag.AutoSize = true;
            this.cbxMainFlag.Location = new System.Drawing.Point(456, 8);
            this.cbxMainFlag.Name = "cbxMainFlag";
            this.cbxMainFlag.Size = new System.Drawing.Size(60, 16);
            this.cbxMainFlag.TabIndex = 25;
            this.cbxMainFlag.Text = "主效果";
            this.cbxMainFlag.UseVisualStyleBackColor = true;
            // 
            // combBuffId
            // 
            this.combBuffId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combBuffId.FormattingEnabled = true;
            this.combBuffId.Location = new System.Drawing.Point(93, 6);
            this.combBuffId.Name = "combBuffId";
            this.combBuffId.Size = new System.Drawing.Size(100, 20);
            this.combBuffId.TabIndex = 23;
            // 
            // lblBuffId
            // 
            this.lblBuffId.AutoSize = true;
            this.lblBuffId.Location = new System.Drawing.Point(21, 11);
            this.lblBuffId.Name = "lblBuffId";
            this.lblBuffId.Size = new System.Drawing.Size(65, 12);
            this.lblBuffId.TabIndex = 22;
            this.lblBuffId.Text = "效果类型：";
            // 
            // lblExecType
            // 
            this.lblExecType.AutoSize = true;
            this.lblExecType.Location = new System.Drawing.Point(215, 9);
            this.lblExecType.Name = "lblExecType";
            this.lblExecType.Size = new System.Drawing.Size(89, 12);
            this.lblExecType.TabIndex = 29;
            this.lblExecType.Text = "概率执行类型：";
            // 
            // combExecType
            // 
            this.combExecType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combExecType.FormattingEnabled = true;
            this.combExecType.Location = new System.Drawing.Point(310, 7);
            this.combExecType.Name = "combExecType";
            this.combExecType.Size = new System.Drawing.Size(100, 20);
            this.combExecType.TabIndex = 28;
            // 
            // txtBlurType
            // 
            this.txtBlurType.Location = new System.Drawing.Point(601, 4);
            this.txtBlurType.Name = "txtBlurType";
            this.txtBlurType.Size = new System.Drawing.Size(43, 21);
            this.txtBlurType.TabIndex = 30;
            this.txtBlurType.Visible = false;
            // 
            // BlurEffectCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "BlurEffectCtrl";
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblExecType;
        public ControlLib.VComboBox combBuffId;
        private System.Windows.Forms.Label lblBuffId;
        public ControlLib.VComboBox combExecType;
        public System.Windows.Forms.CheckBox cbxMainFlag;
        public System.Windows.Forms.CheckBox cbxPureFlag;
        private System.Windows.Forms.TextBox txtBlurType;
    }
}
