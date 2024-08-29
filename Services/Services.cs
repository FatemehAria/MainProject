using Models;

namespace Services
{
    public interface IUserServices
    {
        bool createUser(UserModel model);

        List<UserModel> getUsers();

        UserModel getUserById();

        UserModel editUser(UserModel model);

        bool deleteContactById(int id);
    }
    public class UserService : IUserServices
    {
        public bool createUser(UserModel model)
        {
            throw new NotImplementedException();
        }

        public bool deleteContactById(int id)
        {
            throw new NotImplementedException();
        }

        public UserModel editUser(UserModel model)
        {
            throw new NotImplementedException();
        }

        public UserModel getUserById()
        {
            throw new NotImplementedException();
        }

        public List<UserModel> getUsers()
        {
            throw new NotImplementedException();
        }
    }
}
