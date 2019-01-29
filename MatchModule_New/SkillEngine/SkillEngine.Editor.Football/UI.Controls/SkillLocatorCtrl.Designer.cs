namespace SkillEngine.Editor.Football.UI.Controls
{
    partial class SkillLocatorCtrl
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
            this.pnlPLocator = new System.Windows.Forms.Panel();
            this.ucPLocator = new SkillEngine.Editor.Football.UI.Controls.PlayreLocatorCtrl();
            this.cbPLocator = new System.Windows.Forms.CheckBox();
            this.pnlMSkillSeeker = new System.Windows.Forms.Panel();
            this.ucMSkillSeeker = new SkillEngine.Editor.Football.UI.SeekerControls.ManagerSkillSeekerCtrl();
            this.cbMSkillSeeker = new System.Windows.Forms.CheckBox();
            this.pnlPSkillSeeker = new System.Windows.Forms.Panel();
            this.ucPSkillSeeker = new SkillEngine.Editor.Football.UI.SeekerControls.PlayerSkillSeekerCtrl();
            this.cbPSkillSeeker = new System.Windows.Forms.CheckBox();
            this.pnlPLocator.SuspendLayout();
            this.pnlMSkillSeeker.SuspendLayout();
            this.pnlPSkillSeeker.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPLocator
            // 
            this.pnlPLocator.AutoSize = true;
            this.pnlPLocator.Controls.Add(this.ucPLocator);
            this.pnlPLocator.Controls.Add(this.cbPLocator);
            this.pnlPLocator.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPLocator.Location = new System.Drawing.Point(0, 0);
            this.pnlPLocator.Name = "pnlPLocator";
            this.pnlPLocator.Size = new System.Drawing.Size(681, 66);
            this.pnlPLocator.TabIndex = 11;
            // 
            // ucPLocator
            // 
            this.ucPLocator.AutoScroll = true;
            this.ucPLocator.AutoSize = true;
            this.ucPLocator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucPLocator.ClassFlag = SkillEngine.Editor.Football.Data.EnumClassFlag.None;
            this.ucPLocator.ClassRank = SkillEngine.Editor.Football.Data.EnumClassRank.None;
            this.ucPLocator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPLocator.Enabled = false;
            this.ucPLocator.Location = new System.Drawing.Point(0, 16);
            this.ucPLocator.Name = "ucPLocator";
            this.ucPLocator.Size = new System.Drawing.Size(681, 50);
            this.ucPLocator.TabIndex = 1;
            // 
            // cbPLocator
            // 
            this.cbPLocator.AutoSize = true;
            this.cbPLocator.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbPLocator.Location = new System.Drawing.Point(0, 0);
            this.cbPLocator.Name = "cbPLocator";
            this.cbPLocator.Size = new System.Drawing.Size(681, 16);
            this.cbPLocator.TabIndex = 0;
            this.cbPLocator.Text = "满足以下条件的球员中查找技能：";
            this.cbPLocator.UseVisualStyleBackColor = true;
            // 
            // pnlMSkillSeeker
            // 
            this.pnlMSkillSeeker.AutoSize = true;
            this.pnlMSkillSeeker.Controls.Add(this.ucMSkillSeeker);
            this.pnlMSkillSeeker.Controls.Add(this.cbMSkillSeeker);
            this.pnlMSkillSeeker.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMSkillSeeker.Location = new System.Drawing.Point(0, 66);
            this.pnlMSkillSeeker.Name = "pnlMSkillSeeker";
            this.pnlMSkillSeeker.Size = new System.Drawing.Size(681, 136);
            this.pnlMSkillSeeker.TabIndex = 12;
            // 
            // ucMSkillSeeker
            // 
            this.ucMSkillSeeker.AutoScroll = true;
            this.ucMSkillSeeker.AutoSize = true;
            this.ucMSkillSeeker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucMSkillSeeker.ClassFlag = SkillEngine.Editor.Football.Data.EnumClassFlag.None;
            this.ucMSkillSeeker.ClassRank = SkillEngine.Editor.Football.Data.EnumClassRank.None;
            this.ucMSkillSeeker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMSkillSeeker.Enabled = false;
            this.ucMSkillSeeker.Location = new System.Drawing.Point(0, 16);
            this.ucMSkillSeeker.Name = "ucMSkillSeeker";
            this.ucMSkillSeeker.Size = new System.Drawing.Size(681, 120);
            this.ucMSkillSeeker.TabIndex = 1;
            // 
            // cbMSkillSeeker
            // 
            this.cbMSkillSeeker.AutoSize = true;
            this.cbMSkillSeeker.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbMSkillSeeker.Location = new System.Drawing.Point(0, 0);
            this.cbMSkillSeeker.Name = "cbMSkillSeeker";
            this.cbMSkillSeeker.Size = new System.Drawing.Size(681, 16);
            this.cbMSkillSeeker.TabIndex = 0;
            this.cbMSkillSeeker.Text = "满足以下条件的经理技能：";
            this.cbMSkillSeeker.UseVisualStyleBackColor = true;
            // 
            // pnlPSkillSeeker
            // 
            this.pnlPSkillSeeker.AutoSize = true;
            this.pnlPSkillSeeker.Controls.Add(this.ucPSkillSeeker);
            this.pnlPSkillSeeker.Controls.Add(this.cbPSkillSeeker);
            this.pnlPSkillSeeker.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPSkillSeeker.Location = new System.Drawing.Point(0, 202);
            this.pnlPSkillSeeker.Name = "pnlPSkillSeeker";
            this.pnlPSkillSeeker.Size = new System.Drawing.Size(681, 136);
            this.pnlPSkillSeeker.TabIndex = 13;
            // 
            // ucPSkillSeeker
            // 
            this.ucPSkillSeeker.AutoSize = true;
            this.ucPSkillSeeker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucPSkillSeeker.ClassFlag = SkillEngine.Editor.Football.Data.EnumClassFlag.None;
            this.ucPSkillSeeker.ClassRank = SkillEngine.Editor.Football.Data.EnumClassRank.None;
            this.ucPSkillSeeker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPSkillSeeker.Enabled = false;
            this.ucPSkillSeeker.Location = new System.Drawing.Point(0, 16);
            this.ucPSkillSeeker.Name = "ucPSkillSeeker";
            this.ucPSkillSeeker.Size = new System.Drawing.Size(681, 120);
            this.ucPSkillSeeker.TabIndex = 1;
            // 
            // cbPSkillSeeker
            // 
            this.cbPSkillSeeker.AutoSize = true;
            this.cbPSkillSeeker.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbPSkillSeeker.Location = new System.Drawing.Point(0, 0);
            this.cbPSkillSeeker.Name = "cbPSkillSeeker";
            this.cbPSkillSeeker.Size = new System.Drawing.Size(681, 16);
            this.cbPSkillSeeker.TabIndex = 0;
            this.cbPSkillSeeker.Text = "满足以下条件的球员技能：";
            this.cbPSkillSeeker.UseVisualStyleBackColor = true;
            // 
            // SkillLocatorCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoSize = true;
            this.Controls.Add(this.pnlPSkillSeeker);
            this.Controls.Add(this.pnlMSkillSeeker);
            this.Controls.Add(this.pnlPLocator);
            this.Name = "SkillLocatorCtrl";
            this.Size = new System.Drawing.Size(681, 343);
            this.pnlPLocator.ResumeLayout(false);
            this.pnlPLocator.PerformLayout();
            this.pnlMSkillSeeker.ResumeLayout(false);
            this.pnlMSkillSeeker.PerformLayout();
            this.pnlPSkillSeeker.ResumeLayout(false);
            this.pnlPSkillSeeker.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel pnlPLocator;
        public PlayreLocatorCtrl ucPLocator;
        private System.Windows.Forms.CheckBox cbPLocator;
        public System.Windows.Forms.Panel pnlMSkillSeeker;
        public SeekerControls.ManagerSkillSeekerCtrl ucMSkillSeeker;
        private System.Windows.Forms.CheckBox cbMSkillSeeker;
        public System.Windows.Forms.Panel pnlPSkillSeeker;
        public SeekerControls.PlayerSkillSeekerCtrl ucPSkillSeeker;
        private System.Windows.Forms.CheckBox cbPSkillSeeker;


    }
}
