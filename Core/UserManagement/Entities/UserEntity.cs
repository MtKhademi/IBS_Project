using Common.Entities;

namespace Core.UserManagement.Entities
{
    public class UserEntity : BaseEntity<int>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Otp { get; set; }
        public int Age { get; set; }
        public bool Sex { get; set; }
        public bool IsMarried { get; set; }
        public string Education { get; set; }
        public string Work { get; set; }
        public string LocationOfLiving { get; set; }
    }
}
