using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControloDespesas.Models
{
    public class Despesas
    {
        public int DespesaId { get; set; }
        public int MesId { get; set; }
        public Meses Meses { get; set; }
        public int TipoDespesaId { get; set; }
        public TipoDespesas TipoDespesas { get; set; }
        [Required(ErrorMessage = "Campo Obrigatório")]
        [Range(0, double.MaxValue, ErrorMessage = "Valor Inválido")]
        public double Valor { get; set; }

    }
}
