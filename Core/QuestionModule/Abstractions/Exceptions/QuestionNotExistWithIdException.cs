//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Common.Exceptions;
using Core.QuestionModule.Entities;

namespace Core.QuestionModule.Abstractions.Exceptions;

internal class QuestionNotExistWithIdException : NotExistDataException
{
    public QuestionNotExistWithIdException(int questionId) :
        base(nameof(QuestionEntity), $"Not exist any question with id :{questionId}")
    {
    }
}
