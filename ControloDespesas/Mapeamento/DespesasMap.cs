using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControloDespesas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControloDespesas.Mapeamento
{
    public class DespesasMap: IEntityTypeConfiguration<Despesas>
    {
        public void Configure(EntityTypeBuilder<Despesas> builder)
        {
            builder.HasKey(d => d.DespesaId);
            builder.Property(d => d.DespesaId).IsRequired();
            builder.HasOne(d => d.Meses).WithMany(d =>d.Despesas ).HasForeignKey(d => d.MesId);
            builder.HasOne(d => d.TipoDespesas).WithMany(d => d.Despesas).HasForeignKey(d => d.TipoDespesaId);
            builder.ToTable("Despesas");
        }
    }
}
