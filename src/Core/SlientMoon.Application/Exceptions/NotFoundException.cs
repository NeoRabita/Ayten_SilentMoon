using System;

namespace SlientMoon.Application.Exceptions
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}