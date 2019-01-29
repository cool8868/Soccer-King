namespace SkillEngine.Editor.Football.UI.ControlBase
{
    partial class SkillCtrl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtOpenClipId = new System.Windows.Forms.TextBox();
            this.lblOpenClipId = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtCD = new SkillEngine.Editor.Football.UI.ControlLib.VMaskedTextBox();
            this.lblMinute = new System.Windows.Forms.Label();
            this.combCD = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.txtSkillCode = new System.Windows.Forms.TextBox();
            this.combParalFlag = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.combCastFlag = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.combTimeType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.combActType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.combSrcType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.txtRate = new SkillEngine.Editor.Football.UI.ControlLib.VMaskedTextBox();
            this.lblRate = new System.Windows.Forms.Label();
            this.txtSkillName = new System.Windows.Forms.TextBox();
            this.txtSkillLevel = new SkillEngine.Editor.Football.UI.ControlLib.VMaskedTextBox();
            this.txtSKillId = new SkillEngine.Editor.Football.UI.ControlLib.VMaskedTextBox();
            this.lblSkillName = new System.Windows.Forms.Label();
            this.lblSkillCode = new System.Windows.Forms.Label();
            this.lblParalFlag = new System.Windows.Forms.Label();
            this.lblCastFlag = new System.Windows.Forms.Label();
            this.lblTimeType = new System.Windows.Forms.Label();
            this.lblActType = new System.Windows.Forms.Label();
            this.lblSrcType = new System.Windows.Forms.Label();
            this.lblSkillLevel = new System.Windows.Forms.Label();
            this.lblCD = new System.Windows.Forms.Label();
            this.lblSkillId = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucTriggers = new SkillEngine.Editor.Football.UI.ControlBase.FlatListCtrl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ucEffectors = new SkillEngine.Editor.Football.UI.ControlBase.FlatListCtrl();
            this.txtLast = new SkillEngine.Editor.Football.UI.ControlLib.VMaskedTextBox();
            this.lblMinute2 = new System.Windows.Forms.Label();
            this.combLast = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblLast = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtLast);
            this.panel1.Controls.Add(this.lblMinute2);
            this.panel1.Controls.Add(this.combLast);
            this.panel1.Controls.Add(this.lblLast);
            this.panel1.Controls.Add(this.txtOpenClipId);
            this.panel1.Controls.Add(this.lblOpenClipId);
            this.panel1.Controls.Add(this.txtType);
            this.panel1.Controls.Add(this.txtCD);
            this.panel1.Controls.Add(this.lblMinute);
            this.panel1.Controls.Add(this.combCD);
            this.panel1.Controls.Add(this.txtSkillCode);
            this.panel1.Controls.Add(this.combParalFlag);
            this.panel1.Controls.Add(this.combCastFlag);
            this.panel1.Controls.Add(this.combTimeType);
            this.panel1.Controls.Add(this.combActType);
            this.panel1.Controls.Add(this.combSrcType);
            this.panel1.Controls.Add(this.txtRate);
            this.panel1.Controls.Add(this.lblRate);
            this.panel1.Controls.Add(this.txtSkillName);
            this.panel1.Controls.Add(this.txtSkillLevel);
            this.panel1.Controls.Add(this.txtSKillId);
            this.panel1.Controls.Add(this.lblSkillName);
            this.panel1.Controls.Add(this.lblSkillCode);
            this.panel1.Controls.Add(this.lblParalFlag);
            this.panel1.Controls.Add(this.lblCastFlag);
            this.panel1.Controls.Add(this.lblTimeType);
            this.panel1.Controls.Add(this.lblActType);
            this.panel1.Controls.Add(this.lblSrcType);
            this.panel1.Controls.Add(this.lblSkillLevel);
            this.panel1.Controls.Add(this.lblCD);
            this.panel1.Controls.Add(this.lblSkillId);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(980, 137);
            this.panel1.TabIndex = 45;
            // 
            // txtOpenClipId
            // 
            this.txtOpenClipId.Location = new System.Drawing.Point(874, 98);
            this.txtOpenClipId.Name = "txtOpenClipId";
            this.txtOpenClipId.Size = new System.Drawing.Size(85, 21);
            this.txtOpenClipId.TabIndex = 68;
            // 
            // lblOpenClipId
            // 
            this.lblOpenClipId.AutoSize = true;
            this.lblOpenClipId.Location = new System.Drawing.Point(800, 101);
            this.lblOpenClipId.Name = "lblOpenClipId";
            this.lblOpenClipId.Size = new System.Drawing.Size(77, 12);
            this.lblOpenClipId.TabIndex = 67;
            this.lblOpenClipId.Text = "开场动画Id：";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(802, 18);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(10, 21);
            this.txtType.TabIndex = 66;
            this.txtType.Visible = false;
            // 
            // txtCD
            // 
            this.txtCD.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtCD.Location = new System.Drawing.Point(551, 57);
            this.txtCD.Mask = "999";
            this.txtCD.Name = "txtCD";
            this.txtCD.PromptChar = '0';
            this.txtCD.Size = new System.Drawing.Size(29, 21);
            this.txtCD.TabIndex = 65;
            this.txtCD.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            // 
            // lblMinute
            // 
            this.lblMinute.AutoSize = true;
            this.lblMinute.Location = new System.Drawing.Point(580, 60);
            this.lblMinute.Name = "lblMinute";
            this.lblMinute.Size = new System.Drawing.Size(29, 12);
            this.lblMinute.TabIndex = 64;
            this.lblMinute.Text = "分钟";
            // 
            // combCD
            // 
            this.combCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combCD.FormattingEnabled = true;
            this.combCD.Location = new System.Drawing.Point(446, 57);
            this.combCD.Name = "combCD";
            this.combCD.Size = new System.Drawing.Size(99, 20);
            this.combCD.TabIndex = 63;
            // 
            // txtSkillCode
            // 
            this.txtSkillCode.Location = new System.Drawing.Point(103, 18);
            this.txtSkillCode.Name = "txtSkillCode";
            this.txtSkillCode.Size = new System.Drawing.Size(100, 21);
            this.txtSkillCode.TabIndex = 0;
            // 
            // combParalFlag
            // 
            this.combParalFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combParalFlag.FormattingEnabled = true;
            this.combParalFlag.Location = new System.Drawing.Point(689, 98);
            this.combParalFlag.Name = "combParalFlag";
            this.combParalFlag.Size = new System.Drawing.Size(85, 20);
            this.combParalFlag.TabIndex = 62;
            // 
            // combCastFlag
            // 
            this.combCastFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combCastFlag.FormattingEnabled = true;
            this.combCastFlag.Location = new System.Drawing.Point(103, 58);
            this.combCastFlag.Name = "combCastFlag";
            this.combCastFlag.Size = new System.Drawing.Size(100, 20);
            this.combCastFlag.TabIndex = 61;
            // 
            // combTimeType
            // 
            this.combTimeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combTimeType.FormattingEnabled = true;
            this.combTimeType.Location = new System.Drawing.Point(484, 98);
            this.combTimeType.Name = "combTimeType";
            this.combTimeType.Size = new System.Drawing.Size(100, 20);
            this.combTimeType.TabIndex = 60;
            // 
            // combActType
            // 
            this.combActType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combActType.FormattingEnabled = true;
            this.combActType.Location = new System.Drawing.Point(285, 98);
            this.combActType.Name = "combActType";
            this.combActType.Size = new System.Drawing.Size(100, 20);
            this.combActType.TabIndex = 59;
            // 
            // combSrcType
            // 
            this.combSrcType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSrcType.FormattingEnabled = true;
            this.combSrcType.Location = new System.Drawing.Point(103, 98);
            this.combSrcType.Name = "combSrcType";
            this.combSrcType.Size = new System.Drawing.Size(100, 20);
            this.combSrcType.TabIndex = 58;
            // 
            // txtRate
            // 
            this.txtRate.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtRate.Location = new System.Drawing.Point(285, 57);
            this.txtRate.Mask = "000%";
            this.txtRate.Name = "txtRate";
            this.txtRate.PromptChar = '0';
            this.txtRate.Size = new System.Drawing.Size(100, 21);
            this.txtRate.TabIndex = 57;
            this.txtRate.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            // 
            // lblRate
            // 
            this.lblRate.AutoSize = true;
            this.lblRate.Location = new System.Drawing.Point(226, 61);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(53, 12);
            this.lblRate.TabIndex = 56;
            this.lblRate.Text = "触发率：";
            // 
            // txtSkillName
            // 
            this.txtSkillName.Location = new System.Drawing.Point(484, 18);
            this.txtSkillName.Name = "txtSkillName";
            this.txtSkillName.Size = new System.Drawing.Size(100, 21);
            this.txtSkillName.TabIndex = 55;
            // 
            // txtSkillLevel
            // 
            this.txtSkillLevel.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtSkillLevel.Location = new System.Drawing.Point(689, 18);
            this.txtSkillLevel.Mask = "9";
            this.txtSkillLevel.Name = "txtSkillLevel";
            this.txtSkillLevel.PromptChar = '0';
            this.txtSkillLevel.Size = new System.Drawing.Size(85, 21);
            this.txtSkillLevel.TabIndex = 53;
            this.txtSkillLevel.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            // 
            // txtSKillId
            // 
            this.txtSKillId.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtSKillId.Location = new System.Drawing.Point(285, 18);
            this.txtSKillId.Mask = "999999";
            this.txtSKillId.Name = "txtSKillId";
            this.txtSKillId.PromptChar = '0';
            this.txtSKillId.Size = new System.Drawing.Size(100, 21);
            this.txtSKillId.TabIndex = 51;
            this.txtSKillId.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            // 
            // lblSkillName
            // 
            this.lblSkillName.AutoSize = true;
            this.lblSkillName.Location = new System.Drawing.Point(413, 21);
            this.lblSkillName.Name = "lblSkillName";
            this.lblSkillName.Size = new System.Drawing.Size(65, 12);
            this.lblSkillName.TabIndex = 49;
            this.lblSkillName.Text = "技能名称：";
            // 
            // lblSkillCode
            // 
            this.lblSkillCode.AutoSize = true;
            this.lblSkillCode.ForeColor = System.Drawing.Color.Purple;
            this.lblSkillCode.Location = new System.Drawing.Point(32, 21);
            this.lblSkillCode.Name = "lblSkillCode";
            this.lblSkillCode.Size = new System.Drawing.Size(65, 12);
            this.lblSkillCode.TabIndex = 48;
            this.lblSkillCode.Text = "技能Code：";
            // 
            // lblParalFlag
            // 
            this.lblParalFlag.AutoSize = true;
            this.lblParalFlag.Location = new System.Drawing.Point(618, 101);
            this.lblParalFlag.Name = "lblParalFlag";
            this.lblParalFlag.Size = new System.Drawing.Size(65, 12);
            this.lblParalFlag.TabIndex = 47;
            this.lblParalFlag.Text = "并发控制：";
            // 
            // lblCastFlag
            // 
            this.lblCastFlag.AutoSize = true;
            this.lblCastFlag.Location = new System.Drawing.Point(32, 61);
            this.lblCastFlag.Name = "lblCastFlag";
            this.lblCastFlag.Size = new System.Drawing.Size(65, 12);
            this.lblCastFlag.TabIndex = 46;
            this.lblCastFlag.Text = "效果视角：";
            // 
            // lblTimeType
            // 
            this.lblTimeType.AutoSize = true;
            this.lblTimeType.Location = new System.Drawing.Point(413, 101);
            this.lblTimeType.Name = "lblTimeType";
            this.lblTimeType.Size = new System.Drawing.Size(65, 12);
            this.lblTimeType.TabIndex = 45;
            this.lblTimeType.Text = "触发时机：";
            // 
            // lblActType
            // 
            this.lblActType.AutoSize = true;
            this.lblActType.Location = new System.Drawing.Point(221, 101);
            this.lblActType.Name = "lblActType";
            this.lblActType.Size = new System.Drawing.Size(65, 12);
            this.lblActType.TabIndex = 44;
            this.lblActType.Text = "动作类型：";
            // 
            // lblSrcType
            // 
            this.lblSrcType.AutoSize = true;
            this.lblSrcType.Location = new System.Drawing.Point(32, 101);
            this.lblSrcType.Name = "lblSrcType";
            this.lblSrcType.Size = new System.Drawing.Size(65, 12);
            this.lblSrcType.TabIndex = 43;
            this.lblSrcType.Text = "来源类型：";
            // 
            // lblSkillLevel
            // 
            this.lblSkillLevel.AutoSize = true;
            this.lblSkillLevel.Location = new System.Drawing.Point(618, 21);
            this.lblSkillLevel.Name = "lblSkillLevel";
            this.lblSkillLevel.Size = new System.Drawing.Size(65, 12);
            this.lblSkillLevel.TabIndex = 42;
            this.lblSkillLevel.Text = "技能等级：";
            // 
            // lblCD
            // 
            this.lblCD.AutoSize = true;
            this.lblCD.Location = new System.Drawing.Point(413, 61);
            this.lblCD.Name = "lblCD";
            this.lblCD.Size = new System.Drawing.Size(29, 12);
            this.lblCD.TabIndex = 50;
            this.lblCD.Text = "CD：";
            // 
            // lblSkillId
            // 
            this.lblSkillId.AutoSize = true;
            this.lblSkillId.Location = new System.Drawing.Point(226, 21);
            this.lblSkillId.Name = "lblSkillId";
            this.lblSkillId.Size = new System.Drawing.Size(53, 12);
            this.lblSkillId.TabIndex = 41;
            this.lblSkillId.Text = "技能Id：";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 137);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(980, 631);
            this.tabControl1.TabIndex = 47;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucTriggers);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(972, 605);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "触发条件";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ucTriggers
            // 
            this.ucTriggers.AutoScroll = true;
            this.ucTriggers.AutoSize = true;
            this.ucTriggers.ClassFlag = SkillEngine.Editor.Football.Data.EnumClassFlag.Manager;
            this.ucTriggers.ClassRank = SkillEngine.Editor.Football.Data.EnumClassRank.None;
            this.ucTriggers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTriggers.Location = new System.Drawing.Point(3, 3);
            this.ucTriggers.Name = "ucTriggers";
            this.ucTriggers.Size = new System.Drawing.Size(966, 599);
            this.ucTriggers.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucEffectors);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(834, 605);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "目标效果";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ucEffectors
            // 
            this.ucEffectors.AutoScroll = true;
            this.ucEffectors.AutoSize = true;
            this.ucEffectors.ClassFlag = SkillEngine.Editor.Football.Data.EnumClassFlag.Manager;
            this.ucEffectors.ClassRank = SkillEngine.Editor.Football.Data.EnumClassRank.None;
            this.ucEffectors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucEffectors.Location = new System.Drawing.Point(3, 3);
            this.ucEffectors.Name = "ucEffectors";
            this.ucEffectors.Size = new System.Drawing.Size(828, 599);
            this.ucEffectors.TabIndex = 2;
            // 
            // txtLast
            // 
            this.txtLast.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Overwrite;
            this.txtLast.Location = new System.Drawing.Point(792, 56);
            this.txtLast.Mask = "999";
            this.txtLast.Name = "txtLast";
            this.txtLast.PromptChar = '0';
            this.txtLast.Size = new System.Drawing.Size(29, 21);
            this.txtLast.TabIndex = 72;
            this.txtLast.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePrompt;
            // 
            // lblMinute2
            // 
            this.lblMinute2.AutoSize = true;
            this.lblMinute2.Location = new System.Drawing.Point(821, 59);
            this.lblMinute2.Name = "lblMinute2";
            this.lblMinute2.Size = new System.Drawing.Size(29, 12);
            this.lblMinute2.TabIndex = 71;
            this.lblMinute2.Text = "分钟";
            // 
            // combLast
            // 
            this.combLast.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combLast.FormattingEnabled = true;
            this.combLast.Location = new System.Drawing.Point(687, 56);
            this.combLast.Name = "combLast";
            this.combLast.Size = new System.Drawing.Size(99, 20);
            this.combLast.TabIndex = 70;
            // 
            // lblLast
            // 
            this.lblLast.AutoSize = true;
            this.lblLast.Location = new System.Drawing.Point(618, 61);
            this.lblLast.Name = "lblLast";
            this.lblLast.Size = new System.Drawing.Size(65, 12);
            this.lblLast.TabIndex = 69;
            this.lblLast.Text = "持续时间：";
            // 
            // SkillCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "SkillCtrl";
            this.Size = new System.Drawing.Size(980, 768);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private ControlLib.VComboBox combParalFlag;
        private ControlLib.VComboBox combCastFlag;
        private ControlLib.VComboBox combTimeType;
        private ControlLib.VComboBox combActType;
        private ControlLib.VComboBox combSrcType;
        public ControlLib.VMaskedTextBox txtRate;
        private System.Windows.Forms.Label lblRate;
        private System.Windows.Forms.TextBox txtSkillName;
        private ControlLib.VMaskedTextBox txtSkillLevel;
        public ControlLib.VMaskedTextBox txtSKillId;
        private System.Windows.Forms.Label lblSkillName;
        private System.Windows.Forms.Label lblSkillCode;
        private System.Windows.Forms.Label lblParalFlag;
        private System.Windows.Forms.Label lblCastFlag;
        private System.Windows.Forms.Label lblTimeType;
        private System.Windows.Forms.Label lblActType;
        private System.Windows.Forms.Label lblSrcType;
        private System.Windows.Forms.Label lblSkillLevel;
        private System.Windows.Forms.Label lblCD;
        private System.Windows.Forms.Label lblSkillId;
        public System.Windows.Forms.TextBox txtSkillCode;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private FlatListCtrl ucTriggers;
        private System.Windows.Forms.TabPage tabPage2;
        private FlatListCtrl ucEffectors;
        public ControlLib.VMaskedTextBox txtCD;
        private System.Windows.Forms.Label lblMinute;
        public ControlLib.VComboBox combCD;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.TextBox txtOpenClipId;
        private System.Windows.Forms.Label lblOpenClipId;
        public ControlLib.VMaskedTextBox txtLast;
        private System.Windows.Forms.Label lblMinute2;
        public ControlLib.VComboBox combLast;
        private System.Windows.Forms.Label lblLast;

    }
}
