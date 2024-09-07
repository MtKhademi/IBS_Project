//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.QuestionModule.Entities;

namespace Core.QuestionModule.Infrastructure.ExcellFileHelper;

internal interface ITransformExcellFileToQuestionList
{
    List<QuestionEntity> Gets(string filePath);
}
