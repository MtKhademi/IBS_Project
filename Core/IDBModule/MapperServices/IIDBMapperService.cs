using Common.Interfaces.MapperServices;
using Core.IDBModule.Abstractions.Dtos;
using Core.IDBModule.Entities;
using Core.SymptomsModule.Entities;

namespace Core.IDBModule.MapperServices
{
    public interface IIDBMapperService :
        IMappersService<IDBEntity, IDBGetDto>
    {
    }
}
