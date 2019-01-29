namespace SkillEngine.Editor.Football.UI.Controls
{
    partial class ManagerLocatorCtrl
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
            this.gbMLocator = new System.Windows.Forms.GroupBox();
            this.combManagerSide = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.ucMFilters = new SkillEngine.Editor.Football.UI.ControlBase.FlatListCtrl();
            this.gbPLocator = new System.Windows.Forms.GroupBox();
            this.ucPLocator = new SkillEngine.Editor.Football.UI.Controls.PlayreLocatorCtrl();
            this.gbMLocator.SuspendLayout();
            this.gbPLocator.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMLocator
            // 
            this.gbMLocator.AutoSize = true;
            this.gbMLocator.Controls.Add(this.combManagerSide);
            this.gbMLocator.Controls.Add(this.ucMFilters);
            this.gbMLocator.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbMLocator.Location = new System.Drawing.Point(0, 0);
            this.gbMLocator.Name = "gbMLocator";
            this.gbMLocator.Size = new System.Drawing.Size(678, 70);
            this.gbMLocator.TabIndex = 4;
            this.gbMLocator.TabStop = false;
            this.gbMLocator.Text = "满足以下条件的经理：";
            // 
            // combManagerSide
            // 
            this.combManagerSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combManagerSide.FormattingEnabled = true;
            this.combManagerSide.Location = new System.Drawing.Point(167, 22);
            this.combManagerSide.Name = "combManagerSide";
            this.combManagerSide.Size = new System.Drawing.Size(105, 20);
            this.combManagerSide.TabIndex = 4;
            // 
            // ucMFilters
            // 
            this.ucMFilters.AutoSize = true;
            this.ucMFilters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucMFilters.ClassFlag = SkillEngine.Editor.Football.Data.EnumClassFlag.Manager;
            this.ucMFilters.ClassRank = SkillEngine.Editor.Football.Data.EnumClassRank.None;
            this.ucMFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucMFilters.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ucMFilters.Location = new System.Drawing.Point(3, 17);
            this.ucMFilters.Name = "ucMFilters";
            this.ucMFilters.Size = new System.Drawing.Size(672, 50);
            this.ucMFilters.TabIndex = 3;
            // 
            // gbPLocator
            // 
            this.gbPLocator.AutoSize = true;
            this.gbPLocator.Controls.Add(this.ucPLocator);
            this.gbPLocator.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPLocator.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gbPLocator.Location = new System.Drawing.Point(0, 70);
            this.gbPLocator.Name = "gbPLocator";
            this.gbPLocator.Size = new System.Drawing.Size(678, 70);
            this.gbPLocator.TabIndex = 6;
            this.gbPLocator.TabStop = false;
            this.gbPLocator.Text = "拥有以下球员的经理：";
            // 
            // ucPLocator
            // 
            this.ucPLocator.AutoScroll = true;
            this.ucPLocator.AutoSize = true;
            this.ucPLocator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucPLocator.ClassFlag = SkillEngine.Editor.Football.Data.EnumClassFlag.None;
            this.ucPLocator.ClassRank = SkillEngine.Editor.Football.Data.EnumClassRank.None;
            this.ucPLocator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucPLocator.Location = new System.Drawing.Point(3, 17);
            this.ucPLocator.Name = "ucPLocator";
            this.ucPLocator.Size = new System.Drawing.Size(672, 50);
            this.ucPLocator.TabIndex = 0;
            // 
            // ManagerLocatorCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoSize = true;
            this.Controls.Add(this.gbPLocator);
            this.Controls.Add(this.gbMLocator);
            this.Name = "ManagerLocatorCtrl";
            this.Size = new System.Drawing.Size(678, 140);
            this.gbMLocator.ResumeLayout(false);
            this.gbMLocator.PerformLayout();
            this.gbPLocator.ResumeLayout(false);
            this.gbPLocator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ControlBase.FlatListCtrl ucMFilters;
        private SkillEngine.Editor.Football.UI.ControlLib.VComboBox combManagerSide;
        public System.Windows.Forms.GroupBox gbMLocator;
        public System.Windows.Forms.GroupBox gbPLocator;
        private PlayreLocatorCtrl ucPLocator;


    }
}
