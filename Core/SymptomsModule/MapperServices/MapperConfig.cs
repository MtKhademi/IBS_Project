//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using AutoMapper;
using Core.SymptomsModule.Abstractions.Dtos;
using Core.SymptomsModule.Entities;

namespace Core.SymptomsModule.MapperServices;

internal class MapperConfig
{
    public class IDBMapperConfig : Profile
    {
        public IDBMapperConfig()
        {
            CreateMap<SymptomAddOrUpdateDto, SymptomEntity>();
        }
    }
}
