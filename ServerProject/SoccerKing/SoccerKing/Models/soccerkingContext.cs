using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SoccerKing.Models
{
    public partial class soccerkingContext : DbContext
    {
        public soccerkingContext()
        {
        }

        public soccerkingContext(DbContextOptions<soccerkingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Clubinfo> Clubinfo { get; set; }
        public virtual DbSet<Coach> Coach { get; set; }
        public virtual DbSet<Dicplayers> Dicplayers { get; set; }
        public virtual DbSet<DicplayersD> DicplayersD { get; set; }
        public virtual DbSet<DicplayersF> DicplayersF { get; set; }
        public virtual DbSet<DicplayersGk> DicplayersGk { get; set; }
        public virtual DbSet<DicplayersM> DicplayersM { get; set; }
        public virtual DbSet<Dicplayersdrelation> Dicplayersdrelation { get; set; }
        public virtual DbSet<Dicplayersfrelation> Dicplayersfrelation { get; set; }
        public virtual DbSet<Dicplayersgkrelation> Dicplayersgkrelation { get; set; }
        public virtual DbSet<Dicplayersmrelation> Dicplayersmrelation { get; set; }
        public virtual DbSet<Dictalent> Dictalent { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Options> Options { get; set; }
        public virtual DbSet<Randomstory> Randomstory { get; set; }
        public virtual DbSet<Story> Story { get; set; }
        public virtual DbSet<Talent> Talent { get; set; }
        public virtual DbSet<Teamplayersrandom> Teamplayersrandom { get; set; }
        public virtual DbSet<Usermoneychangelog> Usermoneychangelog { get; set; }
        public virtual DbSet<Userplayers> Userplayers { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=localhost;User Id=root;Password=haitao;Database=soccerking");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clubinfo>(entity =>
            {
                entity.ToTable("clubinfo");

                entity.HasIndex(e => e.Uid)
                    .HasName("Uid");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Rowtime)
                    .HasColumnName("rowtime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Coach>(entity =>
            {
                entity.ToTable("coach");

                entity.HasIndex(e => e.Uid)
                    .HasName("Uid");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Age)
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Lv)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Rowtime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Special).HasColumnType("tinyint(4)");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Type).HasColumnType("tinyint(4)");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Dicplayers>(entity =>
            {
                entity.ToTable("dicplayers");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.任意球).HasColumnType("int(11)");

                entity.Property(e => e.传中).HasColumnType("int(11)");

                entity.Property(e => e.传球t)
                    .HasColumnName("传球T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.位置).HasColumnType("int(11)");

                entity.Property(e => e.体能).HasColumnType("int(11)");

                entity.Property(e => e.侵略性).HasColumnType("int(11)");

                entity.Property(e => e.凌空).HasColumnType("int(11)");

                entity.Property(e => e.力量t)
                    .HasColumnName("力量T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.加速).HasColumnType("int(11)");

                entity.Property(e => e.名字).HasColumnType("varchar(50)");

                entity.Property(e => e.国籍).HasColumnType("varchar(50)");

                entity.Property(e => e.国际声誉).HasColumnType("int(11)");

                entity.Property(e => e.大位置).HasColumnType("int(11)");

                entity.Property(e => e.头球).HasColumnType("int(11)");

                entity.Property(e => e.守门反应).HasColumnType("int(11)");

                entity.Property(e => e.射术).HasColumnType("int(11)");

                entity.Property(e => e.射门t)
                    .HasColumnName("射门T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.射门力量).HasColumnType("int(11)");

                entity.Property(e => e.平衡).HasColumnType("int(11)");

                entity.Property(e => e.开球).HasColumnType("int(11)");

                entity.Property(e => e.弧线).HasColumnType("int(11)");

                entity.Property(e => e.弹跳).HasColumnType("int(11)");

                entity.Property(e => e.强壮).HasColumnType("int(11)");

                entity.Property(e => e.惯用脚).HasColumnType("varchar(50)");

                entity.Property(e => e.手形).HasColumnType("int(11)");

                entity.Property(e => e.抢断).HasColumnType("int(11)");

                entity.Property(e => e.拦截意识).HasColumnType("int(11)");

                entity.Property(e => e.控球).HasColumnType("int(11)");

                entity.Property(e => e.敏捷).HasColumnType("int(11)");

                entity.Property(e => e.沉着).HasColumnType("int(11)");

                entity.Property(e => e.潜力).HasColumnType("int(11)");

                entity.Property(e => e.点球).HasColumnType("int(11)");

                entity.Property(e => e.盘带).HasColumnType("int(11)");

                entity.Property(e => e.盘带t)
                    .HasColumnName("盘带T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.盯人).HasColumnType("int(11)");

                entity.Property(e => e.短传).HasColumnType("int(11)");

                entity.Property(e => e.移动反应).HasColumnType("int(11)");

                entity.Property(e => e.站位).HasColumnType("int(11)");

                entity.Property(e => e.综合).HasColumnType("int(11)");

                entity.Property(e => e.花式技巧).HasColumnType("int(11)");

                entity.Property(e => e.视野).HasColumnType("int(11)");

                entity.Property(e => e.跑位).HasColumnType("int(11)");

                entity.Property(e => e.远射).HasColumnType("int(11)");

                entity.Property(e => e.逆足能力).HasColumnType("int(11)");

                entity.Property(e => e.速度).HasColumnType("int(11)");

                entity.Property(e => e.速度t)
                    .HasColumnName("速度T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.铲球).HasColumnType("int(11)");

                entity.Property(e => e.长传).HasColumnType("int(11)");

                entity.Property(e => e.防守t)
                    .HasColumnName("防守T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.鱼跃).HasColumnType("int(11)");
            });

            modelBuilder.Entity<DicplayersD>(entity =>
            {
                entity.ToTable("dicplayers_d");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.任意球).HasColumnType("int(11)");

                entity.Property(e => e.传中).HasColumnType("int(11)");

                entity.Property(e => e.传球t)
                    .HasColumnName("传球T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.位置).HasColumnType("int(11)");

                entity.Property(e => e.体能).HasColumnType("int(11)");

                entity.Property(e => e.侵略性).HasColumnType("int(11)");

                entity.Property(e => e.凌空).HasColumnType("int(11)");

                entity.Property(e => e.力量t)
                    .HasColumnName("力量T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.加速).HasColumnType("int(11)");

                entity.Property(e => e.名字).HasColumnType("varchar(50)");

                entity.Property(e => e.国籍).HasColumnType("varchar(50)");

                entity.Property(e => e.国际声誉).HasColumnType("int(11)");

                entity.Property(e => e.头球).HasColumnType("int(11)");

                entity.Property(e => e.守门反应).HasColumnType("int(11)");

                entity.Property(e => e.射术).HasColumnType("int(11)");

                entity.Property(e => e.射门t)
                    .HasColumnName("射门T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.射门力量).HasColumnType("int(11)");

                entity.Property(e => e.平衡).HasColumnType("int(11)");

                entity.Property(e => e.开球).HasColumnType("int(11)");

                entity.Property(e => e.弧线).HasColumnType("int(11)");

                entity.Property(e => e.弹跳).HasColumnType("int(11)");

                entity.Property(e => e.强壮).HasColumnType("int(11)");

                entity.Property(e => e.惯用脚).HasColumnType("varchar(50)");

                entity.Property(e => e.手形).HasColumnType("int(11)");

                entity.Property(e => e.抢断).HasColumnType("int(11)");

                entity.Property(e => e.拦截意识).HasColumnType("int(11)");

                entity.Property(e => e.控球).HasColumnType("int(11)");

                entity.Property(e => e.敏捷).HasColumnType("int(11)");

                entity.Property(e => e.沉着).HasColumnType("int(11)");

                entity.Property(e => e.潜力).HasColumnType("int(11)");

                entity.Property(e => e.点球).HasColumnType("int(11)");

                entity.Property(e => e.盘带).HasColumnType("int(11)");

                entity.Property(e => e.盘带t)
                    .HasColumnName("盘带T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.盯人).HasColumnType("int(11)");

                entity.Property(e => e.短传).HasColumnType("int(11)");

                entity.Property(e => e.移动反应).HasColumnType("int(11)");

                entity.Property(e => e.站位).HasColumnType("int(11)");

                entity.Property(e => e.综合).HasColumnType("int(11)");

                entity.Property(e => e.花式技巧).HasColumnType("int(11)");

                entity.Property(e => e.视野).HasColumnType("int(11)");

                entity.Property(e => e.跑位).HasColumnType("int(11)");

                entity.Property(e => e.远射).HasColumnType("int(11)");

                entity.Property(e => e.逆足能力).HasColumnType("int(11)");

                entity.Property(e => e.速度).HasColumnType("int(11)");

                entity.Property(e => e.速度t)
                    .HasColumnName("速度T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.铲球).HasColumnType("int(11)");

                entity.Property(e => e.长传).HasColumnType("int(11)");

                entity.Property(e => e.防守t)
                    .HasColumnName("防守T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.鱼跃).HasColumnType("int(11)");
            });

            modelBuilder.Entity<DicplayersF>(entity =>
            {
                entity.ToTable("dicplayers_f");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.任意球).HasColumnType("int(11)");

                entity.Property(e => e.传中).HasColumnType("int(11)");

                entity.Property(e => e.传球t)
                    .HasColumnName("传球T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.位置).HasColumnType("int(11)");

                entity.Property(e => e.体能).HasColumnType("int(11)");

                entity.Property(e => e.侵略性).HasColumnType("int(11)");

                entity.Property(e => e.凌空).HasColumnType("int(11)");

                entity.Property(e => e.力量t)
                    .HasColumnName("力量T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.加速).HasColumnType("int(11)");

                entity.Property(e => e.名字).HasColumnType("varchar(50)");

                entity.Property(e => e.国籍).HasColumnType("varchar(50)");

                entity.Property(e => e.国际声誉).HasColumnType("int(11)");

                entity.Property(e => e.头球).HasColumnType("int(11)");

                entity.Property(e => e.守门反应).HasColumnType("int(11)");

                entity.Property(e => e.射术).HasColumnType("int(11)");

                entity.Property(e => e.射门t)
                    .HasColumnName("射门T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.射门力量).HasColumnType("int(11)");

                entity.Property(e => e.平衡).HasColumnType("int(11)");

                entity.Property(e => e.开球).HasColumnType("int(11)");

                entity.Property(e => e.弧线).HasColumnType("int(11)");

                entity.Property(e => e.弹跳).HasColumnType("int(11)");

                entity.Property(e => e.强壮).HasColumnType("int(11)");

                entity.Property(e => e.惯用脚).HasColumnType("varchar(50)");

                entity.Property(e => e.手形).HasColumnType("int(11)");

                entity.Property(e => e.抢断).HasColumnType("int(11)");

                entity.Property(e => e.拦截意识).HasColumnType("int(11)");

                entity.Property(e => e.控球).HasColumnType("int(11)");

                entity.Property(e => e.敏捷).HasColumnType("int(11)");

                entity.Property(e => e.沉着).HasColumnType("int(11)");

                entity.Property(e => e.潜力).HasColumnType("int(11)");

                entity.Property(e => e.点球).HasColumnType("int(11)");

                entity.Property(e => e.盘带).HasColumnType("int(11)");

                entity.Property(e => e.盘带t)
                    .HasColumnName("盘带T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.盯人).HasColumnType("int(11)");

                entity.Property(e => e.短传).HasColumnType("int(11)");

                entity.Property(e => e.移动反应).HasColumnType("int(11)");

                entity.Property(e => e.站位).HasColumnType("int(11)");

                entity.Property(e => e.综合).HasColumnType("int(11)");

                entity.Property(e => e.花式技巧).HasColumnType("int(11)");

                entity.Property(e => e.视野).HasColumnType("int(11)");

                entity.Property(e => e.跑位).HasColumnType("int(11)");

                entity.Property(e => e.远射).HasColumnType("int(11)");

                entity.Property(e => e.逆足能力).HasColumnType("int(11)");

                entity.Property(e => e.速度).HasColumnType("int(11)");

                entity.Property(e => e.速度t)
                    .HasColumnName("速度T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.铲球).HasColumnType("int(11)");

                entity.Property(e => e.长传).HasColumnType("int(11)");

                entity.Property(e => e.防守t)
                    .HasColumnName("防守T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.鱼跃).HasColumnType("int(11)");
            });

            modelBuilder.Entity<DicplayersGk>(entity =>
            {
                entity.ToTable("dicplayers_gk");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.任意球).HasColumnType("int(11)");

                entity.Property(e => e.传中).HasColumnType("int(11)");

                entity.Property(e => e.传球t)
                    .HasColumnName("传球T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.位置).HasColumnType("int(11)");

                entity.Property(e => e.体能).HasColumnType("int(11)");

                entity.Property(e => e.侵略性).HasColumnType("int(11)");

                entity.Property(e => e.凌空).HasColumnType("int(11)");

                entity.Property(e => e.力量t)
                    .HasColumnName("力量T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.加速).HasColumnType("int(11)");

                entity.Property(e => e.名字).HasColumnType("varchar(50)");

                entity.Property(e => e.国籍).HasColumnType("varchar(50)");

                entity.Property(e => e.国际声誉).HasColumnType("int(11)");

                entity.Property(e => e.头球).HasColumnType("int(11)");

                entity.Property(e => e.守门反应).HasColumnType("int(11)");

                entity.Property(e => e.射术).HasColumnType("int(11)");

                entity.Property(e => e.射门t)
                    .HasColumnName("射门T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.射门力量).HasColumnType("int(11)");

                entity.Property(e => e.平衡).HasColumnType("int(11)");

                entity.Property(e => e.开球).HasColumnType("int(11)");

                entity.Property(e => e.弧线).HasColumnType("int(11)");

                entity.Property(e => e.弹跳).HasColumnType("int(11)");

                entity.Property(e => e.强壮).HasColumnType("int(11)");

                entity.Property(e => e.惯用脚).HasColumnType("varchar(50)");

                entity.Property(e => e.手形).HasColumnType("int(11)");

                entity.Property(e => e.抢断).HasColumnType("int(11)");

                entity.Property(e => e.拦截意识).HasColumnType("int(11)");

                entity.Property(e => e.控球).HasColumnType("int(11)");

                entity.Property(e => e.敏捷).HasColumnType("int(11)");

                entity.Property(e => e.沉着).HasColumnType("int(11)");

                entity.Property(e => e.潜力).HasColumnType("int(11)");

                entity.Property(e => e.点球).HasColumnType("int(11)");

                entity.Property(e => e.盘带).HasColumnType("int(11)");

                entity.Property(e => e.盘带t)
                    .HasColumnName("盘带T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.盯人).HasColumnType("int(11)");

                entity.Property(e => e.短传).HasColumnType("int(11)");

                entity.Property(e => e.移动反应).HasColumnType("int(11)");

                entity.Property(e => e.站位).HasColumnType("int(11)");

                entity.Property(e => e.综合).HasColumnType("int(11)");

                entity.Property(e => e.花式技巧).HasColumnType("int(11)");

                entity.Property(e => e.视野).HasColumnType("int(11)");

                entity.Property(e => e.跑位).HasColumnType("int(11)");

                entity.Property(e => e.远射).HasColumnType("int(11)");

                entity.Property(e => e.逆足能力).HasColumnType("int(11)");

                entity.Property(e => e.速度).HasColumnType("int(11)");

                entity.Property(e => e.速度t)
                    .HasColumnName("速度T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.铲球).HasColumnType("int(11)");

                entity.Property(e => e.长传).HasColumnType("int(11)");

                entity.Property(e => e.防守t)
                    .HasColumnName("防守T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.鱼跃).HasColumnType("int(11)");
            });

            modelBuilder.Entity<DicplayersM>(entity =>
            {
                entity.ToTable("dicplayers_m");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.任意球).HasColumnType("int(11)");

                entity.Property(e => e.传中).HasColumnType("int(11)");

                entity.Property(e => e.传球t)
                    .HasColumnName("传球T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.位置).HasColumnType("int(11)");

                entity.Property(e => e.体能).HasColumnType("int(11)");

                entity.Property(e => e.侵略性).HasColumnType("int(11)");

                entity.Property(e => e.凌空).HasColumnType("int(11)");

                entity.Property(e => e.力量t)
                    .HasColumnName("力量T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.加速).HasColumnType("int(11)");

                entity.Property(e => e.名字).HasColumnType("varchar(50)");

                entity.Property(e => e.国籍).HasColumnType("varchar(50)");

                entity.Property(e => e.国际声誉).HasColumnType("int(11)");

                entity.Property(e => e.头球).HasColumnType("int(11)");

                entity.Property(e => e.守门反应).HasColumnType("int(11)");

                entity.Property(e => e.射术).HasColumnType("int(11)");

                entity.Property(e => e.射门t)
                    .HasColumnName("射门T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.射门力量).HasColumnType("int(11)");

                entity.Property(e => e.平衡).HasColumnType("int(11)");

                entity.Property(e => e.开球).HasColumnType("int(11)");

                entity.Property(e => e.弧线).HasColumnType("int(11)");

                entity.Property(e => e.弹跳).HasColumnType("int(11)");

                entity.Property(e => e.强壮).HasColumnType("int(11)");

                entity.Property(e => e.惯用脚).HasColumnType("varchar(50)");

                entity.Property(e => e.手形).HasColumnType("int(11)");

                entity.Property(e => e.抢断).HasColumnType("int(11)");

                entity.Property(e => e.拦截意识).HasColumnType("int(11)");

                entity.Property(e => e.控球).HasColumnType("int(11)");

                entity.Property(e => e.敏捷).HasColumnType("int(11)");

                entity.Property(e => e.沉着).HasColumnType("int(11)");

                entity.Property(e => e.潜力).HasColumnType("int(11)");

                entity.Property(e => e.点球).HasColumnType("int(11)");

                entity.Property(e => e.盘带).HasColumnType("int(11)");

                entity.Property(e => e.盘带t)
                    .HasColumnName("盘带T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.盯人).HasColumnType("int(11)");

                entity.Property(e => e.短传).HasColumnType("int(11)");

                entity.Property(e => e.移动反应).HasColumnType("int(11)");

                entity.Property(e => e.站位).HasColumnType("int(11)");

                entity.Property(e => e.综合).HasColumnType("int(11)");

                entity.Property(e => e.花式技巧).HasColumnType("int(11)");

                entity.Property(e => e.视野).HasColumnType("int(11)");

                entity.Property(e => e.跑位).HasColumnType("int(11)");

                entity.Property(e => e.远射).HasColumnType("int(11)");

                entity.Property(e => e.逆足能力).HasColumnType("int(11)");

                entity.Property(e => e.速度).HasColumnType("int(11)");

                entity.Property(e => e.速度t)
                    .HasColumnName("速度T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.铲球).HasColumnType("int(11)");

                entity.Property(e => e.长传).HasColumnType("int(11)");

                entity.Property(e => e.防守t)
                    .HasColumnName("防守T")
                    .HasColumnType("int(11)");

                entity.Property(e => e.鱼跃).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Dicplayersdrelation>(entity =>
            {
                entity.ToTable("dicplayersdrelation");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.PlayerId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Dicplayersfrelation>(entity =>
            {
                entity.ToTable("dicplayersfrelation");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.PlayerId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Dicplayersgkrelation>(entity =>
            {
                entity.ToTable("dicplayersgkrelation");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.PlayerId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Dicplayersmrelation>(entity =>
            {
                entity.ToTable("dicplayersmrelation");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.PlayerId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Dictalent>(entity =>
            {
                entity.ToTable("dictalent");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Des)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Idx)
                    .HasName("PRIMARY");

                entity.ToTable("employee");

                entity.Property(e => e.Idx).HasColumnType("int(11)");

                entity.Property(e => e.Age).HasColumnType("tinyint(4)");

                entity.Property(e => e.Lv).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Rowtime).HasColumnType("datetime");

                entity.Property(e => e.Status).HasColumnType("tinyint(4)");

                entity.Property(e => e.Type).HasColumnType("tinyint(4)");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("log");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("varchar(500)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Rowtime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'current_timestamp()'");
            });

            modelBuilder.Entity<Options>(entity =>
            {
                entity.ToTable("options");

                entity.HasIndex(e => e.Sid)
                    .HasName("Sid");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Num).HasColumnType("int(10)");

                entity.Property(e => e.Ops)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Pro).HasColumnType("tinyint(3)");

                entity.Property(e => e.Sid).HasColumnType("int(11)");
            });

            modelBuilder.Entity<Randomstory>(entity =>
            {
                entity.ToTable("randomstory");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Line)
                    .IsRequired()
                    .HasColumnName("line")
                    .HasColumnType("varchar(150)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Num1).HasColumnType("int(11)");

                entity.Property(e => e.Num2).HasColumnType("int(11)");

                entity.Property(e => e.Op1)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Op2)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Pro1)
                    .HasColumnName("pro1")
                    .HasColumnType("tinyint(4)");

                entity.Property(e => e.Pro2)
                    .HasColumnName("pro2")
                    .HasColumnType("tinyint(4)");
            });

            modelBuilder.Entity<Story>(entity =>
            {
                entity.HasKey(e => e.Idx)
                    .HasName("PRIMARY");

                entity.ToTable("story");

                entity.Property(e => e.Idx).HasColumnType("int(11)");

                entity.Property(e => e.Lines)
                    .IsRequired()
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.Nid).HasColumnType("int(11)");

                entity.Property(e => e.Scene)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Sub)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Who)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<Talent>(entity =>
            {
                entity.ToTable("talent");

                entity.HasIndex(e => e.UserPlayerId)
                    .HasName("UserPlayerId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Lv)
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Tid)
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UserPlayerId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Teamplayersrandom>(entity =>
            {
                entity.ToTable("teamplayersrandom");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.PlayerIdString)
                    .HasColumnName("playerIdString")
                    .HasColumnType("varchar(80)");

                entity.Property(e => e.PzString)
                    .HasColumnName("pzString")
                    .HasColumnType("varchar(30)");
            });

            modelBuilder.Entity<Usermoneychangelog>(entity =>
            {
                entity.ToTable("usermoneychangelog");

                entity.HasIndex(e => e.UserId)
                    .HasName("UserId");

                entity.Property(e => e.Id).HasColumnType("bigint(20)");

                entity.Property(e => e.CashBefore)
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CashChangeNum)
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.DiamondBefore)
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.DiamondChangeNum)
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Rowtime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Userplayers>(entity =>
            {
                entity.ToTable("userplayers");

                entity.HasIndex(e => e.Pid)
                    .HasName("FK_userplayers_dicplayers");

                entity.HasIndex(e => e.Uid)
                    .HasName("UId");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Age).HasColumnType("tinyint(4)");

                entity.Property(e => e.Jx).HasColumnType("tinyint(4)");

                entity.Property(e => e.Lv).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Pid).HasColumnType("int(11)");

                entity.Property(e => e.Pz).HasColumnType("int(11)");

                entity.Property(e => e.Rowtime).HasColumnType("datetime");

                entity.Property(e => e.Status).HasColumnType("tinyint(4)");

                entity.Property(e => e.Type).HasColumnType("tinyint(4)");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasColumnName("UId")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'0'");

                entity.HasOne(d => d.P)
                    .WithMany(p => p.Userplayers)
                    .HasForeignKey(d => d.Pid)
                    .HasConstraintName("FK_userplayers_dicplayers");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.OpenId)
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.Property(e => e.OpenId).HasColumnType("varchar(50)");

                entity.Property(e => e.Cash)
                    .HasColumnName("cash")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Diamond).HasColumnType("int(11)");

                entity.Property(e => e.Fans).HasColumnType("bigint(20)");

                entity.Property(e => e.Nick)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.RowTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Status).HasColumnType("tinyint(4)");

                entity.Property(e => e.Sw).HasColumnType("int(11)");

                entity.Property(e => e.TimeSpan)
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'current_timestamp()'")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Train)
                    .HasColumnName("train")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UnionId).HasColumnType("varchar(50)");
            });
        }
    }
}
