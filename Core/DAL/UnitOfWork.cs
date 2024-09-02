//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Core.UserManagement.Repositories;

namespace Core.DAL
{
    internal class UnitOfWork : IUnitOfWork
    {

        private readonly DatabaseContext _context;
        private IDbContextTransaction _transaction;


        #region private repositories

        private IUserRepository _userRepository = null;

        #endregion

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        #region Public Methods

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }
        public async Task<IDbTransaction> CreateTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted)
        {
            _transaction = await _context.Database.BeginTransactionAsync();
            //return _transaction.GetDbTransaction();
            return null;
        }
        public async ValueTask DisposeAsync()
        {
            if (_context != null) { await _context.DisposeAsync(); }
            GC.SuppressFinalize(this);
        }
        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }
        public async Task SaveAsync()
        {
            try
            {
                //Calling DbContext Class SaveChanges method 
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbEx)
            {
                foreach (var validationErrors in dbEx.Entries)
                {
                    //foreach (var validationError in validationErrors.)
                    //{
                    //    _errorMessage = _errorMessage + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage} {Environment.NewLine}";
                    //}
                }
                //throw new Exception(_errorMessage, dbEx);
            }
        }




        #endregion

        #region Public properties

        public DatabaseContext Context => _context;
        public IUserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_context));


        #endregion
    }
}
