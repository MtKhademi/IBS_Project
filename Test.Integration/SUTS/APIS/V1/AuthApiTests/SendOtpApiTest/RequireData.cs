using MDF.Test.Fixtures;

namespace MDF.Test.SUTs.APIs.V2.Common
{
    public partial class WebAppFactory
    {
        private async Task SendOtpApiTestRequierAsync(DatabaseRepositoryFixture repository)
        {
            await repository.UserAddAsyc("User15", "p15", "p15");
        }

    }
}
