using ApiProdutosPessoas.Models;
using ApiProdutosPessoas.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace ApiProdutosPessoas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProdutoController : ControllerBase
    {
        private readonly InterfaceProduto _produtoRepositorio;

        public ProdutoController(InterfaceProduto produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProdutoModel>>> BuscarTodosProdutos()
        {
            List<ProdutoModel> produto = await _produtoRepositorio.BuscarTodosProdutos();
            return Ok(produto);
        }

        [HttpGet("marca/{marcaId}")]
        public async Task<ActionResult<List<ProdutoModel>>> BuscarProdutosPorMarca(int marcaId)
        {
            List<ProdutoModel> produtos = await _produtoRepositorio.BuscarProdutosPorMarca(marcaId);
            return Ok(produtos);
        }

        [HttpGet("descricao/{descricao}")]
        public async Task<ActionResult<List<ProdutoModel>>> BuscarProdutosPorDescricao(string descricao)
        {
            List<ProdutoModel> produtos = await _produtoRepositorio.BuscarProdutosPorDescricao(descricao);
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoModel>> BuscarIDProduto(int id)
        {
            ProdutoModel produto = await _produtoRepositorio.BuscarIDProduto(id);
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoModel>> AdicionarProduto([FromBody] ProdutoModel produtoModel)
        {
            ProdutoModel produto = await _produtoRepositorio.AdicionarProduto(produtoModel);
            return Ok(produto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProdutoModel>> AtualizarProduto([FromBody] ProdutoModel produtoModel, int id)
        {
            produtoModel.Codigo = id;
            ProdutoModel produto = await _produtoRepositorio.AtualizarProduto(produtoModel, id);
            return Ok(produto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoModel>> DeletarProduto(int id)
        {
            bool deletar = await _produtoRepositorio.DeletarProduto(id);
            return Ok(deletar);
        }
    }
}
