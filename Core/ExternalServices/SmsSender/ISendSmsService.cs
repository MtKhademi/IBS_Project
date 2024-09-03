using Common.Interfaces;

namespace Core.ExternalServices.SmsSender;

public interface ISendSmsService : IBaseService
{
    Task<SendOtpResult> SendOtpMessageAsync(string phone);
}
