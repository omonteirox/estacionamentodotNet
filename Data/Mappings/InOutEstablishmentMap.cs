using estacionamento.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace estacionamento.Data.Mappings
{
    public class InOutEstablishmentMap : IEntityTypeConfiguration<InOutEstablishment>
    {
        public void Configure(EntityTypeBuilder<InOutEstablishment> builder)
        {
            builder.ToTable("InOutEstablishment");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(x => x.DateStart).HasColumnName("DateStart").HasMaxLength(100).IsRequired();
            builder.Property(x => x.DateEnd).HasColumnName("DateEnd").HasMaxLength(100);

            
            
    }
    }
}
