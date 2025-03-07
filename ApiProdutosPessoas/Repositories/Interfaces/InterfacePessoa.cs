using ApiProdutosPessoas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProdutosPessoas.Repositories.Interfaces
{
    public interface InterfacePessoa
    {
        Task<List<PessoaModel>> BuscarTodos();
        Task<PessoaModel> BuscarID(int id);
        Task<PessoaModel> Adicionar(PessoaModel usuario);
        Task<PessoaModel> Atualizar(PessoaModel usuario, int id);
        Task<bool> Deletar(int id);
        Task<List<PessoaModel>> BuscarPessoasPorNome(string nome);
        Task<List<PessoaModel>> BuscarPessoasPorCidade(int cidadeId);
    }
}
