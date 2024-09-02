namespace Common.Extentions
{
    public static class IsNullOrEmptyExtentions
    {
        public static bool IsNull(this int? value)
        {
            if (!value.HasValue) return true;
            return false;
        }
        public static bool IsEmpty(this int? value)
        {
            if (!value.HasValue) return true;
            if (value.Value == 0) return true;
            return false;
        }
        
        public static bool IsNull(this double? value)
        {
            if (!value.HasValue) return true;
            return false;
        }
        public static bool IsEmpty(this double? value)
        {
            if (value.IsNull()) return true;
            if (value == 0) return true;
            return false;
        }


        public static bool IsNull(this long? value)
        {
            if (!value.HasValue) return true;
            return false;
        }
        public static bool IsEmpty(this long? value)
        {
            if (value.IsNull()) return true;
            if (value == 0) return true;
            return false;
        }


        public static bool IsNull<T>(this ICollection<T>? value)
        {
            if (value == null) return true;
            return false;
        }
        public static bool IsEmpty<T>(this ICollection<T>? value)
        {
            if (value.IsNull()) return true;
            if (value.Count == 0) return true;
            return false;
        }
        public static bool IsNullOrEmpty<T>(this ICollection<T>? value)
        {
            if (value.IsNull()) return true;
            if (value.IsEmpty()) return true;
            return false;
        }


        public static bool IsNull(this DateTime? value)
        {
            if (!value.HasValue) return true;
            return false;
        }

    }
}
