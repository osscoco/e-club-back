using DomainModels.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureEFcore.Persistence.Configurations
{
    public class ClubConfiguration : IEntityTypeConfiguration<Club>
    {
        public void Configure(EntityTypeBuilder<Club> builder)
        {
            builder.ToTable("Clubs");

            builder.HasKey(u => u.ClubId);

            builder.Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.CA)
                .HasPrecision(14, 2)  // 100 000 000 000,00 Max (100 Milliards)
                .IsRequired();

            // Mapping
            builder.HasMany(c => c.Courts)
               .WithOne(c => c.Club)
               .HasForeignKey(c => c.ClubId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Users)
               .WithOne(c => c.Club)
               .HasForeignKey(c => c.ClubId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
