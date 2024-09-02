using Common.Interfaces;
using Core.UserManagement.Abstractions.Dtos;

namespace Core.UserManagement.Abstractions
{
    public interface IUserManagement : IBaseModule
    {
        Task<string> LoginAsync(UserLoginDto? userLogin);
        Task<string> RegisterAsync(UserRegisterDto? register);
    }
}
