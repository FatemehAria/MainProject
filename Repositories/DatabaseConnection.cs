using Microsoft.Extensions.Options;
using Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IDatabaseConnection
    {
        Task<CustomActionResult<IDbConnection>> connectToDatabase();
    }
    public class DatabaseConnection : IDatabaseConnection
    {
        private IDbConnection _connection;
        private readonly DatabaseConnectionModel _connectionModel;

        public DatabaseConnection(IOptions<DatabaseConnectionModel> options)
        {
            _connectionModel = options.Value;
        }
        public async Task<CustomActionResult<IDbConnection>> connectToDatabase()
        {
            CustomActionResult<IDbConnection> connectionStatus = new CustomActionResult<IDbConnection>();
            try
            {
                if (_connection == null)
                {
                    _connection = new MySqlConnection("server=localhost;database=main_project_db;user=root;password=;");
                }
                connectionStatus.success = true;
                connectionStatus.data = _connection;
                connectionStatus.message = "connection to database successfull.";
            }
            catch
            {
                connectionStatus.message = "error connecting to database";
                connectionStatus.success = false;
            }

            return connectionStatus;
        }
    }
}
