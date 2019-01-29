namespace SkillEngine.Editor.Football.UI.ControlBase
{
    partial class EffectorCtrl
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtClipLast = new System.Windows.Forms.TextBox();
            this.txtClipId = new System.Windows.Forms.TextBox();
            this.lblClipLast = new System.Windows.Forms.Label();
            this.lblClipId = new System.Windows.Forms.Label();
            this.cbClipType = new System.Windows.Forms.CheckBox();
            this.cbClip = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabEffector = new System.Windows.Forms.TabControl();
            this.tabLocator = new System.Windows.Forms.TabPage();
            this.tabEffects = new System.Windows.Forms.TabPage();
            this.pnlTab = new System.Windows.Forms.Panel();
            this.pnlLocator = new System.Windows.Forms.Panel();
            this.pnlEffects = new System.Windows.Forms.Panel();
            this.ucEffects = new SkillEngine.Editor.Football.UI.ControlBase.FlatListCtrl();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabEffector.SuspendLayout();
            this.pnlTab.SuspendLayout();
            this.pnlEffects.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(678, 42);
            this.panel1.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtClipLast);
            this.groupBox1.Controls.Add(this.txtClipId);
            this.groupBox1.Controls.Add(this.lblClipLast);
            this.groupBox1.Controls.Add(this.lblClipId);
            this.groupBox1.Controls.Add(this.cbClipType);
            this.groupBox1.Controls.Add(this.cbClip);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(151, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(526, 42);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "技能输出";
            // 
            // txtClipLast
            // 
            this.txtClipLast.Enabled = false;
            this.txtClipLast.Location = new System.Drawing.Point(439, 15);
            this.txtClipLast.Name = "txtClipLast";
            this.txtClipLast.Size = new System.Drawing.Size(80, 21);
            this.txtClipLast.TabIndex = 24;
            // 
            // txtClipId
            // 
            this.txtClipId.Enabled = false;
            this.txtClipId.Location = new System.Drawing.Point(154, 15);
            this.txtClipId.Name = "txtClipId";
            this.txtClipId.Size = new System.Drawing.Size(80, 21);
            this.txtClipId.TabIndex = 24;
            // 
            // lblClipLast
            // 
            this.lblClipLast.AutoSize = true;
            this.lblClipLast.Location = new System.Drawing.Point(368, 18);
            this.lblClipLast.Name = "lblClipLast";
            this.lblClipLast.Size = new System.Drawing.Size(65, 12);
            this.lblClipLast.TabIndex = 22;
            this.lblClipLast.Text = "持续回合：";
            // 
            // lblClipId
            // 
            this.lblClipId.AutoSize = true;
            this.lblClipId.Location = new System.Drawing.Point(95, 18);
            this.lblClipId.Name = "lblClipId";
            this.lblClipId.Size = new System.Drawing.Size(53, 12);
            this.lblClipId.TabIndex = 22;
            this.lblClipId.Text = "动画Id：";
            // 
            // cbClipType
            // 
            this.cbClipType.AutoSize = true;
            this.cbClipType.Enabled = false;
            this.cbClipType.Location = new System.Drawing.Point(257, 17);
            this.cbClipType.Name = "cbClipType";
            this.cbClipType.Size = new System.Drawing.Size(96, 16);
            this.cbClipType.TabIndex = 23;
            this.cbClipType.Text = "输出技能目标";
            this.cbClipType.UseVisualStyleBackColor = true;
            // 
            // cbClip
            // 
            this.cbClip.AutoSize = true;
            this.cbClip.Location = new System.Drawing.Point(12, 17);
            this.cbClip.Name = "cbClip";
            this.cbClip.Size = new System.Drawing.Size(72, 16);
            this.cbClip.TabIndex = 23;
            this.cbClip.Text = "技能动画";
            this.cbClip.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.tabEffector);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.ForeColor = System.Drawing.Color.Maroon;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(151, 42);
            this.panel2.TabIndex = 0;
            // 
            // tabEffector
            // 
            this.tabEffector.Controls.Add(this.tabLocator);
            this.tabEffector.Controls.Add(this.tabEffects);
            this.tabEffector.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabEffector.ItemSize = new System.Drawing.Size(56, 26);
            this.tabEffector.Location = new System.Drawing.Point(0, 15);
            this.tabEffector.Name = "tabEffector";
            this.tabEffector.SelectedIndex = 0;
            this.tabEffector.Size = new System.Drawing.Size(151, 27);
            this.tabEffector.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabEffector.TabIndex = 1;
            // 
            // tabLocator
            // 
            this.tabLocator.BackColor = System.Drawing.Color.Transparent;
            this.tabLocator.ForeColor = System.Drawing.Color.Lime;
            this.tabLocator.Location = new System.Drawing.Point(4, 30);
            this.tabLocator.Name = "tabLocator";
            this.tabLocator.Padding = new System.Windows.Forms.Padding(3);
            this.tabLocator.Size = new System.Drawing.Size(143, 0);
            this.tabLocator.TabIndex = 0;
            this.tabLocator.Text = "目 标";
            // 
            // tabEffects
            // 
            this.tabEffects.Location = new System.Drawing.Point(4, 30);
            this.tabEffects.Name = "tabEffects";
            this.tabEffects.Size = new System.Drawing.Size(143, 0);
            this.tabEffects.TabIndex = 1;
            this.tabEffects.Text = "效 果";
            this.tabEffects.UseVisualStyleBackColor = true;
            // 
            // pnlTab
            // 
            this.pnlTab.AutoSize = true;
            this.pnlTab.Controls.Add(this.pnlLocator);
            this.pnlTab.Controls.Add(this.pnlEffects);
            this.pnlTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTab.Location = new System.Drawing.Point(0, 42);
            this.pnlTab.Name = "pnlTab";
            this.pnlTab.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.pnlTab.Size = new System.Drawing.Size(678, 56);
            this.pnlTab.TabIndex = 12;
            // 
            // pnlLocator
            // 
            this.pnlLocator.AutoSize = true;
            this.pnlLocator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLocator.Location = new System.Drawing.Point(0, 5);
            this.pnlLocator.Name = "pnlLocator";
            this.pnlLocator.Size = new System.Drawing.Size(678, 51);
            this.pnlLocator.TabIndex = 12;
            // 
            // pnlEffects
            // 
            this.pnlEffects.AutoSize = true;
            this.pnlEffects.Controls.Add(this.ucEffects);
            this.pnlEffects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEffects.Location = new System.Drawing.Point(0, 5);
            this.pnlEffects.Name = "pnlEffects";
            this.pnlEffects.Size = new System.Drawing.Size(678, 51);
            this.pnlEffects.TabIndex = 13;
            this.pnlEffects.Visible = false;
            // 
            // ucEffects
            // 
            this.ucEffects.AutoSize = true;
            this.ucEffects.ClassFlag = SkillEngine.Editor.Football.Data.EnumClassFlag.None;
            this.ucEffects.ClassRank = SkillEngine.Editor.Football.Data.EnumClassRank.None;
            this.ucEffects.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucEffects.Location = new System.Drawing.Point(0, 0);
            this.ucEffects.Name = "ucEffects";
            this.ucEffects.Size = new System.Drawing.Size(678, 48);
            this.ucEffects.TabIndex = 13;
            // 
            // EffectorCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoSize = true;
            this.Controls.Add(this.pnlTab);
            this.Controls.Add(this.panel1);
            this.Name = "EffectorCtrl";
            this.Size = new System.Drawing.Size(678, 98);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabEffector.ResumeLayout(false);
            this.pnlTab.ResumeLayout(false);
            this.pnlTab.PerformLayout();
            this.pnlEffects.ResumeLayout(false);
            this.pnlEffects.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox txtClipLast;
        public System.Windows.Forms.TextBox txtClipId;
        private System.Windows.Forms.Label lblClipLast;
        private System.Windows.Forms.Label lblClipId;
        public System.Windows.Forms.CheckBox cbClipType;
        public System.Windows.Forms.CheckBox cbClip;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabEffector;
        private System.Windows.Forms.TabPage tabLocator;
        private System.Windows.Forms.TabPage tabEffects;
        private System.Windows.Forms.Panel pnlTab;
        protected System.Windows.Forms.Panel pnlLocator;
        public FlatListCtrl ucEffects;
        public System.Windows.Forms.Panel pnlEffects;


    }
}
