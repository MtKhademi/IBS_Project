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
