using ApiProdutosPessoas.Data;
using ApiProdutosPessoas.Models;
using ApiProdutosPessoas.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProdutosPessoas.Repositories
{
    public class ProdutoRepositorio : InterfaceProduto
    {
        private readonly ProdutosPessoasdbContext _dbContext;

        public ProdutoRepositorio(ProdutosPessoasdbContext produtosPessoasDBContext)
        {
            _dbContext = produtosPessoasDBContext;
        }

        public async Task<List<ProdutoModel>> BuscarTodosProdutos()
        {
            return await _dbContext.Produtos.ToListAsync();
        }

        public async Task<ProdutoModel> BuscarIDProduto(int id)
        {
            return await _dbContext.Produtos.FirstOrDefaultAsync(x => x.Codigo == id);
        }

        public async Task<ProdutoModel> AdicionarProduto(ProdutoModel produto)
        {
            await _dbContext.Produtos.AddAsync(produto);
            await _dbContext.SaveChangesAsync();
            return produto;
        }

        public async Task<ProdutoModel> AtualizarProduto(ProdutoModel produto, int id)
        {
            ProdutoModel produtoId = await BuscarIDProduto(id);

            if (produtoId == null)
            {
                throw new Exception($"Produto para o ID: {id} não foi encontrado no banco de dados.");
            }
            produtoId.Descricao = produto.Descricao;
            produtoId.Codigo = produto.Codigo;
            produtoId.Estoque = produto.Estoque;

            _dbContext.Produtos.Update(produtoId);
            await _dbContext.SaveChangesAsync();

            return produtoId;
        }

        public async Task<bool> DeletarProduto(int id)
        {
            ProdutoModel produtoId = await BuscarIDProduto(id);

            if (produtoId == null)
            {
                throw new Exception($"Produto para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Produtos.Remove(produtoId);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<ProdutoModel>> BuscarProdutosPorMarca(int marcaId)
        {
            return await _dbContext.Produtos
                .Where(p => p.Marca.Codigo == marcaId)
                .ToListAsync();
        }

        public async Task<List<ProdutoModel>> BuscarProdutosPorDescricao(string descricao)
        {
            return await _dbContext.Produtos
                .Where(p => p.Marca.Descricao.Contains(descricao))
                .ToListAsync();
        }
    }
}
