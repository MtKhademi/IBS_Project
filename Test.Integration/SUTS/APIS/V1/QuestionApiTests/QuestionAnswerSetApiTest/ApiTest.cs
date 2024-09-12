//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------


using Common.Extentions;
using Core.QuestionModule.Abstractions.Enumerations;
using MDF.DTOS;
using MDF.Test.Common.Extentions;
using MDF.Test.SUTs.APIs.V2.Common;
using Test.Integration.SUTS.APIS.V1.Fixtures.Dtos;

namespace Test.Integration.SUTS.APIS.V1.QuestionApiTests.QuestionAnswerSetApiTest;



[Collection("Collection tests V1")]
[Trait("Api.V1.Question", nameof(QuestionAnswerSetApiTest))]

public class QuestionAnswerSetApiTest :
    IClassFixture<WebAppFactory>
{
    WebAppFactory _factory;
    private readonly ITestOutputHelper _outPutHelper;
    private HttpClient _client;
    private string _apiAddress = "/api/V1/QuestionApi/QuestionAnswerSet";

    public QuestionAnswerSetApiTest(WebAppFactory factory, ITestOutputHelper outPutHelper)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _client.DefaultRequestHeaders.Add("Authorization", "TEST_TOKEN");
        _outPutHelper = outPutHelper;

    }

    [Fact]
    public async Task When_send_currect_data_Expect_get_ok_response()
    {
        //- ARRANGE
        var setDto = new QuestionAnswerSetDtoTest()
        {
            Degree = 10,
            QuestionId = (await _factory.Repositories.QuestionGetsAsync(ETypeOfQuestion.SUS)).First().Id,
            UserName = "User15"
        };

        //- ACT
        var response = await _client.PostAsync(_apiAddress, setDto.ToContentHttpString());
        await response.WriteOnConsoleAsync(_outPutHelper);

        //- ASSERTION
        var result = await response.Content.ReadModelFromJsonAsync<ApiResult>();
        result.IsSuccess.Should().BeTrue();
        var questionAnswer = await _factory.Repositories
            .QuestionAnswerGetAsync(setDto.UserName, setDto.QuestionId.Value);
        questionAnswer.Should().NotBeNull();
    }
}
