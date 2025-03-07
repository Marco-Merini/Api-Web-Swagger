using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProdutosPessoas.Models
{
    public class MarcaModel
    {
        [Key]
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
