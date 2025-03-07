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
    public class DependenteRepositorio : InterfaceDependente
    {
        private readonly ProdutosPessoasdbContext _dbContext;

        public DependenteRepositorio(ProdutosPessoasdbContext produtosPessoasDBContext)
        {
            _dbContext = produtosPessoasDBContext;
        }

        public async Task<DependentesModel> VincularDependente(DependentesModel dependente)
        {
            var pessoaPrincipal = await _dbContext.Pessoas.FindAsync(dependente.PessoaModelCodigo);
            if (pessoaPrincipal == null)
            {
                throw new Exception("Pessoa principal não encontrada.");
            }

            await _dbContext.Dependentes.AddAsync(dependente);
            await _dbContext.SaveChangesAsync();

            return dependente;
        }

        public async Task<bool> DeletarDependente(int id)
        {
            var dependente = await _dbContext.Dependentes.FirstOrDefaultAsync(x => x.Id == id);

            if (dependente == null)
            {
                throw new Exception($"Dependente com ID {id} não encontrada.");
            }

            _dbContext.Dependentes.Remove(dependente);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
