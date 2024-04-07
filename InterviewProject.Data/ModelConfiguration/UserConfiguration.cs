using InterviewProject.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewProject.Data.ModelConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Id).HasMaxLength(255).ValueGeneratedOnAdd();
            builder.Property(p => p.FirstName).HasMaxLength(35);
            builder.Property(p => p.LastName).HasMaxLength(35);
            builder.Property(p => p.PhoneNumber).HasMaxLength(20);
            builder.Property(x => x.SecurityStamp).HasMaxLength(256);
            builder.Property(x => x.PasswordHash).HasMaxLength(256);
            builder.Property(x => x.ConcurrencyStamp).HasMaxLength(256);
        }
    }
}

