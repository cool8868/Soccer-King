namespace SkillEngine.Editor.Football.UI.ControlBase
{
    partial class FilterCtrl
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
            this.pnlBox = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnlBox
            // 
            this.pnlBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBox.Location = new System.Drawing.Point(0, 0);
            this.pnlBox.Name = "pnlBox";
            this.pnlBox.Size = new System.Drawing.Size(678, 40);
            this.pnlBox.TabIndex = 0;
            // 
            // FilterCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.pnlBox);
            this.Name = "FilterCtrl";
            this.Size = new System.Drawing.Size(678, 40);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnlBox;

    }
}
