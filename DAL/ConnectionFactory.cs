using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ConnectionFactory
{
    public class ConnectionFactory
    {
        private readonly IConfiguration _configuration;
        public ConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<MySqlConnection> CreateConnectionAsync()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();
            return connection;
        }

    }
}
