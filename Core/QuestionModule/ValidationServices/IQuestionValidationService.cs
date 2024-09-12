//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Common.Interfaces;
using Core.QuestionModule.Abstractions.Dtos;
using Core.QuestionModule.Abstractions.Dtos.QuestionAnswerDtos;

namespace Core.QuestionModule.ValidationServices;

internal interface IQuestionValidationService :
    IValidationInputService<QuestionGetFilterDto?>,
    IValidationInputService<QuestionAnswerSetDto?>
{
}
