using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class UserModelAfterRegistration : UserInfoModel
    {
        public string token { get; set; }
        public decimal is_deleted { get; set; }
    }
    public class UserModel 
    {
        public int userId { get; set; }
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string phoneNumber { get; set; }
        public string password { get; set; }
    }

    public class UserInfoModel
    {
        public int userId { get; set; }
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string phoneNumber { get; set; }
    }

    public class LoginModel
    {
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public string password { get; set; }
    }

}
