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

namespace SkillEngine.Editor.Football.UI.ControlBase
{
    public partial class FlatListCtrl : FlatCtrl
    {
        public FlatListCtrl()
        {
            InitializeComponent();
            this.btnFold.Click += btnFold_Click;
            this.btnAdd.Click += btnAdd_Click;
        }

        #region EventHandler
        void btnAdd_Click(object sender, EventArgs e)
        {
            var flatCtrl = GetFlatAddCtrl();
            if (null == flatCtrl)
                return;
            this.pnlList.SuspendLayout();
            int cnt = this.pnlList.Controls.Count + 1;
            var ctrls = new Control[cnt];
            cnt = 0;
            foreach (var item in this.pnlList.Controls)
            {
                ctrls[++cnt] = item as Control;
            }
            ctrls[0] = flatCtrl;
            this.pnlList.Controls.Clear();
            this.pnlList.Controls.AddRange(ctrls);
            this.pnlList.Visible = true;
            this.pnlList.ResumeLayout();
        }
        void btnFold_Click(object sender, EventArgs e)
        {
            this.pnlList.Visible = !this.pnlList.Visible;
            if (this.pnlList.Visible)
                this.btnFold.Image = global::SkillEngine.Editor.Football.Properties.Resources.arrrow_up;
            else
                this.btnFold.Image = global::SkillEngine.Editor.Football.Properties.Resources.arrow_down;
        }
        #endregion

        #region FlatCtrl
        public override void InitData()
        {
            this.lblTip.Text = GetClassRankName(this.ClassRank, true);
        }
        public override XElement GetValue()
        {
            lock (_rootXe)
            {
                this.GetBindCotrols();
                this._dicNValues.Clear();
                ProcessControlsValue(DataAccessMode.GetData, _dicNControls, _dicNValues);
                _rootXe.RemoveAll();
                foreach (var kvp in _dicNValues)
                {
                    IncludeXElement(_rootXe, kvp.Value, SharedData.KEYAdd);
                }
                return _rootXe;
            }
        }
        public override bool SetValue(XElement xe)
        {
            lock (_rootXe)
            {
                this.GenBindControls(xe);
                ProcessControlsValue(DataAccessMode.SetData, _dicNControls, _dicNValues);
                return true;
            }
        }
        #endregion

        #region Tools
        void GetBindCotrols()
        {
            this._dicNControls.Clear();
            string key = string.Empty;
            int cnt = this.pnlList.Controls.Count;
            if (cnt == 0)
                return;
            var ctrls = new Control[cnt];
            foreach (var item in this.pnlList.Controls)
            {
                ctrls[--cnt] = item as Control;
            }
            int idx = 0;
            foreach (var item in ctrls)
            {
                if (!(item is FlatCtrl))
                    continue;
                key = SharedData.KEYAdd;
                if (idx > 0)
                    key = string.Concat(key, KEYSplit, idx);
                _dicNControls[key] = (Control)item;
                idx++;
            }
        }
        void GenBindControls(XElement xe)
        {
            this._dicNControls.Clear();
            this._dicNAValues.Clear();
            this.pnlList.Controls.Clear();
            if (null == xe)
                return;
            string key = string.Empty;
            int idx = 0;
            foreach (var xe2 in xe.Elements(SharedData.KEYAdd))
            {
                key = xe2.Name.LocalName;
                if (idx > 0)
                    key = string.Concat(key, KEYSplit, idx);
                var flatCtrl = GetFlatAddCtrl();
                if (null == flatCtrl)
                    continue;
                _dicNControls[key] = flatCtrl;
                _dicNValues[key] = xe2;
                ++idx;
            }
            idx=_dicNControls.Count;
            if (idx > 0)
            {
                Control[] ctrls = new Control[idx];
                foreach (var item in _dicNControls.Values)
                {
                    ctrls[--idx] = item;
                }
                this.pnlList.Controls.AddRange(ctrls);
            }
        }
        FlatAddCtrl GetFlatAddCtrl()
        {
            FlatAddCtrl flatCtrl=null;
            if(this.ClassRank==EnumClassRank.Trigger)
                flatCtrl = new TriggerCtrl();
            else
                flatCtrl = new FlatAddCtrl();
            flatCtrl.ClassRank = this.ClassRank;
            flatCtrl.ClassFlag = this.ClassFlag;
            flatCtrl.Dock = DockStyle.Top;
            switch (this.ClassRank)
            {
                case EnumClassRank.Trigger:
                case EnumClassRank.Effector:
                case EnumClassRank.Effect:
                case EnumClassRank.Seeker:
                    flatCtrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    break;
            }
            flatCtrl.InitData();
            return flatCtrl;
        }
        #endregion

    }
}
