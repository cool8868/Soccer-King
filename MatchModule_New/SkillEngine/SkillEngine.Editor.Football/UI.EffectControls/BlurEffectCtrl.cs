using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SkillEngine.Editor.Football.Data;
using SkillEngine.SkillBase.Enum;
using SkillEngine.SkillBase.Enum.Football;

namespace SkillEngine.Editor.Football.UI.EffectControls
{
    public partial class BlurEffectCtrl : SkillEngine.Editor.Football.UI.ControlBase.EffectCtrl
    {
        public BlurEffectCtrl()
        {
            InitializeComponent();
            this.combExecType.Enabled = false;
            this.txtPoint.Enabled = false;
            this.txtPercent.Enabled = false;
            this.combBuffId.SelectedIndexChanged += combBuffId_SelectedIndexChanged;
        }

        void combBuffId_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnumBlurBuffCode blurCode = EnumBlurBuffCode.None;
            EnumBlurType blurType = EnumBlurType.LockMotion;
            var item = this.combBuffId.SelectedItem as BindItemData;
            if (null != item)
                Enum.TryParse(item.Code, out blurCode);
            switch (blurCode)
            {
                case EnumBlurBuffCode.None:
                case EnumBlurBuffCode.Stand:
                case EnumBlurBuffCode.Falldown:
                case EnumBlurBuffCode.Puzzle:
                case EnumBlurBuffCode.Stun:
                case EnumBlurBuffCode.Inertia:
                    blurType = EnumBlurType.LockMotion;
                    break;
                case EnumBlurBuffCode.Rebel:
                case EnumBlurBuffCode.OutHand:
                    blurType = EnumBlurType.SpecMotion;
                    break;
                case EnumBlurBuffCode.Injure:
                case EnumBlurBuffCode.Disable:
                    blurType = EnumBlurType.BanMan;
                    break;
                case EnumBlurBuffCode.Silence:
                    blurType = EnumBlurType.LockSkill;
                    break;

                default:
                    break;
            }
            this.txtBlurType.Text = blurType.ToString();
        }

        public override void InitData()
        {
            this._dicAControls.Add("c.blurType", this.txtBlurType);
            this._dicAControls.Add("c.blurCode", this.combBuffId);
            this._dicAControls.Add("c.execType", this.combExecType);
            this._dicAControls.Add("c.mainFlag", this.cbxMainFlag);
            this._dicAControls.Add("c.pureFlag", this.cbxPureFlag);
            base.InitData();
            this.BindControl(this.combLast, SharedData.Instance.BindEffectLast(false, "Blur"));
            this.BindControl(this.combBuffId, SharedData.Instance.BindBlurBuffId());
            this.BindControl(this.combExecType, SharedData.Instance.BindExecType());
        }
       
    }
}
