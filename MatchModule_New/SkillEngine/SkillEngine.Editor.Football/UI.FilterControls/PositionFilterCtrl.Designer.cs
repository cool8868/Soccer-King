namespace SkillEngine.Editor.Football.UI.FilterControls
{
    partial class PositionFilterCtrl
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
            this.pnlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBox
            // 
            this.pnlBox.Controls.Add(this.combValues);
            this.pnlBox.Controls.Add(this.lblValues);
            // 
            // combValues
            // 
            this.combValues.Location = new System.Drawing.Point(92, 10);
            this.combValues.Name = "combValues";
            this.combValues.Size = new System.Drawing.Size(200, 20);
            this.combValues.TabIndex = 9;
            // 
            // lblValues
            // 
            this.lblValues.AutoSize = true;
            this.lblValues.Location = new System.Drawing.Point(21, 15);
            this.lblValues.Name = "lblValues";
            this.lblValues.Size = new System.Drawing.Size(65, 12);
            this.lblValues.TabIndex = 8;
            this.lblValues.Text = "位置列表：";
            // 
            // PositionFilterCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "PositionFilterCtrl";
            this.pnlBox.ResumeLayout(false);
            this.pnlBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblValues;
        public ControlLib.ValueSetPicker combValues;
    }
}
