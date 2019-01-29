using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SkillEngine.Editor.Football.Data;

namespace SkillEngine.Editor.Football.UI.EffectControls
{
    public partial class EventPropPlusEffectCtrl : SkillEngine.Editor.Football.UI.ControlBase.EffectCtrl
    {
        public EventPropPlusEffectCtrl()
        {
            InitializeComponent();
        }
        public override void InitData()
        {
            this._dicAControls.Add("c.buffId", this.combBuffId);
            this._dicAControls.Add("c.mainFlag", this.cbxMainFlag);
            this._dicAControls.Add("c.pureFlag", this.cbxPureFlag);
            this._dicAControls.Add("c.debuffFlag", this.cbxDebuffFlag);
            this._dicAControls.Add("p.Side", this.combSide);
            this._dicAControls.Add("p.ExecType", this.combExecType);
            this._dicAControls.Add("p.EventType", this.combEventType);
            base.InitData();
            this.BindControl(this.combBuffId, SharedData.Instance.BindPropBuffId(), false);
            this.BindControl(this.combSide, SharedData.Instance.BindEventTagetType());
            this.BindControl(this.combExecType, SharedData.Instance.BindExecType());
            this.BindControl(this.combEventType, SharedData.Instance.BindEventType());

        }

    }
}
