using Common.Entities;
using Core.SymptomsModule.Abstractions.Enums;
using Core.UserManagement.Entities;

namespace Core.SymptomsModule.Entities;

public class SymptomEntity : BaseEntity<int>
{
    public ETypeOfSymptoms TypeOfSymptom { get; set; }
    public int Value { get; set; }

    public int UserId { get; set; }
    public virtual UserEntity User { get; set; }
}
