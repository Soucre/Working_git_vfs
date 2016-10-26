using System;

namespace SwipeJob.Utility.Exceptions
{
    public class RequiredFieldException : UserException
    {
        public RequiredFieldException(string message) : base(message)
        {
        }

        public RequiredFieldException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
