namespace Models
{
    public class UserModelAfterRegistration
    {
        public int userId { get; set; }
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string phoneNumber { get; set; }
    }
    public class UserModel : UserModelAfterRegistration
    {
        public string password { get; set; }
    }

    
}
