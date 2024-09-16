//----------------------------------
//--Creator : MrMohande3 Khademi --
//----------------------------------

using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using MDF.DTOS;
using Common.Exceptions;
using Core.IDBModule.Abstractions.Services;
using Core.IDBModule.Abstractions.Dtos;

namespace Api.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
//[AuthorizationUser]
public class IDBApiController(
    IIDBService service,
    IWebHostEnvironment _env) : ControllerBase
{
    [HttpGet("Gets")]
    public async Task<ApiResult<IEnumerable<IDBGetDto>>> GetsAsync([FromQuery] IDBGetFilterDto? filter)
    => ApiResultCreator.Success(await service.GetsByFilterAsync(filter));


    [HttpPost("SUS/AddWithExcellFile")]
    [Consumes("multipart/form-data")]
    public async Task<ApiResult> AddWithExcellFileAsync(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            //-- SAVE IMAGE
            var directoryToSave = Path.Combine(_env.WebRootPath, "IDB");
            if (!Directory.Exists(directoryToSave))
                Directory.CreateDirectory(directoryToSave);

            var pathToSave = Path.Combine(directoryToSave, "idb.excell");
            if (System.IO.File.Exists(pathToSave))
                System.IO.File.Delete(pathToSave);
            using (var stream = System.IO.File.Create(pathToSave))
            {
                await file.CopyToAsync(stream);
            }

            await service.AddWithExcellFileAsync(pathToSave);
            return ApiResultCreator.Success();
        }
        else
        {
            throw new NotValidDataException($"File can not be empty or null");
        }
    }
}
