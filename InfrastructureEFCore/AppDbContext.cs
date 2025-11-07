using DomainModels.Entities;
using InfrastructureEFcore.Persistence.Configurations;
using InfrastructureEFCore.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureEFCore
{
    public class AppDbContext : DbContext
    {
        public DbSet<Club> Clubs { get; set; } = null!;
        public DbSet<Court> Courts { get; set; } = null!;
        public DbSet<Expense> Expenses { get; set; } = null!;
        public DbSet<Income> Incomes { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<Training> Trainings { get; set; } = null!;
        public DbSet<UserType> UserTypes { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClubConfiguration());
            modelBuilder.ApplyConfiguration(new CourtConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new IncomeConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new TrainingConfiguration());
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}