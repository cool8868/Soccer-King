namespace SkillEngine.Editor.Football.UI.SeekerControls
{
    partial class IdSeekerCtrl
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
            this.combSide = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblSide = new System.Windows.Forms.Label();
            this.lblColours = new System.Windows.Forms.Label();
            this.combColours = new SkillEngine.Editor.Football.UI.ControlLib.ValueSetPicker();
            this.lblPositions = new System.Windows.Forms.Label();
            this.combPositions = new SkillEngine.Editor.Football.UI.ControlLib.ValueSetPicker();
            this.txtIds = new System.Windows.Forms.TextBox();
            this.lblIds = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtIds);
            this.panel1.Controls.Add(this.lblIds);
            this.panel1.Controls.Add(this.combPositions);
            this.panel1.Controls.Add(this.combColours);
            this.panel1.Controls.Add(this.combSide);
            this.panel1.Controls.Add(this.lblPositions);
            this.panel1.Controls.Add(this.lblColours);
            this.panel1.Controls.Add(this.lblSide);
            this.panel1.Size = new System.Drawing.Size(678, 70);
            this.panel1.Controls.SetChildIndex(this.lblJoinType, 0);
            this.panel1.Controls.SetChildIndex(this.combJoinType, 0);
            this.panel1.Controls.SetChildIndex(this.lblSide, 0);
            this.panel1.Controls.SetChildIndex(this.lblColours, 0);
            this.panel1.Controls.SetChildIndex(this.lblPositions, 0);
            this.panel1.Controls.SetChildIndex(this.combSide, 0);
            this.panel1.Controls.SetChildIndex(this.combColours, 0);
            this.panel1.Controls.SetChildIndex(this.combPositions, 0);
            this.panel1.Controls.SetChildIndex(this.lblIds, 0);
            this.panel1.Controls.SetChildIndex(this.txtIds, 0);
            // 
            // combSide
            // 
            this.combSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combSide.FormattingEnabled = true;
            this.combSide.Location = new System.Drawing.Point(283, 9);
            this.combSide.Name = "combSide";
            this.combSide.Size = new System.Drawing.Size(100, 20);
            this.combSide.TabIndex = 13;
            // 
            // lblSide
            // 
            this.lblSide.AutoSize = true;
            this.lblSide.Location = new System.Drawing.Point(211, 14);
            this.lblSide.Name = "lblSide";
            this.lblSide.Size = new System.Drawing.Size(65, 12);
            this.lblSide.TabIndex = 12;
            this.lblSide.Text = "目标球员：";
            // 
            // lblColours
            // 
            this.lblColours.AutoSize = true;
            this.lblColours.Location = new System.Drawing.Point(427, 14);
            this.lblColours.Name = "lblColours";
            this.lblColours.Size = new System.Drawing.Size(65, 12);
            this.lblColours.TabIndex = 12;
            this.lblColours.Text = "颜色列表：";
            // 
            // combColours
            // 
            this.combColours.DataSource = null;
            this.combColours.ForeColor = System.Drawing.SystemColors.WindowText;
            this.combColours.IsDropSizable = true;
            this.combColours.Location = new System.Drawing.Point(499, 9);
            this.combColours.Name = "combColours";
            this.combColours.NoneAsFullFlag = true;
            this.combColours.Size = new System.Drawing.Size(167, 21);
            this.combColours.TabIndex = 13;
            // 
            // lblPositions
            // 
            this.lblPositions.AutoSize = true;
            this.lblPositions.Location = new System.Drawing.Point(427, 43);
            this.lblPositions.Name = "lblPositions";
            this.lblPositions.Size = new System.Drawing.Size(65, 12);
            this.lblPositions.TabIndex = 12;
            this.lblPositions.Text = "位置列表：";
            // 
            // combPositions
            // 
            this.combPositions.DataSource = null;
            this.combPositions.ForeColor = System.Drawing.SystemColors.WindowText;
            this.combPositions.IsDropSizable = true;
            this.combPositions.Location = new System.Drawing.Point(499, 38);
            this.combPositions.Name = "combPositions";
            this.combPositions.NoneAsFullFlag = true;
            this.combPositions.Size = new System.Drawing.Size(167, 21);
            this.combPositions.TabIndex = 13;
            // 
            // txtIds
            // 
            this.txtIds.Location = new System.Drawing.Point(83, 40);
            this.txtIds.Name = "txtIds";
            this.txtIds.Size = new System.Drawing.Size(300, 21);
            this.txtIds.TabIndex = 15;
            // 
            // lblIds
            // 
            this.lblIds.AutoSize = true;
            this.lblIds.Location = new System.Drawing.Point(2, 43);
            this.lblIds.Name = "lblIds";
            this.lblIds.Size = new System.Drawing.Size(77, 12);
            this.lblIds.TabIndex = 14;
            this.lblIds.Text = "球员Id列表：";
            // 
            // IdSeekerCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "IdSeekerCtrl";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPositions;
        private System.Windows.Forms.Label lblColours;
        private System.Windows.Forms.Label lblSide;
        private System.Windows.Forms.Label lblIds;
        public ControlLib.VComboBox combSide;
        public System.Windows.Forms.TextBox txtIds;
        public ControlLib.ValueSetPicker combColours;
        public ControlLib.ValueSetPicker combPositions;
    }
}
