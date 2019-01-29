namespace SkillEngine.Editor.Football.UI
{
    partial class SkillEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SkillEditForm));
            this.btnSave = new System.Windows.Forms.Button();
            this.ucSkill = new SkillEngine.Editor.Football.UI.ControlBase.SkillCtrl();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("华文彩云", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(691, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 32);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // ucSkill
            // 
            this.ucSkill.ClassFlag = SkillEngine.Editor.Football.Data.EnumClassFlag.Manager;
            this.ucSkill.ClassRank = SkillEngine.Editor.Football.Data.EnumClassRank.None;
            this.ucSkill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucSkill.Location = new System.Drawing.Point(0, 0);
            this.ucSkill.Name = "ucSkill";
            this.ucSkill.Size = new System.Drawing.Size(802, 712);
            this.ucSkill.TabIndex = 0;
            // 
            // SkillEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 712);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.ucSkill);
            this.Name = "SkillEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "技能编辑";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private ControlBase.SkillCtrl ucSkill;
        private System.Windows.Forms.Button btnSave;


    }
}