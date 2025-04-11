namespace Examen.Exceptions
{
    public class CustomErrorMessageException : Exception
    {
        public string Code { get; }

        public CustomErrorMessageException (string code) : base(code)
        {
            Code = code;
        }
    }
}
