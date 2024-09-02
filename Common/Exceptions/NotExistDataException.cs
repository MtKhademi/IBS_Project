//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using System;

namespace Common.Exceptions
{
    public class NotExistDataException : Exception
    {
        public NotExistDataException(string nameEntity, string message)
            : base($"not exist {nameEntity} in db - MESSAGE : {message}")
        {

        }
    }
}
