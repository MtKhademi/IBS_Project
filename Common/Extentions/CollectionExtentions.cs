namespace Common.Extentions
{
    public static class CollectionExtentions
    {
        public static bool IsNull<T>(this IEnumerable<T>? value)
        {
            if (value == null) return true;
            return false;
        }

        public static bool IsEmpty<T>(this IEnumerable<T>? value)
        {
            if (value == null) return true;
            if (value.Count() == 0) return true;
            return false;
        }

    }
}
