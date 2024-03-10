using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using ApiProdutos.DAL.Interface;
using DAL.ConnectionFactory;
using Model;
using Model.Enum;
using MySql.Data.MySqlClient;


namespace ApiProdutos.DAL.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ConnectionFactory _connectionFactory;

        public ProdutoRepository(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> InserirProdutoAsync(string codigo, string descricao, string departamento, decimal preco, bool status)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var command = new MySqlCommand("InserirProduto", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@codigo", codigo);
            command.Parameters.AddWithValue("@descricao", descricao);
            command.Parameters.AddWithValue("@departamento", departamento);
            command.Parameters.AddWithValue("@preco", preco);
            command.Parameters.AddWithValue("@status", status);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<IEnumerable<Produto>> ListarProdutosAsync()
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var command = new MySqlCommand("ListarProdutos", connection);
            command.CommandType = CommandType.StoredProcedure;
            using var reader = await command.ExecuteReaderAsync();

            var produtos = new List<Produto>();
            while (await reader.ReadAsync())
            {
                var _departamento = Convert.ToInt32(reader["Departamento"].ToString());
                
                produtos.Add(new Produto
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Codigo = reader["Codigo"].ToString(),
                    Descricao = reader["Descricao"].ToString(),
                    Departamento = Enum.GetName(typeof(Departamento), _departamento),
                    Preco = Convert.ToDecimal(reader["Preco"]),
                    Status = Convert.ToBoolean(reader["Status"])
                });
            }

            return produtos;
        }

        public async Task<Produto> BuscarProdutoPorIDAsync(int produtoId)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var command = new MySqlCommand("BuscarProdutoPorID", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@produto_id", produtoId);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Produto
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Codigo = reader["Codigo"].ToString(),
                    Descricao = reader["Descricao"].ToString(),
                    Departamento = reader["Departamento"].ToString(),
                    Preco = Convert.ToDecimal(reader["Preco"]),
                    Status = Convert.ToBoolean(reader["Status"])
                };
            }
            return null;
        }

        public async Task<bool> AtualizarProdutoAsync(int produtoId, string novoCodigo, string novaDescricao, string novoDepartamento, decimal novoPreco, bool novoStatus)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var command = new MySqlCommand("AtualizarProduto", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@produto_id", produtoId);
            command.Parameters.AddWithValue("@novo_codigo", novoCodigo);
            command.Parameters.AddWithValue("@nova_descricao", novaDescricao);
            command.Parameters.AddWithValue("@novo_departamento", novoDepartamento);
            command.Parameters.AddWithValue("@novo_preco", novoPreco);
            command.Parameters.AddWithValue("@novo_status", novoStatus);

            return await command.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> ExcluirProdutoAsync(int produtoId)
        {
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var command = new MySqlCommand("ExcluirProduto", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@produto_id", produtoId);

            return await command.ExecuteNonQueryAsync() > 0;
        }

    }
}
