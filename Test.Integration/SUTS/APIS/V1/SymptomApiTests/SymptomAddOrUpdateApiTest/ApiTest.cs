//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------


using Common.Extentions;
using Core.QuestionModule.Abstractions.Enumerations;
using Core.SymptomsModule.Abstractions.Dtos;
using Core.SymptomsModule.Abstractions.Enums;
using MDF.DTOS;
using MDF.Test.Common.Extentions;
using MDF.Test.SUTs.APIs.V2.Common;
using Test.Integration.SUTS.APIS.V1.Fixtures.Dtos;

namespace Test.Integration.SUTS.APIS.V1.SymptomApiTests.SymptomAddOrUpdateApiTest;



[Collection("Collection tests V1")]
[Trait("Api.V1.Symptom", nameof(SymptomAddOrUpdateApiTest))]

public class SymptomAddOrUpdateApiTest :
    IClassFixture<WebAppFactory>
{
    WebAppFactory _factory;
    private readonly ITestOutputHelper _outPutHelper;
    private HttpClient _client;
    private string _apiAddress = "/api/V1/SymptomApi/AddOrUpdate";

    public SymptomAddOrUpdateApiTest(WebAppFactory factory, ITestOutputHelper outPutHelper)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _client.DefaultRequestHeaders.Add("Authorization", "TEST_TOKEN");
        _outPutHelper = outPutHelper;

    }

    [Fact]
    public async Task When_add_new_item_for_first_time_Expect_ok_response()
    {
        // ARRANGE
        var dto = new SymptomAddOrUpdateDto()
        {
            UserName = "User20",
            TypeOfSymptom = ETypeOfSymptoms.MucusInTheStool,
            Value = 5
        };

        // ACT
        var response = await _client.PostAsync(_apiAddress, dto.ToContentHttpString());
        await response.WriteOnConsoleAsync(_outPutHelper);

        // ASSERTION
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var apiResult = await response.Content.ReadModelFromJsonAsync<ApiResult>();
        apiResult.IsSuccess.Should().BeTrue();


        var symptoms = await _factory.Repositories.SymptomGetAsync(dto.UserName);
        symptoms.Should().NotBeNull();
        symptoms.Should().HaveCount(1);
    }


    [Fact]
    public async Task When_add_new_item_when_exist_any_item_for_this_user_time_Expect_ok_response()
    {
        // ARRANGE
        var dt = new DateTime(2024, 09, 23, 23, 23, 0);
        var dto = new SymptomAddOrUpdateDto()
        {
            UserName = "User21",
            TypeOfSymptom = ETypeOfSymptoms.MucusInTheStool,
            Value = 5
        };

        // ACT

        //send first time
        var response = await _client.PostAsync(_apiAddress, dto.ToContentHttpString());
        await response.WriteOnConsoleAsync(_outPutHelper);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var apiResult = await response.Content.ReadModelFromJsonAsync<ApiResult>();
        apiResult.IsSuccess.Should().BeTrue();


        // send second for another symptom
        dto.TypeOfSymptom = ETypeOfSymptoms.AbdominalSwelling;
        response = await _client.PostAsync(_apiAddress, dto.ToContentHttpString());
        await response.WriteOnConsoleAsync(_outPutHelper);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        apiResult = await response.Content.ReadModelFromJsonAsync<ApiResult>();
        apiResult.IsSuccess.Should().BeTrue();

        //third times
        dto.TypeOfSymptom = ETypeOfSymptoms.AbdominalCramps;
        response = await _client.PostAsync(_apiAddress, dto.ToContentHttpString());
        await response.WriteOnConsoleAsync(_outPutHelper);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        apiResult = await response.Content.ReadModelFromJsonAsync<ApiResult>();
        apiResult.IsSuccess.Should().BeTrue();


        var symptoms = await _factory.Repositories.SymptomGetAsync(dto.UserName);
        symptoms.Should().NotBeNull();
        symptoms.Should().HaveCount(4);

        foreach (var item in symptoms)
        {
            item.DateTimeOfUpdate.Date.Should().Be(dt.Date);
        }

    }


    [Fact]
    public async Task When_add_new_item_when_exist_that_item_for_this_user_time_Expect_ok_response()
    {
        // ARRANGE
        var dt = new DateTime(2024, 09, 26, 23, 23, 0);
        var dto = new SymptomAddOrUpdateDto()
        {
            UserName = "User22",
            TypeOfSymptom = ETypeOfSymptoms.Constipation,
            Value = 8
        };

        // ACT

        //send first time
        var response = await _client.PostAsync(_apiAddress, dto.ToContentHttpString());
        await response.WriteOnConsoleAsync(_outPutHelper);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var apiResult = await response.Content.ReadModelFromJsonAsync<ApiResult>();
        apiResult.IsSuccess.Should().BeTrue();


        var symptoms = await _factory.Repositories.SymptomGetAsync(dto.UserName);
        symptoms.Should().NotBeNull();
        symptoms.Should().HaveCount(1);

        symptoms.First().Value.Should().Be(8);

    }


    [Fact]
    public async Task When_add_new_item_when_exist_that_but_old_week_item_for_this_user_time_Expect_ok_response()
    {
        // ARRANGE
        var dtOld = new DateTime(2024, 09, 15, 23, 23, 0);
        var dtNew = new DateTime(2024, 09, 22, 23, 23, 0);
        var dto = new SymptomAddOrUpdateDto()
        {
            UserName = "User23",
            TypeOfSymptom = ETypeOfSymptoms.Constipation,
            Value = 8
        };

        // ACT

        //send first time
        var response = await _client.PostAsync(_apiAddress, dto.ToContentHttpString());
        await response.WriteOnConsoleAsync(_outPutHelper);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var apiResult = await response.Content.ReadModelFromJsonAsync<ApiResult>();
        apiResult.IsSuccess.Should().BeTrue();


        var symptoms = await _factory.Repositories.SymptomGetAsync(dto.UserName);
        symptoms.Should().NotBeNull();
        symptoms.Should().HaveCount(2);


        var firstItem = symptoms[0];
        firstItem.Value.Should().Be(5);
        firstItem.DateTimeOfUpdate.Date.Should().Be(dtOld.Date);


        firstItem = symptoms[1];
        firstItem.Value.Should().Be(8);
        firstItem.DateTimeOfUpdate.Date.Should().Be(dtNew.Date);

    }


}
