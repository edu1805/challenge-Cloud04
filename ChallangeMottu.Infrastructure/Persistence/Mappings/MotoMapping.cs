using ChallangeMottu.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChallangeMottu.Infrastructure.Persistence.Mappings;

public class MotoMapping : IEntityTypeConfiguration<Moto>
{
    public void Configure(EntityTypeBuilder<Moto> builder)
    {
        builder.ToTable("T_MOTOS_MOTTU"); // nome da tabela no Oracle

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id).HasColumnName("ID").HasColumnType("uniqueidentifier").
            IsRequired().HasDefaultValueSql("NEWID()");
        builder.Property(m => m.Placa).HasColumnName("PLACA").HasMaxLength(10).IsRequired().HasColumnType("nvarchar(10)");
        builder.Property(m => m.Status).HasColumnName("STATUS").HasMaxLength(50).HasColumnType("nvarchar(10)");
        builder.Property(m => m.UltimaAtualizacao).HasColumnName("ULTIMA_ATUALIZACAO").HasColumnType("datetime2");
    }   
}