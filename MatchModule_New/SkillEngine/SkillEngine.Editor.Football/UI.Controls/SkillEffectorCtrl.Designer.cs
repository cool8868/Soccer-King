namespace SkillEngine.Editor.Football.UI.Controls
{
    partial class SkillEffectorCtrl
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
            this.ucLocator = new SkillEngine.Editor.Football.UI.Controls.SkillLocatorCtrl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ucEffectors = new SkillEngine.Editor.Football.UI.ControlBase.FlatListCtrl();
            this.cbEffectors = new System.Windows.Forms.CheckBox();
            this.pnlLocator.SuspendLayout();
            this.pnlEffects.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLocator
            // 
            this.pnlLocator.Controls.Add(this.ucLocator);
            this.pnlLocator.Size = new System.Drawing.Size(678, 452);
            // 
            // pnlEffects
            // 
            this.pnlEffects.Controls.Add(this.panel3);
            this.pnlEffects.Size = new System.Drawing.Size(678, 121);
            this.pnlEffects.Controls.SetChildIndex(this.ucEffects, 0);
            this.pnlEffects.Controls.SetChildIndex(this.panel3, 0);
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
            this.ucLocator.Size = new System.Drawing.Size(678, 452);
            this.ucLocator.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.ucEffectors);
            this.panel3.Controls.Add(this.cbEffectors);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 48);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(678, 66);
            this.panel3.TabIndex = 14;
            // 
            // ucEffectors
            // 
            this.ucEffectors.AutoSize = true;
            this.ucEffectors.ClassFlag = SkillEngine.Editor.Football.Data.EnumClassFlag.None;
            this.ucEffectors.ClassRank = SkillEngine.Editor.Football.Data.EnumClassRank.None;
            this.ucEffectors.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucEffectors.Enabled = false;
            this.ucEffectors.Location = new System.Drawing.Point(0, 16);
            this.ucEffectors.Name = "ucEffectors";
            this.ucEffectors.Size = new System.Drawing.Size(676, 48);
            this.ucEffectors.TabIndex = 19;
            // 
            // cbEffectors
            // 
            this.cbEffectors.AutoSize = true;
            this.cbEffectors.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbEffectors.Location = new System.Drawing.Point(0, 0);
            this.cbEffectors.Name = "cbEffectors";
            this.cbEffectors.Size = new System.Drawing.Size(676, 16);
            this.cbEffectors.TabIndex = 18;
            this.cbEffectors.Text = "为目标技能附加新的目标和效果：";
            this.cbEffectors.UseVisualStyleBackColor = true;
            // 
            // SkillEffectorCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "SkillEffectorCtrl";
            this.Size = new System.Drawing.Size(678, 168);
            this.pnlLocator.ResumeLayout(false);
            this.pnlLocator.PerformLayout();
            this.pnlEffects.ResumeLayout(false);
            this.pnlEffects.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SkillLocatorCtrl ucLocator;
        private System.Windows.Forms.Panel panel3;
        public ControlBase.FlatListCtrl ucEffectors;
        private System.Windows.Forms.CheckBox cbEffectors;
    }
}
