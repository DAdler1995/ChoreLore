using ChoreLore.Models;

namespace ChoreLore.Services.Interfaces
{
    public interface IChoreService
    {
        Task<List<Chore>> GetUserChoresAsync(string userId);
        Task CompleteChoreAsync(int choreId, string userId);
        int CalculateLevel(int totalXp);
    }
}
