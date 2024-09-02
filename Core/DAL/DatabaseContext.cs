//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.UserManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
