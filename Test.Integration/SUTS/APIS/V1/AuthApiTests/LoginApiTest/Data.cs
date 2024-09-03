using Test.Integration.SUTS.APIS.V1.Fixtures.Dtos;

namespace Test.Integration.SUTS.APIS.V1.AuthApiTests.LoginApiTest;

internal class UserLoginDtoTestNotValidData : TheoryData<UserLoginDtoTest?, IEnumerable<string>>
{
    public UserLoginDtoTestNotValidData()
    {
        Add(null, ["Please enter data"]);
        Add(new UserLoginDtoTest { }, [
             "'User Name' must not be empty.",
             "'Password' must not be empty."
            ]);
        Add(new UserLoginDtoTest { UserName = "x", Password = "" },
            [
                "'Password' must not be empty."
            ]);

        Add(new UserLoginDtoTest { UserName = "", Password = "p" },
            [
                "'User Name' must not be empty."
            ]);
    }
}
