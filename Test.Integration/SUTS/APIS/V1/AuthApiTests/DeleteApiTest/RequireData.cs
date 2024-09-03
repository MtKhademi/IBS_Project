using MDF.Test.Fixtures;

namespace MDF.Test.SUTs.APIs.V2.Common
{
    public partial class WebAppFactoryForDelete
    {
        private async Task DeleteApiTestRequierAsync(DatabaseRepositoryFixture repository)
        {
            await repository.UserAddAsyc("User10", "p10", "p3");
            await repository.UserAddAsyc("User11", "p11", "p3");
            await repository.UserAddAsyc("User12", "p12", "p3");
        }

    }
}
