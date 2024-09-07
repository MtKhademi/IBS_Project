using Common.Exceptions;
using System.Net.Http.Json;

namespace Core.ExternalServices.SmsSender;

internal class SendSmsService : ISendSmsService
{
    public async Task<SendOtpResult> SendOtpMessageAsync(string phone)
    {
        try
        {
            var client = new HttpClient(/*clientHandler*/);
            var request = new HttpRequestMessage(HttpMethod.Post, "https://console.melipayamak.com/api/send/otp/a14b9fff215d422db74b25ded8c9c034");
            var content = new StringContent("{\r\n  \"to\": \"" + phone + "\"\r\n}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<SendOtpResult>();

            if (string.IsNullOrWhiteSpace(result.code))
            {
                throw new NotValidDataException(await response.Content.ReadAsStringAsync());
            }

            return result;
        }
        catch (NotValidDataException) { throw; }
        catch (Exception ex)
        {

            throw new NotValidDataException($"Error in connect to server SMS");
        }
    }

}
