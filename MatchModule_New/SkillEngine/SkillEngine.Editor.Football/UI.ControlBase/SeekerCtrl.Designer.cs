namespace SkillEngine.Editor.Football.UI.ControlBase
{
    partial class SeekerCtrl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.combJoinType = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblJoinType = new System.Windows.Forms.Label();
            this.ucFilters = new SkillEngine.Editor.Football.UI.ControlBase.FlatListCtrl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.combJoinType);
            this.panel1.Controls.Add(this.lblJoinType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(678, 40);
            this.panel1.TabIndex = 0;
            // 
            // combJoinType
            // 
            this.combJoinType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combJoinType.FormattingEnabled = true;
            this.combJoinType.Location = new System.Drawing.Point(83, 9);
            this.combJoinType.Name = "combJoinType";
            this.combJoinType.Size = new System.Drawing.Size(100, 20);
            this.combJoinType.TabIndex = 11;
            // 
            // lblJoinType
            // 
            this.lblJoinType.AutoSize = true;
            this.lblJoinType.ForeColor = System.Drawing.Color.Teal;
            this.lblJoinType.Location = new System.Drawing.Point(11, 14);
            this.lblJoinType.Name = "lblJoinType";
            this.lblJoinType.Size = new System.Drawing.Size(65, 12);
            this.lblJoinType.TabIndex = 10;
            this.lblJoinType.Text = "条件组合：";
            // 
            // ucFilters
            // 
            this.ucFilters.AutoSize = true;
            this.ucFilters.ClassFlag = SkillEngine.Editor.Football.Data.EnumClassFlag.None;
            this.ucFilters.ClassRank = SkillEngine.Editor.Football.Data.EnumClassRank.None;
            this.ucFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFilters.Location = new System.Drawing.Point(0, 40);
            this.ucFilters.Name = "ucFilters";
            this.ucFilters.Size = new System.Drawing.Size(678, 93);
            this.ucFilters.TabIndex = 1;
            // 
            // SeekerCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoSize = true;
            this.Controls.Add(this.ucFilters);
            this.Controls.Add(this.panel1);
            this.Name = "SeekerCtrl";
            this.Size = new System.Drawing.Size(678, 133);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FlatListCtrl ucFilters;
        public SkillEngine.Editor.Football.UI.ControlLib.VComboBox combJoinType;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Label lblJoinType;
    }
}
