using SlientMoon.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlientMoon.Application.Interfaces.Repositories;

public interface IUserTopicRepository
    : IGenericRepository<UserTopic>
{
    Task<List<UserTopic>> GetUserTopicsAsync(int userId);

    Task DeleteUserTopicsAsync(int userId);
}