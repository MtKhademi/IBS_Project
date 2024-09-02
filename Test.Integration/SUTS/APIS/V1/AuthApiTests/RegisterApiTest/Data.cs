using Test.Integration.SUTS.APIS.V1.Fixtures.Dtos;

namespace Test.Integration.SUTS.APIS.V1.AuthApiTests.RegisterApiTest;

internal class UserRegisterDtoTestNotValidData : TheoryData<UserRegisterDtoTest?, IEnumerable<string>>
{
    public UserRegisterDtoTestNotValidData()
    {
        Add(null, ["Please enter data"]);
        Add(new UserRegisterDtoTest { }, [
             "'User Name' must not be empty.",
             "'Password' must not be empty."
            ]);
        Add(new UserRegisterDtoTest { UserName = "x", Phone = "p", ConfirmPhone = "y", Password = "p", ConfirmPassword = "y" }, [
             "\u0027Confirm Phone\u0027 must be equal to \u0027p\u0027."
            ]);
    }
}
