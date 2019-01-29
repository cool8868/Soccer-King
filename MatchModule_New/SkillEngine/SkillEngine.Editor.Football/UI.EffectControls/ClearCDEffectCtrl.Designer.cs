namespace SkillEngine.Editor.Football.UI.EffectControls
{
    partial class ClearCDEffectCtrl
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
            this.lblIds = new System.Windows.Forms.Label();
            this.txtIds = new System.Windows.Forms.TextBox();
            this.cbxDebuffFlag = new System.Windows.Forms.CheckBox();
            this.cbxPureFlag = new System.Windows.Forms.CheckBox();
            this.cbxMainFlag = new System.Windows.Forms.CheckBox();
            this.lblSrcType = new System.Windows.Forms.Label();
            this.combSrcType = new SkillEngine.Editor.Football.UI.ControlLib.ValueSetPicker();
            this.combActType = new SkillEngine.Editor.Football.UI.ControlLib.ValueSetPicker();
            this.lblActType = new System.Windows.Forms.Label();
            this.pnlHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.lblActType);
            this.pnlHead.Controls.Add(this.combActType);
            this.pnlHead.Controls.Add(this.lblSrcType);
            this.pnlHead.Controls.Add(this.combSrcType);
            this.pnlHead.Controls.Add(this.cbxDebuffFlag);
            this.pnlHead.Controls.Add(this.cbxPureFlag);
            this.pnlHead.Controls.Add(this.cbxMainFlag);
            this.pnlHead.Controls.Add(this.txtIds);
            this.pnlHead.Controls.Add(this.lblIds);
            this.pnlHead.Size = new System.Drawing.Size(678, 58);
            // 
            // lblIds
            // 
            this.lblIds.AutoSize = true;
            this.lblIds.Location = new System.Drawing.Point(8, 10);
            this.lblIds.Name = "lblIds";
            this.lblIds.Size = new System.Drawing.Size(89, 12);
            this.lblIds.TabIndex = 0;
            this.lblIds.Text = "技能Code列表：";
            // 
            // txtIds
            // 
            this.txtIds.Location = new System.Drawing.Point(104, 4);
            this.txtIds.Name = "txtIds";
            this.txtIds.Size = new System.Drawing.Size(323, 21);
            this.txtIds.TabIndex = 1;
            // 
            // cbxDebuffFlag
            // 
            this.cbxDebuffFlag.AutoSize = true;
            this.cbxDebuffFlag.Location = new System.Drawing.Point(583, 7);
            this.cbxDebuffFlag.Name = "cbxDebuffFlag";
            this.cbxDebuffFlag.Size = new System.Drawing.Size(84, 16);
            this.cbxDebuffFlag.TabIndex = 22;
            this.cbxDebuffFlag.Text = "Debuff效果";
            this.cbxDebuffFlag.UseVisualStyleBackColor = true;
            // 
            // cbxPureFlag
            // 
            this.cbxPureFlag.AutoSize = true;
            this.cbxPureFlag.Location = new System.Drawing.Point(505, 7);
            this.cbxPureFlag.Name = "cbxPureFlag";
            this.cbxPureFlag.Size = new System.Drawing.Size(72, 16);
            this.cbxPureFlag.TabIndex = 23;
            this.cbxPureFlag.Text = "神圣效果";
            this.cbxPureFlag.UseVisualStyleBackColor = true;
            // 
            // cbxMainFlag
            // 
            this.cbxMainFlag.AutoSize = true;
            this.cbxMainFlag.Location = new System.Drawing.Point(439, 7);
            this.cbxMainFlag.Name = "cbxMainFlag";
            this.cbxMainFlag.Size = new System.Drawing.Size(60, 16);
            this.cbxMainFlag.TabIndex = 24;
            this.cbxMainFlag.Text = "主效果";
            this.cbxMainFlag.UseVisualStyleBackColor = true;
            // 
            // lblSrcType
            // 
            this.lblSrcType.AutoSize = true;
            this.lblSrcType.Location = new System.Drawing.Point(8, 32);
            this.lblSrcType.Name = "lblSrcType";
            this.lblSrcType.Size = new System.Drawing.Size(89, 12);
            this.lblSrcType.TabIndex = 28;
            this.lblSrcType.Text = "技能来源类型：";
            // 
            // combSrcType
            // 
            this.combSrcType.Location = new System.Drawing.Point(104, 29);
            this.combSrcType.Name = "combSrcType";
            this.combSrcType.Size = new System.Drawing.Size(207, 20);
            this.combSrcType.TabIndex = 26;
            // 
            // combActType
            // 
            this.combActType.Location = new System.Drawing.Point(417, 29);
            this.combActType.Name = "combActType";
            this.combActType.Size = new System.Drawing.Size(207, 20);
            this.combActType.TabIndex = 26;
            // 
            // lblActType
            // 
            this.lblActType.AutoSize = true;
            this.lblActType.Location = new System.Drawing.Point(322, 32);
            this.lblActType.Name = "lblActType";
            this.lblActType.Size = new System.Drawing.Size(89, 12);
            this.lblActType.TabIndex = 28;
            this.lblActType.Text = "技能动作类型：";
            // 
            // ClearCDEffectCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "ClearCDEffectCtrl";
            this.Size = new System.Drawing.Size(678, 195);
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblIds;
        private System.Windows.Forms.Label lblActType;
        private System.Windows.Forms.Label lblSrcType;
        public System.Windows.Forms.TextBox txtIds;
        public System.Windows.Forms.CheckBox cbxMainFlag;
        public System.Windows.Forms.CheckBox cbxDebuffFlag;
        public System.Windows.Forms.CheckBox cbxPureFlag;
        public ControlLib.ValueSetPicker combSrcType;
        public ControlLib.ValueSetPicker combActType;
    }
}
