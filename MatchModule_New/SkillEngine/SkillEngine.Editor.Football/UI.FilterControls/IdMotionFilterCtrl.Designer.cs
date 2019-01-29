namespace SkillEngine.Editor.Football.UI.FilterControls
{
    partial class IdMotionFilterCtrl
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
            this.combStateType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.combValues = new SkillEngine.Editor.Football.UI.ControlLib.ValueSetPicker();
            this.combSeekType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblStateType = new System.Windows.Forms.Label();
            this.lblValues = new System.Windows.Forms.Label();
            this.lblSeekType = new System.Windows.Forms.Label();
            this.txtIds = new System.Windows.Forms.TextBox();
            this.lblIds = new System.Windows.Forms.Label();
            this.pnlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBox
            // 
            this.pnlBox.Controls.Add(this.txtIds);
            this.pnlBox.Controls.Add(this.lblIds);
            this.pnlBox.Controls.Add(this.combStateType);
            this.pnlBox.Controls.Add(this.combValues);
            this.pnlBox.Controls.Add(this.combSeekType);
            this.pnlBox.Controls.Add(this.lblStateType);
            this.pnlBox.Controls.Add(this.lblValues);
            this.pnlBox.Controls.Add(this.lblSeekType);
            this.pnlBox.Size = new System.Drawing.Size(678, 70);
            // 
            // combStateType
            // 
            this.combStateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combStateType.FormattingEnabled = true;
            this.combStateType.Location = new System.Drawing.Point(517, 40);
            this.combStateType.Name = "combStateType";
            this.combStateType.Size = new System.Drawing.Size(120, 20);
            this.combStateType.TabIndex = 5;
            // 
            // combValues
            // 
            this.combValues.Location = new System.Drawing.Point(305, 12);
            this.combValues.Name = "combValues";
            this.combValues.Size = new System.Drawing.Size(332, 20);
            this.combValues.TabIndex = 6;
            // 
            // combSeekType
            // 
            this.combSeekType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSeekType.FormattingEnabled = true;
            this.combSeekType.Location = new System.Drawing.Point(92, 12);
            this.combSeekType.Name = "combSeekType";
            this.combSeekType.Size = new System.Drawing.Size(120, 20);
            this.combSeekType.TabIndex = 7;
            // 
            // lblStateType
            // 
            this.lblStateType.AutoSize = true;
            this.lblStateType.Location = new System.Drawing.Point(446, 43);
            this.lblStateType.Name = "lblStateType";
            this.lblStateType.Size = new System.Drawing.Size(65, 12);
            this.lblStateType.TabIndex = 2;
            this.lblStateType.Text = "完成状态：";
            // 
            // lblValues
            // 
            this.lblValues.AutoSize = true;
            this.lblValues.Location = new System.Drawing.Point(234, 15);
            this.lblValues.Name = "lblValues";
            this.lblValues.Size = new System.Drawing.Size(65, 12);
            this.lblValues.TabIndex = 3;
            this.lblValues.Text = "动作列表：";
            // 
            // lblSeekType
            // 
            this.lblSeekType.AutoSize = true;
            this.lblSeekType.Location = new System.Drawing.Point(21, 15);
            this.lblSeekType.Name = "lblSeekType";
            this.lblSeekType.Size = new System.Drawing.Size(65, 12);
            this.lblSeekType.TabIndex = 4;
            this.lblSeekType.Text = "查找类型：";
            // 
            // txtIds
            // 
            this.txtIds.Location = new System.Drawing.Point(92, 40);
            this.txtIds.Name = "txtIds";
            this.txtIds.Size = new System.Drawing.Size(333, 21);
            this.txtIds.TabIndex = 9;
            // 
            // lblIds
            // 
            this.lblIds.AutoSize = true;
            this.lblIds.Location = new System.Drawing.Point(9, 43);
            this.lblIds.Name = "lblIds";
            this.lblIds.Size = new System.Drawing.Size(77, 12);
            this.lblIds.TabIndex = 8;
            this.lblIds.Text = "球员Id列表：";
            // 
            // IdMotionFilterCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "IdMotionFilterCtrl";
            this.Size = new System.Drawing.Size(678, 70);
            this.pnlBox.ResumeLayout(false);
            this.pnlBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblStateType;
        private System.Windows.Forms.Label lblValues;
        private System.Windows.Forms.Label lblSeekType;
        private System.Windows.Forms.Label lblIds;
        public ControlLib.VComboBox combSeekType;
        public ControlLib.ValueSetPicker combValues;
        public ControlLib.VComboBox combStateType;
        public System.Windows.Forms.TextBox txtIds;
    }
}
