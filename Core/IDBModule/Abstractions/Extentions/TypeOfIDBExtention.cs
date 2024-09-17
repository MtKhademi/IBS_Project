using Common.Extentions;
using Core.IDBModule.Abstractions.Enums;

namespace Core.IDBModule.Abstractions.Extentions;

public static class TypeOfIDBExtention
{
    public static ETypeOfIDBType GetIDB(this string value)
    {
        return EnumExtensions.ToDictionaryWithNameAndType<ETypeOfIDBType>()
            .Where(x => x.Key == value.Trim().ToLower()).FirstOrDefault().Value;
    }
}
