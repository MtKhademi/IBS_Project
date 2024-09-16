using Common.Entities;
using Core.IDBModule.Abstractions.Enums;

namespace Core.IDBModule.Entities;

public class IDBEntity : BaseEntity<int>
{
    public ETypeOfIDBType TypeOfIDB { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}
