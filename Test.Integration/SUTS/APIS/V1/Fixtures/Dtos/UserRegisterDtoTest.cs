namespace Test.Integration.SUTS.APIS.V1.Fixtures.Dtos;


public class UserGetDtoTest
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

public class UserRegisterDtoTest
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public string? Phone { get; set; }
    public string? ConfirmPhone { get; set; }
}
