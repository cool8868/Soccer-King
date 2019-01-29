namespace SkillEngine.Editor.Football.UI.SeekerControls
{
    partial class MotionSeekerCtrl
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
            this.combSeekType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblSeekType = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.combSeekType);
            this.panel1.Controls.Add(this.lblSeekType);
            this.panel1.Controls.SetChildIndex(this.lblJoinType, 0);
            this.panel1.Controls.SetChildIndex(this.combJoinType, 0);
            this.panel1.Controls.SetChildIndex(this.lblSeekType, 0);
            this.panel1.Controls.SetChildIndex(this.combSeekType, 0);
            // 
            // combSeekType
            // 
            this.combSeekType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSeekType.FormattingEnabled = true;
            this.combSeekType.Location = new System.Drawing.Point(315, 10);
            this.combSeekType.Name = "combSeekType";
            this.combSeekType.Size = new System.Drawing.Size(120, 20);
            this.combSeekType.TabIndex = 13;
            // 
            // lblSeekType
            // 
            this.lblSeekType.AutoSize = true;
            this.lblSeekType.Location = new System.Drawing.Point(244, 13);
            this.lblSeekType.Name = "lblSeekType";
            this.lblSeekType.Size = new System.Drawing.Size(65, 12);
            this.lblSeekType.TabIndex = 12;
            this.lblSeekType.Text = "查找类型：";
            // 
            // MotionSeekerCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "MotionSeekerCtrl";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSeekType;
        public ControlLib.VComboBox combSeekType;
    }
}
