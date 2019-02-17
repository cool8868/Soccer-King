using System;
using System.Collections.Generic;

namespace SoccerKing.Models
{
    public partial class Dicplayers
    {
        public Dicplayers()
        {
            Userplayers = new HashSet<Userplayers>();
        }

        public int Id { get; set; }
        public int? 速度t { get; set; }
        public int? 射门t { get; set; }
        public int? 传球t { get; set; }
        public int? 盘带t { get; set; }
        public int? 防守t { get; set; }
        public int? 力量t { get; set; }
        public int? 位置 { get; set; }
        public int? 大位置 { get; set; }
        public string 名字 { get; set; }
        public string 国籍 { get; set; }
        public int? 综合 { get; set; }
        public int? 潜力 { get; set; }
        public int? 传中 { get; set; }
        public int? 射术 { get; set; }
        public int? 头球 { get; set; }
        public int? 短传 { get; set; }
        public int? 凌空 { get; set; }
        public int? 盘带 { get; set; }
        public int? 弧线 { get; set; }
        public int? 任意球 { get; set; }
        public int? 长传 { get; set; }
        public int? 控球 { get; set; }
        public int? 加速 { get; set; }
        public int? 速度 { get; set; }
        public int? 敏捷 { get; set; }
        public int? 移动反应 { get; set; }
        public int? 平衡 { get; set; }
        public int? 射门力量 { get; set; }
        public int? 弹跳 { get; set; }
        public int? 体能 { get; set; }
        public int? 强壮 { get; set; }
        public int? 远射 { get; set; }
        public int? 侵略性 { get; set; }
        public int? 拦截意识 { get; set; }
        public int? 跑位 { get; set; }
        public int? 视野 { get; set; }
        public int? 点球 { get; set; }
        public int? 沉着 { get; set; }
        public int? 盯人 { get; set; }
        public int? 抢断 { get; set; }
        public int? 铲球 { get; set; }
        public int? 鱼跃 { get; set; }
        public int? 手形 { get; set; }
        public int? 开球 { get; set; }
        public int? 站位 { get; set; }
        public int? 守门反应 { get; set; }
        public string 惯用脚 { get; set; }
        public int? 国际声誉 { get; set; }
        public int? 逆足能力 { get; set; }
        public int? 花式技巧 { get; set; }

        public virtual ICollection<Userplayers> Userplayers { get; set; }
    }
}
