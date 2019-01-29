namespace SkillEngine.Editor.Football.UI.ControlBase
{
    partial class TriggerCtrl
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
            this.cbxRepeat = new System.Windows.Forms.CheckBox();
            this.cbxRecycle = new System.Windows.Forms.CheckBox();
            this.cbxDelay = new System.Windows.Forms.CheckBox();
            this.pnlTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRemove
            // 
            this.btnRemove.FlatAppearance.BorderSize = 0;
            // 
            // pnlTool
            // 
            this.pnlTool.Controls.Add(this.cbxRecycle);
            this.pnlTool.Controls.Add(this.cbxDelay);
            this.pnlTool.Controls.Add(this.cbxRepeat);
            this.pnlTool.Controls.SetChildIndex(this.btnFold, 0);
            this.pnlTool.Controls.SetChildIndex(this.btnRemove, 0);
            this.pnlTool.Controls.SetChildIndex(this.combClassType, 0);
            this.pnlTool.Controls.SetChildIndex(this.cbxRepeat, 0);
            this.pnlTool.Controls.SetChildIndex(this.cbxDelay, 0);
            this.pnlTool.Controls.SetChildIndex(this.cbxRecycle, 0);
            // 
            // btnFold
            // 
            this.btnFold.FlatAppearance.BorderSize = 0;
            // 
            // cbxRepeat
            // 
            this.cbxRepeat.AutoSize = true;
            this.cbxRepeat.Location = new System.Drawing.Point(388, 7);
            this.cbxRepeat.Name = "cbxRepeat";
            this.cbxRepeat.Size = new System.Drawing.Size(84, 16);
            this.cbxRepeat.TabIndex = 3;
            this.cbxRepeat.Text = "可重复条件";
            this.cbxRepeat.UseVisualStyleBackColor = true;
            // 
            // cbxRecycle
            // 
            this.cbxRecycle.AutoSize = true;
            this.cbxRecycle.Location = new System.Drawing.Point(494, 7);
            this.cbxRecycle.Name = "cbxRecycle";
            this.cbxRecycle.Size = new System.Drawing.Size(84, 16);
            this.cbxRecycle.TabIndex = 3;
            this.cbxRecycle.Text = "可回收条件";
            this.cbxRecycle.UseVisualStyleBackColor = true;
            // 
            // cbxDelay
            // 
            this.cbxDelay.AutoSize = true;
            this.cbxDelay.Location = new System.Drawing.Point(591, 8);
            this.cbxDelay.Name = "cbxDelay";
            this.cbxDelay.Size = new System.Drawing.Size(48, 16);
            this.cbxDelay.TabIndex = 3;
            this.cbxDelay.Text = "延迟";
            this.cbxDelay.UseVisualStyleBackColor = true;
            // 
            // TriggerCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "TriggerCtrl";
            this.pnlTool.ResumeLayout(false);
            this.pnlTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox cbxRepeat;
        public System.Windows.Forms.CheckBox cbxRecycle;
        public System.Windows.Forms.CheckBox cbxDelay;

    }
}
