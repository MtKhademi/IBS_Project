//----------------------------------
//--Creator : MrMohande3 Khademi --
//----------------------------------

using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using MDF.DTOS;
using Core.SymptomsModule.Abstractions.Services;
using Core.SymptomsModule.Entities;
using Core.SymptomsModule.Abstractions.Dtos;

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
        return ApiResultCreator.Success(await service.GetChartAsync(userName));
    }



    [HttpPost("AddOrUpdate")]
    public async Task<ApiResult> AddOrUpdateAsync([FromBody] SymptomAddOrUpdateDto addOrUpdateDto)
    {
        await service.AddOrUpdateAsync(addOrUpdateDto);
        return ApiResultCreator.Success();
    }


    [HttpGet("GetReport")]
    public async Task<IActionResult> GetReportAsync()
    {
        var pathFile = await service.GetReportAsync();
        var fileBytes = await System.IO.File.ReadAllBytesAsync(pathFile);
        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "GetReportSymptoms.xlsx");
    }

}
