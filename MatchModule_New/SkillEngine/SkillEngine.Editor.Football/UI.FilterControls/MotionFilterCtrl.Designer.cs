namespace SkillEngine.Editor.Football.UI.FilterControls
{
    partial class MotionFilterCtrl
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
            this.lblSeekType = new System.Windows.Forms.Label();
            this.combSeekType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblValues = new System.Windows.Forms.Label();
            this.combValues = new SkillEngine.Editor.Football.UI.ControlLib.ValueSetPicker();
            this.lblStateType = new System.Windows.Forms.Label();
            this.combStateType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.pnlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBox
            // 
            this.pnlBox.Controls.Add(this.combStateType);
            this.pnlBox.Controls.Add(this.combValues);
            this.pnlBox.Controls.Add(this.combSeekType);
            this.pnlBox.Controls.Add(this.lblStateType);
            this.pnlBox.Controls.Add(this.lblValues);
            this.pnlBox.Controls.Add(this.lblSeekType);
            // 
            // lblSeekType
            // 
            this.lblSeekType.AutoSize = true;
            this.lblSeekType.Location = new System.Drawing.Point(21, 15);
            this.lblSeekType.Name = "lblSeekType";
            this.lblSeekType.Size = new System.Drawing.Size(65, 12);
            this.lblSeekType.TabIndex = 0;
            this.lblSeekType.Text = "查找类型：";
            // 
            // combSeekType
            // 
            this.combSeekType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSeekType.FormattingEnabled = true;
            this.combSeekType.Location = new System.Drawing.Point(92, 12);
            this.combSeekType.Name = "combSeekType";
            this.combSeekType.Size = new System.Drawing.Size(120, 20);
            this.combSeekType.TabIndex = 1;
            // 
            // lblValues
            // 
            this.lblValues.AutoSize = true;
            this.lblValues.Location = new System.Drawing.Point(234, 15);
            this.lblValues.Name = "lblValues";
            this.lblValues.Size = new System.Drawing.Size(65, 12);
            this.lblValues.TabIndex = 0;
            this.lblValues.Text = "动作列表：";
            // 
            // combValues
            // 
            this.combValues.Location = new System.Drawing.Point(305, 12);
            this.combValues.Name = "combValues";
            this.combValues.Size = new System.Drawing.Size(120, 20);
            this.combValues.TabIndex = 1;
            // 
            // lblStateType
            // 
            this.lblStateType.AutoSize = true;
            this.lblStateType.Location = new System.Drawing.Point(446, 15);
            this.lblStateType.Name = "lblStateType";
            this.lblStateType.Size = new System.Drawing.Size(65, 12);
            this.lblStateType.TabIndex = 0;
            this.lblStateType.Text = "完成状态：";
            // 
            // combStateType
            // 
            this.combStateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combStateType.FormattingEnabled = true;
            this.combStateType.Location = new System.Drawing.Point(517, 12);
            this.combStateType.Name = "combStateType";
            this.combStateType.Size = new System.Drawing.Size(120, 20);
            this.combStateType.TabIndex = 1;
            // 
            // MotionFilterCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "MotionFilterCtrl";
            this.pnlBox.ResumeLayout(false);
            this.pnlBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblSeekType;
        private System.Windows.Forms.Label lblStateType;
        private System.Windows.Forms.Label lblValues;
        public ControlLib.VComboBox combSeekType;
        public ControlLib.ValueSetPicker combValues;
        public ControlLib.VComboBox combStateType;

    }
}
