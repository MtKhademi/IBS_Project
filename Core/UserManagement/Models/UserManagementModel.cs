namespace Core.UserManagement.Models
{
    public class UserManagementModel
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public string NationalId { get; set; }
        public string UserName { get; set; }
        public string Family { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<AccessList> AccessList { get; set; }
    }
    public class AccessList
    {
    }
}
