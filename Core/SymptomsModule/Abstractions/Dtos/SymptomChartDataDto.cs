//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Core.SymptomsModule.Abstractions.Enums;

namespace Core.SymptomsModule.Abstractions.Dtos;

public class SymptomChartDataDto
{
    public ETypeOfSymptoms TypeOfSymptoms { get; set; }
    public List<SymptomChartDataSpotDto> Spots { get; set; }
}
public class SymptomChartDataSpotDto
{
    public int Week { get; set; }
    public int Degree { get; set; }
}
