//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------


using Common.Extentions;
using Core.QuestionModule.Abstractions.Enumerations;
using MDF.DTOS;
using MDF.Test.Common.Extentions;
using MDF.Test.SUTs.APIs.V2.Common;
using Test.Integration.SUTS.APIS.V1.Fixtures.Dtos;

namespace Test.Integration.SUTS.APIS.V1.QuestionApiTests.QuestionGetsApiTest;



[Collection("Collection tests V1")]
[Trait("Api.V1.Question", nameof(QuestionGetsApiTest))]

public class QuestionGetsApiTest :
    IClassFixture<WebAppFactory>
{
    WebAppFactory _factory;
    private readonly ITestOutputHelper _outPutHelper;
    private HttpClient _client;
    private string _apiAddress = "/api/V1/QuestionApi/QuestionGets?TypeOfQuestion=";

    public QuestionGetsApiTest(WebAppFactory factory, ITestOutputHelper outPutHelper)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _client.DefaultRequestHeaders.Add("Authorization", "TEST_TOKEN");
        _client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("multipart/form-data"));
        //_client.header.Add("Content-Type", "multipart/form-data");
        _outPutHelper = outPutHelper;

    }

    [Theory]
    [InlineData(ETypeOfQuestion.Symptoms)]
    [InlineData(ETypeOfQuestion.SUS)]
    [InlineData(ETypeOfQuestion.QualityOfLife)]
    public async Task When_call_with_special_filter_Expect_get_ok_response(ETypeOfQuestion typeOfQurestion)
    {
        //- ARRANGE
        var apiAddress = $"{_apiAddress}{typeOfQurestion}";

        //- ACT
        var response = await _client.GetAsync(apiAddress);
        await response.WriteOnConsoleAsync(_outPutHelper);

        //- ASSERTION
        var result = await response.Content.ReadModelFromJsonAsync<ApiResult<IEnumerable<QuestionGetDtoTest>>>();
        result.IsSuccess.Should().BeTrue();
        foreach (var item in result.Result)
        {
            item.Title.Should().NotBeNullOrWhiteSpace();
            item.TypeOfQuestion.Should().Be(typeOfQurestion);
        }
    }
}
