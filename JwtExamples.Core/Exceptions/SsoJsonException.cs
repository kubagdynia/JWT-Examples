namespace JwtExamples.Core.Exceptions;

public class SsoJsonException(string message, Exception? innerException = null)
    : SsoException(message, innerException);