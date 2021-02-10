using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControloDespesas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ControloDespesas.Mapeamento
{
    public class TipoDesepsasMap:IEntityTypeConfiguration<TipoDespesas>
    {
        public void Configure(EntityTypeBuilder<TipoDespesas>builder)
        {
            builder.HasKey(td => td.TipoDespesaID);
            builder.Property(td => td.Nome).HasMaxLength(50).IsRequired();
            builder.HasMany(td => td.Despesas).WithOne(td => td.TipoDespesas).HasForeignKey(td => td.TipoDespesaId);
            builder.ToTable("TipoDespesas");
        }

    }
}
