using Common.Interfaces;
using Core.UserManagement.Entities;


namespace Core.UserManagement.Repositories
{
    public interface IUserRepository :
        ICRUDRepository<int,UserEntity>,
        //IGetsTrackingRepository<UserEntity>,
        //IGetsNoTrackingRepository<UserEntity>,
        //ICreateRepository<UserEntity>,
        ITruncateRepository
    {
    }
}
