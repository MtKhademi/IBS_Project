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

    internal class UserGetDtoValidator : AbstractValidator<UserGetDto>
    {
        public UserGetDtoValidator()
        {
            RuleFor(user => user.UserName).NotEmpty();
            RuleFor(user => user.Phone).NotEmpty();
            RuleFor(user => user.Work).NotEmpty();
            RuleFor(user => user.Age).NotEmpty();
            RuleFor(user => user.Sex).NotEmpty();
            RuleFor(user => user.IsMarried).NotEmpty();
            RuleFor(user => user.Education).NotEmpty();
            RuleFor(user => user.LocationOfLiving).NotEmpty();
        }
    }
}
