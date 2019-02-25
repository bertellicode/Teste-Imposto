using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Imposto.Domain.NotaFiscalAggregate.Entities;

namespace Imposto.Infra.Data.EntityConfig
{
    public class NotaFiscalConfiguration : EntityTypeConfiguration<NotaFiscal>
    {

        public NotaFiscalConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.NumeroNotaFiscal)
                .IsOptional();

            this.Property(t => t.Serie)
                .IsOptional();

            this.Property(t => t.NomeCliente)
                .HasMaxLength(50)
                .IsOptional();

            this.Property(t => t.EstadoDestino)
                .HasMaxLength(50)
                .IsOptional();

            this.Property(t => t.EstadoOrigem)
                .HasMaxLength(50)
                .IsOptional();

            this.Ignore(t => t.ValidationResult);

            this.Ignore(t => t.CascadeMode);

            // Table & Column Mappings
            this.ToTable("NotaFiscal");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.NumeroNotaFiscal).HasColumnName("NumeroNotaFiscal");
            this.Property(t => t.Serie).HasColumnName("Serie");
            this.Property(t => t.NomeCliente).HasColumnName("NomeCliente");
            this.Property(t => t.EstadoDestino).HasColumnName("EstadoDestino");
            this.Property(t => t.EstadoOrigem).HasColumnName("EstadoOrigem");
        }

    }
}
