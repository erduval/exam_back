namespace Examen.Exceptions
{
    public class ElementNotFoundExcetion : Exception
    {
        public string Code { get; }

        public ElementNotFoundExcetion (string code) : base(code)
        {
            Code = code;
        }
    }
}
