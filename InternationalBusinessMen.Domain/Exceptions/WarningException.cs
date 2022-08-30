using System;

namespace InternationalBusinessMen.Domain.Exceptions
{
    public class WarningException: Exception
    {

        public WarningException() { }
        public WarningException(string message): base(message) { }
        public WarningException(string message, Exception innerException) : base(message, innerException) { }
    }
}
