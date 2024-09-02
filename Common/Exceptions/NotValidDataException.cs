//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using FluentValidation.Results;
using Common.Extentions;
using Common.Extentions;
using System;

namespace Common.Exceptions
{
    public class NotValidDataException : Exception
    {
        public List<string> Errors { get; set; } = new List<string>();
        public override string Message => Errors.InLine();
        public NotValidDataException(string error)
            : this(new List<string>() { error })
        {

        }
        public NotValidDataException(params string[] errors) :
            this(errors.ToList())
        { }
        public NotValidDataException(List<string> errors) :
            base()
        {
            this.Errors = errors;
        }
        public NotValidDataException(List<ValidationFailure> errors) : base()
        {
            this.Errors = errors.Select(x => x.ErrorMessage).ToList();
        }


    }
}
