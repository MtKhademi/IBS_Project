using Core.SymptomsModule.Abstractions.Enums;
using MDF.Test.Fixtures;

namespace MDF.Test.SUTs.APIs.V2.Common;

public partial class WebAppFactory
{
    private async Task SymptomAddOrUpdateApiTestRequierAsync(DatabaseRepositoryFixture repository)
    {
        await repository.UserAddAsyc("User20", "User20", "User20");


        var dt = new DateTime(2024, 09, 23, 23, 23, 0);
        await repository.UserAddAsyc("User21", "User20", "User20");
        await repository.SymptomAddAsync("User21", ETypeOfSymptoms.Constipation, 5, dt);


        dt = new DateTime(2024, 09, 23, 23, 23, 0);
        await repository.UserAddAsyc("User22", "User20", "User20");
        await repository.SymptomAddAsync("User22", ETypeOfSymptoms.Constipation, 5, dt);


        dt = new DateTime(2024, 09, 15, 23, 23, 0);
        await repository.UserAddAsyc("User23", "User20", "User20");
        await repository.SymptomAddAsync("User23", ETypeOfSymptoms.Constipation, 5, dt);

    }
}
