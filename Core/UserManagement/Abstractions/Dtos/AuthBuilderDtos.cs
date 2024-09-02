using Common.Infrastructure;

namespace Core.UserManagement.Abstractions.Dtos
{
    #region UserRegisterBuilderDto
    public class UserRegisterBuilderDto : BaseBuilders<UserRegisterDto>
    {
        private readonly UserRegisterDto _userRegisterDto;
        public UserRegisterBuilderDto()
        {
            _userRegisterDto = new UserRegisterDto();
        }
        public override UserRegisterDto Build() => _userRegisterDto;
    }
    #endregion

    #region UserLoginBuilderDto
    public class UserLoginBuilderDto : BaseBuilders<UserLoginDto>
    {
        private readonly UserLoginDto _userLoginDto;
        public UserLoginBuilderDto()
        {
            _userLoginDto = new UserLoginDto();
        }

        public override UserLoginDto Build() => _userLoginDto;
    }
    #endregion
}
