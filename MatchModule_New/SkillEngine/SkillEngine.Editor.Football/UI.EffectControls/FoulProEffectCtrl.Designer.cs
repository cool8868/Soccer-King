namespace SkillEngine.Editor.Football.UI.EffectControls
{
    partial class FoulProEffectCtrl
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
            this.cbxPureFlag = new System.Windows.Forms.CheckBox();
            this.cbxMainFlag = new System.Windows.Forms.CheckBox();
            this.combBuffId = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblBuffId = new System.Windows.Forms.Label();
            this.pnlHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPoint
            // 
            this.lblPoint.Location = new System.Drawing.Point(456, 79);
            // 
            // lblPercent
            // 
            this.lblPercent.Location = new System.Drawing.Point(226, 79);
            this.lblPercent.Size = new System.Drawing.Size(65, 12);
            this.lblPercent.Text = "得牌概率：";
            // 
            // lblRate
            // 
            this.lblRate.Text = "犯规概率：";
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.cbxPureFlag);
            this.pnlHead.Controls.Add(this.cbxMainFlag);
            this.pnlHead.Controls.Add(this.combBuffId);
            this.pnlHead.Controls.Add(this.lblBuffId);
            // 
            // txtPoint
            // 
            this.txtPoint.Location = new System.Drawing.Point(539, 76);
            // 
            // txtPercent
            // 
            this.txtPercent.Location = new System.Drawing.Point(309, 76);
            // 
            // cbxPureFlag
            // 
            this.cbxPureFlag.AutoSize = true;
            this.cbxPureFlag.Location = new System.Drawing.Point(302, 7);
            this.cbxPureFlag.Name = "cbxPureFlag";
            this.cbxPureFlag.Size = new System.Drawing.Size(72, 16);
            this.cbxPureFlag.TabIndex = 28;
            this.cbxPureFlag.Text = "神圣效果";
            this.cbxPureFlag.UseVisualStyleBackColor = true;
            // 
            // cbxMainFlag
            // 
            this.cbxMainFlag.AutoSize = true;
            this.cbxMainFlag.Location = new System.Drawing.Point(236, 7);
            this.cbxMainFlag.Name = "cbxMainFlag";
            this.cbxMainFlag.Size = new System.Drawing.Size(60, 16);
            this.cbxMainFlag.TabIndex = 29;
            this.cbxMainFlag.Text = "主效果";
            this.cbxMainFlag.UseVisualStyleBackColor = true;
            // 
            // combBuffId
            // 
            this.combBuffId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combBuffId.FormattingEnabled = true;
            this.combBuffId.Location = new System.Drawing.Point(93, 5);
            this.combBuffId.Name = "combBuffId";
            this.combBuffId.Size = new System.Drawing.Size(100, 20);
            this.combBuffId.TabIndex = 27;
            // 
            // lblBuffId
            // 
            this.lblBuffId.AutoSize = true;
            this.lblBuffId.Location = new System.Drawing.Point(21, 10);
            this.lblBuffId.Name = "lblBuffId";
            this.lblBuffId.Size = new System.Drawing.Size(65, 12);
            this.lblBuffId.TabIndex = 26;
            this.lblBuffId.Text = "得牌类型：";
            // 
            // FoulProEffectCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "FoulProEffectCtrl";
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.CheckBox cbxPureFlag;
        public System.Windows.Forms.CheckBox cbxMainFlag;
        public ControlLib.VComboBox combBuffId;
        private System.Windows.Forms.Label lblBuffId;
    }
}
