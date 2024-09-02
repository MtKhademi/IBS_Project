using AutoMapper;
using Core.UserManagement.Entities;
using Core.UserManagement.Models;
using Core.UserManagement.Abstractions.Dtos;

namespace Core.UserManagement.MapperServices
{
    internal class MapperConfig
    {
        #region User Mapper Config
        public class UserMapperConfig : Profile
        {
            public UserMapperConfig()
            {
                CreateMap<UserEntity, UserManagementModel>();
                CreateMap<UserRegisterDto, UserEntity>();
            }
        }

        #endregion Private Helper

    }
}