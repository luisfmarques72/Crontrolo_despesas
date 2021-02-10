using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControloDespesas.Models
{
    public class Salarios
    {
        public int SalarioId { get; set; }

        public int Mesid { get; set; }
        public Meses Meses { get; set; }
        [Required(ErrorMessage ="Campo Obrigatório")]
        [Range (0,double.MaxValue,ErrorMessage ="Valor Inválido" )]
        public double valor { get; set; }
    }
}
