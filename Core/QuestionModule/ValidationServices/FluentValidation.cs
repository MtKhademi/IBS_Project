//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.QuestionModule.Abstractions.Dtos;
using Core.QuestionModule.Abstractions.Dtos.QuestionAnswerDtos;
using FluentValidation;

namespace Core.QuestionModule.ValidationServices;

internal class QuestionGetFilterDtoValidator : AbstractValidator<QuestionGetFilterDto>
{
    public QuestionGetFilterDtoValidator()
    {
        RuleFor(user => user.TypeOfQuestion).NotEmpty();
    }
}


internal class QuestionAnswerSetDtoValidator : AbstractValidator<QuestionAnswerSetDto>
{
    public QuestionAnswerSetDtoValidator()
    {
        RuleFor(user => user.UserName).NotEmpty().NotNull();
        RuleFor(user => user.QuestionId).NotEmpty().NotNull();
        RuleFor(user => user.Degree).NotEmpty().NotNull();
    }
}
