using Common.Interfaces;
using Core.SymptomsModule.Abstractions.Dtos;

namespace Core.SymptomsModule.ValidationServices;

public interface ISymptomValidationService
    : IValidationInputService<SymptomAddOrUpdateDto>
{
}
