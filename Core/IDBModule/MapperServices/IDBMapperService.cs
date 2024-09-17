using AutoMapper;
using Common.Interfaces.MapperServices;
using Core.IDBModule.Abstractions.Dtos;
using Core.IDBModule.Entities;
using Core.SymptomsModule.Entities;

namespace Core.IDBModule.MapperServices
{
    internal class IDBMapperService
        (IMapper mapper): IIDBMapperService
    {
        public IEnumerable<IDBGetDto> Maps(IEnumerable<IDBEntity> models)
        {
            return mapper.Map<IEnumerable<IDBGetDto>>(models);
        }
    }
}
