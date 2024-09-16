using Common.Interfaces;
using Core.IDBModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IDBModule.Repositories
{
    public interface IIDBRepository : ICRUDRepository<int, IDBEntity>
    {
    }
}
