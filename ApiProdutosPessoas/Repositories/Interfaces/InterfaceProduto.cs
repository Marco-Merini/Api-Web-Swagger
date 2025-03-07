using ApiProdutosPessoas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProdutosPessoas.Repositories.Interfaces
{
    public interface InterfaceProduto
    {
        Task<List<ProdutoModel>> BuscarTodosProdutos();
        Task<ProdutoModel> BuscarIDProduto(int id);
        Task<ProdutoModel> AdicionarProduto(ProdutoModel produto);
        Task<ProdutoModel> AtualizarProduto(ProdutoModel produto, int id);
        Task<bool> DeletarProduto(int id);
        Task<List<ProdutoModel>> BuscarProdutosPorMarca(int marcaId);
        Task<List<ProdutoModel>> BuscarProdutosPorDescricao(string descricao);
    }
}
