using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StockDBUpdate.EF
{
    public partial class stockdbContext : DbContext
    {
        public stockdbContext()
        {
        }

        public stockdbContext(DbContextOptions<stockdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Instrument> Instruments { get; set; } = null!;
        public virtual DbSet<InstrumentHistory> InstrumentHistories { get; set; } = null!;
        public virtual DbSet<Type1Stock> Type1Stocks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new ArgumentException(nameof(optionsBuilder));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instrument>(entity =>
            {
                entity.ToTable("Instrument");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Eps)
                    .HasMaxLength(50)
                    .HasColumnName("eps");

                entity.Property(e => e.GroupName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<InstrumentHistory>(entity =>
            {
                //entity.HasNoKey();

                entity.ToTable("InstrumentHistory");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.LastPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Symbol).HasMaxLength(50);

                entity.Property(e => e.Tmst)
                    .HasColumnType("datetime")
                    .HasColumnName("tmst")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Type1Stock>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Type1Stock");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.LastPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SupportPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Symbol).HasMaxLength(50);

                entity.Property(e => e.TargetPrice1).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TargetPrice2).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.TargetPrice3).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Tmst)
                    .HasColumnType("datetime")
                    .HasColumnName("tmst")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
