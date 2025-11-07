using DomainModels.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureEFcore.Persistence.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");

            builder.HasKey(u => u.ReservationId);

            // Mapping
            builder.HasOne(c => c.FirstPlayer)
               .WithMany(c => c.ReservationsFirstPlayer)
               .HasForeignKey(c => c.FirstPlayerId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.SecondPlayer)
               .WithMany(c => c.ReservationsSecondPlayer)
               .HasForeignKey(c => c.SecondPlayerId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Court)
               .WithMany(c => c.Reservations)
               .HasForeignKey(c => c.CourtId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
