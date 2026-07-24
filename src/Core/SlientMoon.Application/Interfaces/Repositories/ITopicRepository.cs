using SlientMoon.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlientMoon.Application.Interfaces.Repositories
{
    public interface ITopicRepository : IGenericRepository<Topic>
    {
        Task<List<Topic>> GetAllTopicsAsync();
    }
}