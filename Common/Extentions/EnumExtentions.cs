namespace Common.Extentions;

public static class EnumExtensions
{

    public static IEnumerable<T> GetEnumerable<T>() where T : struct
    {
        if (!typeof(T).IsEnum)
            throw new NotSupportedException();

        return Enum.GetValues(typeof(T))
            .Cast<T>()
            .ToList();
    }

    public static Dictionary<int, string> ToDictionaryWithKey<T>() where T : struct
    {
        return Enum.GetValues(typeof(T)).Cast<T>()
            .ToDictionary(p => Convert.ToInt32(p), q => q.ToString());
    }
    public static Dictionary<string, int> ToDictionaryWithName<T>() where T : struct
    {
        return Enum.GetValues(typeof(T)).Cast<T>()
            .ToDictionary(q => q.ToString().ToLower(), p => Convert.ToInt32(p));
    }

    public static Dictionary<string, T> ToDictionaryWithNameAndType<T>() where T : struct
    {
        return Enum.GetValues(typeof(T)).Cast<T>()
            .ToDictionary(q => q.ToString().ToLower(), p => p);
    }

    public static T GetRandom<T>() where T : struct
    {
        var oRandom = new Random();
        var data = GetEnumerable<T>().ToList();
        var index = oRandom.Next(0, data.Count());
        return data[index];
    }
}
