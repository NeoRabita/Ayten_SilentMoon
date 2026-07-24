using System;

namespace SlientMoon.Application.Exceptions
{
    public class ConflictException : ApiException
    {
        public ConflictException(string message) : base(message)
        {
        }
    }
}