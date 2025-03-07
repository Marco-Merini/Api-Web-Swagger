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
    public class PessoaRepositorio : InterfacePessoa
    {
        private readonly ProdutosPessoasdbContext _dbContext;

        public PessoaRepositorio(ProdutosPessoasdbContext produtosPessoasDBContext)
        {
            _dbContext = produtosPessoasDBContext;
        }

        //SearchAllUsers
        public async Task<List<PessoaModel>> BuscarTodos()
        {
            return await _dbContext.Pessoas.ToListAsync();
        }

        //SearchId
        public async Task<PessoaModel> BuscarID(int codigo)
        {
            return await _dbContext.Pessoas.FirstOrDefaultAsync(x => x.Codigo == codigo);
        }

        //Add
        public async Task<PessoaModel> Adicionar(PessoaModel usuario)
        {
            await _dbContext.Pessoas.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }

        //Update
        public async Task<PessoaModel> Atualizar(PessoaModel usuario, int id)
        {
            PessoaModel usuarioId = await BuscarID(id);

            if (usuarioId == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }
            usuarioId.Nome = usuario.Nome;
            usuarioId.Idade = usuario.Idade;
            usuarioId.CPF = usuario.CPF;
            usuarioId.Cidade = usuario.Cidade;
            usuarioId.Logradouro = usuario.Logradouro;
            usuarioId.NumeroEstabelecimento = usuario.NumeroEstabelecimento;
            usuarioId.Bairro = usuario.Bairro;
            usuarioId.CEP = usuario.CEP;

            _dbContext.Pessoas.Update(usuarioId);
            await _dbContext.SaveChangesAsync();

            return usuarioId;
        }

        //Delete
        public async Task<bool> Deletar(int id)
        {
            PessoaModel usuarioId = await _dbContext.Pessoas
                .Include(p => p.Dependentes) // Inclui os dependentes para remover
                .FirstOrDefaultAsync(p => p.Codigo == id);

            if (usuarioId == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            // Remove os dependentes antes da pessoa
            _dbContext.Dependentes.RemoveRange(usuarioId.Dependentes);

            // Agora pode remover a pessoa
            _dbContext.Pessoas.Remove(usuarioId);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<PessoaModel>> BuscarPessoasPorNome(string nome)
        {
            return await _dbContext.Pessoas
                .Where(p => p.Nome.Contains(nome))
                .ToListAsync();
        }

        public async Task<List<PessoaModel>> BuscarPessoasPorCidade(int cidadeId)
        {
            return await _dbContext.Pessoas
                .Where(p => p.CodigoCidade == cidadeId)
                .ToListAsync();
        }
    }
}
