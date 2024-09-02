using MDF.Test.Fixtures;

namespace MDF.Test.SUTs.APIs.V2.Common
{
    public partial class WebAppFactory
    {
        private async Task RegisterApiTestRequierAsync(DatabaseRepositoryFixture repository)
        {
            await repository.UserAddAsyc("User2", "p2", "p2");
        }

    }
}
