//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Common.Interfaces;
using Core.QuestionModule.Abstractions.Dtos;

namespace Core.QuestionModule.ValidationServices;

internal interface IQuestionValidationService :
    IValidationInputService<QuestionGetFilterDto?>
{
}
