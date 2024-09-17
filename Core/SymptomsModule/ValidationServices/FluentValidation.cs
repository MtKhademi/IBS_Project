//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.SymptomsModule.Abstractions.Dtos;
using FluentValidation;

namespace Core.SymptomsModule.ValidationServices;

internal class SymptomAddOrUpdateDtoValidator : AbstractValidator<SymptomAddOrUpdateDto>
{
    public SymptomAddOrUpdateDtoValidator()
    {
        RuleFor(user => user.TypeOfSymptom).NotEmpty();
        RuleFor(user => user.UserName).NotEmpty();
        RuleFor(user => user.Value).NotEmpty();
    }
}

