using Bitsa.Base.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitsa.Base
{
    class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        { }
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseMySQL("server=localhost;port=3306;database=bitsa;user=bitnovo;password=#J1n3md#");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region User properties

            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<User>()
                .Property(u => u.Password)
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity<User>()
                .Property(u => u.Entry_Date)
                .IsRequired()
                .HasDefaultValueSql("Now()");

            builder.Entity<User>()
                .Property(u => u.Enabled)
                .IsRequired()
                .HasDefaultValue(1);

            builder.Entity<User>()
                .Property(u => u.Administrator)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Entity<User>()
                .Property(u => u.Balance)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Entity<User>()
                .Property(u => u.First_Name)
                .HasMaxLength(50);

            builder.Entity<User>()
                .Property(u => u.Last_Name)
                .HasMaxLength(50);

            builder.Entity<User>()
                .Property(u => u.Alias)
                .HasMaxLength(30)
                .IsRequired();

            #endregion
        }
        public DbSet<User> Users { get; set; }
    }
}
