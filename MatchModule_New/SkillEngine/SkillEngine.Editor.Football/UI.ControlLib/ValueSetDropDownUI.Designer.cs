namespace SkillEngine.Editor.Football.UI.ControlLib
{
    partial class ValueSetDropDownUI
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.cbxAll = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.clbList = new SkillEngine.Editor.Football.UI.ControlLib.VCheckedListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.cbxAll);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 315);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(435, 49);
            this.panel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(330, 15);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(225, 15);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // cbxAll
            // 
            this.cbxAll.AutoSize = true;
            this.cbxAll.Location = new System.Drawing.Point(37, 19);
            this.cbxAll.Name = "cbxAll";
            this.cbxAll.Size = new System.Drawing.Size(72, 16);
            this.cbxAll.TabIndex = 0;
            this.cbxAll.Text = "所有选项";
            this.cbxAll.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.clbList);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(435, 315);
            this.panel2.TabIndex = 1;
            // 
            // clbList
            // 
            this.clbList.CheckOnClick = true;
            this.clbList.ColumnWidth = 150;
            this.clbList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clbList.FormattingEnabled = true;
            this.clbList.Location = new System.Drawing.Point(0, 0);
            this.clbList.MultiColumn = true;
            this.clbList.Name = "clbList";
            this.clbList.ScrollAlwaysVisible = true;
            this.clbList.Size = new System.Drawing.Size(435, 315);
            this.clbList.TabIndex = 0;
            // 
            // ValueSetDropDownUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ValueSetDropDownUI";
            this.Size = new System.Drawing.Size(435, 364);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox cbxAll;
        private System.Windows.Forms.Panel panel2;
        private SkillEngine.Editor.Football.UI.ControlLib.VCheckedListBox clbList;
    }
}
