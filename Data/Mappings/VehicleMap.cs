using estacionamento.Models;
using estacionamento.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace estacionamento.Data.Mappings
{
    public class VehicleMap : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicle");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(x => x.Brand).HasColumnName("Brand").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Color).HasColumnName("Color").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Plate).HasColumnName("Plate").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Type).HasColumnName("Type").HasMaxLength(2).IsRequired();

            builder.HasMany(x => x.InOutEstablishments).WithOne(x => x.Vehicle).HasForeignKey(x => x.VehicleId);


        }



    }
}


