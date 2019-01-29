namespace SkillEngine.Editor.Football.UI.EffectControls
{
    partial class BoostEffectCtrl
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
            this.lblBoostType = new System.Windows.Forms.Label();
            this.combBoostType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.cbxMainFlag = new System.Windows.Forms.CheckBox();
            this.cbxPureFlag = new System.Windows.Forms.CheckBox();
            this.cbxDebuffFlag = new System.Windows.Forms.CheckBox();
            this.combBuffId = new SkillEngine.Editor.Football.UI.ControlLib.ValueSetPicker();
            this.lblBuffId = new System.Windows.Forms.Label();
            this.lblAntiSkiilType = new System.Windows.Forms.Label();
            this.combAntiSkillType = new SkillEngine.Editor.Football.UI.ControlLib.ValueSetPicker();
            this.pnlHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.combAntiSkillType);
            this.pnlHead.Controls.Add(this.lblAntiSkiilType);
            this.pnlHead.Controls.Add(this.combBuffId);
            this.pnlHead.Controls.Add(this.lblBuffId);
            this.pnlHead.Controls.Add(this.cbxDebuffFlag);
            this.pnlHead.Controls.Add(this.cbxPureFlag);
            this.pnlHead.Controls.Add(this.cbxMainFlag);
            this.pnlHead.Controls.Add(this.combBoostType);
            this.pnlHead.Controls.Add(this.lblBoostType);
            this.pnlHead.Size = new System.Drawing.Size(678, 65);
            // 
            // txtRate
            // 
            this.txtRate.Enabled = false;
            // 
            // lblBoostType
            // 
            this.lblBoostType.AutoSize = true;
            this.lblBoostType.Location = new System.Drawing.Point(21, 10);
            this.lblBoostType.Name = "lblBoostType";
            this.lblBoostType.Size = new System.Drawing.Size(65, 12);
            this.lblBoostType.TabIndex = 0;
            this.lblBoostType.Text = "加强类型：";
            // 
            // combBoostType
            // 
            this.combBoostType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combBoostType.FormattingEnabled = true;
            this.combBoostType.Location = new System.Drawing.Point(93, 5);
            this.combBoostType.Name = "combBoostType";
            this.combBoostType.Size = new System.Drawing.Size(162, 20);
            this.combBoostType.TabIndex = 1;
            // 
            // cbxMainFlag
            // 
            this.cbxMainFlag.AutoSize = true;
            this.cbxMainFlag.Location = new System.Drawing.Point(304, 9);
            this.cbxMainFlag.Name = "cbxMainFlag";
            this.cbxMainFlag.Size = new System.Drawing.Size(60, 16);
            this.cbxMainFlag.TabIndex = 2;
            this.cbxMainFlag.Text = "主效果";
            this.cbxMainFlag.UseVisualStyleBackColor = true;
            // 
            // cbxPureFlag
            // 
            this.cbxPureFlag.AutoSize = true;
            this.cbxPureFlag.Location = new System.Drawing.Point(370, 9);
            this.cbxPureFlag.Name = "cbxPureFlag";
            this.cbxPureFlag.Size = new System.Drawing.Size(72, 16);
            this.cbxPureFlag.TabIndex = 2;
            this.cbxPureFlag.Text = "神圣效果";
            this.cbxPureFlag.UseVisualStyleBackColor = true;
            // 
            // cbxDebuffFlag
            // 
            this.cbxDebuffFlag.AutoSize = true;
            this.cbxDebuffFlag.Location = new System.Drawing.Point(448, 9);
            this.cbxDebuffFlag.Name = "cbxDebuffFlag";
            this.cbxDebuffFlag.Size = new System.Drawing.Size(84, 16);
            this.cbxDebuffFlag.TabIndex = 2;
            this.cbxDebuffFlag.Text = "Debuff效果";
            this.cbxDebuffFlag.UseVisualStyleBackColor = true;
            // 
            // combBuffId
            // 
            this.combBuffId.DataSource = null;
            this.combBuffId.ForeColor = System.Drawing.SystemColors.WindowText;
            this.combBuffId.IsDropSizable = true;
            this.combBuffId.Location = new System.Drawing.Point(93, 35);
            this.combBuffId.Name = "combBuffId";
            this.combBuffId.NoneAsFullFlag = false;
            this.combBuffId.Size = new System.Drawing.Size(162, 21);
            this.combBuffId.TabIndex = 4;
            // 
            // lblBuffId
            // 
            this.lblBuffId.AutoSize = true;
            this.lblBuffId.Location = new System.Drawing.Point(21, 40);
            this.lblBuffId.Name = "lblBuffId";
            this.lblBuffId.Size = new System.Drawing.Size(65, 12);
            this.lblBuffId.TabIndex = 3;
            this.lblBuffId.Text = "效果类型：";
            // 
            // lblAntiSkiilType
            // 
            this.lblAntiSkiilType.AutoSize = true;
            this.lblAntiSkiilType.Location = new System.Drawing.Point(283, 40);
            this.lblAntiSkiilType.Name = "lblAntiSkiilType";
            this.lblAntiSkiilType.Size = new System.Drawing.Size(89, 12);
            this.lblAntiSkiilType.TabIndex = 5;
            this.lblAntiSkiilType.Text = "免疫技能类型：";
            // 
            // combAntiSkillType
            // 
            this.combAntiSkillType.DataSource = null;
            this.combAntiSkillType.ForeColor = System.Drawing.SystemColors.WindowText;
            this.combAntiSkillType.IsDropSizable = true;
            this.combAntiSkillType.Location = new System.Drawing.Point(377, 35);
            this.combAntiSkillType.Name = "combAntiSkillType";
            this.combAntiSkillType.NoneAsFullFlag = false;
            this.combAntiSkillType.Size = new System.Drawing.Size(162, 21);
            this.combAntiSkillType.TabIndex = 6;
            // 
            // BoostEffectCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "BoostEffectCtrl";
            this.Size = new System.Drawing.Size(678, 241);
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblBoostType;
        private System.Windows.Forms.Label lblBuffId;
        public ControlLib.VComboBox combBoostType;
        public ControlLib.ValueSetPicker combBuffId;
        public System.Windows.Forms.CheckBox cbxMainFlag;
        public System.Windows.Forms.CheckBox cbxPureFlag;
        public System.Windows.Forms.CheckBox cbxDebuffFlag;
        public ControlLib.ValueSetPicker combAntiSkillType;
        private System.Windows.Forms.Label lblAntiSkiilType;
    }
}
