//----------------------------------
//--Creator : MrMohande3 Khademi --
//----------------------------------

using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using MDF.DTOS;
using Core.UserManagement.Abstractions;
using Core.UserManagement.Abstractions.Dtos;

namespace Api.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    //[AuthorizationUser]
    public class AuthApiController(
        IUserManagement _module,
        IWebHostEnvironment _env) : ControllerBase
    {


        [HttpPost("Login")]
        public async Task<ApiResult<string>> LoginAsync(UserLoginDto userLogin)
        {
            return ApiResultCreator.Success(await _module.LoginAsync(userLogin));
        }

        [HttpPost("Register")]
        public async Task<ApiResult<string>> RegisterAsync(UserRegisterDto? register)
        {
            return ApiResultCreator.Success(await _module.RegisterAsync(register));
        }


        //[HttpGet("download")]
        //public IActionResult DownloadHelperFile()
        //{
        //    string webRootPath = _env.WebRootPath;
        //    string outputFilePath = Path.Combine(webRootPath, "Helper.pdf");

        //    if (!System.IO.File.Exists(outputFilePath))
        //    {
        //        // Return a 404 Not Found error if the file does not exist
        //        return NotFound();
        //    }

        //    var fileInfo = new System.IO.FileInfo(outputFilePath);
        //    Response.ContentType = "application/pdf";
        //    Response.Headers.Add("Content-Disposition", "attachment;filename=\"" + fileInfo.Name + "\"");
        //    Response.Headers.Add("Content-Length", fileInfo.Length.ToString());

        //    // Send the file to the client
        //    return File(System.IO.File.ReadAllBytes(outputFilePath), "application/pdf", fileInfo.Name);
        //}

    }
}
