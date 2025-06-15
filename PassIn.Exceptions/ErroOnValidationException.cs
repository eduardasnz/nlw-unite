namespace PassIn.Exceptions;
public class ErroOnValidationException : PassInException
{
    public ErroOnValidationException(string message) : base(message)
    {
    }
}
