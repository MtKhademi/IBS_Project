using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.UnitOfWork
{
    public interface IBaseUnitOfWork
    {
        /// <summary>
        /// Start the database Transaction
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <returns></returns>
        Task<IDbTransaction> CreateTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted);

        /// <summary>
        /// Commit the database Transaction
        /// </summary>
        /// <returns></returns>
        Task CommitAsync();

        /// <summary>
        /// Rollback the database Transaction
        /// </summary>
        /// <returns></returns>
        Task RollbackAsync();


        /// <summary>
        /// DbContext Class SaveChanges method
        /// </summary>
        /// <returns></returns>
        Task SaveChangeAsync();
    }
}
