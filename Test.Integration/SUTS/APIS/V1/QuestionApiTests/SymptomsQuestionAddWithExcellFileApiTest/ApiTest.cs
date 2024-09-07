//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------


using Common.Extentions;
using Core.QuestionModule.Abstractions.Enumerations;
using MDF.DTOS;
using MDF.Test.Common.Extentions;
using MDF.Test.SUTs.APIs.V2.Common;
using System.Net.Http.Json;

namespace Test.Integration.SUTS.APIS.V1.QuestionApiTests.SymptomsQuestionAddWithExcellFileApiTest;



[Collection("Collection tests V1")]
[Trait("Api.V1.Question.SYMPTOMS", nameof(QuestionAddWithExcellFileApiTest))]

public class QuestionAddWithExcellFileApiTest :
    IClassFixture<WebAppFactory>
{
    WebAppFactory _factory;
    private readonly ITestOutputHelper _outPutHelper;
    private HttpClient _client;
    private string _apiAddress = "/api/V1/QuestionApi/Symptoms/AddWithExcellFile";

    public QuestionAddWithExcellFileApiTest(WebAppFactory factory, ITestOutputHelper outPutHelper)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _client.DefaultRequestHeaders.Add("Authorization", "TEST_TOKEN");
        _client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("multipart/form-data"));
        //_client.header.Add("Content-Type", "multipart/form-data");
        _outPutHelper = outPutHelper;

    }

    [Fact]
    public async Task Step1_When_not_valid_data_Expect_get_bad_response()
    {
        //-ARRANGE
        var pathFile = Path.Combine(
            Directory.GetCurrentDirectory(),
            nameof(SUTS),
            nameof(APIS),
            nameof(V1),
            nameof(QuestionApiTests),
            nameof(SymptomsQuestionAddWithExcellFileApiTest),
            "exc.xlsx");

        var checkExistExcellFile = File.Exists(pathFile);

        checkExistExcellFile.Should().Be(true);

        using var stream = File.OpenRead(pathFile);

        var formContent = new MultipartFormDataContent
        {
            // Send form text values here
            {new StringContent("value1"),"key1"},
            {new StringContent("value2"),"key2" },
            // Send file Here
            {new StreamContent(stream),"file","file"}
        };


        //ACT
        var response = await _client.PostAsync(_apiAddress, formContent);
        await response.WriteOnConsoleAsync(_outPutHelper);

        //ASSERTION
        response.IsSuccessStatusCode.Should().BeTrue();
        var apiResult = await response.Content.ReadModelFromJsonAsync<ApiResult>();
        apiResult.Should().NotBeNull();
        apiResult.IsSuccess.Should().BeTrue();
        var questions = await _factory.Repositories.QuestionGetsAsync(ETypeOfQuestion.Symptoms);
        questions.Count.Should().Be(1);
    }
}
