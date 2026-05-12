using System.Net;

namespace Task_manager.Exceptions;

public abstract class ApiException : Exception
{
  public HttpStatusCode StatusCode { get; }
  public string Error { get; }

  protected ApiException(string message, HttpStatusCode statusCode, string title)
      : base(message)
  {
    StatusCode = statusCode;
    Error = title;
  }
}

public class ConflictException : ApiException
{
  public ConflictException(string message = "Resource already exists")
      : base(message, HttpStatusCode.Conflict, "Conflict") { }
}

public class NotFoundException : ApiException
{
  public NotFoundException(string message = "Resource does not exist")
      : base(message, HttpStatusCode.NotFound, "Not found") { }
}