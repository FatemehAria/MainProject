using Models;
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
        public Task<CustomActionResult<IDbConnection>> connectToDatabase()
        {
            throw new NotImplementedException();
        }
    }
}
