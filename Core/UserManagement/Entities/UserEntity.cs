using Common.Entities;

namespace Core.UserManagement.Entities
{
    public class UserEntity : BaseEntity<int>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Otp { get; set; }
    }
}
