using ApiProdutosPessoas.Models;
using ApiProdutosPessoas.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProdutosPessoas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PessoaController : ControllerBase
    {
        private readonly InterfacePessoa _usuarioRepositorio;

        public PessoaController(InterfacePessoa usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<PessoaModel>>> BuscarTodos()
        {
            List<PessoaModel> usuarios = await _usuarioRepositorio.BuscarTodos();
            return Ok(usuarios);
        }

        [HttpGet("nome/{nome}")]
        public async Task<ActionResult<List<PessoaModel>>> BuscarPessoasPorNome(string nome)
        {
            List<PessoaModel> pessoas = await _usuarioRepositorio.BuscarPessoasPorNome(nome);
            return Ok(pessoas);
        }

        [HttpGet("cidade/{cidadeId}")]
        public async Task<ActionResult<List<PessoaModel>>> BuscarPessoasPorCidade(int cidadeId)
        {
            List<PessoaModel> pessoas = await _usuarioRepositorio.BuscarPessoasPorCidade(cidadeId);
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaModel>> BuscarID(int id)
        {
            PessoaModel usuario = await _usuarioRepositorio.BuscarID(id);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<PessoaModel>> Adicionar([FromBody] PessoaModel usuarioModel)
        {
            PessoaModel usuario = await _usuarioRepositorio.Adicionar(usuarioModel);
            return Ok(usuario);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PessoaModel>> Atualizar([FromBody] PessoaModel usuarioModel, int id)
        {
            usuarioModel.Codigo = id;
            PessoaModel usuario = await _usuarioRepositorio.Atualizar(usuarioModel, id);
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PessoaModel>> Deletar(int id)
        {
            bool deletar = await _usuarioRepositorio.Deletar(id);
            return Ok(deletar);
        }
    }
}
