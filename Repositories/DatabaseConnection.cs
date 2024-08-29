using Microsoft.Extensions.Options;
using Models;
using MySql.Data.MySqlClient;
using System.Data;

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
                    _connection = new MySqlConnection(_connectionModel.connectionString);
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
