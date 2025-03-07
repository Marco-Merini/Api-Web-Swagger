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
    public class MarcaRepositorio : InterfaceMarca
    {
        private readonly ProdutosPessoasdbContext _dbContext;

        public MarcaRepositorio(ProdutosPessoasdbContext produtosPessoasDBContext)
        {
            _dbContext = produtosPessoasDBContext;
        }

        public async Task<MarcaModel> AdicionarMarca(MarcaModel marca)
        {
            var marcaExistente = await _dbContext.Marcas
                                                 .FirstOrDefaultAsync(m => m.Codigo == marca.Codigo);

            if (marcaExistente != null)
            {
                throw new Exception($"Marca com código {marca.Codigo} já existe.");
            }

            await _dbContext.Marcas.AddAsync(marca);
            await _dbContext.SaveChangesAsync();

            return marca;
        }

        public async Task<MarcaModel> BuscarIDMarca(int id)
        {
            return await _dbContext.Marcas.FirstOrDefaultAsync(m => m.Codigo == id);
        }

        public async Task<MarcaModel> AtualizarMarca(MarcaModel marca, int id)
        {
            var marcaExistente = await _dbContext.Marcas.FirstOrDefaultAsync(m => m.Codigo == id);

            if (marcaExistente == null)
            {
                throw new Exception($"Marca com ID {id} não encontrada.");
            }

            marcaExistente.Descricao = marca.Descricao;

            _dbContext.Marcas.Update(marcaExistente);
            await _dbContext.SaveChangesAsync();

            return marcaExistente;
        }

        public async Task<bool> DeletarMarca(int id)
        {
            var produtos = _dbContext.Produtos.Where(p => p.Codigo == id);
            _dbContext.Produtos.RemoveRange(produtos);

            var marca = await _dbContext.Marcas.FirstOrDefaultAsync(x => x.Codigo == id);

            if (marca == null)
            {
                throw new Exception($"Marca com ID {id} não encontrada.");
            }

            _dbContext.Marcas.Remove(marca);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
