using AutoMapper;
using Core.UserManagement.Entities;
using Core.UserManagement.Models;
using Core.UserManagement.Abstractions.Dtos;
using Core.UserManagement.MapperService;
using Common.Interfaces.MapperServices;

namespace Core.UserManagement.MapperServices
{
    internal class UserMapperService : IUserMapperService
    {
        private readonly IMapper _mapper;

        public UserMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public UserManagementModel Map(UserEntity model) => _mapper.Map<UserManagementModel>(model);

        public UserEntity Map(UserRegisterDto model) => _mapper.Map<UserEntity>(model);

        public void MapUpdate(UserEntity source, UserGetDto destination)
        {
            _mapper.Map(source, destination);
        }

        public UserGetDto MapToUserGet(UserEntity model)
              => _mapper.Map<UserGetDto>(model);
    }
}
