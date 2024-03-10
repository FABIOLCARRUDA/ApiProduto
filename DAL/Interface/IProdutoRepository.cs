using Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiProdutos.DAL.Interface
{
    public interface IProdutoRepository
    {
        public Task<int> InserirProdutoAsync(string codigo, string descricao, string departamento, decimal preco, bool status);
        public Task<IEnumerable<Produto>> ListarProdutosAsync();
        public Task<Produto> BuscarProdutoPorIDAsync(int produtoId);
        public Task<bool> AtualizarProdutoAsync(int produtoId, string novoCodigo, string novaDescricao, string novoDepartamento, decimal novoPreco, bool novoStatus);
        public Task<bool> ExcluirProdutoAsync(int produtoId);
    }
}
