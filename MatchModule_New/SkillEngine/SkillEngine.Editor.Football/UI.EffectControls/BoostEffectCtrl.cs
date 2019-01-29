using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SkillEngine.Editor.Football.Data;
using SkillEngine.SkillBase.Enum;

namespace SkillEngine.Editor.Football.UI.EffectControls
{
    public partial class BoostEffectCtrl : SkillEngine.Editor.Football.UI.ControlBase.EffectCtrl
    {
        public BoostEffectCtrl()
        {
            InitializeComponent();
            this.txtRate.Enabled = false;
            this.combBoostType.SelectedIndexChanged += combBoostType_SelectedIndexChanged;
        }

        void combBoostType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string boostType = string.Empty;
            var item = this.combBoostType.SelectedItem as BindItemData;
            if (null != item)
                boostType = item.Code;
            this.combBuffId.Value = string.Empty;
            this.BindControl(this.combBuffId, SharedData.Instance.BindBoostBuffId(boostType), false);
            this.combAntiSkillType.Value = string.Empty;
            this.combAntiSkillType.Enabled = boostType == EnumBoostType.AntiRate.ToString();
        }

        public override void InitData()
        {
            this._dicAControls.Add("c.boostType", this.combBoostType);
            this._dicAControls.Add("c.buffId", this.combBuffId);
            this._dicAControls.Add("c.mainFlag", this.cbxMainFlag);
            this._dicAControls.Add("c.pureFlag", this.cbxPureFlag);
            this._dicAControls.Add("c.debuffFlag", this.cbxDebuffFlag);
            this._dicAControls.Add("p.AntiSkillType", this.combAntiSkillType);
            base.InitData();
            this.BindControl(this.combBoostType, SharedData.Instance.BindBoostType());
            this.BindControl(this.combAntiSkillType, SharedData.Instance.BindSkillSrcTypeInt(true));
        }
      
    }
}
