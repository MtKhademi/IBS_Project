using Common.Exceptions;
using Core.UserManagement.Entities;

namespace Core.UserManagement.Abstractions.Exceptions;

internal class PhoneAlreadyExceptions : AlreadyExistDataException
{
    public PhoneAlreadyExceptions(string phone) :
        base(nameof(UserEntity), nameof(UserEntity.Phone), phone)
    {
    }
}
