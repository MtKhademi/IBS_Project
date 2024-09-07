//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Common.Entities;
using Core.QuestionModule.Abstractions.Enumerations;

namespace Core.QuestionModule.Entities;

public class QuestionEntity : BaseEntity<int>
{
    public string Title { get; set; }
    public ETypeOfQuestion TypeOfQuestion { get; set; }
    public List<QuestionOption> QuestionOptions { get; set; }
}
