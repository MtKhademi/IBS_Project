using Common.Interfaces;
using Core.UserManagement.Abstractions.Dtos;

namespace Core.UserManagement.ValidationServices;

public interface IUserValidationInputService :
    IValidationInputService<UserLoginDto?>,
    IValidationInputService<UserRegisterDto?>
{
}
