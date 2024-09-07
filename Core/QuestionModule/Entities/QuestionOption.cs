//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Common.Entities;

namespace Core.QuestionModule.Entities;

public class QuestionOption : BaseEntity<int>
{
    public string Name { get; set; }
    public int Value { get; set; }

    public int QuestionId { get; set; }
    public QuestionEntity Question { get; set; }
}
