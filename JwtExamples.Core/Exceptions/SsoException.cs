namespace JwtExamples.Core.Exceptions;

public class SsoException(string message, Exception? innerException = null) : Exception(message, innerException);