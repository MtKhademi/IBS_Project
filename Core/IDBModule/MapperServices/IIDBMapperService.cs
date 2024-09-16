using Common.Interfaces.MapperServices;
using Core.IDBModule.Abstractions.Dtos;
using Core.IDBModule.Entities;

namespace Core.IDBModule.MapperServices
{
    public interface IIDBMapperService :
        IMappersService<IDBEntity, IDBGetDto>
    {
    }
}
