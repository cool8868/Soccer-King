using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SkillEngine.Editor.Football.Data;

namespace SkillEngine.Editor.Football.UI.SeekerControls
{
    public partial class PlayerSkillSeekerCtrl : SkillEngine.Editor.Football.UI.ControlBase.SeekerCtrl
    {
        public PlayerSkillSeekerCtrl()
        {
            InitializeComponent();
            this.txtType.Text = "Seeker.PlayerSkill";
        }
        protected TextBox txtType = new TextBox();
        public override void InitData()
        {
            this.ClassFlag = EnumClassFlag.Skill;
            this._dicAControls.Add("type", this.txtType);
            base.InitData();
            this._dicAControls.Add("p.Ids", this.txtIds);
            this._dicAControls.Add("p.SrcTypes", this.combSrcType);
            this._dicAControls.Add("p.ActTypes", this.combActType);
            this._dicAControls.Add("p.SkillFlag", this.combSkillFlag);
            this.BindControl(this.combSrcType, SharedData.Instance.BindSkillSrcType());
            this.BindControl(this.combActType, SharedData.Instance.BindSkillActType());
            this.BindControl(this.combSkillFlag, SharedData.Instance.BindSkillFlag());
        }

    }
}
