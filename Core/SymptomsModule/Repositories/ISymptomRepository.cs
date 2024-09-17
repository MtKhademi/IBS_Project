using Common.Interfaces;
using Core.IDBModule.Entities;
using Core.SymptomsModule.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SymptomsModule.Repositories;

public interface ISymptomRepository : ICRUDRepository<int, SymptomEntity>
{
}
