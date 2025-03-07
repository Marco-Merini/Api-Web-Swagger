using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProdutosPessoas.Models
{
    public class DependentesModel
    {
        [Key]
        public int Id { get; set; }
        public int IdDependente { get; set; }

        [ForeignKey("PessoaModel")]
        public int PessoaModelCodigo { get; set; }
        public PessoaModel Pessoa { get; set; }
    }
}
