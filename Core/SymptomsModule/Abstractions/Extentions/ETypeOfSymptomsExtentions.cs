using Common.Extentions;
using Core.SymptomsModule.Abstractions.Enums;

namespace Core.SymptomsModule.Abstractions.Extentions;

public static class ETypeOfSymptomsExtentions
{
    public static ETypeOfSymptoms GetSymptom(this string value)
    {
        return EnumExtensions.ToDictionaryWithNameAndType<ETypeOfSymptoms>()
            .Where(x => x.Key == value.Trim().ToLower()).FirstOrDefault().Value;
    }
}
