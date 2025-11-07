using DomainModels.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureEFcore.Persistence.Configurations
{
    public class TrainingConfiguration : IEntityTypeConfiguration<Training>
    {
        public void Configure(EntityTypeBuilder<Training> builder)
        {
            builder.ToTable("Trainings");

            builder.HasKey(u => u.TrainingId);

            // Mapping
            builder.HasOne(c => c.Court)
               .WithMany(c => c.Trainings)
               .HasForeignKey(c => c.CourtId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Users)
               .WithMany(c => c.Trainings);
        }
    }
}
