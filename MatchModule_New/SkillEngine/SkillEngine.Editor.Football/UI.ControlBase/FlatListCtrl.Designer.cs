namespace SkillEngine.Editor.Football.UI.ControlBase
{
    partial class FlatListCtrl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlatListCtrl));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnFold = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.gbList = new System.Windows.Forms.GroupBox();
            this.pnlList = new System.Windows.Forms.Panel();
            this.lblTip = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.gbList.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTip);
            this.panel1.Controls.Add(this.btnFold);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(678, 28);
            this.panel1.TabIndex = 0;
            // 
            // btnFold
            // 
            this.btnFold.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFold.FlatAppearance.BorderSize = 0;
            this.btnFold.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFold.Image = ((System.Drawing.Image)(resources.GetObject("btnFold.Image")));
            this.btnFold.Location = new System.Drawing.Point(6, 4);
            this.btnFold.Name = "btnFold";
            this.btnFold.Size = new System.Drawing.Size(25, 25);
            this.btnFold.TabIndex = 3;
            this.btnFold.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Image = global::SkillEngine.Editor.Football.Properties.Resources.miniplus;
            this.btnAdd.Location = new System.Drawing.Point(42, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(25, 25);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // gbList
            // 
            this.gbList.AutoSize = true;
            this.gbList.Controls.Add(this.pnlList);
            this.gbList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbList.Location = new System.Drawing.Point(0, 28);
            this.gbList.Name = "gbList";
            this.gbList.Size = new System.Drawing.Size(678, 24);
            this.gbList.TabIndex = 1;
            this.gbList.TabStop = false;
            // 
            // pnlList
            // 
            this.pnlList.AutoScroll = true;
            this.pnlList.AutoSize = true;
            this.pnlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlList.Location = new System.Drawing.Point(3, 17);
            this.pnlList.Name = "pnlList";
            this.pnlList.Size = new System.Drawing.Size(672, 4);
            this.pnlList.TabIndex = 0;
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTip.ForeColor = System.Drawing.Color.Blue;
            this.lblTip.Location = new System.Drawing.Point(83, 11);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(53, 12);
            this.lblTip.TabIndex = 4;
            this.lblTip.Text = "指示文字";
            // 
            // FlatListCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.gbList);
            this.Controls.Add(this.panel1);
            this.Name = "FlatListCtrl";
            this.Size = new System.Drawing.Size(678, 52);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gbList.ResumeLayout(false);
            this.gbList.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Button btnAdd;
        protected System.Windows.Forms.GroupBox gbList;
        protected System.Windows.Forms.Panel pnlList;
        public System.Windows.Forms.Button btnFold;
        private System.Windows.Forms.Label lblTip;
    }
}
