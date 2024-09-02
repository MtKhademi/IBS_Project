//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Common.Extentions;
using System;

namespace Common.Exceptions
{
    public class NotAccessException : Exception
    {
        public string Action { get; set; }
        public string Location { get; set; }
        public string Data { get; set; }

        public NotAccessException(string location = "", string action = "", string data = "")
        {
            this.Action = action;
            this.Location = location;
            this.Data = data;
        }


    }
}
