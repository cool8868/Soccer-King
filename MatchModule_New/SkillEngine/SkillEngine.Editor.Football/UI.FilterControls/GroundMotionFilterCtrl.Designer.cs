namespace SkillEngine.Editor.Football.UI.FilterControls
{
    partial class GroundMotionFilterCtrl
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
            this.combStateType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.combValues = new SkillEngine.Editor.Football.UI.ControlLib.ValueSetPicker();
            this.combSeekType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblStateType = new System.Windows.Forms.Label();
            this.lblValues = new System.Windows.Forms.Label();
            this.lblSeekType = new System.Windows.Forms.Label();
            this.combGroudType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblGroundType = new System.Windows.Forms.Label();
            this.pnlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBox
            // 
            this.pnlBox.Controls.Add(this.combGroudType);
            this.pnlBox.Controls.Add(this.lblGroundType);
            this.pnlBox.Controls.Add(this.combStateType);
            this.pnlBox.Controls.Add(this.combValues);
            this.pnlBox.Controls.Add(this.combSeekType);
            this.pnlBox.Controls.Add(this.lblStateType);
            this.pnlBox.Controls.Add(this.lblValues);
            this.pnlBox.Controls.Add(this.lblSeekType);
            this.pnlBox.Size = new System.Drawing.Size(933, 40);
            // 
            // combStateType
            // 
            this.combStateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combStateType.FormattingEnabled = true;
            this.combStateType.Location = new System.Drawing.Point(750, 11);
            this.combStateType.Name = "combStateType";
            this.combStateType.Size = new System.Drawing.Size(120, 20);
            this.combStateType.TabIndex = 5;
            // 
            // combValues
            // 
            this.combValues.DataSource = null;
            this.combValues.ForeColor = System.Drawing.SystemColors.WindowText;
            this.combValues.IsDropSizable = true;
            this.combValues.Location = new System.Drawing.Point(538, 11);
            this.combValues.Name = "combValues";
            this.combValues.NoneAsFullFlag = false;
            this.combValues.Size = new System.Drawing.Size(120, 21);
            this.combValues.TabIndex = 6;
            // 
            // combSeekType
            // 
            this.combSeekType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSeekType.FormattingEnabled = true;
            this.combSeekType.Location = new System.Drawing.Point(102, 10);
            this.combSeekType.Name = "combSeekType";
            this.combSeekType.Size = new System.Drawing.Size(120, 20);
            this.combSeekType.TabIndex = 7;
            // 
            // lblStateType
            // 
            this.lblStateType.AutoSize = true;
            this.lblStateType.Location = new System.Drawing.Point(679, 14);
            this.lblStateType.Name = "lblStateType";
            this.lblStateType.Size = new System.Drawing.Size(65, 12);
            this.lblStateType.TabIndex = 2;
            this.lblStateType.Text = "完成状态：";
            // 
            // lblValues
            // 
            this.lblValues.AutoSize = true;
            this.lblValues.Location = new System.Drawing.Point(467, 14);
            this.lblValues.Name = "lblValues";
            this.lblValues.Size = new System.Drawing.Size(65, 12);
            this.lblValues.TabIndex = 3;
            this.lblValues.Text = "动作列表：";
            // 
            // lblSeekType
            // 
            this.lblSeekType.AutoSize = true;
            this.lblSeekType.Location = new System.Drawing.Point(31, 13);
            this.lblSeekType.Name = "lblSeekType";
            this.lblSeekType.Size = new System.Drawing.Size(65, 12);
            this.lblSeekType.TabIndex = 4;
            this.lblSeekType.Text = "查找类型：";
            // 
            // combGroudType
            // 
            this.combGroudType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combGroudType.FormattingEnabled = true;
            this.combGroudType.Location = new System.Drawing.Point(323, 10);
            this.combGroudType.Name = "combGroudType";
            this.combGroudType.Size = new System.Drawing.Size(120, 20);
            this.combGroudType.TabIndex = 9;
            // 
            // lblGroundType
            // 
            this.lblGroundType.AutoSize = true;
            this.lblGroundType.Location = new System.Drawing.Point(252, 13);
            this.lblGroundType.Name = "lblGroundType";
            this.lblGroundType.Size = new System.Drawing.Size(65, 12);
            this.lblGroundType.TabIndex = 8;
            this.lblGroundType.Text = "球员位置：";
            // 
            // GroundMotionFilterCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "GroundMotionFilterCtrl";
            this.Size = new System.Drawing.Size(933, 40);
            this.pnlBox.ResumeLayout(false);
            this.pnlBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public ControlLib.VComboBox combStateType;
        public ControlLib.ValueSetPicker combValues;
        public ControlLib.VComboBox combSeekType;
        private System.Windows.Forms.Label lblStateType;
        private System.Windows.Forms.Label lblValues;
        private System.Windows.Forms.Label lblSeekType;
        public ControlLib.VComboBox combGroudType;
        private System.Windows.Forms.Label lblGroundType;
    }
}
