namespace SkillEngine.Editor.Football.UI.SeekerControls
{
    partial class TalentPositonSeekerCtrl
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
            this.combValues = new SkillEngine.Editor.Football.UI.ControlLib.ValueSetPicker();
            this.lblValues = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.combValues);
            this.panel1.Controls.Add(this.lblValues);
            this.panel1.Controls.SetChildIndex(this.lblJoinType, 0);
            this.panel1.Controls.SetChildIndex(this.combJoinType, 0);
            this.panel1.Controls.SetChildIndex(this.lblValues, 0);
            this.panel1.Controls.SetChildIndex(this.combValues, 0);
            // 
            // combValues
            // 
            this.combValues.Location = new System.Drawing.Point(304, 11);
            this.combValues.Name = "combValues";
            this.combValues.Size = new System.Drawing.Size(200, 20);
            this.combValues.TabIndex = 15;
            // 
            // lblValues
            // 
            this.lblValues.AutoSize = true;
            this.lblValues.Location = new System.Drawing.Point(221, 14);
            this.lblValues.Name = "lblValues";
            this.lblValues.Size = new System.Drawing.Size(77, 12);
            this.lblValues.TabIndex = 14;
            this.lblValues.Text = "天赋位列表：";
            // 
            // TalentPositonSeekerCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "TalentPositonSeekerCtrl";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblValues;
        public ControlLib.ValueSetPicker combValues;
    }
}
