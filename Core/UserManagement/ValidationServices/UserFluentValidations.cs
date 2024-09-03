using Core.UserManagement.Abstractions.Dtos;
using FluentValidation;

namespace Core.UserManagement.ValidationServices
{
    internal class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(user => user.UserName).NotEmpty();
            RuleFor(user => user.Password).NotEmpty();
        }
    }

    internal class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterDtoValidator()
        {
            RuleFor(user => user.UserName).NotEmpty();
            RuleFor(user => user.Password).NotEmpty();
            RuleFor(user => user.ConfirmPassword).NotEmpty().Equal(user => user.Password);
            RuleFor(user => user.Phone).NotEmpty();
            RuleFor(user => user.ConfirmPhone).NotEmpty().Equal(user => user.Phone);
        }
    }
}
