using Common.Extentions;
using MDF.Test.Common.Dtos;
using MDF.Test.Common.Extentions;
using MDF.Test.SUTs.APIs.V2.Common;

namespace Test.Integration.SUTS.APIS.V1.AuthApiTests.SendOtpApiTest;


[Collection("Collection tests V1")]
[Trait("Api.V1.AuthApis", nameof(SendOtpApiTest))]
public class SendOtpApiTest
{
    private readonly string _apiAddress = $"/api/v1/AuthApi/SendOtp";
    private readonly WebAppFactory _factory;
    private HttpClient _client;
    private readonly ITestOutputHelper _outPutHelper;
    public SendOtpApiTest(WebAppFactory factory, ITestOutputHelper outPutHelper)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _client.DefaultRequestHeaders.Add("Authorization", "TEST_TOKEN");
        _outPutHelper = outPutHelper;
    }


    [Theory]
    [InlineData("")]
    public async Task When_send_not_valid_data_Expect_get_bad_request(string userName)
    {
        //-ARRANGE

        //-ACT
        var response = await _client.GetAsync($"{_apiAddress}?userName={userName}");
        await response.WriteOnConsoleAsync(_outPutHelper);

        //-ASSERT
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var apiResult = await response.Content.ReadModelFromJsonAsync<ApiResult>();
        apiResult.Should().NotBeNull();
        apiResult.StatusCode.Should().Be(ETypeOfApiResultStatusCode.BadRequest);
        apiResult.Messages.Should().Contain("Please enter user name");
    }
       
}
