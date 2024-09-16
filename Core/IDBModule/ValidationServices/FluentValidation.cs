//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.IDBModule.Abstractions.Dtos;
using Core.QuestionModule.Abstractions.Dtos;
using Core.QuestionModule.Abstractions.Dtos.QuestionAnswerDtos;
using FluentValidation;

namespace Core.IDBModule.ValidationServices;

internal class IDBGetFilterDtoValidator : AbstractValidator<IDBGetFilterDto>
{
    public IDBGetFilterDtoValidator()
    {
        RuleFor(user => user.TypeOfIDB).NotEmpty();
    }
}

