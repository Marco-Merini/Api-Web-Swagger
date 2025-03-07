using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProdutosPessoas.Models
{
    public class ProdutoModel
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public MarcaModel Marca { get; set; }
        public int Estoque { get; set; }
    }
}
