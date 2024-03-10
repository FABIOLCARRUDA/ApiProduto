using ApiProdutos.DAL.Interface;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Model;
using Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpPost("InserirProduto")]
        public async Task<ActionResult> InserirProduto([FromBody] Produto produto)
        {
            if (!Authenticate())
                return Unauthorized("401 - Não Autorizado");

            await _produtoRepository.InserirProdutoAsync(produto.Codigo, produto.Descricao, produto.Departamento, produto.Preco, produto.Status);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> ListarProdutos()
        {
            var produtos = await _produtoRepository.ListarProdutosAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> BuscarProdutoPorID(int id)
        {
            var produto = await _produtoRepository.BuscarProdutoPorIDAsync(id);
            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

        [HttpPut("AtualizarProduto")]
        public async Task<ActionResult> AtualizarProduto([FromBody] Produto produto)
        {
            if (!Authenticate())
                return Unauthorized("401 - Não Autorizado");

            var produtoExistente = await _produtoRepository.BuscarProdutoPorIDAsync(produto.ID);
            if (produtoExistente == null)
                return NotFound();

            await _produtoRepository.AtualizarProdutoAsync(produto.ID, produto.Codigo, produto.Descricao, produto.Departamento, produto.Preco, produto.Status);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirProduto(int id)
        {
            if (!Authenticate())
                return Unauthorized("401 - Não Autorizado");

            var produtoExistente = await _produtoRepository.BuscarProdutoPorIDAsync(id);
            if (produtoExistente == null)
                return NotFound();

            await _produtoRepository.ExcluirProdutoAsync(id);
            return Ok();
        }

        [HttpGet("listaDepartamentos")]
        public ActionResult<List<DepartamentoInfo>> ListaDepartamentos()
        {
            var departamentos = new List<DepartamentoInfo>();
            foreach (var departamento in Enum.GetValues(typeof(Departamento)))
            {
                var departamentoInfo = new DepartamentoInfo
                {
                    Codigo = (int)departamento,
                    Descricao = Enum.GetName(typeof(Departamento), departamento)
                };
                departamentos.Add(departamentoInfo);
            }
            return Ok(departamentos);
        }


        bool Authenticate()
        {
            var chavesSecretas = new[] { "fabioarruda@2024", "twly@wzpou#1pcmb8cnm45uz@m0yr@" };
            StringValues key = Request.Headers["Key"];
            int count = (from t in chavesSecretas where t == key select t).Count();
            return count == 0 ? false : true;
        }
    }
}