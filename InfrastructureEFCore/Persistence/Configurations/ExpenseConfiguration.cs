using DomainModels.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureEFcore.Persistence.Configurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Expenses");

            builder.HasKey(u => u.ExpenseId);

            builder.Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Description)
                .HasMaxLength(50);

            builder.Property(u => u.Amount)
                .HasPrecision(14, 2) // 100 000 000 000,00 Max (100 Milliards)
                .IsRequired();

            builder.Property(u => u.Receiver)
                .HasMaxLength(50)
                .IsRequired();

            // Mapping
            builder.HasOne(c => c.UserCreator)
               .WithMany(c => c.Expenses)
               .HasForeignKey(c => c.UserCreatorId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
