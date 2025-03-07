using ApiProdutosPessoas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProdutosPessoas.Repositories.Interfaces
{
    public interface InterfaceCidade
    {
        Task<CidadeModel> AdicionarCidade(CidadeModel cidade);
        Task<CidadeModel> AtualizarCidade(CidadeModel cidade, int id);
        Task<bool> DeletarCidade(int id);
    }
}
