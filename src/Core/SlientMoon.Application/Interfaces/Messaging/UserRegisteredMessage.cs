using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlientMoon.Application.Messages;

public sealed record UserRegisteredMessage(
    int UserId,
    string Email
);