using Backend.Models.Db;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class FinancialDbContext : DbContext
{
    public DbSet<FinancialOperation> FinancialOperations { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Day> Days { get; set; }
    
    public FinancialDbContext() { }

    public FinancialDbContext(DbContextOptions<FinancialDbContext> options)
        : base(options)
    {
    }

    // Стыдно? Возможно. Хочу ли я его теперь поменять? Тоже возможно. Но уже поздно :)
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("server=localhost;port=3309;database=financial;user=juster;password=justersampler;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User
        modelBuilder.Entity<User>()
            .HasMany(u => u.Categories)
            .WithOne(c => c.User)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.FinancialOperations)
            .WithOne(c => c.User)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Days)
            .WithOne(c => c.User)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.Partners)
            .WithOne(c => c.Partner)
            .HasForeignKey(u => u.IdPartner)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<User>()
            .Property(b => b.StartBalance)
            .HasDefaultValue(0);

        // Categories
        modelBuilder.Entity<Category>()
            .HasMany(x => x.ChildCategories)
            .WithOne(x => x.ParentCategory)
            .HasForeignKey(x => x.IdParent)
            .OnDelete(DeleteBehavior.SetNull);
        
        // Financial operations

        modelBuilder.Entity<FinancialOperation>()
            .HasOne(x => x.Day)
            .WithMany(x => x.FinancialOperations);
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
