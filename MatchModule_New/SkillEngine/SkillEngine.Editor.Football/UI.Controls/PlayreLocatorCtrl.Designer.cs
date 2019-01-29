namespace SkillEngine.Editor.Football.UI.Controls
{
    partial class PlayreLocatorCtrl
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
            this.ucSeekers = new SkillEngine.Editor.Football.UI.ControlBase.FlatListCtrl();
            this.SuspendLayout();
            // 
            // ucSeekers
            // 
            this.ucSeekers.AutoSize = true;
            this.ucSeekers.ClassFlag = SkillEngine.Editor.Football.Data.EnumClassFlag.Manager;
            this.ucSeekers.ClassRank = SkillEngine.Editor.Football.Data.EnumClassRank.None;
            this.ucSeekers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSeekers.Location = new System.Drawing.Point(0, 0);
            this.ucSeekers.Name = "ucSeekers";
            this.ucSeekers.Size = new System.Drawing.Size(678, 42);
            this.ucSeekers.TabIndex = 2;
            // 
            // PlayreLocatorCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Controls.Add(this.ucSeekers);
            this.Name = "PlayreLocatorCtrl";
            this.Size = new System.Drawing.Size(678, 42);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ControlBase.FlatListCtrl ucSeekers;


    }
}
