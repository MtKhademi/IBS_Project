using Common.Interfaces;
using Core.SymptomsModule.Abstractions.Dtos;
using Core.SymptomsModule.Entities;

namespace Core.SymptomsModule.Abstractions.Services;

public interface ISymptomService
    : IAddOrUpdateServiceAsync<SymptomAddOrUpdateDto>,
    IGetAllServiceAsync<SymptomEntity>
{
}
