//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Common.Exceptions;
using Core.QuestionModule.Abstractions.Enumerations;
using Core.QuestionModule.Entities;
using Core.QuestionModule.Infrastructure.ExcellFileHelper.Transforms;

namespace Core.QuestionModule.Infrastructure.ExcellFileHelper;

internal class ExcellFileManager
{
    private readonly Dictionary<ETypeOfQuestion, ITransformExcellFileToQuestionList> _transfore;

    public ExcellFileManager()
    {
        _transfore = new Dictionary<ETypeOfQuestion, ITransformExcellFileToQuestionList>
        {
            { ETypeOfQuestion.SUS, new SusQuestionTransform() }
        };
    }

    public List<QuestionEntity> GetQuestions(string filePath, ETypeOfQuestion typeOfQuestion)
    {
        var transform = _transfore.GetValueOrDefault(typeOfQuestion);
        if (transform == null)
            throw new NotValidDataException($"Please create a transfor for type : {typeOfQuestion}");

        return transform.Gets(filePath);
    }
}
