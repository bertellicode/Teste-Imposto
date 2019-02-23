﻿
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Imposto.Domain.Entities;

namespace Imposto.Infra.Data.EntityConfig
{
    public class NotaFiscalItemConfiguration : EntityTypeConfiguration<NotaFiscalItem>
    {

        public NotaFiscalItemConfiguration()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Cfop)
                .HasMaxLength(5);

            this.Property(t => t.TipoIcms)
                .HasMaxLength(20);

            this.Property(t => t.BaseIcms)
                .HasPrecision(18, 5);

            this.Property(t => t.AliquotaIcms)
                .HasPrecision(18, 5);

            this.Property(t => t.ValorIcms)
                .HasPrecision(18, 5);

            this.Property(t => t.NomeProduto)
                .HasMaxLength(50);

            this.Property(t => t.CodigoProduto)
                .HasMaxLength(20);

            this.Property(t => t.BaseCalculoIpi)
                .HasPrecision(18, 5);

            this.Property(t => t.AliquotaIpi)
                .HasPrecision(18, 5);

            this.Property(t => t.ValorIpi)
                .HasPrecision(18, 5);

            this.Property(t => t.Desconto)
                .HasPrecision(18, 5);


            // Table & Column Mappings
            this.ToTable("NotaFiscalItem");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdNotaFiscal).HasColumnName("IdNotaFiscal");
            this.Property(t => t.Cfop).HasColumnName("Cfop");
            this.Property(t => t.TipoIcms).HasColumnName("TipoIcms");
            this.Property(t => t.BaseIcms).HasColumnName("BaseIcms");
            this.Property(t => t.AliquotaIcms).HasColumnName("AliquotaIcms");
            this.Property(t => t.ValorIcms).HasColumnName("ValorIcms");
            this.Property(t => t.NomeProduto).HasColumnName("NomeProduto");
            this.Property(t => t.CodigoProduto).HasColumnName("CodigoProduto");
            this.Property(t => t.BaseCalculoIpi).HasColumnName("BaseCalculoIpi");
            this.Property(t => t.AliquotaIpi).HasColumnName("AliquotaIpi");
            this.Property(t => t.ValorIpi).HasColumnName("ValorIpi");
            this.Property(t => t.Desconto).HasColumnName("Desconto");

            // Relationships
            this.HasRequired(t => t.NotaFiscal)
                .WithMany(t => t.ItensDaNotaFiscal)
                .HasForeignKey(d => d.IdNotaFiscal);

        }

    }
}