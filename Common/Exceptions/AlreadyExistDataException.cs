//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

namespace Common.Exceptions
{
    public class AlreadyExistDataException : Exception
    {
        public AlreadyExistDataException(string nameModel, string field, object data) :
            base($"already exsit data in {nameModel} [{field} : {data}]")
        {

        }
    }
}
