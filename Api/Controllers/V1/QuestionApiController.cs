//----------------------------------
//--Creator : MrMohande3 Khademi --
//----------------------------------

using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using MDF.DTOS;
using Core.QuestionModule.Abstractions.Services;
using Api.Commons.Extentions;
using Common.Exceptions;
using Core.QuestionModule.Abstractions.Dtos;
using Core.QuestionModule.Abstractions.Enumerations;

namespace Api.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
//[AuthorizationUser]
public class QuestionApiController(
    IQuestionService susQuestionService,
    IWebHostEnvironment _env) : ControllerBase
{
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

    [HttpGet("QuestionGets")]
    public async Task<ApiResult<IEnumerable<QuestionGetDto>>> QuestionGetsAsync([FromQuery] QuestionGetFilterDto? filter)
    => ApiResultCreator.Success(await susQuestionService.GetsByFilterAsync(filter));

    #region SUS

    [HttpPost("SUS/AddWithExcellFile")]
    [Consumes("multipart/form-data")]
    public async Task<ApiResult> AddWithExcellFileAsync(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            //-- SAVE IMAGE
            var directoryToSave = Path.Combine(_env.WebRootPath, "Question");
            if (!Directory.Exists(directoryToSave))
                Directory.CreateDirectory(directoryToSave);

            var pathToSave = Path.Combine(directoryToSave, "ques.excell");
            if (System.IO.File.Exists(pathToSave))
                System.IO.File.Delete(pathToSave);
            using (var stream = System.IO.File.Create(pathToSave))
            {
                await file.CopyToAsync(stream);
            }

            await susQuestionService.AddWithExcellFileAsync(Request.HttpContext.GetUserFromContext(), pathToSave,
                ETypeOfQuestion.SUS);
            return ApiResultCreator.Success();
        }
        else
        {
            throw new NotValidDataException($"File can not be empty or null");
        }
    }

    #endregion

    #region QUALITY
  
    [HttpPost("Quality/AddWithExcellFile")]
    [Consumes("multipart/form-data")]
    public async Task<ApiResult> QualityAddWithExcellFileAsync(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            //-- SAVE IMAGE
            var directoryToSave = Path.Combine(_env.WebRootPath, "Question");
            if (!Directory.Exists(directoryToSave))
                Directory.CreateDirectory(directoryToSave);

            var pathToSave = Path.Combine(directoryToSave, "ques.excell");
            if (System.IO.File.Exists(pathToSave))
                System.IO.File.Delete(pathToSave);
            using (var stream = System.IO.File.Create(pathToSave))
            {
                await file.CopyToAsync(stream);
            }

            await susQuestionService.AddWithExcellFileAsync(Request.HttpContext.GetUserFromContext(), pathToSave,
                ETypeOfQuestion.QualityOfLife);
            return ApiResultCreator.Success();
        }
        else
        {
            throw new NotValidDataException($"File can not be empty or null");
        }
    }

    #endregion

    #region Symptoms

    [HttpPost("Symptoms/AddWithExcellFile")]
    [Consumes("multipart/form-data")]
    public async Task<ApiResult> SymptomsAddWithExcellFileAsync(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            //-- SAVE IMAGE
            var directoryToSave = Path.Combine(_env.WebRootPath, "Question");
            if (!Directory.Exists(directoryToSave))
                Directory.CreateDirectory(directoryToSave);

            var pathToSave = Path.Combine(directoryToSave, "ques.excell");
            if (System.IO.File.Exists(pathToSave))
                System.IO.File.Delete(pathToSave);
            using (var stream = System.IO.File.Create(pathToSave))
            {
                await file.CopyToAsync(stream);
            }

            await susQuestionService.AddWithExcellFileAsync(Request.HttpContext.GetUserFromContext(), pathToSave,
                ETypeOfQuestion.Symptoms);
            return ApiResultCreator.Success();
        }
        else
        {
            throw new NotValidDataException($"File can not be empty or null");
        }
    }

    #endregion
}
