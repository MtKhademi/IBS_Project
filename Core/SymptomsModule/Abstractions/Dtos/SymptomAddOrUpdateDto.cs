//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.SymptomsModule.Abstractions.Enums;

namespace Core.SymptomsModule.Abstractions.Dtos;

public class SymptomAddOrUpdateDto
{
    public string? UserName { get; set; }
    public ETypeOfSymptoms? TypeOfSymptom { get; set; }
    public int? Value { get; set; }
}
