using Application.Abstractions.Messaging;
using SlientMoon.Application.DTOs.Profile;
using System;

namespace SlientMoon.Application.Features.Profile.Queries.GetProfile;

public sealed record GetProfileQuery() : ICommand<UserProfileResponse>;