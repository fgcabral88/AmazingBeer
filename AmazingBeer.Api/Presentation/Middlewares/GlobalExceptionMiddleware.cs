using AmazingBeer.Api.Domain.Exceptions;
using System.Net;
using System.Text.Json;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Continua o pipeline normalmente
            await _next(context);
        }
        catch (Exception ex)
        {
            // Captura qualquer exceção não tratada
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        HttpStatusCode statusCode;
        string message;

        // Define o tipo de erro baseado na exceção capturada
        switch (exception)
        {
            case CustomExceptions.NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = notFoundException.Message;
                break;

            case CustomExceptions.ValidationException validationException:
                statusCode = HttpStatusCode.BadRequest;
                message = validationException.Message;
                break;

            case CustomExceptions.UnauthorizedException authorizationException:
                statusCode = HttpStatusCode.Unauthorized;
                message = authorizationException.Message;
                break;

            case CustomExceptions.DuplicateRecordException duplicateRecordException:
                statusCode = HttpStatusCode.Conflict;
                message = duplicateRecordException.Message;
                break;

            case CustomExceptions.DatabaseException databaseException:
                statusCode = HttpStatusCode.InternalServerError;
                message = databaseException.Message;
                break;

            case CustomExceptions.ForbiddenException forbiddenException:
                statusCode = HttpStatusCode.Forbidden;
                message = forbiddenException.Message;
                break;

            case CustomExceptions.ConflictException conflictException:
                statusCode = HttpStatusCode.Conflict;
                message = conflictException.Message;
                break;

            case CustomExceptions.BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                message = badRequestException.Message;
                break;

            case CustomExceptions.UnprocessableEntityException unprocessableEntityException:
                statusCode = HttpStatusCode.UnprocessableEntity;
                message = unprocessableEntityException.Message;
                break;

            case CustomExceptions.InternalServerErrorException internalServerErrorException:
                statusCode = HttpStatusCode.InternalServerError;
                message = internalServerErrorException.Message;
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError;
                message = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.";
                break;
        }

        // Cria a estrutura de resposta de erro
        var errorResponse = new
        {
            StatusCode = (int)statusCode,
            Message = message,
            Path = context.Request.Path, // Endpoint que causou o erro
            Timestamp = DateTime.UtcNow // Horário do erro
        };

        var errorJson = JsonSerializer.Serialize(errorResponse);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        // Retorna a resposta JSON
        return context.Response.WriteAsync(errorJson);
    }
}
