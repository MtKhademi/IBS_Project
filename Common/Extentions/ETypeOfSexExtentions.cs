//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Common;
using Common.Extentions;

namespace Common.Extentions
{
    public static class ETypeOfSexExtentions
    {
        public static ETypeOfSex ConvertToTypeOfSex(this int? value)
        {
            if (value.IsNull())
                return ETypeOfSex.NotRecognize;
            return (ETypeOfSex)value.Value;
        }
    }
}
