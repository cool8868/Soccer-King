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
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Options> Options { get; set; }
        public virtual DbSet<Randomstory> Randomstory { get; set; }
        public virtual DbSet<Story> Story { get; set; }
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

            modelBuilder.Entity<Userplayers>(entity =>
            {
                entity.ToTable("userplayers");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Age).HasColumnType("tinyint(4)");

                entity.Property(e => e.Defence)
                    .HasColumnName("defence")
                    .HasColumnType("int(4)");

                entity.Property(e => e.Gate)
                    .HasColumnName("gate")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Lv).HasColumnType("int(11)");

                entity.Property(e => e.MaxPower).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Pass)
                    .HasColumnName("pass")
                    .HasColumnType("int(4)");

                entity.Property(e => e.Position)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Rowtime).HasColumnType("datetime");

                entity.Property(e => e.Shoot)
                    .HasColumnName("shoot")
                    .HasColumnType("int(4)");

                entity.Property(e => e.Status).HasColumnType("tinyint(4)");

                entity.Property(e => e.Type).HasColumnType("tinyint(4)");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasColumnName("UId")
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'0'");
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

                entity.Property(e => e.Train)
                    .HasColumnName("train")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UnionId).HasColumnType("varchar(50)");
            });
        }
    }
}
