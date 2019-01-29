namespace SkillEngine.Editor.Football.UI.EffectControls
{
    partial class FactPropPlusEffectCtrl
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
            this.lblSide = new System.Windows.Forms.Label();
            this.combSide = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.combFactType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblFactType = new System.Windows.Forms.Label();
            this.combValues = new SkillEngine.Editor.Football.UI.ControlLib.ValueSetPicker();
            this.lblValues = new System.Windows.Forms.Label();
            this.pnlHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.lblValues);
            this.pnlHead.Controls.Add(this.lblFactType);
            this.pnlHead.Controls.Add(this.lblSide);
            this.pnlHead.Controls.Add(this.cbxDebuffFlag);
            this.pnlHead.Controls.Add(this.combValues);
            this.pnlHead.Controls.Add(this.cbxPureFlag);
            this.pnlHead.Controls.Add(this.combFactType);
            this.pnlHead.Controls.Add(this.cbxMainFlag);
            this.pnlHead.Controls.Add(this.combSide);
            this.pnlHead.Controls.Add(this.combBuffId);
            this.pnlHead.Controls.Add(this.lblBuffId);
            this.pnlHead.Size = new System.Drawing.Size(678, 58);
            // 
            // cbxDebuffFlag
            // 
            this.cbxDebuffFlag.AutoSize = true;
            this.cbxDebuffFlag.Location = new System.Drawing.Point(579, 6);
            this.cbxDebuffFlag.Name = "cbxDebuffFlag";
            this.cbxDebuffFlag.Size = new System.Drawing.Size(84, 16);
            this.cbxDebuffFlag.TabIndex = 10;
            this.cbxDebuffFlag.Text = "Debuff效果";
            this.cbxDebuffFlag.UseVisualStyleBackColor = true;
            // 
            // cbxPureFlag
            // 
            this.cbxPureFlag.AutoSize = true;
            this.cbxPureFlag.Location = new System.Drawing.Point(501, 6);
            this.cbxPureFlag.Name = "cbxPureFlag";
            this.cbxPureFlag.Size = new System.Drawing.Size(72, 16);
            this.cbxPureFlag.TabIndex = 11;
            this.cbxPureFlag.Text = "神圣效果";
            this.cbxPureFlag.UseVisualStyleBackColor = true;
            // 
            // cbxMainFlag
            // 
            this.cbxMainFlag.AutoSize = true;
            this.cbxMainFlag.Location = new System.Drawing.Point(435, 6);
            this.cbxMainFlag.Name = "cbxMainFlag";
            this.cbxMainFlag.Size = new System.Drawing.Size(60, 16);
            this.cbxMainFlag.TabIndex = 12;
            this.cbxMainFlag.Text = "主效果";
            this.cbxMainFlag.UseVisualStyleBackColor = true;
            // 
            // combBuffId
            // 
            this.combBuffId.Location = new System.Drawing.Point(92, 4);
            this.combBuffId.Name = "combBuffId";
            this.combBuffId.Size = new System.Drawing.Size(318, 20);
            this.combBuffId.TabIndex = 9;
            // 
            // lblBuffId
            // 
            this.lblBuffId.AutoSize = true;
            this.lblBuffId.Location = new System.Drawing.Point(21, 9);
            this.lblBuffId.Name = "lblBuffId";
            this.lblBuffId.Size = new System.Drawing.Size(65, 12);
            this.lblBuffId.TabIndex = 8;
            this.lblBuffId.Text = "效果类型：";
            // 
            // lblSide
            // 
            this.lblSide.AutoSize = true;
            this.lblSide.Location = new System.Drawing.Point(9, 34);
            this.lblSide.Name = "lblSide";
            this.lblSide.Size = new System.Drawing.Size(77, 12);
            this.lblSide.TabIndex = 13;
            this.lblSide.Text = "球员所属方：";
            // 
            // combSide
            // 
            this.combSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSide.FormattingEnabled = true;
            this.combSide.Location = new System.Drawing.Point(92, 30);
            this.combSide.Name = "combSide";
            this.combSide.Size = new System.Drawing.Size(100, 20);
            this.combSide.TabIndex = 9;
            // 
            // combFactType
            // 
            this.combFactType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combFactType.FormattingEnabled = true;
            this.combFactType.Location = new System.Drawing.Point(310, 31);
            this.combFactType.Name = "combFactType";
            this.combFactType.Size = new System.Drawing.Size(100, 20);
            this.combFactType.TabIndex = 9;
            // 
            // lblFactType
            // 
            this.lblFactType.AutoSize = true;
            this.lblFactType.Location = new System.Drawing.Point(239, 34);
            this.lblFactType.Name = "lblFactType";
            this.lblFactType.Size = new System.Drawing.Size(65, 12);
            this.lblFactType.TabIndex = 13;
            this.lblFactType.Text = "计算类型：";
            // 
            // combValues
            // 
            this.combValues.DataSource = null;
            this.combValues.ForeColor = System.Drawing.SystemColors.WindowText;
            this.combValues.IsDropSizable = true;
            this.combValues.Location = new System.Drawing.Point(527, 30);
            this.combValues.Name = "combValues";
            this.combValues.NoneAsFullFlag = true;
            this.combValues.Size = new System.Drawing.Size(100, 21);
            this.combValues.TabIndex = 9;
            // 
            // lblValues
            // 
            this.lblValues.AutoSize = true;
            this.lblValues.Location = new System.Drawing.Point(456, 33);
            this.lblValues.Name = "lblValues";
            this.lblValues.Size = new System.Drawing.Size(65, 12);
            this.lblValues.TabIndex = 13;
            this.lblValues.Text = "计算因素：";
            // 
            // FactPropPlusEffectCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "FactPropPlusEffectCtrl";
            this.Size = new System.Drawing.Size(678, 195);
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblBuffId;
        private System.Windows.Forms.Label lblValues;
        private System.Windows.Forms.Label lblFactType;
        private System.Windows.Forms.Label lblSide;
        public ControlLib.ValueSetPicker combBuffId;
        public System.Windows.Forms.CheckBox cbxMainFlag;
        public System.Windows.Forms.CheckBox cbxPureFlag;
        public System.Windows.Forms.CheckBox cbxDebuffFlag;
        public ControlLib.VComboBox combSide;
        public ControlLib.VComboBox combFactType;
        public ControlLib.ValueSetPicker combValues;
    }
}
