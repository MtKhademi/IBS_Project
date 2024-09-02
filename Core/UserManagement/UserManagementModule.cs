﻿using Common.Exceptions;
using Common.DependencyInjectionHelpers;
using Core.UserManagement.Abstractions;
using Core.UserManagement.Abstractions.Services;
using Core.UserManagement.Abstractions.Dtos;
using Core.UserManagement.ValidationServices;
using Core.UserManagement.MapperService;
using Core.UserManagement.Abstractions.Exceptions;

namespace Core.UserManagement
{
    [Scope]
    public class UserManagement : IUserManagement
    {

        private readonly IUserService _userService;
        private readonly IUserValidationInputService _userValidationService;
        private readonly IJWTUserHandlerService _jwtUserHandlerService;
        private readonly IUserMapperService _userMapperService;
        public UserManagement(IUserService userService,
            IUserValidationInputService userValidationService,
            IJWTUserHandlerService jwtUserHandlerService,
            IUserMapperService userMapperService)
        {
            _userService = userService;
            _userValidationService = userValidationService;
            _jwtUserHandlerService = jwtUserHandlerService;
            _userMapperService = userMapperService;
        }

        public async Task<string> LoginAsync(UserLoginDto? userLogin)
        {
            _userValidationService.IsValidAndThrowException(userLogin);

            var userEntity = await _userService.GetByUserNameAsync(userLogin.UserName);

            if (userEntity.Password != userLogin.Password)
                throw new NotValidDataException($"Username or password were wrong");

            return _jwtUserHandlerService.GenerateToken(_userMapperService.Map(userEntity));
        }

        public async Task<string> RegisterAsync(UserRegisterDto? register)
        {
            _userValidationService.IsValidAndThrowException(register);

            var user = await _userService.GetByUserNameAsync(register.UserName);
            if (user is not null)
                throw new UserNameAlreadyExceptions(register.UserName);

            var userWithPhone = await _userService.GetByPhoneAsync(register.Phone);
            if (userWithPhone is not null)
                throw new PhoneAlreadyExceptions(register.Phone);

            var userEntity = _userMapperService.Map(register);
            await _userService.AddAsync(userEntity);

            var userModel = _userMapperService.Map(userEntity);

            return _jwtUserHandlerService.GenerateToken(userModel);
        }
    }
}
