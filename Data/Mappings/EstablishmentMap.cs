using estacionamento.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace estacionamento.Data.Mappings
{
    public class EstablishmentMap : IEntityTypeConfiguration<Establishment>
    {
        public void Configure(EntityTypeBuilder<Establishment> builder)
        {
            builder.ToTable("Establishment");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Address).HasColumnName("Address").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Phone).HasColumnName("Phone").HasMaxLength(100).IsRequired();
            builder.Property(x => x.MotorcycleSpotsAvailable).HasColumnName("MotorcycleSpotsAvailable").HasMaxLength(100).IsRequired();
            builder.Property(x => x.CarSpotsAvailable).HasColumnName("CarSpotsAvailable").HasMaxLength(100).IsRequired();


            builder.HasMany(x => x.Vehicles).WithOne(x=> x.Establishment).HasForeignKey(x => x.EstablishmentId);
            builder.HasMany(x=> x.InOutEstablishments).WithOne(x=> x.Establishment).HasForeignKey(x=> x.EstablishmentId);

        }
    }
}
