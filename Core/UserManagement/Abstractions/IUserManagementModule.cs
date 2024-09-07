using Common.Interfaces;
using Core.UserManagement.Abstractions.Dtos;

namespace Core.UserManagement.Abstractions
{
    public interface IUserManagement : IBaseModule
    {
        Task DeleteAllAsync(string key);
        Task<UserGetDto> LoginAsync(UserLoginDto? userLogin);
        Task<UserGetDto> RegisterAsync(UserRegisterDto? register);
        Task SendOtpAsync(string? userName);
        Task UpdateAsync(UserGetDto? register);
    }
}
