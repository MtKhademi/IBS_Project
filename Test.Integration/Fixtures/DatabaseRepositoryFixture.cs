using Core.DAL;
using Core.UserManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace MDF.Test.Fixtures
{
    internal class DatabaseRepositoryFixture
    {
        private readonly DatabaseContext _context;

        public DatabaseRepositoryFixture(DatabaseContext context)
        {
            _context = context;
        }


        #region User

        public async Task<UserEntity?> UserGetWithUserNameAsync(string userName)
        {
            return await _context.Users.SingleOrDefaultAsync(us => us.UserName == userName);
        }

        public async Task<UserEntity?> UserAddAsyc(string userName, string phone, string password)
        {
            var entity = new UserEntity() { UserName = userName, Phone = phone, Password = password };
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        #endregion

    }
}
