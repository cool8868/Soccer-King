namespace SkillEngine.Editor.Football.UI.SeekerControls
{
    partial class GraphSeekerCtrl
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
            this.combSeekType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblSeekType = new System.Windows.Forms.Label();
            this.txtMaxSpan = new SkillEngine.Editor.Football.UI.ControlLib.VMaskedTextBox();
            this.txtMinSpan = new SkillEngine.Editor.Football.UI.ControlLib.VMaskedTextBox();
            this.combSide = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblMaxSpan = new System.Windows.Forms.Label();
            this.lblMinSpan = new System.Windows.Forms.Label();
            this.lblSide = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtMaxSpan);
            this.panel1.Controls.Add(this.txtMinSpan);
            this.panel1.Controls.Add(this.combSide);
            this.panel1.Controls.Add(this.lblMaxSpan);
            this.panel1.Controls.Add(this.lblMinSpan);
            this.panel1.Controls.Add(this.lblSide);
            this.panel1.Controls.Add(this.combSeekType);
            this.panel1.Controls.Add(this.lblSeekType);
            this.panel1.Size = new System.Drawing.Size(678, 70);
            this.panel1.Controls.SetChildIndex(this.lblSeekType, 0);
            this.panel1.Controls.SetChildIndex(this.combSeekType, 0);
            this.panel1.Controls.SetChildIndex(this.lblJoinType, 0);
            this.panel1.Controls.SetChildIndex(this.combJoinType, 0);
            this.panel1.Controls.SetChildIndex(this.lblSide, 0);
            this.panel1.Controls.SetChildIndex(this.lblMinSpan, 0);
            this.panel1.Controls.SetChildIndex(this.lblMaxSpan, 0);
            this.panel1.Controls.SetChildIndex(this.combSide, 0);
            this.panel1.Controls.SetChildIndex(this.txtMinSpan, 0);
            this.panel1.Controls.SetChildIndex(this.txtMaxSpan, 0);
            // 
            // combSeekType
            // 
            this.combSeekType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSeekType.FormattingEnabled = true;
            this.combSeekType.Location = new System.Drawing.Point(315, 10);
            this.combSeekType.Name = "combSeekType";
            this.combSeekType.Size = new System.Drawing.Size(120, 20);
            this.combSeekType.TabIndex = 15;
            // 
            // lblSeekType
            // 
            this.lblSeekType.AutoSize = true;
            this.lblSeekType.Location = new System.Drawing.Point(244, 13);
            this.lblSeekType.Name = "lblSeekType";
            this.lblSeekType.Size = new System.Drawing.Size(65, 12);
            this.lblSeekType.TabIndex = 14;
            this.lblSeekType.Text = "查找类型：";
            // 
            // txtMaxSpan
            // 
            this.txtMaxSpan.Location = new System.Drawing.Point(398, 40);
            this.txtMaxSpan.Mask = "0000";
            this.txtMaxSpan.Name = "txtMaxSpan";
            this.txtMaxSpan.PromptChar = '0';
            this.txtMaxSpan.Size = new System.Drawing.Size(40, 21);
            this.txtMaxSpan.TabIndex = 20;
            this.txtMaxSpan.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            // 
            // txtMinSpan
            // 
            this.txtMinSpan.Location = new System.Drawing.Point(275, 40);
            this.txtMinSpan.Mask = "0000";
            this.txtMinSpan.Name = "txtMinSpan";
            this.txtMinSpan.PromptChar = '0';
            this.txtMinSpan.Size = new System.Drawing.Size(40, 21);
            this.txtMinSpan.TabIndex = 21;
            this.txtMinSpan.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            // 
            // combSide
            // 
            this.combSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSide.FormattingEnabled = true;
            this.combSide.Location = new System.Drawing.Point(83, 38);
            this.combSide.Name = "combSide";
            this.combSide.Size = new System.Drawing.Size(100, 20);
            this.combSide.TabIndex = 19;
            // 
            // lblMaxSpan
            // 
            this.lblMaxSpan.AutoSize = true;
            this.lblMaxSpan.Location = new System.Drawing.Point(326, 43);
            this.lblMaxSpan.Name = "lblMaxSpan";
            this.lblMaxSpan.Size = new System.Drawing.Size(65, 12);
            this.lblMaxSpan.TabIndex = 16;
            this.lblMaxSpan.Text = "最大距离：";
            // 
            // lblMinSpan
            // 
            this.lblMinSpan.AutoSize = true;
            this.lblMinSpan.Location = new System.Drawing.Point(203, 43);
            this.lblMinSpan.Name = "lblMinSpan";
            this.lblMinSpan.Size = new System.Drawing.Size(65, 12);
            this.lblMinSpan.TabIndex = 17;
            this.lblMinSpan.Text = "最小距离：";
            // 
            // lblSide
            // 
            this.lblSide.AutoSize = true;
            this.lblSide.Location = new System.Drawing.Point(11, 43);
            this.lblSide.Name = "lblSide";
            this.lblSide.Size = new System.Drawing.Size(65, 12);
            this.lblSide.TabIndex = 18;
            this.lblSide.Text = "目标球员：";
            // 
            // GraphSeekerCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "GraphSeekerCtrl";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSeekType;
        private System.Windows.Forms.Label lblMaxSpan;
        private System.Windows.Forms.Label lblMinSpan;
        private System.Windows.Forms.Label lblSide;
        public ControlLib.VComboBox combSeekType;
        public ControlLib.VComboBox combSide;
        public ControlLib.VMaskedTextBox txtMinSpan;
        public ControlLib.VMaskedTextBox txtMaxSpan;
    }
}
