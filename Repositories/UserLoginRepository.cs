using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUserLoginRepository
    {
        Task<CustomActionResult<decimal>> getUserByUsernameAndPassword(string username,string password)
    }

    public class UserLoginRepository : IUserLoginRepository
    {
        public Task<CustomActionResult<decimal>> getUserByUsernameAndPassword(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
