namespace SkillEngine.Editor.Football.UI.EffectControls
{
    partial class ForceStateEffectCtrl
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
            this.combForceState = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblBuffId = new System.Windows.Forms.Label();
            this.cbxDebuffFlag = new System.Windows.Forms.CheckBox();
            this.cbxMainFlag = new System.Windows.Forms.CheckBox();
            this.pnlHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.cbxDebuffFlag);
            this.pnlHead.Controls.Add(this.cbxMainFlag);
            this.pnlHead.Controls.Add(this.combForceState);
            this.pnlHead.Controls.Add(this.lblBuffId);
            // 
            // combForceState
            // 
            this.combForceState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combForceState.FormattingEnabled = true;
            this.combForceState.Location = new System.Drawing.Point(93, 5);
            this.combForceState.Name = "combForceState";
            this.combForceState.Size = new System.Drawing.Size(211, 20);
            this.combForceState.TabIndex = 25;
            // 
            // lblBuffId
            // 
            this.lblBuffId.AutoSize = true;
            this.lblBuffId.Location = new System.Drawing.Point(21, 10);
            this.lblBuffId.Name = "lblBuffId";
            this.lblBuffId.Size = new System.Drawing.Size(65, 12);
            this.lblBuffId.TabIndex = 24;
            this.lblBuffId.Text = "强制状态：";
            // 
            // cbxDebuffFlag
            // 
            this.cbxDebuffFlag.AutoSize = true;
            this.cbxDebuffFlag.Location = new System.Drawing.Point(497, 7);
            this.cbxDebuffFlag.Name = "cbxDebuffFlag";
            this.cbxDebuffFlag.Size = new System.Drawing.Size(84, 16);
            this.cbxDebuffFlag.TabIndex = 26;
            this.cbxDebuffFlag.Text = "Debuff效果";
            this.cbxDebuffFlag.UseVisualStyleBackColor = true;
            // 
            // cbxMainFlag
            // 
            this.cbxMainFlag.AutoSize = true;
            this.cbxMainFlag.Location = new System.Drawing.Point(377, 7);
            this.cbxMainFlag.Name = "cbxMainFlag";
            this.cbxMainFlag.Size = new System.Drawing.Size(60, 16);
            this.cbxMainFlag.TabIndex = 27;
            this.cbxMainFlag.Text = "主效果";
            this.cbxMainFlag.UseVisualStyleBackColor = true;
            // 
            // ForceStateEffectCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ForceStateEffectCtrl";
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public ControlLib.VComboBox combForceState;
        private System.Windows.Forms.Label lblBuffId;
        public System.Windows.Forms.CheckBox cbxDebuffFlag;
        public System.Windows.Forms.CheckBox cbxMainFlag;
    }
}
