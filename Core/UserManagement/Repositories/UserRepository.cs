using Core.DAL;
using Core.DAL.Implementations;
using Core.UserManagement.Entities;

namespace Core.UserManagement.Repositories
{
    public class UserRepository : BaseRepository<int, UserEntity>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }


    }
}
