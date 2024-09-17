using Common.Interfaces.MapperServices;
using Core.SymptomsModule.Abstractions.Dtos;
using Core.SymptomsModule.Entities;

namespace Core.SymptomsModule.MapperServices;

public interface ISymptomMapperService :
    IMapperService<SymptomAddOrUpdateDto, SymptomEntity>
{
}
