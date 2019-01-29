namespace SkillEngine.Editor.Football.UI.EffectControls
{
    partial class PropPlusEffectCtrl
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
            this.cbxDebuffFlag = new System.Windows.Forms.CheckBox();
            this.cbxPureFlag = new System.Windows.Forms.CheckBox();
            this.cbxMainFlag = new System.Windows.Forms.CheckBox();
            this.combBuffId = new SkillEngine.Editor.Football.UI.ControlLib.ValueSetPicker();
            this.lblBuffId = new System.Windows.Forms.Label();
            this.pnlHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.cbxDebuffFlag);
            this.pnlHead.Controls.Add(this.cbxPureFlag);
            this.pnlHead.Controls.Add(this.cbxMainFlag);
            this.pnlHead.Controls.Add(this.combBuffId);
            this.pnlHead.Controls.Add(this.lblBuffId);
            // 
            // cbxDebuffFlag
            // 
            this.cbxDebuffFlag.AutoSize = true;
            this.cbxDebuffFlag.Location = new System.Drawing.Point(582, 8);
            this.cbxDebuffFlag.Name = "cbxDebuffFlag";
            this.cbxDebuffFlag.Size = new System.Drawing.Size(84, 16);
            this.cbxDebuffFlag.TabIndex = 5;
            this.cbxDebuffFlag.Text = "Debuff效果";
            this.cbxDebuffFlag.UseVisualStyleBackColor = true;
            // 
            // cbxPureFlag
            // 
            this.cbxPureFlag.AutoSize = true;
            this.cbxPureFlag.Location = new System.Drawing.Point(504, 8);
            this.cbxPureFlag.Name = "cbxPureFlag";
            this.cbxPureFlag.Size = new System.Drawing.Size(72, 16);
            this.cbxPureFlag.TabIndex = 6;
            this.cbxPureFlag.Text = "神圣效果";
            this.cbxPureFlag.UseVisualStyleBackColor = true;
            // 
            // cbxMainFlag
            // 
            this.cbxMainFlag.AutoSize = true;
            this.cbxMainFlag.Location = new System.Drawing.Point(438, 8);
            this.cbxMainFlag.Name = "cbxMainFlag";
            this.cbxMainFlag.Size = new System.Drawing.Size(60, 16);
            this.cbxMainFlag.TabIndex = 7;
            this.cbxMainFlag.Text = "主效果";
            this.cbxMainFlag.UseVisualStyleBackColor = true;
            // 
            // combBuffId
            // 
            this.combBuffId.DataSource = null;
            this.combBuffId.ForeColor = System.Drawing.SystemColors.WindowText;
            this.combBuffId.IsDropSizable = true;
            this.combBuffId.Location = new System.Drawing.Point(93, 5);
            this.combBuffId.Name = "combBuffId";
            this.combBuffId.NoneAsFullFlag = true;
            this.combBuffId.Size = new System.Drawing.Size(317, 21);
            this.combBuffId.TabIndex = 4;
            // 
            // lblBuffId
            // 
            this.lblBuffId.AutoSize = true;
            this.lblBuffId.Location = new System.Drawing.Point(21, 10);
            this.lblBuffId.Name = "lblBuffId";
            this.lblBuffId.Size = new System.Drawing.Size(65, 12);
            this.lblBuffId.TabIndex = 3;
            this.lblBuffId.Text = "效果类型：";
            // 
            // PropPlusEffectCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "PropPlusEffectCtrl";
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblBuffId;
        public ControlLib.ValueSetPicker combBuffId;
        public System.Windows.Forms.CheckBox cbxMainFlag;
        public System.Windows.Forms.CheckBox cbxPureFlag;
        public System.Windows.Forms.CheckBox cbxDebuffFlag;
    }
}
