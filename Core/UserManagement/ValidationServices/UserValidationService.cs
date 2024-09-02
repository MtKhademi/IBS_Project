using Core.UserManagement.Abstractions.Dtos;
using Common.Exceptions;

namespace Core.UserManagement.ValidationServices;

public class UserValidationService : IUserValidationInputService
{
    public void IsValidAndThrowException(UserLoginDto? model)
    {
        if (model == null)
            throw new NotValidDataException($"Please enter data");

        var valid = new UserLoginDtoValidator().Validate(model);

        if (!valid.IsValid)
            throw new NotValidDataException(valid.Errors);

    }

    public void IsValidAndThrowException(UserRegisterDto? model)
    {
        if (model == null)
            throw new NotValidDataException($"Please enter data");

        var valid = new UserRegisterDtoValidator().Validate(model);

        if (!valid.IsValid)
            throw new NotValidDataException(valid.Errors);
    }
}
