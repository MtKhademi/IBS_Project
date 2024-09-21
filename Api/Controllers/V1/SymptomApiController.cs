//----------------------------------
//--Creator : MrMohande3 Khademi --
//----------------------------------

using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using MDF.DTOS;
using Core.SymptomsModule.Abstractions.Services;
using Core.SymptomsModule.Entities;
using Core.SymptomsModule.Abstractions.Dtos;
using Common.Extentions;
using Core.SymptomsModule.Abstractions.Enums;

namespace Api.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
//[AuthorizationUser]
public class SymptomApiController(
    ISymptomService service,
    IWebHostEnvironment _env) : ControllerBase
{
    [HttpGet("Gets")]
    public async Task<ApiResult<IEnumerable<SymptomEntity>>> GetsAsync()
        => ApiResultCreator.Success(await service.GetAllAsync());

    [HttpGet("GetChart")]
    public async Task<ApiResult<List<SymptomChartDataDto>>> GetChartAsync([FromQuery] string? userName)
    {
        var result = new List<SymptomChartDataDto>();

        var types = EnumExtensions.ToDictionaryWithNameAndType<ETypeOfSymptoms>();
        foreach (var typeOfSymptoms in types)
        {
            result.Add(new SymptomChartDataDto
            {
                TypeOfSymptoms = typeOfSymptoms.Value,
                Spots = GetSpots().ToList()
            });
        }
        return ApiResultCreator.Success(result);
    }
    private IEnumerable<SymptomChartDataSpotDto> GetSpots()
    {
        var result = new List<SymptomChartDataSpotDto>();
        for (int i = 1; i < 9; i++)
        {
            result.Add(new SymptomChartDataSpotDto
            {
                Week = i,
                Degree = (new Random()).Next(0, 11)
            });
        }
        return result;
    }


    [HttpPost("AddOrUpdate")]
    public async Task<ApiResult> AddOrUpdateAsync([FromBody] SymptomAddOrUpdateDto addOrUpdateDto)
    {
        await service.AddOrUpdateAsync(addOrUpdateDto);
        return ApiResultCreator.Success();
    }


}
