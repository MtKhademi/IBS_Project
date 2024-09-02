using FluentValidation;
using Common.Formatters.DateTimeFormatter;

namespace Common.Extentions
{
    public static class CustomFluentValidation
    {
        //public static IRuleBuilderOptions<T, int?> IfExistThenGreaterThan<T>(this IRuleBuilder<T, int?> ruleBuilder, int num)
        //{
        //    return ruleBuilder.Must(number => (!number.HasValue) || (number.HasValue && number.Value > num));
        //}

        //public static IRuleBuilderOptions<T, string?> IfExistDateThenCheckFormatter<T>(this IRuleBuilder<T, string?> ruleBuilder,
        //    IDateTimeFormatter dateTimeFormatter)
        //{
        //    return ruleBuilder
        //            .Must(dt =>
        //                !string.IsNullOrWhiteSpace(dt) ?
        //            dateTimeFormatter.CanBeConvertToDate(dt) : true)
        //            .WithMessage(model => "{PropertyName} can not be convert to date => sample YYYY-MM-DD :: {PropertyValue}");

        //}
        //public static IRuleBuilderOptions<T, string?> IfExistDateTimeThenCheckFormatter<T>(this IRuleBuilder<T, string?> ruleBuilder,
        //    IDateTimeFormatter dateTimeFormatter)
        //{
        //    return ruleBuilder
        //            .Must(dt =>
        //                !string.IsNullOrWhiteSpace(dt) ?
        //            dateTimeFormatter.CanBeConvertToDateTime(dt) : true)
        //            .WithMessage(model => "{PropertyName} can not be convert to date => sample YYYY-MM-DDThh:mm:ss :: {PropertyValue}");

        //}


        //public static IRuleBuilderOptions<T, string?> CheckDateFormatter<T>(this IRuleBuilder<T, string?> ruleBuilder,
        //    IDateTimeFormatter dateTimeFormatter)
        //{
        //    return ruleBuilder
        //            .Must(dt => dateTimeFormatter.CanBeConvertToDate(dt))
        //            .WithMessage(model => "{PropertyName} can not be convert to date => sample YYYY-MM-DD :: {PropertyValue}");

        //}
        //public static IRuleBuilderOptions<T, string?> CheckDateTimeFormatter<T>(this IRuleBuilder<T, string?> ruleBuilder,
        //    IDateTimeFormatter dateTimeFormatter)
        //{
        //    return ruleBuilder
        //            .Must(dt => dateTimeFormatter.CanBeConvertToDateTime(dt))
        //            .WithMessage(model => "{PropertyName} can not be convert to date => sample YYYY-MM-DDThh:mm:ss :: {PropertyValue}");

        //}



    }
}
