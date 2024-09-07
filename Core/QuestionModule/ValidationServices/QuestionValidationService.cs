//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Common.Exceptions;
using Core.QuestionModule.Abstractions.Dtos;

namespace Core.QuestionModule.ValidationServices;

internal class QuestionValidationService : IQuestionValidationService
{
    public void IsValidAndThrowException(QuestionGetFilterDto? model)
    {
        if (model is null)
            throw new NotValidDataException("Please enter filter");

        var validator = (new QuestionGetFilterDtoValidator()).Validate(model);
        if (!validator.IsValid)
            throw new NotValidDataException(validator.Errors);
    }
}
