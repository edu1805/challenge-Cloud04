using ChallangeMottu.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChallangeMottu.Infrastructure.Persistence.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("T_USUARIOS_MOTTU");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnName("ID")
            .HasColumnType("uniqueidentifier")
            .IsRequired()
            .HasDefaultValueSql("NEWID()");

        builder.Property(u => u.Nome)
            .HasColumnName("NOME")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasColumnName("EMAIL")
            .HasMaxLength(150)
            .IsRequired();

// 🆕 Campos de senha
        builder.Property<String>("SenhaHash")
            .HasColumnName("SENHA_HASH")
            .HasColumnType("nvarchar(max)") 
            .IsRequired();

        builder.Property<String>("SenhaSalt")
            .HasColumnName("SENHA_SALT")
            .HasColumnType("nvarchar(max)")
            .IsRequired();

// Relacionamento opcional com Moto
        builder.HasOne(u => u.Moto)
            .WithMany()
            .HasForeignKey(u => u.MotoId)
            .IsRequired(false);

    }
}