using Common.Extentions;
using Common;

namespace Common.Extentions
{
    public static class ETypeOfEducationExtentions
    {
        public static ETypeOfEducation ConvertToTypeOfEducation(this int? value)
        {
            if (value.IsNull())
                return ETypeOfEducation.NotRecognize;
            return (ETypeOfEducation)value.Value;
        }
    }
}

