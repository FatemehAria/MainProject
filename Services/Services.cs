using Models;

namespace Services
{
    public interface IUserServices
    {
        bool createUser(UserModel model);
    }
    public class UserService : IUserServices
    {
        public bool createUser(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
