namespace TestProject.Core.Infrastructure.Exceptions;

public class InvalidArgumentException : Exception
{
    public object ErrorData { get; set; }

    public InvalidArgumentException()
    {
    }
    public InvalidArgumentException(string message) : base(message)
    {
    }

    public InvalidArgumentException(string message, object errorData) : this(message)
    {
        ErrorData = errorData;
    }
}

