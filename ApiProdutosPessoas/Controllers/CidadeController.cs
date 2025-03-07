using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApiProdutosPessoas.Repositories.Interfaces;
using ApiProdutosPessoas.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApiProdutosPessoas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CidadeController : ControllerBase
    {
        private readonly InterfaceCidade _cidadeRepositorio;

        public CidadeController(InterfaceCidade cidadeRepositorio)
        {
            _cidadeRepositorio = cidadeRepositorio;
        }

        [HttpPost]
        public async Task<ActionResult<CidadeModel>> AdicionarCidade([FromBody] CidadeModel cidadeModel)
        {
            CidadeModel cidade = await _cidadeRepositorio.AdicionarCidade(cidadeModel);
            return Ok(cidade);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CidadeModel>> AtualizarCidade([FromBody] CidadeModel cidadeModel, int id)
        {
            cidadeModel.codigoIBGE = id;
            CidadeModel cidade = await _cidadeRepositorio.AtualizarCidade(cidadeModel, id);
            return Ok(cidade);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeletarCidade(int id)
        {
            bool deletar = await _cidadeRepositorio.DeletarCidade(id);
            return Ok(deletar);
        }
    }
}