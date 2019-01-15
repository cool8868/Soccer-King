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

        public virtual DbSet<Story> Story { get; set; }
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
            modelBuilder.Entity<Story>(entity =>
            {
                entity.HasKey(e => e.Idx)
                    .HasName("PRIMARY");

                entity.ToTable("story");

                entity.Property(e => e.Idx).HasColumnType("int(11)");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("varchar(300)");

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

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.OpenId)
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.Property(e => e.OpenId).HasColumnType("varchar(50)");

                entity.Property(e => e.Cash)
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Fans)
                    .HasColumnType("bigint(20)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Nick)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.RowTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'current_timestamp()'");

                entity.Property(e => e.Status)
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UnionId).HasColumnType("varchar(50)");
            });
        }
    }
}
