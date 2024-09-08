using Core.QuestionModule.Abstractions.Enumerations;

namespace Test.Integration.SUTS.APIS.V1.Fixtures.Dtos;

public class QuestionGetDtoTest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ETypeOfQuestion TypeOfQuestion { get; set; }

    public List<QuestionOptionGetDtoTest> Options { get; set; }

}

public class QuestionOptionGetDtoTest
{
    public string Name { get; set; }
    public int Value { get; set; }
}
