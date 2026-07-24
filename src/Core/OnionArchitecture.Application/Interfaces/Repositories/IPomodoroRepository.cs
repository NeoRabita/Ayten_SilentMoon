using System.Collections.Generic;
using System.Threading.Tasks;
using OnionArchitecture.Application.DTOs.ViewModels;
using OnionArchitecture.Domain.Entities;
using OnionArchitecture.Domain.Enums;

namespace OnionArchitecture.Application.Interfaces.Repositories
{
    public interface IPomodoroRepository : IGenericRepository<Pomodoro>
    {
        public Task<IEnumerable<PomodoroViewModel>> GetUserPomodoros(string userId);
        public List<PomodoroColors> GetPomodoroColors();
        public Task<PomodoroDetailsViewModel> GetPomodoroDetails(string userId, int pomodoroId);
        public Task<string> CreatePomodoroLog(int pomodoroId);

    }
}