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

        CustomActionResult<IDbConnection> connectionStatus = new CustomActionResult<IDbConnection>();

        public Task<CustomActionResult<IDbConnection>> connectToDatabase()
        {
            try
            {
                if(_connection == null)
                {
                    _connection = new MySqlConnection("server=localhost;database=main_project_db;user=root;password=;");
                }
                connectionStatus.success = true;
                connectionStatus.message = "connection to database successfull.";
            }
            catch
            {

            }
        }
    }
}
