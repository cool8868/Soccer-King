namespace SkillEngine.Editor.Football.UI.FilterControls
{
    partial class BallStateFilterCtrl
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
            this.combBallState = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.combBallSide = new SkillEngine.Editor.Football.UI.ControlLib.VComboBox();
            this.lblBallState = new System.Windows.Forms.Label();
            this.lblBallSide = new System.Windows.Forms.Label();
            this.pnlBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBox
            // 
            this.pnlBox.Controls.Add(this.combBallState);
            this.pnlBox.Controls.Add(this.combBallSide);
            this.pnlBox.Controls.Add(this.lblBallState);
            this.pnlBox.Controls.Add(this.lblBallSide);
            // 
            // combBallState
            // 
            this.combBallState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combBallState.FormattingEnabled = true;
            this.combBallState.Location = new System.Drawing.Point(305, 12);
            this.combBallState.Name = "combBallState";
            this.combBallState.Size = new System.Drawing.Size(120, 20);
            this.combBallState.TabIndex = 10;
            // 
            // combBallSide
            // 
            this.combBallSide.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combBallSide.FormattingEnabled = true;
            this.combBallSide.Location = new System.Drawing.Point(92, 12);
            this.combBallSide.Name = "combBallSide";
            this.combBallSide.Size = new System.Drawing.Size(120, 20);
            this.combBallSide.TabIndex = 11;
            // 
            // lblBallState
            // 
            this.lblBallState.AutoSize = true;
            this.lblBallState.Location = new System.Drawing.Point(234, 15);
            this.lblBallState.Name = "lblBallState";
            this.lblBallState.Size = new System.Drawing.Size(65, 12);
            this.lblBallState.TabIndex = 8;
            this.lblBallState.Text = "持球状态：";
            // 
            // lblBallSide
            // 
            this.lblBallSide.AutoSize = true;
            this.lblBallSide.Location = new System.Drawing.Point(21, 15);
            this.lblBallSide.Name = "lblBallSide";
            this.lblBallSide.Size = new System.Drawing.Size(65, 12);
            this.lblBallSide.TabIndex = 9;
            this.lblBallSide.Text = "攻守状态：";
            // 
            // BallStateFilterCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.Name = "BallStateFilterCtrl";
            this.pnlBox.ResumeLayout(false);
            this.pnlBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblBallSide;
        public ControlLib.VComboBox combBallSide;
        private System.Windows.Forms.Label lblBallState;
        public ControlLib.VComboBox combBallState;
    }
}
