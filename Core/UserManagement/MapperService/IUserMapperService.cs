using Common.Interfaces.MapperServices;
using Core.UserManagement.Entities;
using Core.UserManagement.Models;
using Core.UserManagement.Abstractions.Dtos;

namespace Core.UserManagement.MapperService
{
    public interface IUserMapperService :
        IMapperService<UserEntity, UserManagementModel>,
        IMapperService<UserRegisterDto, UserEntity>
    {
    }
}
