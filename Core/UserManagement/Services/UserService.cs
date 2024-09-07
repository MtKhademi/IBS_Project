using Core.UserManagement.Abstractions.Services;
using Microsoft.EntityFrameworkCore;
using Core.UserManagement.Entities;
using Core.DAL;
using Common.Exceptions;
using Core.UserManagement.Abstractions.Dtos;
using Core.UserManagement.Abstractions.Exceptions;
using Core.UserManagement.MapperService;

namespace Core.UserManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserMapperService mapperService;
        public UserService(IUnitOfWork unitOfWork, IUserMapperService mapperService)
        {
            _unitOfWork = unitOfWork;
            this.mapperService = mapperService;
        }


        #region Get Methods


        public async Task<UserEntity?> GetByUserNameAsync(string userName)
        {
            return await _unitOfWork.UserRepository.GetsQueryableTracker()
               .SingleOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<UserEntity?> GetByPhoneAsync(string phone)
        {
            return await _unitOfWork.UserRepository.GetsQueryableNoTracker()
                .SingleOrDefaultAsync(x => x.Phone == phone);
        }

        #endregion

        #region Add - Update Methods

        public async Task<UserEntity> AddAsync(UserEntity userEntity)
        {
            await _unitOfWork.UserRepository.CreateAsync(userEntity);
            return userEntity;
        }

        public async Task TruncateAsync()
        {
            await _unitOfWork.UserRepository.DeletesAllHardAsync();
        }

        public async Task SetOtpAsync(string userName, string code)
        {
            var entity = await _unitOfWork.UserRepository.GetsQueryableTracker()
               .SingleOrDefaultAsync(x => x.UserName == userName);

            entity.Otp = code;
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<UserEntity> UpdateAsync(UserGetDto updateDto)
        {
            var user = await GetByUserNameAsync(updateDto.UserName);
            if (user is null)
                throw new UserNameNotExistException(updateDto.UserName);

            mapperService.MapUpdate(user, updateDto);
            await _unitOfWork.SaveChangeAsync();

            return user;
        }



        #endregion

    }
}
