namespace Core.UserManagement.Abstractions.Dtos
{
    #region UserRegisterDto
    public class UserRegisterDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Phone { get; set; }
        public string? ConfirmPhone { get; set; }
    }

    public class UserGetDto
    {
        public string? UserName { get; set; }
        public string? Phone { get; set; }
        public int? Age { get; set; }
        public bool? Sex { get; set; }
        public bool? IsMarried { get; set; }
        public string? Education { get; set; }
        public string? Work { get; set; }
        public string? LocationOfLiving { get; set; }
    }

    #endregion

    #region UserLoginDto
    public class UserLoginDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool? IsRememberMe { get; set; }
        public override string ToString()
        {
            return $"{UserName} : {Password}";
        }

    }
    #endregion
}
