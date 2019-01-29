namespace SkillEngine.Editor.Football.UI.SeekerControls
{
    partial class PlayerSkillSeekerCtrl
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
            this.lblSkillFlag = new System.Windows.Forms.Label();
            this.combSkillFlag = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.combSkillFlag);
            this.panel1.Controls.Add(this.lblSkillFlag);
            this.panel1.Controls.Add(this.txtIds);
            this.panel1.Controls.Add(this.lblIds);
            this.panel1.Controls.Add(this.lblActType);
            this.panel1.Controls.Add(this.combActType);
            this.panel1.Controls.Add(this.lblSrcType);
            this.panel1.Controls.Add(this.combSrcType);
            this.panel1.Size = new System.Drawing.Size(690, 70);
            this.panel1.Controls.SetChildIndex(this.lblJoinType, 0);
            this.panel1.Controls.SetChildIndex(this.combJoinType, 0);
            this.panel1.Controls.SetChildIndex(this.combSrcType, 0);
            this.panel1.Controls.SetChildIndex(this.lblSrcType, 0);
            this.panel1.Controls.SetChildIndex(this.combActType, 0);
            this.panel1.Controls.SetChildIndex(this.lblActType, 0);
            this.panel1.Controls.SetChildIndex(this.lblIds, 0);
            this.panel1.Controls.SetChildIndex(this.txtIds, 0);
            this.panel1.Controls.SetChildIndex(this.lblSkillFlag, 0);
            this.panel1.Controls.SetChildIndex(this.combSkillFlag, 0);
            // 
            // lblActType
            // 
            this.lblActType.AutoSize = true;
            this.lblActType.Location = new System.Drawing.Point(291, 42);
            this.lblActType.Name = "lblActType";
            this.lblActType.Size = new System.Drawing.Size(65, 12);
            this.lblActType.TabIndex = 37;
            this.lblActType.Text = "动作类型：";
            // 
            // combActType
            // 
            this.combActType.DataSource = null;
            this.combActType.ForeColor = System.Drawing.SystemColors.WindowText;
            this.combActType.IsDropSizable = true;
            this.combActType.Location = new System.Drawing.Point(362, 39);
            this.combActType.Name = "combActType";
            this.combActType.NoneAsFullFlag = false;
            this.combActType.Size = new System.Drawing.Size(144, 21);
            this.combActType.TabIndex = 35;
            // 
            // lblSrcType
            // 
            this.lblSrcType.AutoSize = true;
            this.lblSrcType.Location = new System.Drawing.Point(11, 42);
            this.lblSrcType.Name = "lblSrcType";
            this.lblSrcType.Size = new System.Drawing.Size(65, 12);
            this.lblSrcType.TabIndex = 38;
            this.lblSrcType.Text = "来源类型：";
            // 
            // combSrcType
            // 
            this.combSrcType.DataSource = null;
            this.combSrcType.ForeColor = System.Drawing.SystemColors.WindowText;
            this.combSrcType.IsDropSizable = true;
            this.combSrcType.Location = new System.Drawing.Point(82, 39);
            this.combSrcType.Name = "combSrcType";
            this.combSrcType.NoneAsFullFlag = false;
            this.combSrcType.Size = new System.Drawing.Size(194, 21);
            this.combSrcType.TabIndex = 36;
            // 
            // txtIds
            // 
            this.txtIds.Location = new System.Drawing.Point(327, 9);
            this.txtIds.Name = "txtIds";
            this.txtIds.Size = new System.Drawing.Size(345, 21);
            this.txtIds.TabIndex = 40;
            // 
            // lblIds
            // 
            this.lblIds.AutoSize = true;
            this.lblIds.Location = new System.Drawing.Point(232, 13);
            this.lblIds.Name = "lblIds";
            this.lblIds.Size = new System.Drawing.Size(89, 12);
            this.lblIds.TabIndex = 39;
            this.lblIds.Text = "技能Code列表：";
            // 
            // lblSkillFlag
            // 
            this.lblSkillFlag.AutoSize = true;
            this.lblSkillFlag.Location = new System.Drawing.Point(520, 42);
            this.lblSkillFlag.Name = "lblSkillFlag";
            this.lblSkillFlag.Size = new System.Drawing.Size(65, 12);
            this.lblSkillFlag.TabIndex = 41;
            this.lblSkillFlag.Text = "效益类型：";
            // 
            // combSkillFlag
            // 
            this.combSkillFlag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSkillFlag.FormattingEnabled = true;
            this.combSkillFlag.Location = new System.Drawing.Point(591, 39);
            this.combSkillFlag.Name = "combSkillFlag";
            this.combSkillFlag.Size = new System.Drawing.Size(81, 20);
            this.combSkillFlag.TabIndex = 42;
            // 
            // PlayerSkillSeekerCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "PlayerSkillSeekerCtrl";
            this.Size = new System.Drawing.Size(690, 133);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblActType;
        private System.Windows.Forms.Label lblSrcType;
        private System.Windows.Forms.Label lblIds;
        public System.Windows.Forms.TextBox txtIds;
        public ControlLib.ValueSetPicker combSrcType;
        public ControlLib.ValueSetPicker combActType;
        public ControlLib.VComboBox combSkillFlag;
        private System.Windows.Forms.Label lblSkillFlag;
    }
}
