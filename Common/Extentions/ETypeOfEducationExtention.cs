
using Common;

namespace MrMohande3Project.Common.Extentions
{
    public static class ETypeOfEducationExtention
    {
        public static string GetName(this ETypeOfEducation education)
        {
            switch (education)
            {
                case ETypeOfEducation.Bisavad:
                    return "بی سواد";
                case ETypeOfEducation.Cikle:
                    return "سیکل";
                case ETypeOfEducation.Diplom:
                    return "دیپلم";
                case ETypeOfEducation.FoghDiplom:
                    return "فوق دیپلم";
                case ETypeOfEducation.Lisance:
                    return "لیسانس";
                case ETypeOfEducation.FoghLisance:
                    return "فوق لیسانس";
                case ETypeOfEducation.Doctor:
                    return "دکتر";
                default:
                    return "نامشخص";
            }
        }
        public static ETypeOfEducation GetEducation(this int education)
        {
            return (ETypeOfEducation)education;
        }
        public static ETypeOfEducation GetEducation(this string education)
        {
            if (education.Trim() == "بی سواد")
                return ETypeOfEducation.Bisavad;
            if (education.Trim() == "سیکل")
                return ETypeOfEducation.Bisavad;
            if (education.Trim() == "دیپلم")
                return ETypeOfEducation.Bisavad;
            if (education.Trim() == "فوق دیپلم")
                return ETypeOfEducation.Bisavad;
            if (education.Trim() == "لیسانس")
                return ETypeOfEducation.Bisavad;
            if (education.Trim() == "فوق لیسانس")
                return ETypeOfEducation.Bisavad;
            if (education.Trim() == "دکتر")
                return ETypeOfEducation.Bisavad;
            return ETypeOfEducation.NotRecognize;
        }
    }
}
