using System.Net.Mime;
using System.Net;
using System.Text.Json;
using Common.Exceptions;
using MDF.DTOS;

namespace Api.Commons.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    JsonSerializerOptions _serializeOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<ExceptionMiddleware> _loggerService)
    {
        try
        {
            await _next(context);
        }
        catch (NotValidDataException ex) { await HandleNotValidDataExceptionAsync(context, ex); }
        catch (NotExistDataException ex) { await HandleNotExistDataExceptionAsync(context, ex); }
        catch (NotAccessException ex) { await HandleNotAccessExceptionAsync(context, ex); }
        catch (AlreadyExistDataException ex) { await HandleAlreadyExistDataExceptionAsync(context, ex); }
        catch (Exception ex)
        {
            _loggerService.LogError(ex.ToString());
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = StatusCodes.Status200OK;

        var response = ApiResultCreator.InternalServerError();
        var json = JsonSerializer.Serialize(response, _serializeOptions);
        await context.Response.WriteAsync(json);
    }
    private async Task HandleNotAccessExceptionAsync(HttpContext context, NotAccessException ex)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)HttpStatusCode.OK;

        var response = ApiResultCreator.UnAuthentication("not exist access token");
        var json = JsonSerializer.Serialize(response, _serializeOptions);
        await context.Response.WriteAsync(json);
    }
    private async Task HandleNotExistDataExceptionAsync(HttpContext context, NotExistDataException ex)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)HttpStatusCode.OK;

        var response = ApiResultCreator.NotFound(ex.Message);
        var json = JsonSerializer.Serialize(response, _serializeOptions);
        await context.Response.WriteAsync(json);
    }
    private async Task HandleNotValidDataExceptionAsync(HttpContext context, NotValidDataException ex)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)HttpStatusCode.OK;

        var response = ApiResultCreator.BadRequest(ex.Errors.ToList());
        var json = JsonSerializer.Serialize(response, _serializeOptions);
        await context.Response.WriteAsync(json);
    }
    private async Task HandleAlreadyExistDataExceptionAsync(HttpContext context, AlreadyExistDataException ex)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = (int)HttpStatusCode.OK;

        var response = ApiResultCreator.AlreadyExist(ex.Message);
        var json = JsonSerializer.Serialize(response, _serializeOptions);
        await context.Response.WriteAsync(json);
    }

}