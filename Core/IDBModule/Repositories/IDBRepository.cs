using Common.Entities;
using Core.DAL;
using Core.DAL.Implementations;
using Core.IDBModule.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.IDBModule.Repositories
{
    internal class IDBRepository : BaseRepository<int, IDBEntity>, IIDBRepository
    {
        private readonly DatabaseContext _context;
        public IDBRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async override Task DeletesAllHardAsync()
        {
            string cmd = $"DELETE FROM IDBs";
            await _context.Database.ExecuteSqlRawAsync(cmd);
            //
        }
    }
}
