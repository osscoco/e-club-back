using DomainModels.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureEFcore.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.UserId);

            builder.Property(u => u.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Age)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.PasswordHashed)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.Phone)
                .HasMaxLength(10);

            builder.Property(u => u.PasswordHashed)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(u => u.UserTypeId)
                .IsRequired();

            builder.Property(u => u.ClubId)
                .IsRequired();

            // Mapping
            builder.HasOne(u => u.UserType)
               .WithMany(c => c.Users);

            builder.HasOne(u => u.Club)
               .WithMany(c => c.Users);

            builder.HasMany(c => c.Trainings)
               .WithMany(c => c.Users);

            builder.HasMany(c => c.Incomes)
               .WithOne(c => c.UserCreator)
               .HasForeignKey(c => c.IncomeId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Expenses)
               .WithOne(c => c.UserCreator)
               .HasForeignKey(c => c.ExpenseId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.ReservationsFirstPlayer)
               .WithOne(c => c.FirstPlayer)
               .HasForeignKey(c => c.ReservationId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.ReservationsSecondPlayer)
               .WithOne(c => c.SecondPlayer)
               .HasForeignKey(c => c.ReservationId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
