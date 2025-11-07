using DomainModels.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureEFCore.Persistence.Configurations
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.ToTable("UserTypes");

            builder.HasKey(u => u.UserTypeId);

            builder.Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired();

            // Mapping
            builder.HasMany(c => c.Users)
               .WithOne(c => c.UserType)
               .HasForeignKey(c => c.UserTypeId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}