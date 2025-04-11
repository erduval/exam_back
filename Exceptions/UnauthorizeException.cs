namespace Examen.Exceptions
{
    public class UnauthorizeException: Exception
    {
        public string Code { get; }

        public UnauthorizeException (string code) : base(code)
        {
            Code = code;
        }
    }
}
