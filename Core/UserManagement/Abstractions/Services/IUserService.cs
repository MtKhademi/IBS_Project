using Common.Interfaces;
using Core.UserManagement.Abstractions.Dtos;
using Core.UserManagement.Entities;

namespace Core.UserManagement.Abstractions.Services
{
    public interface IUserService :
        IAddServiceAsync<UserEntity, UserEntity>,
        IUpdateServiceAsync<UserEntity, UserGetDto>,
        ITruncateServiceAsync
    {
        Task<UserEntity?> GetByUserNameAsync(string userName);
        Task<UserEntity?> GetByPhoneAsync(string phone);
        Task SetOtpAsync(string userName, string code);
        //Task<AspnetUser> GetUserBy(UserLoginDto userLogin);
    }
}
