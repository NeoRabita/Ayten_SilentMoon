using System;

namespace SlientMoon.Application.Exceptions;

public class BadRequestException : ApiException
{
    public BadRequestException(string message)
        : base(message)
    {
    }
}