using Common.Interfaces;
using Core.UserManagement.Entities;

namespace Core.UserManagement.Abstractions.Services
{
    public interface IUserService :
        IAddServiceAsync<UserEntity, UserEntity>
    {
        Task<UserEntity?> GetByUserNameAsync(string userName);
        Task<UserEntity?> GetByPhoneAsync(string phone);
        //Task<AspnetUser> GetUserBy(UserLoginDto userLogin);
    }
}
