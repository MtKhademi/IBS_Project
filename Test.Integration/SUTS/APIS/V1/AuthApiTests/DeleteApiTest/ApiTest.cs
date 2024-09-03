using Common.Extentions;
using MDF.Test.Common.Dtos;
using MDF.Test.Common.Extentions;
using MDF.Test.SUTs.APIs.V2.Common;
using Test.Integration.SUTS.APIS.V1.Fixtures.Dtos;

namespace Test.Integration.SUTS.APIS.V1.AuthApiTests.DeleteApiTest;


[Collection("Collection tests V1-ForDelete")]
[Trait("Api.V1.AuthApis", nameof(DeleteApiTest))]
public class DeleteApiTest
{
    private readonly string _apiAddress = $"/api/v1/AuthApi/DeleteAll";
    private readonly WebAppFactoryForDelete _factory;
    private HttpClient _client;
    private readonly ITestOutputHelper _outPutHelper;
    public DeleteApiTest(WebAppFactoryForDelete factory, ITestOutputHelper outPutHelper)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _client.DefaultRequestHeaders.Add("Authorization", "TEST_TOKEN");
        _outPutHelper = outPutHelper;
    }


    [Theory]
    [InlineData("")]
    [InlineData("?Key")]
    [InlineData("?Key=")]
    [InlineData("?Key=12")]
    [InlineData("?Key=121234")]
    public async Task When_send_not_valid_data_Expect_get_bad_request(string key)
    {
        //-ARRANGE

        //-ACT
        var response = await _client.GetAsync($"{_apiAddress}{key}");
        await response.WriteOnConsoleAsync(_outPutHelper);

        //-ASSERT
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var apiResult = await response.Content.ReadModelFromJsonAsync<ApiResult>();
        apiResult.Should().NotBeNull();
        apiResult.StatusCode.Should().Be(ETypeOfApiResultStatusCode.BadRequest);
        //apiResult.Messages.Should().Contain(errorMessages);
    }


    [Fact]
    public async Task When_send_valid_data_Expect_get_ok_and_delete_all_user()
    {
        //-ARRANGE

        //-ACT
        var response = await _client.GetAsync($"{_apiAddress}?Key=123");
        await response.WriteOnConsoleAsync(_outPutHelper);

        //-ASSERT
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var apiResult = await response.Content.ReadModelFromJsonAsync<ApiResult>();
        apiResult.Should().NotBeNull();
        apiResult.StatusCode.Should().Be(ETypeOfApiResultStatusCode.Success);

        var count = await _factory.Repositories.UserGetCountAsync();
        count.Should().Be(0);
    }


}
