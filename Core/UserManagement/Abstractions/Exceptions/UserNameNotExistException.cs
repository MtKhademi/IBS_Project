using Common.Exceptions;
using Core.UserManagement.Entities;

namespace Core.UserManagement.Abstractions.Exceptions;

internal class UserNameNotExistException : NotExistDataException
{
    public UserNameNotExistException(string userName) : base(nameof(UserEntity), userName)
    {
    }
}
