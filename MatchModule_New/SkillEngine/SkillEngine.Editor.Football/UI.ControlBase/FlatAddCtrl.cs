using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using SkillEngine.Editor.Football.Util;
using SkillEngine.Editor.Football.Data;
using SkillEngine.Editor.Football.Rules;

namespace SkillEngine.Editor.Football.UI.ControlBase
{
    public partial class FlatAddCtrl : FlatCtrl
    {
        public FlatAddCtrl()
        {
            InitializeComponent();
            this.btnFold.Click += btnFold_Click;
            this.btnRemove.Click += btnRemove_Click;
            this.combClassType.SelectedIndexChanged += combClassType_SelectedIndexChanged;
        }

        #region EventHandler
        void btnFold_Click(object sender, EventArgs e)
        {
            //this.SuspendLayout();
            this.pnlItem.Visible = !this.pnlItem.Visible;
            if (this.pnlItem.Visible)
                this.btnFold.Image = global::SkillEngine.Editor.Football.Properties.Resources.arrrow_up;
            else
                this.btnFold.Image = global::SkillEngine.Editor.Football.Properties.Resources.arrow_down;
            //this.ResumeLayout();
        }
        void btnRemove_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("确定要删除该条目吗？", "系统信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
            //    return;
            this.Parent.Controls.Remove(this);
            this.Dispose();
        }
        void combClassType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string classKey = GetSelectedValue(this.combClassType);
            this.pnlItem.Controls.Clear();
            if (string.IsNullOrEmpty(classKey))
                return;
            if (this.ClassRank == EnumClassRank.Effector)
            {
                switch (classKey)
                {
                    case "Effector.Manager":
                        this.ClassFlag = EnumClassFlag.Manager;
                        break;
                    case "Effector.Player":
                        this.ClassFlag = EnumClassFlag.Player;
                        break;
                    case "Effector.PlayerEvent":
                        this.ClassFlag = EnumClassFlag.PlayerEvent;
                        break;
                    case "Effector.Skill":
                        this.ClassFlag = EnumClassFlag.Skill;
                        break;
                }
            }
            var flatCtrl = GetFlatCtrl(classKey);
            if (null != flatCtrl)
                this.pnlItem.Controls.Add(flatCtrl);
        }
        #endregion

        #region Cache
        readonly Dictionary<string, FlatCtrl> dicFlatCtrl = new Dictionary<string, FlatCtrl>();
        #endregion

        #region FlatCtrl
        public override XElement GetValue()
        {
            string classKey = GetSelectedValue(this.combClassType);
            if (string.IsNullOrEmpty(classKey))
                return null;
            return base.GetValue();
        }
        public override void InitData()
        {
            if (this.ClassRank == EnumClassRank.Effector)
                this.pnlTool.BackColor = System.Drawing.SystemColors.ActiveCaption;
            else
                this.pnlTool.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.lblTip.Text = GetClassRankName(this.ClassRank, false);
            this._dicAControls.Add("type", this.combClassType);
            this._dicNAControls.Add("na", this.pnlItem);
            this.BindControl(this.combClassType, SharedData.Instance.BindClassType(true, this.ClassRank, this.ClassFlag));
        }
        protected override bool PreProcessControlValue(DataAccessMode daMode, Control control, ref XElement xe)
        {
            if (control != this.pnlItem)
                return false;
            if (this.pnlItem.Controls.Count == 0)
                return false;
            var flat = this.pnlItem.Controls[0] as FlatCtrl;
            if (null == flat)
                return false;
            return this.ProcessFlatControlValue(daMode, flat, ref xe);
        }
        #endregion

        #region Tools
        FlatCtrl GetFlatCtrl(string classKey)
        {
            FlatCtrl flatCtrl = null;
            if (dicFlatCtrl.TryGetValue(classKey, out flatCtrl) && null != flatCtrl)
                return flatCtrl;
            flatCtrl = SharedLogic.GetFlatControl(classKey);
            if (null != flatCtrl)
            {
                flatCtrl.Dock = DockStyle.Top;
                dicFlatCtrl[classKey] = flatCtrl;
            }
            return flatCtrl;
        }
        #endregion

    }
}
