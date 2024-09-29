namespace Course.Application.Exceptions;

public class InternalServerException(string message) : CustomException(message);
