using ApiProdutosPessoas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProdutosPessoas.Repositories.Interfaces
{
    public interface InterfaceMarca
    {
        Task<MarcaModel> AtualizarMarca(MarcaModel marca, int id);
        Task<MarcaModel> BuscarIDMarca(int id);
        Task<MarcaModel> AdicionarMarca(MarcaModel marca);
        Task<bool> DeletarMarca(int id);
    }
}
