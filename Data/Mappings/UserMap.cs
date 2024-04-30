using estacionamento.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace estacionamento.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(x => x.UserName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.PasswordHash).HasMaxLength(250).IsRequired();
            builder.Property(x => x.TypeUserEnum).HasMaxLength(10).IsRequired();
        }
    }
}
