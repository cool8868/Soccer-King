namespace SkillEngine.Editor.Football.UI.Controls
{
    partial class ManagerEffectorCtrl
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
            this.ucLocator = new SkillEngine.Editor.Football.UI.Controls.ManagerLocatorCtrl();
            this.pnlLocator.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLocator
            // 
            this.pnlLocator.Controls.Add(this.ucLocator);
            this.pnlLocator.Size = new System.Drawing.Size(678, 151);
            // 
            // ucEffects
            // 
            this.ucEffects.Size = new System.Drawing.Size(678, 151);
            // 
            // ucLocator
            // 
            this.ucLocator.AutoScroll = true;
            this.ucLocator.AutoSize = true;
            this.ucLocator.ClassFlag = SkillEngine.Editor.Football.Data.EnumClassFlag.None;
            this.ucLocator.ClassRank = SkillEngine.Editor.Football.Data.EnumClassRank.None;
            this.ucLocator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLocator.Location = new System.Drawing.Point(0, 0);
            this.ucLocator.Name = "ucLocator";
            this.ucLocator.Size = new System.Drawing.Size(678, 151);
            this.ucLocator.TabIndex = 0;
            // 
            // ManagerEffectorCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "ManagerEffectorCtrl";
            this.Size = new System.Drawing.Size(678, 198);
            this.pnlLocator.ResumeLayout(false);
            this.pnlLocator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public ManagerLocatorCtrl ucLocator;
    }
}
