namespace SkillEngine.Editor.Football.UI.FilterControls
{
    partial class SkillStateFilterCtrl
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
            this.lblActType = new System.Windows.Forms.Label();
            this.combActType = new SkillEngine.Editor.Football.UI.ControlLib.ValueSetPicker();
            this.lblSrcType = new System.Windows.Forms.Label();
            this.combSrcType = new SkillEngine.Editor.Football.UI.ControlLib.ValueSetPicker();
            this.txtIds = new System.Windows.Forms.TextBox();
            this.lblIds = new System.Windows.Forms.Label();
            this.pnlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBox
            // 
            this.pnlBox.Controls.Add(this.lblActType);
            this.pnlBox.Controls.Add(this.combActType);
            this.pnlBox.Controls.Add(this.lblSrcType);
            this.pnlBox.Controls.Add(this.combSrcType);
            this.pnlBox.Controls.Add(this.txtIds);
            this.pnlBox.Controls.Add(this.lblIds);
            this.pnlBox.Size = new System.Drawing.Size(678, 61);
            // 
            // lblActType
            // 
            this.lblActType.AutoSize = true;
            this.lblActType.Location = new System.Drawing.Point(325, 36);
            this.lblActType.Name = "lblActType";
            this.lblActType.Size = new System.Drawing.Size(89, 12);
            this.lblActType.TabIndex = 33;
            this.lblActType.Text = "技能动作类型：";
            // 
            // combActType
            // 
            this.combActType.Location = new System.Drawing.Point(421, 33);
            this.combActType.Name = "combActType";
            this.combActType.Size = new System.Drawing.Size(207, 20);
            this.combActType.TabIndex = 31;
            // 
            // lblSrcType
            // 
            this.lblSrcType.AutoSize = true;
            this.lblSrcType.Location = new System.Drawing.Point(12, 36);
            this.lblSrcType.Name = "lblSrcType";
            this.lblSrcType.Size = new System.Drawing.Size(89, 12);
            this.lblSrcType.TabIndex = 34;
            this.lblSrcType.Text = "技能来源类型：";
            // 
            // combSrcType
            // 
            this.combSrcType.Location = new System.Drawing.Point(108, 33);
            this.combSrcType.Name = "combSrcType";
            this.combSrcType.Size = new System.Drawing.Size(207, 20);
            this.combSrcType.TabIndex = 32;
            // 
            // txtIds
            // 
            this.txtIds.Location = new System.Drawing.Point(108, 8);
            this.txtIds.Name = "txtIds";
            this.txtIds.Size = new System.Drawing.Size(377, 21);
            this.txtIds.TabIndex = 30;
            // 
            // lblIds
            // 
            this.lblIds.AutoSize = true;
            this.lblIds.Location = new System.Drawing.Point(12, 14);
            this.lblIds.Name = "lblIds";
            this.lblIds.Size = new System.Drawing.Size(89, 12);
            this.lblIds.TabIndex = 29;
            this.lblIds.Text = "技能Code列表：";
            // 
            // SkillStateFilterCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "SkillStateFilterCtrl";
            this.Size = new System.Drawing.Size(678, 61);
            this.pnlBox.ResumeLayout(false);
            this.pnlBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblActType;
        private System.Windows.Forms.Label lblSrcType;
        private System.Windows.Forms.Label lblIds;
        public System.Windows.Forms.TextBox txtIds;
        public ControlLib.ValueSetPicker combSrcType;
        public ControlLib.ValueSetPicker combActType;

    }
}
