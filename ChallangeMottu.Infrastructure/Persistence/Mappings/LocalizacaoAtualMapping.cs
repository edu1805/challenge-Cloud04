using ChallangeMottu.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChallangeMottu.Infrastructure.Persistence.Mappings;

public class LocalizacaoAtualMapping : IEntityTypeConfiguration<LocalizacaoAtual>
{
    public void Configure(EntityTypeBuilder<LocalizacaoAtual> builder)
    {
        builder.ToTable("T_LOCALIZACAO_ATUAL_MOTTU");

        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id)
            .HasColumnName("ID")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasDefaultValueSql("NEWID()");

        builder.Property(l => l.CoordenadaX).IsRequired().HasColumnType("float");
        builder.Property(l => l.CoordenadaY).IsRequired().HasColumnType("float");
        builder.Property(l => l.DataHoraAtualizacao).IsRequired().HasColumnType("datetime2");

        builder.HasOne(l => l.Moto)
            .WithMany()
            .HasForeignKey(l => l.MotoId);
    }
}