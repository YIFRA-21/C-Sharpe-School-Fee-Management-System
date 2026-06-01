using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ScoolFeeManagementSystem.Data.Entities;

namespace ScoolFeeManagementSystem.Data.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
    
        public DbSet<Student> Students { get; set; }
        public DbSet<FeeCategory> FeeCategories { get; set; }
        public DbSet<FeeStructure> FeeStructures { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Student)
                .WithMany(s => s.Payments)
                .HasForeignKey(p => p.StudentId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.FeeStructure)
                .WithMany(f => f.Payments)
                .HasForeignKey(p => p.FeeStructureId);
        }
    }
}
