using DomainModels.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureEFcore.Persistence.Configurations
{
    public class CourtConfiguration : IEntityTypeConfiguration<Court>
    {
        public void Configure(EntityTypeBuilder<Court> builder)
        {
            builder.ToTable("Courts");

            builder.HasKey(u => u.CourtId);

            builder.Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();

            // Mapping
            builder.HasOne(c => c.Club)
               .WithMany(c => c.Courts)
               .HasForeignKey(c => c.ClubId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Reservations)
               .WithOne(c => c.Court)
               .HasForeignKey(c => c.ReservationId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Trainings)
               .WithOne(c => c.Court)
               .HasForeignKey(c => c.TrainingId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
