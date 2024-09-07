//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.QuestionModule.Abstractions.Dtos;
using FluentValidation;

namespace Core.QuestionModule.ValidationServices;

internal class QuestionGetFilterDtoValidator : AbstractValidator<QuestionGetFilterDto>
{
    public QuestionGetFilterDtoValidator()
    {
        RuleFor(user => user.TypeOfQuestion).NotEmpty();
    }
}
