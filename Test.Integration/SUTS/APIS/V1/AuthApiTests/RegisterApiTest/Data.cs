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
        Add(new UserRegisterDtoTest { UserName = "x", Phone = "p", ConfirmPhone = "y", Password = "p", ConfirmPassword = "y" },
            [ 
                "'Confirm Phone' must be equal to 'p'."
            ]);


        Add(new UserRegisterDtoTest { UserName = "x", Phone = "p", ConfirmPhone = "p", Password = "p", ConfirmPassword = "y" },
            [
                "'Confirm Password' must be equal to 'p'."
            ]);
    }
}
