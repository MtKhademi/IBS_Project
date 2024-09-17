//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.IDBModule.Entities;
using Core.SymptomsModule.Entities;

namespace Core.IDBModule.Infrastructure.ExcellFileHelper;

internal class ExcellFileManager
{
    public List<IDBEntity> GetQuestions(string filePath)
    {
        return (new Transform()).Gets(filePath);
    }
}
