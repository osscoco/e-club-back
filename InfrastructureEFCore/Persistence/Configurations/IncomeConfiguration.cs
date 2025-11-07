using DomainModels.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureEFcore.Persistence.Configurations
{
    public class IncomeConfiguration : IEntityTypeConfiguration<Income>
    {
        public void Configure(EntityTypeBuilder<Income> builder)
        {
            builder.ToTable("Incomes");

            builder.HasKey(u => u.IncomeId);

            builder.Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Description)
                .HasMaxLength(50);

            builder.Property(u => u.Amount)
                .HasPrecision(14, 2) // 100 000 000 000,00 Max (100 Milliards)
                .IsRequired();

            builder.Property(u => u.Payer)
                .HasMaxLength(50)
                .IsRequired();

            // Mapping
            builder.HasOne(c => c.UserCreator)
               .WithMany(c => c.Incomes)
               .HasForeignKey(c => c.UserCreatorId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
