//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using AutoMapper;
using Core.IDBModule.Abstractions.Dtos;
using Core.IDBModule.Entities;

namespace Core.IDBModule.MapperServices;

internal class MapperConfig
{
    public class IDBMapperConfig : Profile
    {
        public IDBMapperConfig()
        {
            CreateMap<IDBEntity, IDBGetDto>();
        }
    }
}
