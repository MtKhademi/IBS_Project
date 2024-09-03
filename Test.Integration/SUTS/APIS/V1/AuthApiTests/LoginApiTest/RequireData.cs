using MDF.Test.Fixtures;

namespace MDF.Test.SUTs.APIs.V2.Common
{
    public partial class WebAppFactory
    {
        private async Task LoginApiTestRequierAsync(DatabaseRepositoryFixture repository)
        {
            await repository.UserAddAsyc("User3", "p3", "p3");
        }

    }
}
