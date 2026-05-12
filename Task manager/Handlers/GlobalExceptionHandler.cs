using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Task_manager.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
  private readonly ILogger<GlobalExceptionHandler> _logger;

  public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
  {
    _logger = logger;
  }

  public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
  {
    _logger.LogError(exception, "An unhandled exception occurred");

    var problemDetails = new ProblemDetails
    {
      Title = "An error occurred",
      Status = StatusCodes.Status500InternalServerError,
      Detail = exception.Message,
    };

    // Customize based on exception type
    switch (exception)
    {
      case ArgumentException:
        problemDetails.Status = StatusCodes.Status400BadRequest;
        problemDetails.Title = "Bad Request";
        break;
      case UnauthorizedAccessException:
        problemDetails.Status = StatusCodes.Status401Unauthorized;
        problemDetails.Title = "Unauthorized";
        break;
      case KeyNotFoundException:
        problemDetails.Status = StatusCodes.Status404NotFound;
        problemDetails.Title = "Not Found";
        break;
      case ConflictException conflictEx:
        problemDetails.Status = (int)conflictEx.StatusCode;
        problemDetails.Title = conflictEx.Error;
        break;
      case NotFoundException notFoundEx:
        problemDetails.Status = (int)notFoundEx.StatusCode;
        problemDetails.Title = notFoundEx.Error;
        break;
    }

    httpContext.Response.StatusCode = problemDetails.Status.Value;
    await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

    return true;
  }
}

