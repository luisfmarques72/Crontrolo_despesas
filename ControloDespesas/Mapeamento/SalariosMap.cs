using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControloDespesas.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControloDespesas.Mapeamento
{
    public class SalariosMap : IEntityTypeConfiguration<Salarios>
    {
        public void Configure(EntityTypeBuilder<Salarios> builder)
        {
            builder.HasKey(s => s.SalarioId);
            builder.Property(s => s.SalarioId).IsRequired();
            builder.HasOne(s => s.Meses).WithOne(s => s.Salarios).HasForeignKey<Salarios>(s => s.Mesid);
            builder.ToTable("Salarios");
        }
    }
}

