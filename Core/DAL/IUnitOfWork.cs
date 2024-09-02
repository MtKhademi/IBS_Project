using Common.UnitOfWork;
using Core.UserManagement.Repositories;

namespace Core.DAL
{
    public interface IUnitOfWork : IBaseUnitOfWork
    {
        IUserRepository UserRepository { get; }
    
    }
}
