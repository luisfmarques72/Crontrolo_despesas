using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ControloDespesas.Models
{
    public class TipoDespesas
    {
        public int TipoDespesaID { get; set; }
        
        [Required(ErrorMessage ="Campo Obrigatório")]
        [StringLength(50,ErrorMessage ="Use menos Caracteres")]
        [Remote("TipoDespesaExiste","TipoDespesas")]
        public string Nome { get; set; }

        public ICollection<Despesas> Despesas { get; set; }

    }
}
