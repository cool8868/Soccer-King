namespace SkillEngine.Editor.Football.UI.Controls
{
    partial class PlayerEffectorCtrl
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ucLocator = new SkillEngine.Editor.Football.UI.Controls.PlayreLocatorCtrl();
            this.pnlLocator.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLocator
            // 
            this.pnlLocator.Controls.Add(this.groupBox2);
            this.pnlLocator.Size = new System.Drawing.Size(678, 85);
            // 
            // ucEffects
            // 
            this.ucEffects.Size = new System.Drawing.Size(678, 124);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.ucLocator);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(678, 85);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "满足以下条件的球员：";
            // 
            // ucLocator
            // 
            this.ucLocator.AutoScroll = true;
            this.ucLocator.AutoSize = true;
            this.ucLocator.ClassFlag = SkillEngine.Editor.Football.Data.EnumClassFlag.None;
            this.ucLocator.ClassRank = SkillEngine.Editor.Football.Data.EnumClassRank.None;
            this.ucLocator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucLocator.Location = new System.Drawing.Point(3, 17);
            this.ucLocator.Name = "ucLocator";
            this.ucLocator.Size = new System.Drawing.Size(672, 65);
            this.ucLocator.TabIndex = 1;
            // 
            // PlayerEffectorCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "PlayerEffectorCtrl";
            this.Size = new System.Drawing.Size(678, 132);
            this.pnlLocator.ResumeLayout(false);
            this.pnlLocator.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.GroupBox groupBox2;
        public PlayreLocatorCtrl ucLocator;

    }
}
