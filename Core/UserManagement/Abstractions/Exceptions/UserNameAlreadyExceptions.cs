using Common.Exceptions;
using Core.UserManagement.Entities;

namespace Core.UserManagement.Abstractions.Exceptions;

internal class UserNameAlreadyExceptions : AlreadyExistDataException
{
    public UserNameAlreadyExceptions(string userName) :
        base(nameof(UserEntity), nameof(UserEntity.UserName), userName)
    {
    }
}
