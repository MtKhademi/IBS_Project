using Core.QuestionModule.Abstractions.Enumerations;
using MDF.Test.Fixtures;

namespace MDF.Test.SUTs.APIs.V2.Common;

public partial class WebAppFactory
{
    private async Task QuestionAnswerSetApiTestRequierAsync(DatabaseRepositoryFixture repository)
    {
        await repository.QuestionAddAsync("TITLE1", ETypeOfQuestion.SUS, [("NAME1", 1), ("NAME2", 2)]);
        await repository.QuestionAddAsync("TITLE1", ETypeOfQuestion.Symptoms, [("NAME1", 1), ("NAME2", 2)]);
        await repository.QuestionAddAsync("TITLE1", ETypeOfQuestion.QualityOfLife, [("NAME1", 1), ("NAME2", 2)]);


        await repository.UserAddAsyc("User15", "P15", "P15");
    }

}
