//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.QuestionModule.Abstractions.Enumerations;

namespace Core.QuestionModule.Abstractions.Dtos;

public class QuestionGetDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ETypeOfQuestion TypeOfQuestion { get; set; }


    public List<QuestionOptionGetDto> Options { get; set; }

}

public class QuestionOptionGetDto
{
    public string Name { get; set; }
    public int Value { get; set; }
}

public class QuestionGetFilterDto
{
    public ETypeOfQuestion? TypeOfQuestion { get; set; }
}
