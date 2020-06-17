using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bitsa.Identity.Api.Model.Classes
{
    public partial class DomainContext : DbContext
    {
        public DomainContext()
        {
        }

        public DomainContext(DbContextOptions<DomainContext> options)
            : base(options)
        {
        }

        public virtual DbSet<users> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;database=bitsa;user=bitnovo;password=#J1n3md#");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<users>(entity =>
            {
                entity.ToTable("users", "bitsa");

                entity.HasIndex(e => e.Email)
                    .HasName("IX_Users_Email")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Administrator)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Alias)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Balance).HasDefaultValueSql("0");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Enabled)
                    .HasColumnType("smallint(6)")
                    .HasDefaultValueSql("1");

                entity.Property(e => e.Entry_Date).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.First_Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Last_Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
