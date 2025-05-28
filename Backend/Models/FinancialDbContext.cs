using System;
using System.Collections.Generic;
using Backend.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class FinancialDbContext : DbContext
{
    public DbSet<FinancialOperation> FinancialOperations { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    
    public FinancialDbContext() { }

    public FinancialDbContext(DbContextOptions<FinancialDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("server=localhost;port=3309;database=financial;user=juster;password=justersampler;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Categories)
            .WithOne(c => c.User)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.FinancialOperations)
            .WithOne(c => c.User)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Childrens)
            .WithOne(c => c.Partner)
            .HasForeignKey(u => u.IdPartner)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<User>()
            .Property(b => b.StartBalance)
            .HasDefaultValue(0);
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
