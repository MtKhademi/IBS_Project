//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.QuestionModule.Abstractions.Enumerations;

namespace Core.QuestionModule.Abstractions.Dtos;

public class QuestionGetDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Group { get; set; }
    public string GroupTitle { get; set; }

    public List<string> Options { get; set; }

}

public class QuestionGetFilterDto
{
    public ETypeOfQuestion? TypeOfQuestion { get; set; }
}
