namespace SkillEngine.Editor.Football.UI.EffectControls
{
    partial class EventPropPlusEffectCtrl
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
            this.lblEventType = new System.Windows.Forms.Label();
            this.lblExecType = new System.Windows.Forms.Label();
            this.lblSide = new System.Windows.Forms.Label();
            this.cbxDebuffFlag = new System.Windows.Forms.CheckBox();
            this.combEventType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.cbxPureFlag = new System.Windows.Forms.CheckBox();
            this.combExecType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.cbxMainFlag = new System.Windows.Forms.CheckBox();
            this.combSide = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.combBuffId = new SkillEngine.Editor.Football.UI.ControlLib.ValueSetPicker();
            this.lblBuffId = new System.Windows.Forms.Label();
            this.pnlHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHead
            // 
            this.pnlHead.Controls.Add(this.lblEventType);
            this.pnlHead.Controls.Add(this.lblExecType);
            this.pnlHead.Controls.Add(this.lblSide);
            this.pnlHead.Controls.Add(this.cbxDebuffFlag);
            this.pnlHead.Controls.Add(this.combEventType);
            this.pnlHead.Controls.Add(this.cbxPureFlag);
            this.pnlHead.Controls.Add(this.combExecType);
            this.pnlHead.Controls.Add(this.cbxMainFlag);
            this.pnlHead.Controls.Add(this.combSide);
            this.pnlHead.Controls.Add(this.combBuffId);
            this.pnlHead.Controls.Add(this.lblBuffId);
            this.pnlHead.Size = new System.Drawing.Size(678, 58);
            // 
            // lblEventType
            // 
            this.lblEventType.AutoSize = true;
            this.lblEventType.Location = new System.Drawing.Point(456, 35);
            this.lblEventType.Name = "lblEventType";
            this.lblEventType.Size = new System.Drawing.Size(65, 12);
            this.lblEventType.TabIndex = 22;
            this.lblEventType.Text = "事件类型：";
            // 
            // lblExecType
            // 
            this.lblExecType.AutoSize = true;
            this.lblExecType.Location = new System.Drawing.Point(215, 35);
            this.lblExecType.Name = "lblExecType";
            this.lblExecType.Size = new System.Drawing.Size(89, 12);
            this.lblExecType.TabIndex = 23;
            this.lblExecType.Text = "概率执行类型：";
            // 
            // lblSide
            // 
            this.lblSide.AutoSize = true;
            this.lblSide.Location = new System.Drawing.Point(21, 36);
            this.lblSide.Name = "lblSide";
            this.lblSide.Size = new System.Drawing.Size(65, 12);
            this.lblSide.TabIndex = 24;
            this.lblSide.Text = "目标球员：";
            // 
            // cbxDebuffFlag
            // 
            this.cbxDebuffFlag.AutoSize = true;
            this.cbxDebuffFlag.Location = new System.Drawing.Point(578, 8);
            this.cbxDebuffFlag.Name = "cbxDebuffFlag";
            this.cbxDebuffFlag.Size = new System.Drawing.Size(84, 16);
            this.cbxDebuffFlag.TabIndex = 19;
            this.cbxDebuffFlag.Text = "Debuff效果";
            this.cbxDebuffFlag.UseVisualStyleBackColor = true;
            // 
            // combEventType
            // 
            this.combEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combEventType.FormattingEnabled = true;
            this.combEventType.Location = new System.Drawing.Point(527, 32);
            this.combEventType.Name = "combEventType";
            this.combEventType.Size = new System.Drawing.Size(100, 20);
            this.combEventType.TabIndex = 15;
            // 
            // cbxPureFlag
            // 
            this.cbxPureFlag.AutoSize = true;
            this.cbxPureFlag.Location = new System.Drawing.Point(500, 8);
            this.cbxPureFlag.Name = "cbxPureFlag";
            this.cbxPureFlag.Size = new System.Drawing.Size(72, 16);
            this.cbxPureFlag.TabIndex = 20;
            this.cbxPureFlag.Text = "神圣效果";
            this.cbxPureFlag.UseVisualStyleBackColor = true;
            // 
            // combExecType
            // 
            this.combExecType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combExecType.FormattingEnabled = true;
            this.combExecType.Location = new System.Drawing.Point(310, 33);
            this.combExecType.Name = "combExecType";
            this.combExecType.Size = new System.Drawing.Size(100, 20);
            this.combExecType.TabIndex = 16;
            // 
            // cbxMainFlag
            // 
            this.cbxMainFlag.AutoSize = true;
            this.cbxMainFlag.Location = new System.Drawing.Point(434, 8);
            this.cbxMainFlag.Name = "cbxMainFlag";
            this.cbxMainFlag.Size = new System.Drawing.Size(60, 16);
            this.cbxMainFlag.TabIndex = 21;
            this.cbxMainFlag.Text = "主效果";
            this.cbxMainFlag.UseVisualStyleBackColor = true;
            // 
            // combSide
            // 
            this.combSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSide.FormattingEnabled = true;
            this.combSide.Location = new System.Drawing.Point(92, 32);
            this.combSide.Name = "combSide";
            this.combSide.Size = new System.Drawing.Size(100, 20);
            this.combSide.TabIndex = 17;
            // 
            // combBuffId
            // 
            this.combBuffId.Location = new System.Drawing.Point(93, 6);
            this.combBuffId.Name = "combBuffId";
            this.combBuffId.Size = new System.Drawing.Size(317, 20);
            this.combBuffId.TabIndex = 18;
            // 
            // lblBuffId
            // 
            this.lblBuffId.AutoSize = true;
            this.lblBuffId.Location = new System.Drawing.Point(21, 11);
            this.lblBuffId.Name = "lblBuffId";
            this.lblBuffId.Size = new System.Drawing.Size(65, 12);
            this.lblBuffId.TabIndex = 14;
            this.lblBuffId.Text = "效果类型：";
            // 
            // EventPropPlusEffectCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "EventPropPlusEffectCtrl";
            this.Size = new System.Drawing.Size(678, 195);
            this.pnlHead.ResumeLayout(false);
            this.pnlHead.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblEventType;
        private System.Windows.Forms.Label lblExecType;
        private System.Windows.Forms.Label lblSide;
        private System.Windows.Forms.Label lblBuffId;
        public ControlLib.ValueSetPicker combBuffId;
        public System.Windows.Forms.CheckBox cbxMainFlag;
        public System.Windows.Forms.CheckBox cbxPureFlag;
        public System.Windows.Forms.CheckBox cbxDebuffFlag;
        public ControlLib.VComboBox combSide;
        public ControlLib.VComboBox combExecType;
        public ControlLib.VComboBox combEventType;
    }
}
