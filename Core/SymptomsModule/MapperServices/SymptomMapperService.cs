using AutoMapper;
using Core.SymptomsModule.Abstractions.Dtos;
using Core.SymptomsModule.Entities;
using Core.SymptomsModule.MapperServices;

namespace Core.IDBModule.MapperServices
{
    internal class SymptomMapperService
        (IMapper mapper) : ISymptomMapperService
    {
        public SymptomEntity Map(SymptomAddOrUpdateDto model)
            => mapper.Map<SymptomEntity>(model);
    }
}
