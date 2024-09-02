//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using System;

namespace Common.Exceptions
{
    public class NotHandleException : Exception
    {
        public NotHandleException(string location, Exception ex) :
            base($"[NOT HANDLED ERROR] - {location} : ", ex)
        {
        }
    }
}
