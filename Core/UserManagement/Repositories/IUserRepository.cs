using Common.Interfaces;
using Core.UserManagement.Entities;


namespace Core.UserManagement.Repositories
{
    public interface IUserRepository :
        IGetsTrackingRepository<UserEntity>,
        IGetsNoTrackingRepository<UserEntity>,
        ICreateRepository<UserEntity>
    {
    }
}
