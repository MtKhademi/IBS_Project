﻿using Common.Extentions;
using MDF.Test.Common.Dtos;
using MDF.Test.Common.Extentions;
using MDF.Test.SUTs.APIs.V2.Common;
using Test.Integration.SUTS.APIS.V1.Fixtures.Dtos;

namespace Test.Integration.SUTS.APIS.V1.AuthApiTests.RegisterApiTest;


[Collection("Collection tests V1")]
[Trait("Api.V1.AuthApis", nameof(RegisterApiTest))]
public class RegisterApiTest
{
    private readonly string _apiAddress = $"/api/v1/AuthApi/Register";
    private readonly WebAppFactory _factory;
    private HttpClient _client;
    private readonly ITestOutputHelper _outPutHelper;
    public RegisterApiTest(WebAppFactory factory, ITestOutputHelper outPutHelper)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _client.DefaultRequestHeaders.Add("Authorization", "TEST_TOKEN");
        _outPutHelper = outPutHelper;
    }



    [Theory]
    [ClassData(typeof(UserRegisterDtoTestNotValidData))]
    public async Task When_send_not_valid_data_Expect_get_bad_request(UserRegisterDtoTest? dto, IEnumerable<string> errorMessages)
    {
        //-ARRANGE

        //-ACT
        var response = await _client.PostAsync(_apiAddress, dto == null ? null : dto.ToContentHttpString());
        await response.WriteOnConsoleAsync(_outPutHelper);

        //-ASSERT
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var apiResult = await response.Content.ReadModelFromJsonAsync<ApiResult>();
        apiResult.Should().NotBeNull();
        apiResult.StatusCode.Should().Be(ETypeOfApiResultStatusCode.BadRequest);
        apiResult.Messages.Should().Contain(errorMessages);
    }

    [Fact]
    public async Task When_send_valid_data_Expect_get_ok()
    {
        //-ARRANGE
        var dto = new UserRegisterDtoTest
        {
            UserName = "User1",
            Password = "p1",
            ConfirmPassword = "p1",
            Phone = "p1",
            ConfirmPhone = "p1"
        };
        //-ACT
        var response = await _client.PostAsync(_apiAddress, dto.ToContentHttpString());
        await response.WriteOnConsoleAsync(_outPutHelper);

        //-ASSERT
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var apiResult = await response.Content.ReadModelFromJsonAsync<ApiResult<UserGetDtoTest>>();
        apiResult.Should().NotBeNull();
        apiResult.IsSuccess.Should().BeTrue();
        apiResult.StatusCode.Should().Be(ETypeOfApiResultStatusCode.Success);
        apiResult.Result.Should().NotBeNull();

        apiResult.Result.Should().NotBeNull();
        apiResult.Result.UserName.Should().Be(dto.UserName);
        apiResult.Result.Phone.Should().Be(dto.Phone);

    }

    [Theory]
    [InlineData("User2", "p1", "already exsit data in UserEntity [UserName : User2]")]
    [InlineData("User5", "p2", "already exsit data in UserEntity [Phone : p2]")]
    public async Task When_try_to_register_already_user_Expect_get_bad_request(string userName, string phone, string errorMessage)
    {
        //-ARRANGE
        var dto = new UserRegisterDtoTest
        {
            UserName = userName,
            Password = "p2",
            ConfirmPassword = "p2",
            Phone = phone,
            ConfirmPhone = phone
        };
        //-ACT
        var response = await _client.PostAsync(_apiAddress, dto.ToContentHttpString());
        await response.WriteOnConsoleAsync(_outPutHelper);

        //-ASSERT
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var apiResult = await response.Content.ReadModelFromJsonAsync<ApiResult>();
        apiResult.Should().NotBeNull();
        apiResult.IsSuccess.Should().BeFalse();
        apiResult.StatusCode.Should().Be(ETypeOfApiResultStatusCode.AlreadyExistData);
        apiResult.Messages.Should().Contain(errorMessage);
    }
}
